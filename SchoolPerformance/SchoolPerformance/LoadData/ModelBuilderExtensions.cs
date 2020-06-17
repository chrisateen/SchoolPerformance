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
            addData("test", "Test");
            //foreach (KeyValuePair<string, string> kvp in _dataLocations)
            //{
            //    addData(kvp.Key,kvp.Value);
            //}
        }

        public static void addData(string modelName, string fileLocation)
        {
            var modelType = Type.GetType("SchoolPerformance.Models.School,SchoolPerformance");

            var getDataMethod = Type.GetType("LoadData.ImportCSV").GetMethod("GetDataFromCSV");
            var genericM = getDataMethod.MakeGenericMethod(modelType);
            var import = new ImportCSV("C:\\Users\\no_ot\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");
            var data = genericM.Invoke(import,null);

            var data2 = ((IEnumerable)data).Cast<object>();



            //_modelBuilder.Entity<School>().HasData(data2);

            var modelEntity = _modelBuilder.GetType().GetMethod("Entity");
            var modelEntityM = modelEntity.MakeGenericMethod(modelType);

            var test = _modelBuilder.Entity<School>();

            //modelEntityM.Invoke(_modelBuilder);


        }


    }
}
