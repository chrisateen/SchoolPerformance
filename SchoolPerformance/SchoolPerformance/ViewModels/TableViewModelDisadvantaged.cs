using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.ViewModels
{
    public class TableViewModelDisadvantaged
    {
        [Display(Name="Unique Reference Number")]
        public int URN { get; set; }

        [Display(Name = "DfE Number")]
        public int LEAESTAB { get; set; }

        [Display(Name = "School Name")]
        public string SCHNAME { get; set; }

        [Display(Name = "Percentage of disadvantaged pupils at the end of KS4")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTFSM6CLA1A { get; set; }

        [Display(Name = "Attainment 8 score disadvantaged pupils")]
        public double? ATT8SCR_FSM6CLA1A { get; set; }


        [Display(Name = "Progress 8 score disadvantaged pupils")]
        public double? P8MEA_FSM6CLA1A { get; set; }


        [Display(Name = "Percentage of disadvantaged pupils achieving grade 9-4 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTFSM6CLA1ABASICS_94 { get; set; }


        [Display(Name = "Percentage of disadvantaged pupils achieving grade 9-5 in English and Maths")]
        [DisplayFormat(DataFormatString = "{0:P0}")]
        public double? PTFSM6CLA1ABASICS_95 { get; set; }



        /// <summary>
        /// Converts from SchoolResult model to TableViewModel
        /// </summary>
        /// <param name="result">SchoolResult model object 
        /// to be converted to TableResultViewModel object</param>
        public static implicit operator TableViewModelDisadvantaged(SchoolResult result)
        {
            //Conversion for national SchoolResult
            if (result.School == null)
            {
                return new TableViewModelDisadvantaged
                {
                    URN = result.URN,
                    LEAESTAB = result.URN,
                    SCHNAME = "National",
                    PTFSM6CLA1A = result.PTFSM6CLA1A,
                    ATT8SCR_FSM6CLA1A = result.ATT8SCR_FSM6CLA1A,
                    P8MEA_FSM6CLA1A = result.P8MEA_FSM6CLA1A,
                    PTFSM6CLA1ABASICS_94 = result.PTFSM6CLA1ABASICS_94,
                    PTFSM6CLA1ABASICS_95 = result.PTFSM6CLA1ABASICS_95
                };
            }

            return new TableViewModelDisadvantaged
            {
                URN = result.URN,
                LEAESTAB = result.School.LAESTAB,
                SCHNAME = result.School.SCHNAME,
                PTFSM6CLA1A = result.PTFSM6CLA1A,
                ATT8SCR_FSM6CLA1A = result.ATT8SCR_FSM6CLA1A,
                P8MEA_FSM6CLA1A = result.P8MEA_FSM6CLA1A,
                PTFSM6CLA1ABASICS_94 = result.PTFSM6CLA1ABASICS_94,
                PTFSM6CLA1ABASICS_95 = result.PTFSM6CLA1ABASICS_95
            };

        }
    }

}


