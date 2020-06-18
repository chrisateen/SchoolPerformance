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

            ImportCSV importCSV = new ImportCSV("C:\\Users\\no_ot\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_ks4final.csv");
            IEnumerable<SchoolResult> data = importCSV.GetDataFromCSV(new SchoolResult());

            Console.WriteLine(data.Where(x=>x.URN != 0).Count());




        }



    }
}
