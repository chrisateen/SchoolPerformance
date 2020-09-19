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
using Microsoft.Extensions.Logging;

namespace SchoolPerformance.Controllers
{
    public class ScatterplotController : Controller
    {

        private ISchoolPerformanceRepository<SchoolResult> _result;
        private IRedisCache _cache;
        private ILogger<ScatterplotController> _logger;

        public ScatterplotController(ISchoolPerformanceRepository<SchoolResult> result, IRedisCache cache, ILogger<ScatterplotController> logger)
        {
            _result = result;
            _cache = cache;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Request made to view Scatterplot page");

            //Check if data is in cache and if so get the data from cache
            IEnumerable<ScatterplotViewModel> schoolLst = await _cache.GetScatterplotData();
            ScatterplotViewModel national = await _cache.GetNationalScatterplotData();

            //if data is not in cache get data from database and save data in cache
            if (schoolLst.Count() == 0)
            {
                var result = await _result.Get(r => r.PTFSM6CLA1A != null,
                    r => r.OrderBy(s => s.School.SCHNAME),
                    r => r.School);

                //Converts from list of SchoolResult to List of ScatterplotViewModel
                schoolLst = result.ConvertToScatterplotViewModel();

                await _cache.SaveScatterplotData(schoolLst);
            }

            //if national data is not in cache get data from database and save data in cache
            if(national == null)
            {
                var result = await _result.GetNational(r => r.School);

                national = result.First();

                await _cache.SaveNationalScatterplotData(national);
            }

            var resultViewModel = new ScatterplotListViewModel()
            {
                schoolData = schoolLst,
                nationalData = national,
            };

            return View(resultViewModel);
        }
    }
}