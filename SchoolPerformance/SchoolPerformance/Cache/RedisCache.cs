using Microsoft.Extensions.Caching.Memory;
using SchoolPerformance.ViewModels;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Cache
{
    public class RedisCache
    {
        private readonly IRedisCacheClient _redisCacheClient;

        public RedisCache(IRedisCacheClient redisCacheClient)
        {
            _redisCacheClient = redisCacheClient;
        }

        public async Task<IEnumerable<ScatterplotViewModel>> getScatterplotData ()
        {
            // Get all the scatterplot data from the cache
            var listofkeys = await _redisCacheClient.Db0.SearchKeysAsync("ScatterplotViewModel*");

            IEnumerable<ScatterplotViewModel> scatterplotDataLst = new List<ScatterplotViewModel>();

            if (listofkeys != null)
            {
                var results = await _redisCacheClient
                                .Db0
                                .GetAllAsync<ScatterplotViewModel>(listofkeys);

                scatterplotDataLst = new List<ScatterplotViewModel>(results.Values);

            }

            return scatterplotDataLst.OrderBy(s => s.SCHNAME);
            
        }

        public async Task saveScatterplotData(IEnumerable<ScatterplotViewModel> scatterplotDataLst)
        {
            var items = new List<Tuple<string, ScatterplotViewModel>>();

            foreach (var item in scatterplotDataLst)
            {
                var key = "ScatterplotViewModel" + item.URN;
                items.Add(new Tuple<string, ScatterplotViewModel> (key, item));
            }

            await _redisCacheClient
                .Db0
                .AddAllAsync(items, DateTimeOffset.Now.AddMinutes(30));
        }



    }
}
