using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Models;
using SchoolPerformance.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformanceTest.ImplicitConversionTest
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
            SchoolResult result = MockData.GetSchoolResult(true);

            //Act
            ScatterplotViewModel resultViewModel = result;

            //Assert

            //Checks the SchoolResult object gets converted to ScatterplotViewModel
            Assert.IsNotNull(resultViewModel);

            //Checks the School name in the school model is included
            //when converting from SchoolResult to ScatterplotViewModel
            Assert.AreEqual(result.School.SCHNAME, resultViewModel.SCHNAME);
        }

        //Test conversion of an object
        //from SchoolResult to ResultViewModel
        [TestMethod]
        public void ResultModelToResultViewModel()
        {
            //Arrange
            SchoolResult result = MockData.GetSchoolResult(true);
           
            //Act
            ResultViewModel resultViewModel = result;

            //Assert

            //Checks the SchoolResult object gets converted to ResultViewModel
            Assert.IsNotNull(resultViewModel);

            //Checks the School name in the school model is included
            //when converting from SchoolResult to ResultViewModel
            Assert.AreEqual(result.School.SCHNAME, resultViewModel.SCHNAME);
        }

        //Test conversion of a list of objects
        //from a list of SchoolResult to a list of ScatterplotViewModel
        [TestMethod]
        public void ListResultModelToListScatterplotViewModel()
        {

            //Arrange
            List<SchoolResult> results = MockData.GetSchoolResultList(false);

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
            SchoolResult result = MockData.GetSchoolResult(true);

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
            SchoolResult result = MockData.GetSchoolResult(false);

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
            List<SchoolResult> results = MockData.GetSchoolResultList(false);

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

            SchoolResult result = MockData.GetSchoolResult(true);
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
            SchoolResult result = MockData.GetSchoolResult(false);

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
            List<SchoolResult> results = MockData.GetSchoolResultList(false);

            //Act
            List<TableViewModelDisadvantaged> resultViewModel = results.ConvertToTableViewModelDisadvantaged();

            //Assert

            //Checks the list of SchoolResults gets converted to a list of ScatterplotViewModel
            Assert.AreEqual(results.Count, resultViewModel.Count);
        }
    }
}
