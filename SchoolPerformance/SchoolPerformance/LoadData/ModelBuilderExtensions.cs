using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoadData
{
    public static class ModelBuilderExtensions
    {
        private static Dictionary<string, string> _dataLocations = new Dictionary<string, string>();
        private static ModelBuilder _modelBuilder;
        private static IEnumerable<int> _urnList;

        public static void Seed(this ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            addSchool("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_ks4final.csv");

            _dataLocations["SchoolResult"] = "D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_ks4final.csv";
            _dataLocations["SchoolDetails"] = "D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv";

            foreach (KeyValuePair<string, string> kvp in _dataLocations)
            {
                addData(kvp.Key, kvp.Value);
            }
        }

        public static void addSchool(string fileLocation)
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

 
        public static void addData(string modelName, string fileLocation)
        {
            var import = new ImportCSV(fileLocation);

            if (modelName == "SchoolResult")
            {

                IEnumerable<SchoolResult> data = import.GetDataFromCSV<SchoolResult>();

                //Remove data without a URN
                data = data.Where(x => x.URN !=0);

                //Academic year to be assigned to each data
                data.ToList().ForEach(x => x.ACADEMICYEAR = 2019);

                _modelBuilder.Entity<SchoolResult>().HasData(data);

            }

            else if (modelName == "SchoolDetails")
            {
                IEnumerable<SchoolDetails> data = import.GetDataFromCSV<SchoolDetails>();

                //Only get data where a school has a result/in the result data
                data = data.Where(x => _urnList.Contains(x.URN));
                
                _modelBuilder.Entity<SchoolDetails>().HasData(data);
            }

            else
            {
                throw new ArgumentException($"Model name does not exist");
            }

        }


    }
}
