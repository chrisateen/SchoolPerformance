using CsvHelper.Configuration;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoadDataTest
{
    public class SchoolResultMap : ClassMap<SchoolResult>
    {
        public SchoolResultMap()
        {
            Map(m => m.URN).TypeConverter<NullIntConverter>();
            Map(m => m.ATT8SCR).TypeConverter<NullConverter>();
            Map(m => m.ATT8SCR_FSM6CLA1A).TypeConverter<NullConverter>();

        }


    }
}
