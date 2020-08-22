using SchoolPerformance.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPerformance.Cache
{
    public interface IRedisCache
    {
        Task<IEnumerable<ScatterplotViewModel>> getScatterplotData();
        Task saveScatterplotData(IEnumerable<ScatterplotViewModel> scatterplotDataLst);
    }
}