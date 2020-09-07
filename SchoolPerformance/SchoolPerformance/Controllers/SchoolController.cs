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

        [Route("{id:int}")]
        [Route("/[controller]/{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            //Get the school data 
            var schoolResult = await _result.Get(s => s.URN == id, null, s => s.School);
            var schoolContextual = await _contextual.Get(s => s.URN == id && s.ACADEMICYEAR == 2019);


            //Get the national data
            var nationalResult = await _result.GetNational(null, null, s => s.School);
            var nationalContextual = await _contextual.GetNational(s => s.ACADEMICYEAR == 2019);


            var schoolViewModel = new SchoolViewModel()
            {
                ResultSchool = schoolResult.First(),
                ResultNational = nationalResult.First(),
                ContextualSchool = schoolContextual.First(),
                ContextualNational = nationalContextual.First()
            };

            return View(schoolViewModel);
        }





    }
}