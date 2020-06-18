using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadDataTest
{
    public class NullConverter : DoubleConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == "NE" || text == "NP" || text == "SUPP" || String.IsNullOrEmpty(text))
            {
                return 0.0;
            }

            try
            {
                return base.ConvertFromString(text, row, memberMapData);
            }
            catch (Exception e)
            {
                return 0.0;
            }
            
        }

    }

    public class NullIntConverter : Int32Converter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
           if(String.IsNullOrEmpty(text))
            {
                return 0;
            }
            return base.ConvertFromString(text, row, memberMapData);

        }

    }
}
