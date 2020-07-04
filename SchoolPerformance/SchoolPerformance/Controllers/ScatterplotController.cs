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
            var result = _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME),r => r.School);

            List<ScatterplotViewModel> resultViewModel = new List<ScatterplotViewModel>();

            foreach(var item in result)
            {
                ScatterplotViewModel viewModelItem = item;
                resultViewModel.Add(viewModelItem);
            }

            return View(resultViewModel);
        }
    }
}