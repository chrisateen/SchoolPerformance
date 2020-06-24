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
            Map(m => m.URN).TypeConverter<NullURNAndLAESTABConverter>();
            Map(m => m.LA).Name("LEA").TypeConverter<NullURNAndLAESTABConverter>();
            Map(m => m.ESTAB).TypeConverter<NullURNAndLAESTABConverter>();
            Map(m => m.SCHNAME).TypeConverter<NullStringConverter>();

        }
    }
}
