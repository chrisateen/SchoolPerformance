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

        public static void Seed(this ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
            _dataLocations.Add("SchoolResult", "C:\\Users\\no_ot\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_ks4final.csv");
            _dataLocations.Add("School", "C:\\Users\\no_ot\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");
            foreach (KeyValuePair<string, string> kvp in _dataLocations)
            {
                addData(kvp.Key, kvp.Value);
            }
        }

        public static void addData(string modelName, string fileLocation)
        {
            var import = new ImportCSV(fileLocation);

            if (modelName == "School")
            {

                IEnumerable<School> data = import.GetDataFromCSV<School>();
                _modelBuilder.Entity<School>().HasData(data);

            }

            if (modelName == "SchoolResult")
            {

                IEnumerable<SchoolResult> data = import.GetDataFromCSV<SchoolResult>();
                data = data.Where(x => x.URN !=0);
                _modelBuilder.Entity<SchoolResult>().HasData(data);

            }

            else
            {
                throw new ArgumentException("Model name does not exist");
            }

        }

        //Used to get school information from results file as we only want schools that exist
        public static void getSchool(string filelocation)
        {

        }


    }
}
