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
        public void ImportSchoolData()
        {
            //Arrange
            var import = new ImportCSV("SchoolTest.csv");

            //Act
            var data = import.GetDataFromCSV<School>();

            //Assert
            Assert.AreEqual(_data["SchoolTest"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into school model 
        //even when there are rows with blank records in some or all of the fields
        [TestMethod]
        public void ImportSchoolDataWithNullRecords()
        {
            //Arrange
            var import = new ImportCSV("SchoolTestNullRecords.csv");
            
            //Act
            var data = import.GetDataFromCSV<School>();

            //Assert
            Assert.AreEqual(_data["SchoolTestNullRecords"].Count(), data.Count());
        }

        //Test ImportCSV class removes rows with less than 3 fields
        //when importing data to School model
        [TestMethod]
        public void ImportSchoolDataIgnoreRowsWithTwoOrLessFields()
        {
            //Arrange
            var import = new ImportCSV("SchoolTestDifferentRowLength.csv");

            //Act
            var data = import.GetDataFromCSV<School>();

            //Assert

            //Checks the data omits the 1 row that has only 2 fields
            Assert.AreEqual(_data["SchoolTestDifferentRowLength"].Count() -1, data.Count());
        }

        //Test ImportCSV class can get data into school model 
        //and excluded fields in the csv that is not in the SchoolMap
        [TestMethod]
        public void ImportSchoolDataExcludesFieldsNotInMap()
        {
            //Arrange
            var import = new ImportCSV("SchoolTestExtraFields.csv");

            //Act
            var data = import.GetDataFromCSV<School>();

            //Assert
            Assert.AreEqual(_data["SchoolTestExtraFields"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into SchoolResult model
        [TestMethod]
        public void ImportSchoolResultData()
        {
            //Arrange
            var import = new ImportCSV("SchoolResultTest.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolResult>();

            //Assert
            Assert.AreEqual(_data["SchoolResultTest"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into SchoolResult model 
        //even when there are rows with blank records in some of the fields
        [TestMethod]
        public void ImportSchoolResultDataWithPartialcialNullRecords()
        {
            //Arrange
            var import = new ImportCSV("SchoolResultTestPartcialNullRecords.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolResult>();

            //Assert
            Assert.AreEqual(_data["SchoolResultTestPartcialNullRecords"].Count(), data.Count());
        }

        //Test ImportCSV class ignore rows where all the attainment 
        //and progress results are null or/and N/A
        [TestMethod]
        public void ImportSchoolResultIgnoreRecordsWithAllNullResults()
        {
            //Arrange
            var import = new ImportCSV("SchoolResultTestAllNullRecords.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolResult>();

            //Assert
            Assert.AreEqual(_data["SchoolResultTestAllNullRecords"].Count(), data.Count());
        }

        //Test ImportCSV class removes rows with less than 3 fields
        //when importing data to SchoolResult model
        [TestMethod]
        public void ImportSchoolResultDataIgnoreRowsWithTwoOrLessFields()
        {
            //Arrange
            var import = new ImportCSV("SchoolResultTestDifferentRowLength.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolResult>();

            //Assert

            //Checks the data omits the 1 row that has only 2 fields
            Assert.AreEqual(_data["SchoolResultTestDifferentRowLength"].Count() - 1, data.Count());
        }

        //Test ImportCSV class can get data into SchoolResult model 
        //and excluded fields in the csv that is not in the SchoolResultMap
        [TestMethod]
        public void ImportSchoolResultDataExcludesFieldsNotInMap()
        {
            //Arrange
            var import = new ImportCSV("SchoolResultTestExtraFields.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolResult>();

            //Assert
            Assert.AreEqual(_data["SchoolResultTestExtraFields"].Count(), data.Count());
        }


        //Test ImportCSV class can get data into SchoolDetails model
        [TestMethod]
        public void ImportSchoolDetailsData()
        {
            //Arrange
            var import = new ImportCSV("SchoolDetailsTest.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolDetails>();

            //Assert
            Assert.AreEqual(_data["SchoolDetailsTest"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into SchoolDetails model 
        //even when there are rows with blank records in some or all of the fields
        [TestMethod]
        public void ImportSchoolDetailsDataWithNullRecords()
        {
            //Arrange
            var import = new ImportCSV("SchoolDetailsTestNullRecords.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolDetails>();

            //Assert
            Assert.AreEqual(_data["SchoolDetailsTestNullRecords"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into SchoolDetails model 
        //and excluded fields in the csv that is not in the SchoolDetailsMap
        [TestMethod]
        public void ImportSchoolDetailsDataExcludesFieldsNotInMap()
        {
            //Arrange
            var import = new ImportCSV("SchoolDetailsTestExtraFields.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolDetails>();

            //Assert
            Assert.AreEqual(_data["SchoolDetailsTestExtraFields"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into SchoolContextual model
        [TestMethod]
        public void ImportSchoolContextualData()
        {
            //Arrange
            var import = new ImportCSV("SchoolContextualTest.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolContextual>();

            //Assert
            Assert.AreEqual(_data["SchoolContextualTest"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into SchoolContextual model 
        //even when there are rows with blank records in some or all of the fields
        [TestMethod]
        public void ImportSchoolContextualDataWithNullRecords()
        {
            //Arrange
            var import = new ImportCSV("SchoolContextualTestNullRecords.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolContextual>();

            //Assert
            Assert.AreEqual(_data["SchoolContextualTestNullRecords"].Count(), data.Count());
        }

        //Test ImportCSV class can get data into SchoolContextual model 
        //and excluded fields in the csv that is not in the SchoolDetailsMap
        [TestMethod]
        public void ImportSchoolContextualDataExcludesFieldsNotInMap()
        {
            //Arrange
            var import = new ImportCSV("SchoolContextualTestExtraFields.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolContextual>();

            //Assert
            Assert.AreEqual(_data["SchoolContextualTestExtraFields"].Count(), data.Count());
        }

        //Test ImportCSV class excludes URN with value of NAT (i.e national data)
        //when loading SchoolContextual data
        [TestMethod]
        public void ImportSchoolContextualDataExcludesNATData()
        {
            //Arrange
            var import = new ImportCSV("SchoolContextualTestSkipRecords.csv");

            //Act
            var data = import.GetDataFromCSV<SchoolContextual>();

            //Assert
            Assert.AreEqual(_data["SchoolContextualTestSkipRecords"].Count() - 1, data.Count());
        }

        //Tests that when a class other than the models for this project is supplied
        //an exception is thrown
        [TestMethod]
        public void ImportWrongClassThrowsException()
        {
            //Arrange
            var import = new ImportCSV("SchoolTest.csv");

            //Act and Assert
            var ex = Assert.ThrowsException<ArgumentException>(() => import.GetDataFromCSV<BlankClass>());

            Assert.AreEqual("Model name does not exist", ex.Message);

        }

        //Tests that an exception is thrown when attempting to read a csv file that doesn't exist
        [TestMethod]
        public void FileDoesNotExistException()
        {
            //Arrange
            var import = new ImportCSV("FileDoesNotExist.csv");


            //Act and Assert
            var ex = Assert.ThrowsException<FileNotFoundException>(() => import.GetDataFromCSV<School>());

            Assert.AreEqual("CSV file does not exist", ex.Message);
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
                new { URN = 2, SCHNAME = "Test 1" ,EXTRA1 = "Extra",
                    LEA = 101, ESTAB = 6045,  EXTRA2 = "Extra"},
                new { URN = 1, SCHNAME = "Test 2", EXTRA1 = "Extra",
                    LEA = 102, ESTAB = 5000, EXTRA2 = "Extra" }
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
                new { URN = 2, PTFSM6CLA1A = 0.2, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43, 
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6, 
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4},
                new { URN = 1, PTFSM6CLA1A = 0.43, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4}
            };

            _data["SchoolResultTestExtraFields"] = new List<object>
            {
                new { EXTRA1 = "Extra", EXTRA2 = "Extra",
                    URN = 2, PTFSM6CLA1A = 0.2, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4},
                new { EXTRA1 = "Extra", EXTRA2 = "Extra",
                    URN = 1, PTFSM6CLA1A = 0.43, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4}
            };

            _data["SchoolResultTestPartcialNullRecords"] = new List<object>
            {
                new { URN = 2, PTFSM6CLA1A = 0.43, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = "", ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = "", P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = "", PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = "", PTNOTFSM6CLA1ABASICS_95 = 0.35},
                new { URN = 3, PTFSM6CLA1A = 0.33, ATT8SCR = "SUPP", ATT8SCR_FSM6CLA1A = "NE", ATT8SCR_NFSM6CLA1A = "NP",
                    P8MEA=0.01, P8MEA_FSM6CLA1A = "NA", P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = "NA", PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = "NA", PTNOTFSM6CLA1ABASICS_95 = 0.35},
                new { URN = 1, PTFSM6CLA1A = 0.20, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4}
            };

            _data["SchoolResultTestAllNullRecords"] = new List<object>
            {
                new { URN = 2, PTFSM6CLA1A = 0.43, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = "", ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = "", P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = "", PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = "", PTNOTFSM6CLA1ABASICS_95 = 0.35},
                new { URN = 3, PTFSM6CLA1A = 0.33, ATT8SCR = "SUPP", ATT8SCR_FSM6CLA1A = "NE", ATT8SCR_NFSM6CLA1A = "NP",
                    P8MEA=0.01, P8MEA_FSM6CLA1A = "NA", P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = "NA", PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = "NA", PTNOTFSM6CLA1ABASICS_95 = 0.35},
                new { URN = 1, PTFSM6CLA1A = 0.20, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4},
                new { URN = "", PTFSM6CLA1A = "", ATT8SCR = "NA", ATT8SCR_FSM6CLA1A = "NA", ATT8SCR_NFSM6CLA1A = "",
                    P8MEA="", P8MEA_FSM6CLA1A = "SUPP", P8MEA_NFSM6CLA1A = "", PTL2BASICS_94 = "",
                    PTFSM6CLA1ABASICS_94 = "", PTNOTFSM6CLA1ABASICS_94 = "", PTL2BASICS_95 = "",
                    PTFSM6CLA1ABASICS_95 = "", PTNOTFSM6CLA1ABASICS_95 = ""},
            };

            _data["SchoolResultTestDifferentRowLength"] = new List<object>
            {
                new { URN = 2, PTFSM6CLA1A = 0.33, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4},
                new { URN = 1, PTFSM6CLA1A = 0.33, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4},
                new { URN = "", ATT8SCR = "" }
            };

            _data["SchoolDetailsTest"] = new List<object>
            {
                new { URN = 2, STREET = "Test 1" , LOCALITY = "Locality 1", ADDRESS3 = "Address 1",
                TOWN = "Town 1", POSTCODE = "POSTCODE 1", SCHOOLTYPE = "Type 1", GENDER = "Mixed",
                RELCHAR = "None"},
                new { URN = 1, STREET = "Test 2", LOCALITY = "Locality 2", ADDRESS3 = "Address 2",
                TOWN = "Town 2", POSTCODE = "POSTCODE 2", SCHOOLTYPE = "Type 2", Gender = "Female",
                RELCHAR = "None"}
            };

            _data["SchoolDetailsTestExtraFields"] = new List<object>
            {
                new { URN = 2, EXTRA1 = "Extra1", STREET = "Test 1" , LOCALITY = "Locality 1", 
                    ADDRESS3 = "Address 1", TOWN = "Town 1", POSTCODE = "POSTCODE 1", SCHOOLTYPE = "Type 1", 
                    EXTRA2 = "Extra2", GENDER = "Mixed", RELCHAR = "None", EXTRA3 = "Extra3"},
                new { URN = 1, EXTRA1 = "Extra1", STREET = "Test 2", LOCALITY = "Locality 2", 
                    ADDRESS3 = "Address 2", TOWN = "Town 2", POSTCODE = "POSTCODE 2", SCHOOLTYPE = "Type 2",
                    EXTRA2 = "Extra2", Gender = "Female", RELCHAR = "None", EXTRA3 = "Extra3"}
            };

            _data["SchoolDetailsTestNullRecords"] = new List<object>
            {
                new { URN = 2, STREET = "Test 1" , LOCALITY = "Locality 1", ADDRESS3 = "Address 1", 
                    TOWN = "Town 1", POSTCODE = "POSTCODE 1", SCHOOLTYPE = "Type 1",
                    GENDER = "Mixed", RELCHAR = "None"},
                new { URN = 1, STREET = "Test 2", LOCALITY = "Locality 2", ADDRESS3 = "Address 2", 
                    TOWN = "Town 2", POSTCODE = "POSTCODE 2", SCHOOLTYPE = "Type 2",
                    Gender = "Female", RELCHAR = "None"},
                new { URN = "", STREET = "", LOCALITY = "",ADDRESS3 = "", 
                    TOWN = "", POSTCODE = "", SCHOOLTYPE = "",
                     Gender = "", RELCHAR = ""},
                new { URN = 3, STREET = "", LOCALITY = "", ADDRESS3 = "", 
                    TOWN = "", POSTCODE = "", SCHOOLTYPE = "Type 2",
                    Gender = "Female", RELCHAR = "None"}
            };

            _data["SchoolContextualTest"] = new List<object>
            {
                new { URN = 2, NOR = 1500, PNORG = 0.52, PSENELSE = 0.01,
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2},
                new { URN = 1, NOR = 1000, PNORG = 0.49, PSENELSE = 0.01,
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2 }
            };

            _data["SchoolContextualTestExtraFields"] = new List<object>
            {
                new { URN = 2, NOR = 1500, EXTRA1 = 0, PNORG = 0.52, 
                    PSENELSE = 0.01, EXTRA2 = 0, EXTRA3 = "Extra3",
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2},
                new { URN = 1, NOR = 1000, EXTRA1 = 0, PNORG = 0.49, 
                    PSENELSE = 0.01, EXTRA2 = 0, EXTRA3 = "Extra3",
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2}
            };

            _data["SchoolContextualTestNullRecords"] = new List<object>
            {
                new { URN = 2, NOR = 1500, PNORG = 0.52, PSENELSE = 0.01,
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2},
                new { URN = 1, NOR = 1000, PNORG = 0.49, PSENELSE = 0.01,
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2},
                new { URN = "", NOR = "", PNORG = "", PSENELSE = "",
                    PSENELK = "", PNUMEAL = "", PNUMFSMEVER = ""},
                new { URN = 3, NOR = 900, PNORG = 0.52, PSENELSE = "",
                    PSENELK = "", PNUMEAL = "", PNUMFSMEVER = ""},
                new { URN = 4, NOR = "NA", PNORG = "NA", PSENELSE = "NA",
                    PSENELK = "NA", PNUMEAL = "NA", PNUMFSMEVER = "NA"}
            };

            _data["SchoolContextualTestSkipRecords"] = new List<object>
            {
                new { URN = 2, NOR = 1500, PNORG = 0.52, PSENELSE = 0.01,
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2},
                new { URN = 1, NOR = 1000, PNORG = 0.49, PSENELSE = 0.01,
                    PSENELK = 0.05, PNUMEAL = 0.12, PNUMFSMEVER = 0.2},
                new { URN = "NAT", NOR = 900, PNORG = 0.52, PSENELSE = 0.02,
                    PSENELK = 0.06, PNUMEAL = 0.2, PNUMFSMEVER = 0.23}
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
