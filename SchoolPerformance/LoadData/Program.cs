using CsvHelper;
using SchoolPerformance.Models;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LoadData
{
    class Program
    {
        static void Main(string[] args)
        {
            ImportCSV importCSV = new ImportCSV("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");
            var data = importCSV.GetDataFromCSV(new School());
            Console.WriteLine(data.Count());
        }
    }
}
