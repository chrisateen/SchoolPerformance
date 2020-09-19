using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Moq;
using SchoolPerformance.Controllers;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using System.Collections.Generic;
using SchoolPerformance.ViewModels;
using System.Linq;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using System.Threading.Tasks;
using SchoolPerformance.Cache;
using SchoolPerformaceTest;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class TableControllerTest
    {
        private Mock<ISchoolPerformanceRepository<SchoolResult>> _mockSchoolResult;
        private Mock<IRedisCache> _mockRedisCache;
        private IEnumerable<SchoolResult> _results;
        private TablesController _controller;
        private ILogger<TablesController> _logger;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            SetupRedisCache();

            SetupRepository();

            _logger = new NullLogger<TablesController>();

            _controller = new TablesController(_mockSchoolResult.Object, _mockRedisCache.Object, _logger);
        }

        //Checks Table view is rendered
        //with an object of type TableViewModel
        [TestMethod]
        public void IndexReturnsPageWithTableViewModel()
        {
            // Act and Assert
            _controller.Index().Should()
                .BeViewResult().WithDefaultViewName();

            var res = _controller.Index().Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<TableViewModelAll>().Subject;
        }

        //Checks when GetResultsAll is called a JSON object is returned
        [TestMethod]
        public async Task GetResultsAllReturnsJSONObject()
        {
            // Act and Assert
           var controller = await _controller.GetResultsAll();
           var data = controller.Should()
                .BeJsonResult().Value;

        }

        //Checks when GetResultsAll is called a JSON object is returned
        //with data from a list of TableViewModel
        [TestMethod]
        public async Task GetResultsAllContainsListOfTableViewModel()
        {
            // Act and Assert
            var controller = await _controller.GetResultsAll();
            var data = controller.Should()
                .BeJsonResult().Value;

            //Get the data property from the JSON object 
            //which contains the list of TableViewModels
            var dataList = (List<TableViewModelAll>)data
                .GetType().GetProperty("data")
                .GetValue(data, null);

            //Get the data property from the JSON object 
            //which contains the national result
            var natResults = (TableViewModelAll)data
                .GetType().GetProperty("national")
                .GetValue(data, null);

            //Check the JSON object returned
            //contains the mock data
            Assert.AreEqual(_results.Where(x => x.URN != 9).Count(), dataList.Count());
            Assert.IsNotNull(natResults);

        }

        //Checks that the All action method redirects to index
        [TestMethod]
        public void AllRedirectsToIndex()
        {
            _controller.All().Should().BeRedirectToActionResult(nameof(Index));
        }

        //Checks disadvantaged view is rendered
        //with an object of type TableViewModel
        [TestMethod]
        public void DisadvantagedReturnsPageWithTableViewModel()
        {

            // Act and Assert
            _controller.Disadvantaged().Should()
                .BeViewResult().WithDefaultViewName();

            var res = _controller.Disadvantaged().Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<TableViewModelDisadvantaged>().Subject;
        }

        //Checks when GetResultsDisadvantaged is called a JSON object is returned
        [TestMethod]
        public async Task GetResultsDisadvantagedReturnsJSONObject()
        {
            // Act and Assert
            var controller = await _controller.GetResultsDisadvantaged();
            var data = controller.Should()
                 .BeJsonResult().Value;

        }

        //Checks when GetResultsDisadvantaged is called a JSON object is returned
        //with data from a list of TableViewModel
        [TestMethod]
        public async Task GetResultsDisadvantagedContainsListOfTableViewModel()
        {
            var controller = await _controller.GetResultsDisadvantaged();
            var data = controller.Should()
                .BeJsonResult().Value;

            //Get the data property from the JSON object 
            //which contains the list of TableViewModels
            var dataList = (List<TableViewModelDisadvantaged>)data
                .GetType().GetProperty("data")
                .GetValue(data, null);

            //Get the data property from the JSON object 
            //which contains the national result
            var natResults = (TableViewModelDisadvantaged)data
                .GetType().GetProperty("national")
                .GetValue(data, null);

            //Check the JSON object returned
            //contains the mock data
            Assert.AreEqual(_results.Where(x => x.URN != 9).Count(), dataList.Count());
            Assert.IsNotNull(natResults);

        }


        //Setup mock objects to be returned 
        //when methods from IRedisCache is called
        [Ignore]
        public void SetupRedisCache()
        {
            //Mock the redis cache
            _mockRedisCache = new Mock<IRedisCache>();

            //Setup empty mock objects
            Task<IEnumerable<TableViewModelAll>> tableViewModelLstAll = 
                Task.FromResult<IEnumerable<TableViewModelAll>>(new List<TableViewModelAll>());

            Task<IEnumerable<TableViewModelDisadvantaged>> tableViewModelLstDisadvantaged =
                Task.FromResult<IEnumerable<TableViewModelDisadvantaged>>(new List<TableViewModelDisadvantaged>());

            Task<TableViewModelAll> tableViewModelNationalAll = 
                Task.FromResult<TableViewModelAll>(null);

            Task<TableViewModelDisadvantaged> tableViewModelNationalDisadvantaged = 
                Task.FromResult<TableViewModelDisadvantaged>(null);

            //Return empty objects/null when methods from IRedisCache
            //is called
            _mockRedisCache.Setup(m => m.GetTableDataAll())
                .Returns(tableViewModelLstAll);

            _mockRedisCache.Setup(m => m.GetTableDataDisadvantaged())
                .Returns(tableViewModelLstDisadvantaged);

            _mockRedisCache.Setup(m => m.GetNationalTableDataAll())
                .Returns(tableViewModelNationalAll);

            _mockRedisCache.Setup(m => m.GetNationalTableDataDisadvantaged())
                .Returns(tableViewModelNationalDisadvantaged);
        }


        //Setup mock objects to be returned 
        //when methods from Repository is called
        [Ignore]
        public void SetupRepository()
        {
            //Mock SchoolPerformanceRepository
            _mockSchoolResult = new Mock<ISchoolPerformanceRepository<SchoolResult>>();

            //Mock data
            _results = MockData.GetSchoolResultList(true);

            //When the GetAll method is called return _results 
            //excluding national data
            _mockSchoolResult.Setup(m => m.GetAll(
                It.IsAny<Func<IQueryable<SchoolResult>, IOrderedQueryable<SchoolResult>>>(),
                It.IsAny<Expression<Func<SchoolResult, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(_results.Where(r => r.URN != 9)));


            //When the Get method is called return _results 
            //excluding national data
            _mockSchoolResult.Setup(m => m.Get(
                It.IsAny<Expression<Func<SchoolResult, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolResult>, IOrderedQueryable<SchoolResult>>>(),
                It.IsAny<Expression<Func<SchoolResult, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(_results.Where(r => r.URN != 9)));

            //When the GetNational method is called return _results 
            //with national data only
            _mockSchoolResult.Setup(m => m.GetNational(
                It.IsAny<Expression<Func<SchoolResult, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolResult>, IOrderedQueryable<SchoolResult>>>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(_results.Where(r => r.URN == 9)));
        }
    }
}