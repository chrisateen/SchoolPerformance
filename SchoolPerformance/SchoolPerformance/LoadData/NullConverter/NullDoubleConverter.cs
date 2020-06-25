using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadData
{
    public class NullDoubleConverter : DoubleConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == "NE" || text == "NA" || text == "NP" || text == "SUPP" || text == "LOWCOV" || String.IsNullOrEmpty(text))
            {
                return null;
            }

            //Sorts out string with a percentage sign
            if (text.Contains("%"))
            {
                var doubleNum = double.Parse(text.Trim().Split('%')[0])/100;

                return doubleNum;
            }

            return base.ConvertFromString(text, row, memberMapData);
        }

    }
}
