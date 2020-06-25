using CsvHelper.Configuration;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LoadData
{
    public class SchoolResultMap : ClassMap<SchoolResult>
    {
        public SchoolResultMap()
        {
            Map(m => m.URN).TypeConverter<NullURNAndLAESTABConverter>();
            Map(m => m.PTFSM6CLA1A).TypeConverter<NullDoubleConverter>();
            Map(m => m.ATT8SCR).TypeConverter<NullDoubleConverter>();
            Map(m => m.ATT8SCR_FSM6CLA1A).TypeConverter<NullDoubleConverter>();
            Map(m => m.ATT8SCR_NFSM6CLA1A).TypeConverter<NullDoubleConverter>();
            Map(m => m.P8MEA).TypeConverter<NullDoubleConverter>();
            Map(m => m.P8MEA_FSM6CLA1A).TypeConverter<NullDoubleConverter>();
            Map(m => m.P8MEA_NFSM6CLA1A).TypeConverter<NullDoubleConverter>();
            Map(m => m.PTL2BASICS_94).TypeConverter<NullDoubleConverter>();
            Map(m => m.PTFSM6CLA1ABASICS_94).TypeConverter<NullDoubleConverter>();
            Map(m => m.PTNOTFSM6CLA1ABASICS_94).TypeConverter<NullDoubleConverter>();
            Map(m => m.PTL2BASICS_95).TypeConverter<NullDoubleConverter>();
            Map(m => m.PTFSM6CLA1ABASICS_95).TypeConverter<NullDoubleConverter>();
            Map(m => m.PTNOTFSM6CLA1ABASICS_95).TypeConverter<NullDoubleConverter>();
        }
    }
}
