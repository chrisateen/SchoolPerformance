using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Models
{
    public class School
    {
        [Display(Name="Unique Reference Number")]
        public int URN { get; set; }

        [Display(Name = "DfE Number")]
        public int LAESTAB { get; set; }

        [Display(Name = "School Name")]
        public string SCHNAME { get; set; }
    }
}
