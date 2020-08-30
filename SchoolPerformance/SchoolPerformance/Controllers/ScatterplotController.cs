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
            IEnumerable<ScatterplotViewModel> resultViewModel = new List<ScatterplotViewModel>();

            //Check if data is in cache and if so get the data from cache
            resultViewModel = await _cache.getScatterplotData();

            //if data is not in cache get data from database and save data in cache
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