using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Models;
using SchoolPerformance.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest.ImplicitConversionTest
{
    [TestClass]
    public class ImplicitConversionTest
    {
        //Test conversion of an object
        //from SchoolResult to ScatterplotViewModel
        [TestMethod]
        public void ResultModelToScatterplotViewModel()
        {
            //Arrange
            SchoolResult result = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School 
                { 
                    URN = 1 , 
                    LA = 100, 
                    ESTAB = 1000,
                    SCHNAME = "Test School"
                }
            };

            //Act
            ScatterplotViewModel resultViewModel = result;

            //Assert

            //Checks the SchoolResult object gets converted to ScatterplotViewModel
            Assert.IsNotNull(resultViewModel);

            //Checks the School name in the school model is included
            //when converting from SchoolResult to ScatterplotViewModel
            Assert.AreEqual(result.School.SCHNAME, resultViewModel.SCHNAME);
        }

        //Test conversion of a list of objects
        //from a list of SchoolResult to a list of ScatterplotViewModel
        [TestMethod]
        public void ListResultModelToListScatterplotViewModel()
        {

            //Arrange
            List<SchoolResult> results = new List<SchoolResult>();
            SchoolResult result1 = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 1,
                    LA = 100,
                    ESTAB = 1000,
                    SCHNAME = "Test School 1"
                }
            };

            SchoolResult result2 = new SchoolResult
            {
                URN = 2,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 2,
                    LA = 100,
                    ESTAB = 1001,
                    SCHNAME = "Test School 2"
                }
            };

            results.Add(result1);
            results.Add(result2);

            //Act
            List<ScatterplotViewModel> resultViewModel = results.ConvertToScatterplotViewModel();

            //Assert

            //Checks the list of SchoolResults gets converted to a list of ScatterplotViewModel
            Assert.AreEqual(results.Count,resultViewModel.Count);
        }

        //Test conversion of an object
        //from SchoolResult to TableViewModelAll
        [TestMethod]
        public void ResultModelToTableViewModelAll()
        {
            //Arrange
            SchoolResult result = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 1,
                    LA = 100,
                    ESTAB = 1000,
                    SCHNAME = "Test School"
                }
            };

            //Act
            TableViewModelAll resultViewModel = result;

            //Assert

            //Checks the Schoolresult object gets converted to TableViewModel
            Assert.IsNotNull(resultViewModel);

            //Checks the School name in the school model is included
            //when converting from SchoolResult to TableViewModel
            Assert.AreEqual(result.School.SCHNAME, resultViewModel.SCHNAME);
        }

        //Test conversion of an object
        //from SchoolResult to TableViewModelAll
        //with no School object
        [TestMethod]
        public void ResultModelWithNoSchoolObjectToTableViewModelAll()
        {
            //Arrange
            SchoolResult result = new SchoolResult
            {
                URN = 9,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28
            };

            //Act
            TableViewModelAll resultViewModel = result;

            //Assert

            //Checks the Schoolresult object gets converted to TableViewModel
            Assert.IsNotNull(resultViewModel);
        }

        //Test conversion of a list of objects
        //from a list of SchoolResult to a list of TableViewModelAll
        [TestMethod]
        public void ListResultModelToListTableViewModelAll()
        {

            //Arrange
            List<SchoolResult> results = new List<SchoolResult>();
            SchoolResult result1 = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 1,
                    LA = 100,
                    ESTAB = 1000,
                    SCHNAME = "Test School 1"
                }
            };

            SchoolResult result2 = new SchoolResult
            {
                URN = 2,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 2,
                    LA = 100,
                    ESTAB = 1001,
                    SCHNAME = "Test School 2"
                }
            };

            results.Add(result1);
            results.Add(result2);

            //Act
            List<TableViewModelAll> resultViewModel = results.ConvertToTableViewModelAll();

            //Assert

            //Checks the list of SchoolResults gets converted to a list of ScatterplotViewModel
            Assert.AreEqual(results.Count, resultViewModel.Count);
        }

        //Test conversion of an object
        //from SchoolResult to TableViewModelDisadvantaged
        [TestMethod]
        public void ResultModelToTableViewModelDisadvantaged()
        {
            //Arrange
            SchoolResult result = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 1,
                    LA = 100,
                    ESTAB = 1000,
                    SCHNAME = "Test School"
                }
            };

            //Act
            TableViewModelDisadvantaged resultViewModel = result;

            //Assert

            //Checks the Schoolresult object gets converted to TableViewModel
            Assert.IsNotNull(resultViewModel);

            //Checks the School name in the school model is included
            //when converting from SchoolResult to TableViewModel
            Assert.AreEqual(result.School.SCHNAME, resultViewModel.SCHNAME);
        }

        //Test conversion of an object
        //from SchoolResult to TableViewModelDisadvantaged
        //with no School object
        [TestMethod]
        public void ResultModelWithNoSchoolObjectToTableViewModelDisadvantaged()
        {
            //Arrange
            SchoolResult result = new SchoolResult
            {
                URN = 9,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28
            };

            //Act
            TableViewModelDisadvantaged resultViewModel = result;

            //Assert

            //Checks the Schoolresult object gets converted to TableViewModel
            Assert.IsNotNull(resultViewModel);
        }

        //Test conversion of a list of objects
        //from a list of SchoolResult to a list of TableViewModelDisadvantaged
        [TestMethod]
        public void ListResultModelToListTableViewModelDisadvantaged()
        {

            //Arrange
            List<SchoolResult> results = new List<SchoolResult>();
            SchoolResult result1 = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 1,
                    LA = 100,
                    ESTAB = 1000,
                    SCHNAME = "Test School 1"
                }
            };

            SchoolResult result2 = new SchoolResult
            {
                URN = 2,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 2,
                    LA = 100,
                    ESTAB = 1001,
                    SCHNAME = "Test School 2"
                }
            };

            results.Add(result1);
            results.Add(result2);

            //Act
            List<TableViewModelDisadvantaged> resultViewModel = results.ConvertToTableViewModelDisadvantaged();

            //Assert

            //Checks the list of SchoolResults gets converted to a list of ScatterplotViewModel
            Assert.AreEqual(results.Count, resultViewModel.Count);
        }
    }
}
