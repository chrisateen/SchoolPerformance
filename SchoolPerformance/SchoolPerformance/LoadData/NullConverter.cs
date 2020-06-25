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
            if (text == "NE" || text == "NP" || text == "SUPP" || String.IsNullOrEmpty(text))
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

    public class NullIntConverter : Int32Converter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (text == "NE" || text == "NP" || text == "SUPP" || String.IsNullOrEmpty(text))
            {
                return null;
            }

            return base.ConvertFromString(text, row, memberMapData);
        }

    }



    public class NullURNAndLAESTABConverter : Int32Converter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (String.IsNullOrEmpty(text))
            {
                return 0;
            }
            return base.ConvertFromString(text, row, memberMapData);

        }

    }

    public class NullStringConverter : StringConverter
    {
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (String.IsNullOrEmpty(text))
            {
                return "";
            }
            return base.ConvertFromString(text, row, memberMapData);

        }

    }
}
