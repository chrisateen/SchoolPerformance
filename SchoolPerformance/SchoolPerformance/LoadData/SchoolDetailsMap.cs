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
            Map(m => m.URN);
            Map(m => m.STREET);
            Map(m => m.LOCALITY);
            Map(m => m.ADDRESS3);
            Map(m => m.TOWN);
            Map(m => m.POSTCODE);


        }
    }
}
