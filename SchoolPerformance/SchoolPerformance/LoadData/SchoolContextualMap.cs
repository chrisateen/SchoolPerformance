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
            Map(m => m.PNORG).TypeConverter<NullDoubleConverter>();
            Map(m => m.PSENELSE).TypeConverter<NullDoubleConverter>();
            Map(m => m.PSENELK).TypeConverter<NullDoubleConverter>();
            Map(m => m.PNUMEAL).TypeConverter<NullDoubleConverter>();
            Map(m => m.PNUMFSMEVER).TypeConverter<NullDoubleConverter>();
        }
    }
}
