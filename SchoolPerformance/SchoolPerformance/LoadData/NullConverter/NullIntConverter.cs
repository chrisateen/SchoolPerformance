using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadData
{

    public class NullIntConverter : Int32Converter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == "NE" || text == "NA" || text == "NP" || text == "SUPP" || text == "LOWCOV" || String.IsNullOrEmpty(text))
            {
                return null;
            }

            return base.ConvertFromString(text, row, memberMapData);
        }

    }

}
