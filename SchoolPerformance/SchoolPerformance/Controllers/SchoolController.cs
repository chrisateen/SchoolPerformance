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
    public class SchoolController : Controller
    {
        private ISchoolPerformanceRepository<SchoolResult> _result;
        private ISchoolPerformanceRepository<SchoolContextual> _contextual;



        public SchoolController(ISchoolPerformanceRepository<SchoolResult>result, 
            ISchoolPerformanceRepository<SchoolContextual>contextual)
        {
            _result = result;
            _contextual = contextual;
        }

        public async Task<IActionResult> Index(int id)
        {
            var schoolViewModel = new SchoolViewModel();
            return View(schoolViewModel);
        }





    }
}