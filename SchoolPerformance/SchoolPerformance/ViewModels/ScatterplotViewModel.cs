using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.ViewModels
{
    public class ScatterplotViewModel
    {
        [Display(Name="Unique Reference Number")]
        public int URN { get; set; }

        [Display(Name = "DfE Number")]
        public int LEAESTAB { get; set; }

        [Display(Name = "School Name")]
        public string SCHNAME { get; set; }

        [Display(Name = "Percentage of Disadvataged Pupils at the end of KS4")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTFSM6CLA1A { get; set; }

        [Display(Name = "Attainment 8 Score")]
        public double? ATT8SCR { get; set; }

        [Display(Name = "Attainment 8 Score Disadvataged Pupils")]
        public double? ATT8SCR_FSM6CLA1A { get; set; }

        [Display(Name = "Attainment 8 Score Non-Disadvataged Pupils")]
        public double? ATT8SCR_NFSM6CLA1A { get; set; }

        [Display(Name = "Progress 8 Score")]
        public double? P8MEA { get; set; }

        [Display(Name = "Progress 8 Score Disadvataged Pupils")]
        public double? P8MEA_FSM6CLA1A { get; set; }

        [Display(Name = "Progress 8 Score Non-Disadvataged Pupils")]
        public double? P8MEA_NFSM6CLA1A { get; set; }

        [Display(Name = "Percentage of pupils achieving grade 9-4 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTL2BASICS_94 { get; set; }

        [Display(Name = "Percentage of Disadvataged pupils achieving grade 9-4 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTFSM6CLA1ABASICS_94 { get; set; }

        [Display(Name = "Percentage of Non-Disadvataged pupils achieving grade 9-4 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTNOTFSM6CLA1ABASICS_94 { get; set; }

        [Display(Name = "Percentage of pupils achieving grade 9-5 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTL2BASICS_95 { get; set; }

        [Display(Name = "Percentage of Disadvataged pupils achieving grade 9-5 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTFSM6CLA1ABASICS_95 { get; set; }

        [Display(Name = "Percentage of Non-Disadvataged pupils achieving grade 9-5 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTNOTFSM6CLA1ABASICS_95 { get; set; }

        public static implicit operator ScatterplotViewModel(SchoolResult result)
        {
            return new ScatterplotViewModel
            {
                URN = result.URN,
                LEAESTAB = result.School.LAESTAB,
                SCHNAME = result.School.SCHNAME,
                PTFSM6CLA1A = result.PTFSM6CLA1A,
                ATT8SCR = result.ATT8SCR,
                ATT8SCR_FSM6CLA1A = result.ATT8SCR_FSM6CLA1A,
                ATT8SCR_NFSM6CLA1A = result.ATT8SCR_NFSM6CLA1A,
                P8MEA = result.P8MEA,
                P8MEA_FSM6CLA1A = result.P8MEA_FSM6CLA1A,
                P8MEA_NFSM6CLA1A = result.P8MEA_NFSM6CLA1A,
                PTL2BASICS_94 = result.PTL2BASICS_94,
                PTFSM6CLA1ABASICS_94 = result.PTFSM6CLA1ABASICS_94,
                PTNOTFSM6CLA1ABASICS_94 = result.PTNOTFSM6CLA1ABASICS_94,
                PTL2BASICS_95 = result.PTL2BASICS_95,
                PTFSM6CLA1ABASICS_95 = result.PTFSM6CLA1ABASICS_95,
                PTNOTFSM6CLA1ABASICS_95 = result.PTFSM6CLA1ABASICS_95
            };
        }

    }
}
