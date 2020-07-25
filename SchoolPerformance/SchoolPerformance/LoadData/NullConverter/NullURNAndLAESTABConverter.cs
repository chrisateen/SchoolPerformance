using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadData
{
    /// <summary>
    /// Deals with empty URN or/and LAESTAB data
    /// </summary>
    public class NullURNAndLAESTABConverter : Int32Converter
    {
        /// <summary>
        /// Deals with empty URN or/and LAESTAB data by returning 0
        /// </summary>
        public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            if (String.IsNullOrEmpty(text))
            {
                return 0;
            }
            if(text == "NAT")
            {
                return 9;
            }

            return base.ConvertFromString(text, row, memberMapData);

        }

    }
}
