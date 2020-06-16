using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoadData
{
    public static class ModelBuilderExtensions
    {

        public static void Seed(this ModelBuilder modelBuilder, string fileLocation)
        {
            var data = GetData<School>("D:\\Google Drive\\Bbk Computer Science\\Project\\Data\\england_school_information.csv");
            modelBuilder.Entity<School>().HasData(data);

        }

        public static IEnumerable<T> GetData<T>(string fileLocation)
        {
            ImportCSV importCSV = new ImportCSV(fileLocation);
            var data = importCSV.GetDataFromCSV(new School());
            return (IEnumerable<T>)data;
        }
    }
}
