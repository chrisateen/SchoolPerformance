using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using System.Linq;
using SchoolPerformance.Models;

namespace LoadData
{
    /// <summary>
    /// Imports data from CSV to an IEnumerable list of a model type
    /// </summary>
    public class ImportCSV
    {
        private string _fileLocation;
        private CsvReader _csv;

        public ImportCSV(string fileLocation)
        {
            _fileLocation = fileLocation;
        }

        /// <summary>
        /// Creates a collection of model data from a CSV file
        /// </summary>
        /// <typeparam name="M">Type of model class data</typeparam>
        /// <returns>IEnumerable list of specified model type </returns>
        public IEnumerable<M> GetDataFromCSV<M> ()
        {
            try
            {
                using (var reader = new StreamReader(_fileLocation))
                using (_csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var modelName = typeof(M).Name;

                    if (modelName == "School")
                    {
                        _csv.Configuration.RegisterClassMap<SchoolMap>();

                        //No exception should be thrown if there are missing data for a field
                        _csv.Configuration.MissingFieldFound = null;

                        //Gets rid of bad data
                        _csv.Configuration.ShouldSkipRecord = row => row.Length < 3;

                        var data = _csv.GetRecords<School>().ToList();

                        return (IEnumerable<M>)data;
                    }

                    if (modelName == "SchoolResult")
                    {
                        _csv.Configuration.RegisterClassMap<SchoolResultMap>();

                        //No exception should be thrown if there are missing data for a field
                        _csv.Configuration.MissingFieldFound = null;

                        //Gets rid of bad data
                        _csv.Configuration.ShouldSkipRecord = row => row.Length < 3;

                        var data = _csv.GetRecords<SchoolResult>().ToList();

                        return (IEnumerable<M>)data;
                    }

                    if (modelName == "SchoolDetails")
                    {
                        _csv.Configuration.RegisterClassMap<SchoolDetailsMap>();

                        //No exception should be thrown if there are missing data for a field
                        _csv.Configuration.MissingFieldFound = null;

                        var data = _csv.GetRecords<SchoolDetails>().ToList();

                        return (IEnumerable<M>)data;
                    }

                    if (modelName == "SchoolContextual")
                    {
                        _csv.Configuration.RegisterClassMap<SchoolContextualMap>();

                        //No exception should be thrown if there are missing data for a field
                        _csv.Configuration.MissingFieldFound = null;

                        //Exclude any national data from being loaded
                        //This is where URN value is NAT
                        _csv.Configuration.ShouldSkipRecord = row => row[0] == "NAT";

                        var data = _csv.GetRecords<SchoolContextual>().ToList();

                        return (IEnumerable<M>)data;
                    }

                    //If the type is not one of the models defined for this project
                    //an exception is thrown
                    else
                    {
                        throw new ArgumentException($"Model name does not exist");
                    }

                }

            } 
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException("CSV file does not exist");
            }

        }

    }
}
