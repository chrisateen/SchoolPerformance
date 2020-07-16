using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadData
{
    /// <summary>
    /// Deals with empty strings
    /// </summary>
    public class NullStringConverter : StringConverter
    {
        /// <summary>
        /// Deals with empty strings by returning null
        /// </summary>
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
