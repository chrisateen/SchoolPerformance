using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.ViewModels
{
    public class AutocompleteViewModel
    {
        public int URN { get; set; }
        public int LAESTAB { get; set; }
        public string SCHNAME { get; set; }

        public static implicit operator AutocompleteViewModel (SchoolResult result)
        {
            return new AutocompleteViewModel
            {
                URN = result.URN,
                LAESTAB = result.School.LAESTAB,
                SCHNAME = result.School.SCHNAME

            };
        }
    }
}
