using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;

namespace SchoolPerformance.Controllers
{
    public class TablesController : Controller
    {

        private ISchoolPerformanceRepository<SchoolResult> _result;

        public TablesController(ISchoolPerformanceRepository<SchoolResult> result)
        {
            _result = result;
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

        [HttpPost]
        public IActionResult GetResultsAll()
        {
            //Get results for all schools
            var result = _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME), r => r.School);

            //Get the national data
            var nationalResult = _result.GetNational().First();

            //Converts from list of SchoolResult to List of TableViewModel
            List<TableViewModelAll> resultViewModel = result.ConvertToTableViewModelAll();

            //Convert national SchoolResult object to a TableViewModel object
            TableViewModelAll resultNatViewModel = nationalResult;

            var data = new { data = resultViewModel, national = resultNatViewModel };

            return Json(data);
        }


        [HttpPost]
        public IActionResult GetResultsDisadvantaged()
        {
            //Get results for all schools
            var result = _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME), r => r.School);

            //Get the national data
            var nationalResult = _result.GetNational().First();

            //Converts from list of SchoolResult to List of TableViewModel
            List<TableViewModelDisadvantaged> resultViewModel = result.ConvertToTableViewModelDisadvantaged();

            //Convert national SchoolResult object to a TableViewModel object
            TableViewModelDisadvantaged resultNatViewModel = nationalResult;

            var data = new { data = resultViewModel, national = resultNatViewModel };

            return Json(data);
        }

    }
}