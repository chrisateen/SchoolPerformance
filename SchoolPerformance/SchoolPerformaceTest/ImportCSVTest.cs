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
        //Stores mock data along with the csv filename as a key
        private Dictionary<String, List<object>> _data = new Dictionary<string, List<object>>();


        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Create mock data
            SetData();

            //Save mock data in a test csv file
            CreateTestCsv();
        }

        //Test ImportCSV class can get data into school model
        [TestMethod]
        public void readSchoolData()
        {
            //Act
            var import = new ImportCSV("SchoolTest.csv");
            var data = import.GetDataFromCSV<School>();

            //Assert
            Assert.AreEqual(_data["SchoolTest"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into school model 
        //even when there are rows with blank records in some or all of the fields
        [TestMethod]
        public void readSchoolDataWithNullRecords()
        {
            //Act
            var import = new ImportCSV("SchoolTestNullRecords.csv");
            var data = import.GetDataFromCSV<School>();

            //Assert
            Assert.AreEqual(_data["SchoolTestNullRecords"].Count(), data.Count());
        }

        //Test ImportCSV class removes rows with less than 3 fields
        [TestMethod]
        public void readSchoolDataIgnoreRowsWithTwoOrLessFields()
        {
            //Act
            var import = new ImportCSV("SchoolTestDifferentRowLength.csv");
            var data = import.GetDataFromCSV<School>();

            //Assert

            //Checks the data omits the 1 row that has only 2 fields
            Assert.AreEqual(_data["SchoolTestDifferentRowLength"].Count() -1, data.Count());
        }

        //Test ImportCSV class can get data into school model 
        //and excluded fields in the csv that is not in the SchoolMap
        [TestMethod]
        public void readSchoolDataExcludesFieldsNotInMap()
        {
            //Act
            var import = new ImportCSV("SchoolTestExtraFields.csv");
            var data = import.GetDataFromCSV<School>();

            //Assert
            Assert.AreEqual(_data["SchoolTestExtraFields"].Count(), data.Count());
        }

        //Remove any mock csv files
        [TestCleanup]
        public void Cleanup()
        {
            foreach (var key in _data.Keys)
            {
                var csvFile = key + ".csv";
                File.Delete(csvFile);
            }
        }

        //Create mock data
        [Ignore]
        public void SetData()
        {
            _data["SchoolTest"] = new List<object>
            {
                new { URN = 2, SCHNAME = "Test 1" ,LEA = 101, ESTAB = 6045},
                new { URN = 1, SCHNAME = "Test 2", LEA = 102, ESTAB = 5000 }
            };

            _data["SchoolTestExtraFields"] = new List<object>
            {
                new { URN = 2, SCHNAME = "Test 1" ,LEA = 101, ESTAB = 6045, 
                    EXTRA1 = "Extra", EXTRA2 = "Extra"},
                new { URN = 1, SCHNAME = "Test 2", LEA = 102, ESTAB = 5000, 
                    EXTRA1 = "Extra", EXTRA2 = "Extra" }
            };

            _data["SchoolTestNullRecords"] = new List<object>
            {
                new { URN = 2, SCHNAME = "Test 1" ,LEA = 101, ESTAB = 6045},
                new { URN = 1, SCHNAME = "Test 2", LEA = 102, ESTAB = 5000 },
                new { URN = 1, SCHNAME = "", LEA = "", ESTAB = "" },
                new { URN = "", SCHNAME = "", LEA = "", ESTAB = "" }
            };

            _data["SchoolTestDifferentRowLength"] = new List<object>
            {
                new { URN = 2, SCHNAME = "Test 1" ,LEA = 101, ESTAB = 6045},
                new { URN = 1, SCHNAME = "Test 2", LEA = 102, ESTAB = 5000 },
                new { URN = "", SCHNAME = "", LEA = "", ESTAB = "" },
                new { URN = 1, SCHNAME = "", LEA = "", ESTAB = "" },
                new { URN = "", SCHNAME = "" }
            };

            _data["SchoolResultTest"] = new List<object>
            {
                new SchoolResult { URN = 2, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.5 },
                new SchoolResult{ URN = 2, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.51 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.62 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.68 }
            };
        }

        //Create mock csv files
        [Ignore]
        public void CreateTestCsv()
        {
            foreach (KeyValuePair<string, List<object>> kvp in _data)
            {
                var csvFile = kvp.Key + ".csv";

                using (var writer = new StreamWriter(csvFile))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(kvp.Value);
                }
            }
        }
    }
}
