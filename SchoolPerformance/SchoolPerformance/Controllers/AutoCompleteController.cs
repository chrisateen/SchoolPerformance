using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;

namespace SchoolPerformance.Controllers
{
    public class AutoCompleteController : Controller
    {
        private ISchoolPerformanceRepository<SchoolResult> _result;
        private static IEnumerable<AutocompleteViewModel> _schools; 

        public AutoCompleteController(ISchoolPerformanceRepository<SchoolResult> result)
        {
            _result = result;
            _schools = new List<AutocompleteViewModel>();
        }

        [HttpPost]
        public async Task<IActionResult> Get()
        {

            return Json(_schools);
        }
    }
}