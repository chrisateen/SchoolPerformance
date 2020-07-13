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
    public class ScatterplotController : Controller
    {

        private ISchoolResultRepository<SchoolResult> _result;

        public ScatterplotController(ISchoolResultRepository<SchoolResult> result)
        {
            _result = result;
        }


        public IActionResult Index()
        {
            var result = _result.Get(r => r.PTFSM6CLA1A != null,r => r.OrderBy(s => s.School.SCHNAME),r => r.School);

            //Converts from list of SchoolResult to List of ScatterplotViewModel
            List<ScatterplotViewModel> resultViewModel = result.Convert();

            return View(resultViewModel);
        }
    }
}