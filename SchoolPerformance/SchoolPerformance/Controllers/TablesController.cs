﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SchoolPerformance.Cache;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;

namespace SchoolPerformance.Controllers
{
    public class TablesController : Controller
    {

        private ISchoolPerformanceRepository<SchoolResult> _result;
        private IRedisCache _cache;
        private ILogger<TablesController> _logger;

        public TablesController(ISchoolPerformanceRepository<SchoolResult> result, IRedisCache cache,
            ILogger<TablesController> logger)
        {
            _result = result;
            _cache = cache;
            _logger = logger;
        }


        public IActionResult Index()
        {
            _logger.LogInformation("Request made to view Results Table All page");

            //Empty TableViewModel returned 
            //to allow me to use HTML display name helpers
            return View(new TableViewModelAll());
        }

        public IActionResult All()
        {
            return RedirectToAction("Index");
        }

        public IActionResult Disadvantaged()
        {
            _logger.LogInformation("Request made to view Results Table Disadvantaged page");

            //Empty TableViewModel returned 
            //to allow me to use HTML display name helpers
            return View(new TableViewModelDisadvantaged());
        }

        [HttpPost]
        public async Task<IActionResult> GetResultsAll()
        {
            _logger.LogInformation("Request made to get JSON object with data for Table All page");

            //Check if data is in cache
            var resultViewModel = await _cache.GetTableDataAll();

            //Get results for all schools from database if data is not in cache
            if (resultViewModel.Count() == 0)
            {
                _logger.LogInformation("Table all data is being retrieved from the database");

                var result = await _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME), r => r.School);

                //Converts from list of SchoolResult to List of TableViewModel
                resultViewModel = result.ConvertToTableViewModelAll();

                //Save list of TableViewModel data to cache
                await _cache.SaveTableDataAll(resultViewModel);
            }

            //Check if data is in cache
            var resultNatViewModel = await _cache.GetNationalTableDataAll();

            //Get the national data from database if data is not in cache
            if (resultNatViewModel == null)
            {
                _logger.LogInformation("Table national all data is being retrieved from the database");

                var nationalResultLst = await _result.GetNational();

                //Because there is only currently national data for 2019 there should only be 1 result
                var nationalResult = nationalResultLst.First();

                //Convert national SchoolResult object to a TableViewModel object
                resultNatViewModel = nationalResult;

                //Save National TableViewModel data to cache
                await _cache.SaveNationalTableDataAll(resultNatViewModel);
            }


            var data = new { data = resultViewModel, national = resultNatViewModel };

            return Json(data);
        }


        [HttpPost]
        public async Task<IActionResult> GetResultsDisadvantaged()
        {
            _logger.LogInformation("Request made to get JSON object with data for Table All page");

            //Check if data is in cache
            var resultViewModel = await _cache.GetTableDataDisadvantaged();

            //Get results for all schools if data is not in cache
            if (resultViewModel.Count() == 0)
            {
                _logger.LogInformation("Table disadvantaged data is being retrieved from the database");

                //Get results for all schools 
                //where the percentage of disadvantaged pupils is not null
                var result = await _result.Get(
                    r => r.PTFSM6CLA1A != null,
                    r => r.OrderBy(s => s.School.SCHNAME),
                    r => r.School);

                //Converts from list of SchoolResult to List of TableViewModel
                resultViewModel = result.ConvertToTableViewModelDisadvantaged();

                //Save list of TableViewModel data to cache
                await _cache.SaveTableDataDisadvantaged(resultViewModel);

            }

            //Check if data is in cache
            var resultNatViewModel = await _cache.GetNationalTableDataDisadvantaged();

            //Get the national data from database if not in cache
            if (resultNatViewModel == null)
            {
                _logger.LogInformation("Table national disadvantaged data is being retrieved from the database");

                var nationalResultLst = await _result.GetNational();

                //Because there is only currently national data for 2019 there should only be 1 result
                var nationalResult = nationalResultLst.First();

                //Convert national SchoolResult object to a TableViewModel object
                resultNatViewModel = nationalResult;

                //Save National TableViewModel data to cache
                await _cache.SaveNationalTableDataDisadvantaged(resultNatViewModel);
            }


            var data = new { data = resultViewModel, national = resultNatViewModel };

            return Json(data);
        }



    }
}