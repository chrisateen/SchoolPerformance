﻿using Microsoft.EntityFrameworkCore;
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
        //Stores the file locations of all the CSV files
        private static Dictionary<string, string> _dataLocations = new Dictionary<string, string>();
        private static ModelBuilder _modelBuilder;

        //Stores the list of URN's so that only schools with a result are loaded
        private static IEnumerable<int> _urnList;

        //Get the directory of the folder where the CSV files are held
        private static string _rootDir = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\"));
        private static string _CSVRootDir = Path.Combine(_rootDir, "SchoolPerformance\\LoadData\\CSVFiles");

        //Academic year in which data should be loaded for
        private static int _academicYear;

        /// <summary>
        /// Seeds data from CSV files to database
        /// </summary>
        /// <param name="modelBuilder">
        /// modelBuilder in the context class
        /// </param>
        /// <param name="academicYear">
        /// Academic year for which data should be loaded for
        /// </param>
        public static void Seed(this ModelBuilder modelBuilder,int academicYear)
        {
            _modelBuilder = modelBuilder;
            _academicYear = academicYear;

            //addSchool(Path.Combine(_CSVRootDir, $"{_academicYear}\\england_ks4final.csv"));

            _dataLocations["SchoolResult"] = Path.Combine(_CSVRootDir, $"{_academicYear}\\england_ks4final.csv");
            _dataLocations["School"] = Path.Combine(_CSVRootDir, $"{_academicYear}\\england_ks4final.csv");
            _dataLocations["SchoolDetails"] = Path.Combine(_CSVRootDir, $"{_academicYear}\\england_school_information.csv");
            _dataLocations["SchoolContextual"] = Path.Combine(_CSVRootDir, $"{_academicYear}\\england_census.csv");

            foreach (KeyValuePair<string, string> kvp in _dataLocations)
            {
                if (kvp.Key == "SchoolResult")
                {
                    addSchool(kvp.Value);
                }
                else
                {
                    addData(kvp.Key, kvp.Value);
                }
                
            }
        }

        /// <summary>
        /// Gets data for entity School and School Result
        /// </summary>
        /// <param name="fileLocation">Location where the CSV file is held</param>
        private static void addSchool(string fileLocation)
        {
            var import = new ImportCSV(fileLocation);

            //Gets the data for school result
            IEnumerable<SchoolResult> schoolResults = import.getDataFromCSV<SchoolResult>();

            //Remove data that do not have a URN
            schoolResults = schoolResults.Where(x => x.URN != 0);

            //Academic year to be assigned to each data
            schoolResults.ToList().ForEach(x => x.ACADEMICYEAR = _academicYear);

            //Creates a list of URN's that we need to import data for
            _urnList = schoolResults.Select(x => x.URN);

            //Adds data to the migrations
            _modelBuilder.Entity<SchoolResult>().HasData(schoolResults);

           
        }


        /// <summary>
        /// Gets the data for all entities except School
        /// </summary>
        /// <param name="modelName">
        /// Name of the model/entity which data school be loaded for
        /// </param>
        /// <param name="fileLocation">
        /// Location where the CSV file is held
        /// </param>
        private static void addData(string modelName, string fileLocation)
        {
            var import = new ImportCSV(fileLocation);

            if (modelName == "School")
            {

                IEnumerable<School> data = import.getDataFromCSV<School>();

                //Only get data where a school has a result/in the result data
                data = data.Where(x => _urnList.Contains(x.URN));

                _modelBuilder.Entity<School>().HasData(data);

            }

            else if (modelName == "SchoolDetails")
            {
                IEnumerable<SchoolDetails> data = import.getDataFromCSV<SchoolDetails>();

                //Only get data where a school has a result/in the result data
                data = data.Where(x => _urnList.Contains(x.URN));
                
                _modelBuilder.Entity<SchoolDetails>().HasData(data);
            }


            else if (modelName == "SchoolContextual")
            {
                IEnumerable<SchoolContextual> data = import.getDataFromCSV<SchoolContextual>();

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

        public static void addNationalData()
        {
            var import = new ImportCSV(_dataLocations["SchoolResult"]);
        }


    }
}
