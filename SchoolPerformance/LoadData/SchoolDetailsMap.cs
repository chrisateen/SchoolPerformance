using CsvHelper.Configuration;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoadDataTest
{
    public class SchoolDetailsMap : ClassMap<SchoolDetails>
    {
        public SchoolDetailsMap()
        {
            Map(m => m.URN);
            Map(m => m.STREET);


        }
    }
}
