using CsvHelper;
using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace LoadDataTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = ModelBuilderExtensions.GetData<School>("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");
            Console.WriteLine(data.Count());

        }
    }
}
