using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Models
{
    public class SchoolDetails
    {
        [Display(Name="Unique Reference Number")]
        public int URN { get; set; }

        [Display(Name = "Street")]
        public int STREET { get; set; }

        [Display(Name = "Locality")]
        public string LOCALITY { get; set; }

        [Display(Name = "Address 3")]
        public string ADDRESS3 { get; set; }

        [Display(Name = "Town")]
        public string TOWN { get; set; }

        [Display(Name = "PostCode")]
        public string POSTCODE { get; set; }

        [Display(Name = "Type of School")]
        public string SCHOOLTYPE { get; set; }

        [Display(Name = "Mixed or Single Sex School")]
        public string GENDER { get; set; }

        [Display(Name = "Religious Character of the School")]
        public string RELCHAR { get; set; }

        public School School { get; set; }
    }
}
