using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace SchoolPerformance.Controllers
{
    public class ScatterplotController : Controller
    {

        private ISchoolPerformanceRepository<SchoolResult> _result;
        private IRedisCacheClient _redisCacheClient;

        public ScatterplotController(ISchoolPerformanceRepository<SchoolResult> result, IRedisCacheClient redisCacheClient)
        {
            _result = result;
            _redisCacheClient = redisCacheClient;
        }


        public async Task<IActionResult> Index()
        {

                var result = await _result.Get(r => r.PTFSM6CLA1A != null,
                    r => r.OrderBy(s => s.School.SCHNAME),
                    r => r.School);

                //Converts from list of SchoolResult to List of ScatterplotViewModel
                var resultViewModel = result.ConvertToScatterplotViewModel();

                await _redisCacheClient.Db0.AddAsync("Test", "Test", DateTimeOffset.Now.AddMinutes(10));

            return View(resultViewModel);
        }
    }
}