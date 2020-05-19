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

        [Display(Name = "Number of Pupils")]
        public int NOR { get; set; }

        [Display(Name = "Percentage of Female Pupils")]
        public double PNORG { get; set; }

        [Display(Name = "Percentage of SEN Pupils with an EHC plan")]
        public double PSENELSE { get; set; }

        [Display(Name = "Percentage of Pupils with SEN support")]
        public double PSENELK { get; set; }

        [Display(Name = "Percentage of EAL Pupils")]
        public double PNUMEAL { get; set; }

        [Display(Name = "Percentage of FSM6 Pupils")]
        public double PNUMFSMEVER { get; set; }

        [Display(Name = "Percentage of Disadvataged Pupils at the end of KS4")]
        public double PTFSM6CLA1A { get; set; }
    }
}
