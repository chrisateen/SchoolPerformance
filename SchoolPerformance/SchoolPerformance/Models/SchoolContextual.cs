using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Models
{
    public class SchoolContextual
    {
        [Display(Name="Unique Reference Number")]
        public int URN { get; set; }

        [Display(Name = "Academic Year")]
        public int ACADEMICYEAR { get; set; }

        [Display(Name = "Number of pupils")]
        public int? NOR { get; set; }

        [Display(Name = "Percentage of female pupils")]
        [DisplayFormat(DataFormatString = "{0:0}%")]
        public double? PNORG { get; set; }

        [Display(Name = "Percentage of SEN pupils with an EHC plan")]
        [DisplayFormat(DataFormatString = "{0:0}%")]
        public double? PSENELSE { get; set; }

        [Display(Name = "Percentage of pupils with SEN support")]
        [DisplayFormat(DataFormatString = "{0:0}%")]
        public double? PSENELK { get; set; }

        [Display(Name = "Percentage of EAL pupils")]
        [DisplayFormat(DataFormatString = "{0:0}%")]
        public double? PNUMEAL { get; set; }

        [Display(Name = "Percentage of FSM6 pupils")]
        [DisplayFormat(DataFormatString = "{0:0}%")]
        public double? PNUMFSMEVER { get; set; }

        public School School { get; set; }
    }
}
