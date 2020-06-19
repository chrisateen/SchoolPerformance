using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using SchoolPerformance.Models;

namespace LoadData
{
    public class ImportCSV
    {
        private string _fileLocation;
        private CsvReader _csv;

        public ImportCSV(string fileLocation)
        {
            _fileLocation = fileLocation;
        }

        public IEnumerable<M> GetDataFromCSV<M> ()
        {
            using (var reader = new StreamReader(_fileLocation))
            using (_csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var modelName = typeof(M).Name;

                if (modelName == "School")
                {
                    _csv.Configuration.RegisterClassMap<SchoolMap>();
                    _csv.Configuration.MissingFieldFound = null;

                    //Gets rid of bad data
                    _csv.Configuration.ShouldSkipRecord = row => row.Length < 3;

                    var data = _csv.GetRecords<School>().ToList();

                    return (IEnumerable<M>)data;
                }

                else
                {
                    _csv.Configuration.RegisterClassMap<SchoolResultMap>();
                    _csv.Configuration.MissingFieldFound = null;
                    //Gets rid of bad data
                    _csv.Configuration.ShouldSkipRecord = row => row.Length < 3;
                    var data = _csv.GetRecords<SchoolResult>().ToList();

                    return (IEnumerable<M>)data;
                }
            }

        }

    }
}
