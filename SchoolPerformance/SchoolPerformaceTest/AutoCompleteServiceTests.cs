using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolPerformance;
using SchoolPerformance.Cache;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPerformanceTest
{
    [TestClass]
    public class AutoCompleteServiceTests
    {
        private Mock<ISchoolPerformanceRepository<SchoolResult>> _mockSchoolResult;
        private Mock<IRedisCache> _mockRedisCache;
        private AutoCompleteService _autoCompleteService;
        private Task<IEnumerable<SchoolResult>> _schoolResults;
        private ILogger<AutoCompleteService> _logger;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Mock the repository
            _mockSchoolResult = new Mock<ISchoolPerformanceRepository<SchoolResult>>();

            //Mock the redis cache
            _mockRedisCache = new Mock<IRedisCache>();

            //Get mock school result data
            _schoolResults = Task.FromResult<IEnumerable<SchoolResult>>(MockData.GetSchoolResultList(false));

            _logger = new NullLogger<AutoCompleteService>();

        }

        //Test that if AutoComplete data is not in cache data is retrieved 
        //from the database via the repository class
        [TestMethod]
        public async Task autoCompleteDataReturnedIfNoDataIsInCache()
        {
            //Arrange
            Task<IEnumerable<AutocompleteViewModel>> autocompleteLst = 
                Task.FromResult<IEnumerable<AutocompleteViewModel>>(new List<AutocompleteViewModel>());

            //Return an empty list when GetAutoCompleteData is called
            _mockRedisCache.Setup(m => m.GetAutoCompleteData()).Returns(autocompleteLst);

            //Return a list of SchoolResult data when get method is called
            _mockSchoolResult.Setup(m => m.GetAll(It.IsAny<Expression<Func<SchoolResult, object>>[]>()))
                .Returns(_schoolResults);

            _autoCompleteService = new AutoCompleteService(_mockSchoolResult.Object, _mockRedisCache.Object, _logger);

            //Act
            var results = await _autoCompleteService.Get();

            //Assert
            Assert.IsNotNull(results);
        }

        //Test that if AutoComplete data is in cache the cache data is returned
        [TestMethod]
        public async Task autoCompleteDataReturnsCacheData()
        {
            //Arrange

            //Mock AutoComplete data
            Task<IEnumerable<AutocompleteViewModel>> autoCompleteLst =
                Task.FromResult<IEnumerable<AutocompleteViewModel>>(
                    MockData.GetSchoolResultList(false).ConvertToAutocompleteViewModel()
                    );

            //Return mock AutoComplete list when GetAutoCompleteData is called
            _mockRedisCache.Setup(m => m.GetAutoCompleteData()).Returns(autoCompleteLst);

            //Return an empty list when GetAll is called
            //This should not be called if test is working correctly)
            _mockSchoolResult.Setup(m => m.GetAll(It.IsAny<Expression<Func<SchoolResult, object>>[]>()))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(new List<SchoolResult>()));

            _autoCompleteService = new AutoCompleteService(_mockSchoolResult.Object, _mockRedisCache.Object, _logger);

            //Act
            var results = await _autoCompleteService.Get();

            //Assert
            Assert.IsNotNull(results);
        }

        //Test that the _schools field in the AutoCompleteService test is returned
        // if _school field list is not empty
        [TestMethod]
        public async Task autoCompleteDataReturnsStaticList()
        {
            //Arrange
            Task<IEnumerable<AutocompleteViewModel>> autocompleteLstEmpty =
                Task.FromResult<IEnumerable<AutocompleteViewModel>>(new List<AutocompleteViewModel>());

            //Return an empty list when GetAutoCompleteData is called
            //This should not be called if test is working correctly
            _mockRedisCache.Setup(m => m.GetAutoCompleteData()).Returns(autocompleteLstEmpty);

            //Return an empty list when GetAll is called
            //This should not be called if test is working correctly)
            _mockSchoolResult.Setup(m => m.GetAll(It.IsAny<Expression<Func<SchoolResult, object>>[]>()))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(new List<SchoolResult>()));

            _autoCompleteService = new AutoCompleteService(_mockSchoolResult.Object, _mockRedisCache.Object, _logger);

            var autoCompleteLst = MockData.GetSchoolResultList(false).ConvertToAutocompleteViewModel();

            //Use reflection to get the private static filed _school from AutoCompleteService
            typeof(AutoCompleteService).GetField("_schools", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(_autoCompleteService, autoCompleteLst);

            //Act
            var results = await _autoCompleteService.Get();

            //Assert
            Assert.IsNotNull(results);

        }
    }
}
