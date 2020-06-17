using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace LoadDataTest
{
    class Program
    {
        public static IConfiguration _configuration;

        static void Main(string[] args)
        {
            //var data = ModelBuilderExtensions.GetData<School>("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");

            //ImportCSV importCSV = new ImportCSV("C:\\Users\\no_ot\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");
            //var data = importCSV.GetDataFromCSV(new School());

            //Console.WriteLine(data.Count());

            //_configuration = new ConfigurationBuilder()
            //                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
            //                .AddJsonFile("appsettings.json", false)
            //                .Build();

            //var optionsBuilder = new DbContextOptionsBuilder<SchoolPerformanceContext>();
            //optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SchoolPerformanceDb"));

            var schools = new List<School>
            {
                new School { URN = 2, LAESTAB = 2,SCHNAME = "Test 1" },
                new School { URN = 1, LAESTAB = 1,SCHNAME = "Test 2" }
            };

            //using (var context = new SchoolPerformanceContext(optionsBuilder.Options))
            //{
            //    context.School.AddRange(schools);
            //    context.SaveChanges();
            //}

            var modelType = Type.GetType("SchoolPerformance.Models.School,SchoolPerformance");
            var getDataMethod = Type.GetType("LoadDataTest.ModelBuilderExtensions").GetMethod("GetData");
            var genericM = getDataMethod.MakeGenericMethod(modelType);
            var data = genericM.Invoke(null, new object[] { "C:\\Users\\no_ot\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv" });

            


        }



    }
}
