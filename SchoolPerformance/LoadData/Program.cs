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
using SchoolPerformaceTest;
using LoadData;
using CsvHelper.Configuration;

namespace LoadDataTest
{
    class Program
    {
        //public static IConfiguration _configuration;

        static void Main(string[] args)
        {
            //var data = ModelBuilderExtensions.GetData<School>("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");

            //ImportCSV importCSV = new ImportCSV("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_ks4final.csv");
            //IEnumerable<School> dataSchool = importCSV.GetDataFromCSV(new School()).Where(x => x.URN != 0);

            //var urn = dataSchool.Select(x => x.URN);

            //importCSV = new ImportCSV("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");
            //IEnumerable<SchoolDetails> dataDetails = importCSV.GetDataFromCSV(new SchoolDetails()).Where(x => urn.Contains(x.URN));

            //Console.WriteLine(dataSchool.Count());

            //Console.WriteLine(dataDetails.Count());

            //var connection = new InMemorySqliteConnection();
            //SchoolPerformanceContext context = connection._context;

            //Console.WriteLine(context.School.Count());

            var converter = new NullDoubleConverter();
            var t = converter.ConvertFromString("0.00", null, new MemberMapData(null));
            Console.WriteLine(t);

        }



    }
}
