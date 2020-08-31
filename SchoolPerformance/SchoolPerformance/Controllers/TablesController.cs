using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public TablesController(ISchoolPerformanceRepository<SchoolResult> result, IRedisCache cache)
        {
            _result = result;
            _cache = cache;
        }


        public IActionResult Index()
        {
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
            //Empty TableViewModel returned 
            //to allow me to use HTML display name helpers
            return View(new TableViewModelDisadvantaged());
        }

        //[HttpGet]
        //public async Task<IActionResult> GetResultsAll()
        //{
        //    //Get results for all schools
        //    var result = await _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME), r => r.School);

        //    //Get the national data
        //    var nationalResultLst = await _result.GetNational();

        //    //Because there is only currently national data for 2019 there should only be 1 result
        //    var nationalResult = nationalResultLst.First();

        //    //Converts from list of SchoolResult to List of TableViewModel
        //    List<TableViewModelAll> resultViewModel = result.ConvertToTableViewModelAll();

        //    //Convert national SchoolResult object to a TableViewModel object
        //    TableViewModelAll resultNatViewModel = nationalResult;

        //    var data = new { data = resultViewModel, national = resultNatViewModel };

        //    return Json(data);
        //}


        //[HttpGet]
        //public async Task<IActionResult> GetResultsDisadvantaged()
        //{
        //    //Get results for all schools 
        //    //if the percentage of disadvantaged pupils is not null
        //    var result = await _result.Get(
        //        r => r.PTFSM6CLA1A != null,
        //        r => r.OrderBy(s => s.School.SCHNAME),
        //        r => r.School);

        //    //Get the national data
        //    var nationalResultLst = await _result.GetNational();

        //    //Because there is only currently national data for 2019 there should only be 1 result
        //    var nationalResult = nationalResultLst.First();

        //    //Converts from list of SchoolResult to List of TableViewModel
        //    List<TableViewModelDisadvantaged> resultViewModel = result.ConvertToTableViewModelDisadvantaged();

        //    //Convert national SchoolResult object to a TableViewModel object
        //    TableViewModelDisadvantaged resultNatViewModel = nationalResult;

        //    var data = new { data = resultViewModel, national = resultNatViewModel };

        //    return Json(data);
        //}

        [HttpPost]
        public async Task<IActionResult> GetResultsAll()
        {
            //Check if data is in cache
            var resultViewModel = await _cache.GetTableDataAll();

            //Get results for all schools from database if data is not in cache
            if (resultViewModel.Count() == 0)
            {

                var result = await _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME), r => r.School);

                //Converts from list of SchoolResult to List of TableViewModel
                resultViewModel = result.ConvertToTableViewModelAll();

                //Save list of TableViewModel data to cache
                //await _cache.SaveTableDataAll(resultViewModel);
            }

            //Check if data is in cache
            var resultNatViewModel = await _cache.GetNationalTableDataAll();

            //Get the national data from database if data is not in cache
            if (resultNatViewModel == null)
            {
                var nationalResultLst = await _result.GetNational();

                //Because there is only currently national data for 2019 there should only be 1 result
                var nationalResult = nationalResultLst.First();

                //Convert national SchoolResult object to a TableViewModel object
                resultNatViewModel = nationalResult;

                //Save National TableViewModel data to cache
                //await _cache.SaveNationalTableDataAll(resultNatViewModel);
            }


            var data = new { data = resultViewModel, national = resultNatViewModel };

            return Json(data);
        }


        [HttpPost]
        public async Task<IActionResult> GetResultsDisadvantaged()
        {
            //Check if data is in cache
            var resultViewModel = await _cache.GetTableDataDisadvantaged();

            //Get results for all schools if data is not in cache
            if (resultViewModel.Count() == 0)
            {
                //Get results for all schools 
                //where the percentage of disadvantaged pupils is not null
                var result = await _result.Get(
                    r => r.PTFSM6CLA1A != null,
                    r => r.OrderBy(s => s.School.SCHNAME),
                    r => r.School);

                //Converts from list of SchoolResult to List of TableViewModel
                resultViewModel = result.ConvertToTableViewModelDisadvantaged();

                //Save list of TableViewModel data to cache
                //await _cache.SaveTableDataDisadvantaged(resultViewModel);

            }

            //Check if data is in cache
            var resultNatViewModel = await _cache.GetNationalTableDataDisadvantaged();

            //Get the national data from database if not in cache
            if (resultNatViewModel == null)
            {

                var nationalResultLst = await _result.GetNational();

                //Because there is only currently national data for 2019 there should only be 1 result
                var nationalResult = nationalResultLst.First();

                //Convert national SchoolResult object to a TableViewModel object
                resultNatViewModel = nationalResult;

                //Save National TableViewModel data to cache
                // await _cache.SaveNationalTableDataDisadvantaged(resultNatViewModel);
            }


            var data = new { data = resultViewModel, national = resultNatViewModel };

            return Json(data);
        }



    }
}