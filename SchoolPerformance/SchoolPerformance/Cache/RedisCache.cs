using Microsoft.Extensions.Caching.Memory;
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

        public RedisCache(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task<IEnumerable<AutocompleteViewModel>> GetAutoCompleteData()
        {
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
            catch (Exception) { }

            return autoCompleteDataLst.OrderBy(s => s.SCHNAME);
        }

        public async Task<TableViewModelAll> GetNationalTableDataAll()
        {
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
            catch (Exception) { }

            return nationalData;
        }

        public async Task<TableViewModelDisadvantaged> GetNationalTableDataDisadvantaged()
        {
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
            catch (Exception) { }

            return nationalData;
        }

        public async Task<IEnumerable<ScatterplotViewModel>> GetScatterplotData()
        {
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
            catch (Exception) { }

            return scatterplotDataLst.OrderBy(s => s.SCHNAME);

        }

        public async Task<IEnumerable<TableViewModelAll>> GetTableDataAll()
        {
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
            catch (Exception) { }

            return tableDataLst.OrderBy(s => s.SCHNAME);
        }

        public async Task<IEnumerable<TableViewModelDisadvantaged>> GetTableDataDisadvantaged()
        {
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
            catch (Exception) { }

            return tableDataLst.OrderBy(s => s.SCHNAME);
        }

        public async Task SaveAutoCompleteData(IEnumerable<AutocompleteViewModel> autoCompleteDataLst)
        {
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

            catch (Exception) { }
        }

        public async Task SaveNationalTableDataAll(TableViewModelAll nationalTableData)
        {
            await _redisCacheClient
                .Db0
                .AddAsync("NationalTableViewModelAll", nationalTableData, DateTimeOffset.Now.AddMinutes(30));
        }

        public async Task SaveNationalTableDataDisadvantaged(TableViewModelDisadvantaged nationalTableData)
        {
            await _redisCacheClient
                .Db0
                .AddAsync("NationalTableViewModelDisadvantaged", nationalTableData, DateTimeOffset.Now.AddMinutes(30));
        }

        public async Task SaveNationalScatterplotData(ScatterplotViewModel nationalScatterplotData)
        {
            await _redisCacheClient
                .Db0
                .AddAsync("NationalScatterplotData", nationalScatterplotData, DateTimeOffset.Now.AddMinutes(30));
        }

        public async Task SaveScatterplotData(IEnumerable<ScatterplotViewModel> scatterplotDataLst)
        {
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

            catch (Exception) { }
            
        }

        public async Task SaveTableDataAll(IEnumerable<TableViewModelAll> tableDataLst)
        {
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

            catch (Exception) { }
        }

        public async Task SaveTableDataDisadvantaged(IEnumerable<TableViewModelDisadvantaged> tableDataLst)
        {
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

            catch (Exception) { }
        }

        public async Task<ScatterplotViewModel> GetNationalScatterplotData()
        {
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
            catch (Exception) { }

            return nationalData;
        }
    }
}
