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

        [HttpPost]
        public IActionResult School(int id)
        {
            //Action method added due to search box
            return RedirectToAction("Index", new { id = id });
        }

        [Route("{id:int}")]
        [Route("/[controller]/{id:int}")]
        public async Task<IActionResult> Index(int id)
        {
            //Get the school data for the 2019 academic year
            var schoolResult = await _result
                .GetByUrnOrLAESATB(id, s => s.ACADEMICYEAR == 2019, s => s.School);

            var schoolContextual = await _contextual
                .GetByUrnOrLAESATB(id ,s => s.ACADEMICYEAR == 2019);

            //Return a page letting the user know the id cannot be found
            //If the school id does not exist
            if (schoolResult.Count() == 0)
            {
                return View("SchoolNotFound", id);
            }

            //Get the national data
            var nationalResult = await _result.GetNational(s => s.ACADEMICYEAR == 2019, s => s.School);
            var nationalContextual = await _contextual.GetNational(s => s.ACADEMICYEAR == 2019);

            //Add School and National data to the SchoolViewModel
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