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

            ImportCSV importCSV = new ImportCSV("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_ks4final.csv");
            IEnumerable<School> data = importCSV.GetDataFromCSV(new School());

            Console.WriteLine(data.Where(x=>x.URN != 0).Count());
            data = data.Take(5);

            foreach(var d in data)
            {
                Console.WriteLine($"{d.LAESTAB} , {d.URN}");
            }



        }



    }
}
