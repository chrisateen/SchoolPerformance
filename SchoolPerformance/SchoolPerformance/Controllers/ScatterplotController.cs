using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;

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
            var result = _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME),r => r.School);

            return View(result);
        }
    }
}