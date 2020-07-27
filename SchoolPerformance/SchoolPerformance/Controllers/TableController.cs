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
    public class TableController : Controller
    {

        private ISchoolPerformanceRepository<SchoolResult> _result;

        public TableController(ISchoolPerformanceRepository<SchoolResult> result)
        {
            _result = result;
        }


        public IActionResult Index()
        {
            //Empty TableViewModel returned 
            //to allow me to use HTML display name helpers
            return View(new TableViewModel());
        }

        [HttpPost]
        public IActionResult GetResultsAll()
        {
            var result = _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME), r => r.School);

            //Converts from list of SchoolResult to List of TableViewModel
            List<TableViewModel> resultViewModel = result.ConvertToTableViewModel();

            var data = new { data = resultViewModel };

            return Json(data);
        }
    }
}