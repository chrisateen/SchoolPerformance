using CsvHelper.Configuration;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoadData
{
    public class SchoolMap : ClassMap<School>
    {
        public SchoolMap()
        {
            Map(m => m.URN);
            Map(m => m.LAESTAB);
            Map(m => m.SCHNAME);

        }
    }
}
