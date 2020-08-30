using Microsoft.Extensions.Caching.Memory;
using SchoolPerformance.ViewModels;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Cache
{
    public class RedisCache : IRedisCache
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public RedisCache(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        /// <summary>
        /// Checks and gets ScatterplotViewModel data from the cache
        /// </summary>
        /// <returns>
        /// List of ScatterplotViewModel object if data is in cache
        /// or an empty ScatterplotViewModel List if data is not in cache
        /// </returns>
        public async Task<IEnumerable<ScatterplotViewModel>> getScatterplotData()
        {
            IEnumerable<ScatterplotViewModel> scatterplotDataLst = new List<ScatterplotViewModel>();

            try
            {
                // Searches and gets all the ScatterplotViewModel keys in the cache
                var listofkeys = await _redisCacheClient.Db0.SearchKeysAsync("ScatterplotViewModel*");

                //Get ScatterplotViewModel data from cache if it's in the cache
                if (listofkeys != null)
                {
                    var results = await _redisCacheClient
                                    .Db0
                                    .GetAllAsync<ScatterplotViewModel>(listofkeys);

                    scatterplotDataLst = new List<ScatterplotViewModel>(results.Values);

                }
            }
            catch (Exception) { }

            return scatterplotDataLst.OrderBy(s => s.SCHNAME);

        }


        /// <summary>
        /// Adds a list of ScatterplotViewModel objects to the cache
        /// </summary>
        /// <param name="scatterplotDataLst">
        /// List of ScatterplotViewModel objects to be added to the cache
        /// </param>
        public async Task saveScatterplotData(IEnumerable<ScatterplotViewModel> scatterplotDataLst)
        {
            var items = new List<Tuple<string, ScatterplotViewModel>>();

            foreach (var item in scatterplotDataLst)
            {
                //A key is created which included the unique reference number of the school
                //before the object is added to the cache
                var key = "ScatterplotViewModel" + item.URN;
                items.Add(new Tuple<string, ScatterplotViewModel>(key, item));
            }

            try
            {
                await _redisCacheClient
                .Db0
                .AddAllAsync(items, DateTimeOffset.Now.AddMinutes(30));
            }

            catch (Exception) { }
            
        }



    }
}
