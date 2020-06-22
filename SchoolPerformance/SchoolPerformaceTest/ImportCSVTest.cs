using CsvHelper;
using LoadData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SchoolPerformaceTest
{
    [TestClass]
    public class ImportCSVTest
    {
        private List<object> _schools;
        private List<SchoolResult> _schoolResults;

        [TestInitialize]
        public void Setup()
        {
            //Create a test file with data
            _schools = new List<object>
            {
                new { URN = 2, SCHNAME = "Test 1" ,LEA = 101, ESTAB = 6045},
                new { URN = 1, SCHNAME = "Test 2", LEA = 102, ESTAB = 5000 }
            };

            using (var writer = new StreamWriter("schoolTest.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_schools);
            }

            _schoolResults = new List<SchoolResult>
            {
                new SchoolResult { URN = 2, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.5 },
                new SchoolResult{ URN = 2, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.51 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.62 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.68 }
            };

            using (var writer = new StreamWriter("schoolResultTest.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(_schoolResults);
            }
        }

        [TestMethod]
        public void readSchoolData()
        {
            var import = new ImportCSV("schoolTest.csv");
            var data = import.GetDataFromCSV<School>();
            Assert.AreEqual(data.Count(), _schools.Count());
        }

        [TestCleanup]
        public void Cleanup()
        {
            File.Delete("schoolTest.csv");
            File.Delete("schoolResultTest.csv");
        }
    }
}
