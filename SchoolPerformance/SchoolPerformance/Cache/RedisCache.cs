﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using SchoolPerformance.ViewModels;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Cache
{
    public class RedisCache : IRedisCache
    {
        private readonly IRedisCacheClient _redisCacheClient;
        private readonly ILogger<RedisCache> _logger;

        public RedisCache(IRedisCacheClient redisCacheClient, ILogger<RedisCache> logger)
        {
            _redisCacheClient = redisCacheClient;
            _logger = logger;
        }

        public async Task<IEnumerable<AutocompleteViewModel>> GetAutoCompleteData()
        {
            _logger.LogInformation("Executing GetAutoCompleteData method in RedisCache class.");

            IEnumerable<AutocompleteViewModel> autoCompleteDataLst = new List<AutocompleteViewModel>();

            try
            {
                // Searches and gets all the AutocompleteViewModel keys in the cache
                var listofkeys = await _redisCacheClient.Db0.SearchKeysAsync("AutocompleteViewModel*");

                //Get ScatterplotViewModel data from cache if it's in the cache
                if (listofkeys != null)
                {
                    var results = await _redisCacheClient
                                    .Db0
                                    .GetAllAsync<AutocompleteViewModel>(listofkeys);

                    autoCompleteDataLst = new List<AutocompleteViewModel>(results.Values);

                }
            }
            catch (Exception e) {

                _logger.LogError("An exception occurred when attempting to get AutocompleteViewModel data from cache." + 
                    " Stack trace: " + e.StackTrace);
            
            }

            return autoCompleteDataLst.OrderBy(s => s.SCHNAME);
        }

        public async Task<TableViewModelAll> GetNationalTableDataAll()
        {
            _logger.LogInformation("Executing GetNationalTableDataAll method in RedisCache class.");

            TableViewModelAll nationalData = null;
            bool dataInCache = false;

            try
            {
                //Check if National TableViewModelAll data is in cache
                dataInCache = await _redisCacheClient.Db0.ExistsAsync("NationalTableViewModelAll");

                if (dataInCache)
                {
                    nationalData = await _redisCacheClient
                                .Db0.GetAsync<TableViewModelAll>("NationalTableViewModelAll");
                }
                
            }
            catch (Exception e) {

                _logger.LogError("An exception occurred when attempting to get NationalTableViewModelAll data from cache." +
                    " Stack trace: " + e.StackTrace);

            }

            return nationalData;
        }

        public async Task<TableViewModelDisadvantaged> GetNationalTableDataDisadvantaged()
        {
            _logger.LogInformation("Executing GetNationalTableDataDisadvantaged method in RedisCache class.");

            TableViewModelDisadvantaged nationalData = null;
            bool dataInCache = false;

            try
            {
                //Check if National TableViewModelDisadvantaged data is in cache
                dataInCache = await _redisCacheClient.Db0.ExistsAsync("NationalTableViewModelDisadvantaged");

                if (dataInCache)
                {
                    nationalData = await _redisCacheClient
                                    .Db0
                                    .GetAsync<TableViewModelDisadvantaged>("NationalTableViewModelDisadvantaged");
                }
            }
            catch (Exception e) {

                _logger.LogError("An exception occurred when attempting to get NationalTableViewModelDisadvantaged data from cache." +
                    " Stack trace: " + e.StackTrace);
            }

            return nationalData;
        }

        public async Task<IEnumerable<ScatterplotViewModel>> GetScatterplotData()
        {
            _logger.LogInformation("Executing GetScatterplotData method in RedisCache class.");

            IEnumerable<ScatterplotViewModel> scatterplotDataLst = new List<ScatterplotViewModel>();

            try
            {
                // Searches and gets all the ScatterplotViewModel keys in the cache
                var listofkeys = await _redisCacheClient.Db0.SearchKeysAsync("ScatterplotViewModel*");

                //Get ScatterplotViewModel data from cache if it's in the cache
                if (listofkeys != null)
                {
                    var results = await _redisCacheClient
                                    .Db0
                                    .GetAllAsync<ScatterplotViewModel>(listofkeys);

                    scatterplotDataLst = new List<ScatterplotViewModel>(results.Values);

                }
            }
            catch (Exception e) {

                _logger.LogError("An exception occurred when attempting to get ScatterplotViewModel data from cache." +
                    " Stack trace: " + e.StackTrace);
            }

            return scatterplotDataLst.OrderBy(s => s.SCHNAME);

        }

        public async Task<IEnumerable<TableViewModelAll>> GetTableDataAll()
        {
            _logger.LogInformation("Executing GetTableDataAll method in RedisCache class.");

            IEnumerable<TableViewModelAll> tableDataLst = new List<TableViewModelAll>();

            try
            {
                // Searches and gets all the TableViewModelAll keys in the cache
                var listofkeys = await _redisCacheClient.Db0.SearchKeysAsync("TableViewModelAll*");

                //Get TableViewModelAll data from cache if it's in the cache
                if (listofkeys != null)
                {
                    var results = await _redisCacheClient
                                    .Db0
                                    .GetAllAsync<TableViewModelAll>(listofkeys);

                    tableDataLst = new List<TableViewModelAll>(results.Values);

                }
            }
            catch (Exception e) {

                _logger.LogError("An exception occurred when attempting to get TableViewModelAll data from cache." +
                    " Stack trace: " + e.StackTrace);

            }

            return tableDataLst.OrderBy(s => s.SCHNAME);
        }

        public async Task<IEnumerable<TableViewModelDisadvantaged>> GetTableDataDisadvantaged()
        {
            _logger.LogInformation("Executing GetTableDataDisadvantaged method in RedisCache class.");

            IEnumerable<TableViewModelDisadvantaged> tableDataLst = new List<TableViewModelDisadvantaged>();

            try
            {
                // Searches and gets all the TableViewModelDisadvantaged keys in the cache
                var listofkeys = await _redisCacheClient.Db0.SearchKeysAsync("TableViewModelDisadvantaged*");

                //Get TableViewModelDisadvantaged data from cache if it's in the cache
                if (listofkeys != null)
                {
                    var results = await _redisCacheClient
                                    .Db0
                                    .GetAllAsync<TableViewModelDisadvantaged>(listofkeys);

                    tableDataLst = new List<TableViewModelDisadvantaged>(results.Values);

                }
            }
            catch (Exception e) 
            {
                _logger.LogError("An exception occurred when attempting to get TableViewModelDisadvantaged data from cache." +
                    " Stack trace: " + e.StackTrace);
            }

            return tableDataLst.OrderBy(s => s.SCHNAME);
        }

        public async Task SaveAutoCompleteData(IEnumerable<AutocompleteViewModel> autoCompleteDataLst)
        {
            _logger.LogInformation("Executing SaveAutoCompleteData method in RedisCache class.");

            var items = new List<Tuple<string, AutocompleteViewModel>>();

            foreach (var item in autoCompleteDataLst)
            {
                //A key is created which included the unique reference number of the school
                //before the object is added to the cache
                var key = "AutocompleteViewModel" + item.URN;
                items.Add(new Tuple<string, AutocompleteViewModel>(key, item));
            }

            try
            {
                 await _redisCacheClient
                .Db0
                .AddAllAsync(items, DateTimeOffset.Now.AddMinutes(30));
            }

            catch (Exception e) {

                _logger.LogError("An exception occurred when attempting to save AutocompleteViewModel data to cache." +
                    " Stack trace: " + e.StackTrace);
            }
        }

        public async Task SaveNationalTableDataAll(TableViewModelAll nationalTableData)
        {
            _logger.LogInformation("Executing SaveNationalTableDataAll method in RedisCache class.");

            try
            {
                await _redisCacheClient
                .Db0
                .AddAsync("NationalTableViewModelAll", nationalTableData, DateTimeOffset.Now.AddMinutes(30));
            }
            catch(Exception e) 
            {
                _logger.LogError("An exception occurred when attempting to save NationalTableViewModelAll data to cache." +
                    " Stack trace: " + e.StackTrace);
            }
            
        }

        public async Task SaveNationalTableDataDisadvantaged(TableViewModelDisadvantaged nationalTableData)
        {
            _logger.LogInformation("Executing SaveNationalTableDataDisadvantaged method in RedisCache class.");

            try
            {
                await _redisCacheClient
                .Db0
                .AddAsync("NationalTableViewModelDisadvantaged", nationalTableData, DateTimeOffset.Now.AddMinutes(30));

            }
            catch (Exception e)
            {
                _logger.LogError("An exception occurred when attempting to save NationalTableViewModelDisadvantaged data to cache." +
                    " Stack trace: " + e.StackTrace);
            }
        }

        public async Task SaveNationalScatterplotData(ScatterplotViewModel nationalScatterplotData)
        {
            _logger.LogInformation("Executing SaveNationalScatterplotData method in RedisCache class.");

            try
            {
                await _redisCacheClient
                .Db0
                .AddAsync("NationalScatterplotData", nationalScatterplotData, DateTimeOffset.Now.AddMinutes(30));
            }
            catch (Exception e)
            {
                _logger.LogError("An exception occurred when attempting to save NationalScatterplotData data to cache." +
                    " Stack trace: " + e.StackTrace);
            }
        }

        public async Task SaveScatterplotData(IEnumerable<ScatterplotViewModel> scatterplotDataLst)
        {
            _logger.LogInformation("Executing SaveScatterplotData method in RedisCache class.");

            var items = new List<Tuple<string, ScatterplotViewModel>>();

            foreach (var item in scatterplotDataLst)
            {
                //A key is created which included the unique reference number of the school
                //before the object is added to the cache
                var key = "ScatterplotViewModel" + item.URN;
                items.Add(new Tuple<string, ScatterplotViewModel>(key, item));
            }

            try
            {
                await _redisCacheClient
                .Db0
                .AddAllAsync(items, DateTimeOffset.Now.AddMinutes(30));
            }

            catch (Exception e) 
            {
                _logger.LogError("An exception occurred when attempting to save ScatterplotViewModel data to cache." +
                    " Stack trace: " + e.StackTrace);
            }
            
        }

        public async Task SaveTableDataAll(IEnumerable<TableViewModelAll> tableDataLst)
        {
            _logger.LogInformation("Executing SaveTableDataAll method in RedisCache class.");

            var items = new List<Tuple<string, TableViewModelAll>>();

            foreach (var item in tableDataLst)
            {
                //A key is created which included the unique reference number of the school
                //before the object is added to the cache
                var key = "TableViewModelAll" + item.URN;
                items.Add(new Tuple<string, TableViewModelAll>(key, item));
            }

            try
            {
                await _redisCacheClient
                .Db0
                .AddAllAsync(items, DateTimeOffset.Now.AddMinutes(30));
            }

            catch (Exception e) 
            {
                _logger.LogError("An exception occurred when attempting to save TableViewModelAll data to cache." +
                    " Stack trace: " + e.StackTrace);
            }
        }

        public async Task SaveTableDataDisadvantaged(IEnumerable<TableViewModelDisadvantaged> tableDataLst)
        {
            _logger.LogInformation("Executing SaveTableDataDisadvantaged method in RedisCache class.");

            var items = new List<Tuple<string, TableViewModelDisadvantaged>>();

            foreach (var item in tableDataLst)
            {
                //A key is created which included the unique reference number of the school
                //before the object is added to the cache
                var key = "TableViewModelDisadvantaged" + item.URN;
                items.Add(new Tuple<string, TableViewModelDisadvantaged>(key, item));
            }

            try
            {
                await _redisCacheClient
                .Db0
                .AddAllAsync(items, DateTimeOffset.Now.AddMinutes(30));
            }

            catch (Exception e) 
            {
                _logger.LogError("An exception occurred when attempting to save TableViewModelDisadvantaged data to cache." +
                    " Stack trace: " + e.StackTrace);
            }
        }

        public async Task<ScatterplotViewModel> GetNationalScatterplotData()
        {
            _logger.LogInformation("Executing GetNationalScatterplotData method in RedisCache class.");

            ScatterplotViewModel nationalData = null;
            bool dataInCache = false;

            try
            {
                //Check if National ScatterplotViewModel data is in cache
                dataInCache = await _redisCacheClient.Db0.ExistsAsync("NationalScatterplotViewModel");

                if (dataInCache)
                {
                    nationalData = await _redisCacheClient
                                    .Db0
                                    .GetAsync<ScatterplotViewModel>("NationalScatterplotViewModel");
                }
            }
            catch (Exception e) 
            {
                _logger.LogError("An exception occurred when attempting to save NationalScatterplotViewModel data to cache." +
                    " Stack trace: " + e.StackTrace);
            }

            return nationalData;
        }
    }
}
