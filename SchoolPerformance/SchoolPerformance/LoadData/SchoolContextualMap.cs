using CsvHelper.Configuration;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoadData
{
    public class SchoolContextualMap : ClassMap<SchoolContextual>
    {
        public SchoolContextualMap()
        {
            Map(m => m.URN).TypeConverter<NullURNAndLAESTABConverter>();
            Map(m => m.NOR).TypeConverter<NullIntConverter>();


        }
    }
}
