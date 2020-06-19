using CsvHelper.Configuration;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoadDataTest
{
    public class SchoolMap : ClassMap<School>
    {
        public SchoolMap()
        {
            Map(m => m.URN).TypeConverter<NullIntConverter>();
            Map(m => m.LA).Name("LEA").TypeConverter<NullIntConverter>();
            Map(m => m.ESTAB).TypeConverter<NullIntConverter>();
            Map(m => m.SCHNAME).TypeConverter<NullStringConverter>();

        }
    }
}
