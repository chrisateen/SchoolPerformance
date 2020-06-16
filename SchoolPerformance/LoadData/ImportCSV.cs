using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.IO;
using CsvHelper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;

namespace LoadDataTest
{
    public class ImportCSV
    {
        private readonly string _fileLocation;
        private CsvReader _csv;

        public ImportCSV(string fileLocation)
        {
            _fileLocation = fileLocation;
        }

        public IEnumerable<M> GetDataFromCSV <M> (M Model) 
        {
            using (var reader = new StreamReader(_fileLocation))
            using (_csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                RegisterMap();
                var data = _csv.GetRecords<M>().ToList();
                
               return data;

            }
        }

        private void RegisterMap()
        {
            _csv.Configuration.RegisterClassMap<SchoolMap>();
            _csv.Configuration.RegisterClassMap<SchoolResultMap>();
        }

    }
}
