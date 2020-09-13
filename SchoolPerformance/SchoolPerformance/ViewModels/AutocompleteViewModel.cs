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
        public string schoolName { get; set; }

        public static implicit operator AutocompleteViewModel (SchoolResult result)
        {
            return new AutocompleteViewModel
            {
                URN = result.URN,
                LAESTAB = result.School.LAESTAB,
                schoolName = result.School.SCHNAME

            };
        }
    }
}
