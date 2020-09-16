using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.ViewModels
{
    public class ScatterplotListViewModel
    {
        public IEnumerable<ScatterplotViewModel> schoolData { get; set; }
        public ScatterplotViewModel nationalData { get; set;}
    }
}
