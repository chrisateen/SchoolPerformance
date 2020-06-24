using CsvHelper.Configuration;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoadData
{
    public class SchoolDetailsMap : ClassMap<SchoolDetails>
    {
        public SchoolDetailsMap()
        {
            Map(m => m.URN).TypeConverter<NullURNAndLAESTABConverter>();
            Map(m => m.STREET).TypeConverter<NullStringConverter>();
            Map(m => m.LOCALITY).TypeConverter<NullStringConverter>();
            Map(m => m.ADDRESS3).TypeConverter<NullStringConverter>();
            Map(m => m.TOWN).TypeConverter<NullStringConverter>();
            Map(m => m.POSTCODE).TypeConverter<NullStringConverter>();
            Map(m => m.SCHOOLTYPE).TypeConverter<NullStringConverter>();
            Map(m => m.GENDER).TypeConverter<NullStringConverter>();
            Map(m => m.RELCHAR).TypeConverter<NullStringConverter>();
        }
    }
}
