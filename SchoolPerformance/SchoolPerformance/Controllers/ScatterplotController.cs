using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;
using StackExchange.Redis.Extensions.Core.Abstractions;
using SchoolPerformance.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace SchoolPerformance.Controllers
{
    public class ScatterplotController : Controller
    {

        private ISchoolPerformanceRepository<SchoolResult> _result;
        private IRedisCache _cache;

        public ScatterplotController(ISchoolPerformanceRepository<SchoolResult> result, IRedisCache cache)
        {
            _result = result;
            _cache = cache;

        }


        public async Task<IActionResult> Index()
        {

            //var result = await _result.Get(r => r.PTFSM6CLA1A != null,
            //        r => r.OrderBy(s => s.School.SCHNAME),
            //        r => r.School);

            ////Converts from list of SchoolResult to List of ScatterplotViewModel
            //var resultViewModel = result.ConvertToScatterplotViewModel();

            var resultViewModel = await _cache.getScatterplotData();

            if (resultViewModel.Count() == 0)
            {
                var result = await _result.Get(r => r.PTFSM6CLA1A != null,
                    r => r.OrderBy(s => s.School.SCHNAME),
                    r => r.School);

                //Converts from list of SchoolResult to List of ScatterplotViewModel
                resultViewModel = result.ConvertToScatterplotViewModel();

                await _cache.saveScatterplotData(resultViewModel);
            }


            return View(resultViewModel);
        }
    }
}