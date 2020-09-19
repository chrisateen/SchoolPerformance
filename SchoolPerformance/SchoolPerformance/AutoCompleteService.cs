using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SchoolPerformance.Cache;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;

namespace SchoolPerformance
{
    public class AutoCompleteService
    {
        private ISchoolPerformanceRepository<SchoolResult> _result;
        private static IEnumerable<AutocompleteViewModel> _schools;
        private IRedisCache _cache;
        private ILogger<AutoCompleteService> _logger;

        public AutoCompleteService(ISchoolPerformanceRepository<SchoolResult> result, IRedisCache cache,
            ILogger<AutoCompleteService> logger)
        {
            _result = result;
            _schools = new List<AutocompleteViewModel>();
            _cache = cache;
            _logger = logger;
        }

        /// <summary>
        /// Get school data to use for search autocomplete functionality
        /// </summary>
        /// <returns>
        /// List of autcomplete data to use for search autocomplete functionality
        /// </returns>
        public async Task<IEnumerable<AutocompleteViewModel>> Get()
        {
            _logger.LogInformation("Executing Get method in AutoCompleteService class.");

            //Get data if the _schools list is empty
            if (_schools.Count() == 0)
            {
                //Attempt to get data from cache first
                var cacheData = await _cache.GetAutoCompleteData();

                //Get data from database if it is not in cache
                if (cacheData.Count() == 0)
                {

                    _logger.LogInformation("Autocomplete data is being retrieved from the database");

                    var schoolLst = await _result.GetAll(r => r.School);

                    _schools = schoolLst.ConvertToAutocompleteViewModel();

                    //Save data to cache for future use
                    await _cache.SaveAutoCompleteData(_schools);
                }
                else
                {
                    _schools = cacheData;
                }
               
            }

            return _schools;
        }
    }
}
