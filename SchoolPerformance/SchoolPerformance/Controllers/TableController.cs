﻿using System;
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

        private ISchoolResultRepository<SchoolResult> _result;

        public TableController(ISchoolResultRepository<SchoolResult> result)
        {
            _result = result;
        }


        public IActionResult Index()
        {
            //Empty TableViewModel returned 
            //to allow me to use html display name helpers
            return View(new TableViewModel());
        }

        [HttpGet]
        public IActionResult OnGet()
        {
            var result = _result.GetAll(r => r.OrderBy(s => s.School.SCHNAME), r => r.School);

            //Converts from list of SchoolResult to List of TableViewModel
            List<TableViewModel> resultViewModel = result.ConvertToTableViewModel();

            var data = new { data = resultViewModel };

            return Json(data);
        }
    }
}