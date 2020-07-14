using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LoadData
{
    /// <summary>
    /// Seeds data from CSV files to database
    /// </summary>
    public static class ModelBuilderExtensions
    {
        private static Dictionary<string, string> _dataLocations = new Dictionary<string, string>();
        private static ModelBuilder _modelBuilder;
        private static IEnumerable<int> _urnList;

        //Get the directory of the folder where the csv files are held
        private static string _rootDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
        private static string _CSVRootDir = Path.Combine(_rootDir, "SchoolPerformance\\LoadData\\CSVFiles");

        //Academic year in which data should be loaded for
        private static int _academicYear;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="academicYear"></param>
        public static void Seed(this ModelBuilder modelBuilder,int academicYear)
        {
            _modelBuilder = modelBuilder;
            _academicYear = academicYear;

            addSchool(Path.Combine(_CSVRootDir, $"{_academicYear}\\england_ks4final.csv"));

            _dataLocations["SchoolResult"] = Path.Combine(_CSVRootDir, $"{_academicYear}\\england_ks4final.csv");
            _dataLocations["SchoolDetails"] = Path.Combine(_CSVRootDir, $"{_academicYear}\\england_school_information.csv");
            _dataLocations["SchoolContextual"] = Path.Combine(_CSVRootDir, $"{_academicYear}\\england_census.csv");

            foreach (KeyValuePair<string, string> kvp in _dataLocations)
            {
                addData(kvp.Key, kvp.Value);
            }
        }

        /// <summary>
        /// Gets data for entity School 
        /// </summary>
        /// <param name="fileLocation">Location where the CSV file is held</param>
        private static void addSchool(string fileLocation)
        {
            var import = new ImportCSV(fileLocation);

            //Gets the data for school
            IEnumerable<School> school = import.GetDataFromCSV<School>();

            //Remove data that do not have a URN
            school = school.Where(x => x.URN != 0);

            //Adds data to the migrations
            _modelBuilder.Entity<School>().HasData(school);

            //Creates a list of URN's that we need to import data for
            _urnList = school.Select(x => x.URN);
        }

 
        private static void addData(string modelName, string fileLocation)
        {
            var import = new ImportCSV(fileLocation);

            if (modelName == "SchoolResult")
            {

                IEnumerable<SchoolResult> data = import.GetDataFromCSV<SchoolResult>();

                //Remove data without a URN
                data = data.Where(x => x.URN !=0);

                //Academic year to be assigned to each data
                data.ToList().ForEach(x => x.ACADEMICYEAR = _academicYear);

                _modelBuilder.Entity<SchoolResult>().HasData(data);

            }

            else if (modelName == "SchoolDetails")
            {
                IEnumerable<SchoolDetails> data = import.GetDataFromCSV<SchoolDetails>();

                //Only get data where a school has a result/in the result data
                data = data.Where(x => _urnList.Contains(x.URN));
                
                _modelBuilder.Entity<SchoolDetails>().HasData(data);
            }


            else if (modelName == "SchoolContextual")
            {
                IEnumerable<SchoolContextual> data = import.GetDataFromCSV<SchoolContextual>();

                //Only get data where a school has a result/in the result data
                data = data.Where(x => _urnList.Contains(x.URN));

                //Academic year to be assigned to each data
                data.ToList().ForEach(x => x.ACADEMICYEAR = 2019);

                _modelBuilder.Entity<SchoolContextual>().HasData(data);
            }

            else
            {
                throw new ArgumentException($"Model name does not exist");
            }

        }


    }
}
