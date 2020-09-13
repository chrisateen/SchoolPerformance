using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolPerformance.Cache;
using SchoolPerformance.ViewModels;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPerformanceTest.CacheTest
{
    [TestClass]
    public class RedisCacheTest
    {
        IRedisCache _redisCache;
        Mock<IRedisCacheClient> _redisCacheClient;
        Mock<IRedisDatabase> _redisDatabase;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            _redisDatabase = new Mock<IRedisDatabase>();
        }

        //Tests that if the ScatterplotViewModel data 
        //are not in the redis database (i.e keys could not be found)
        //an empty list is returned
        [TestMethod]
        public async Task GetScatterplotDataReturnsEmptyListIfItemsAreNotInCache()
        {
            //Arrange
            SetupEmptyMockCache();

            //Act
            IEnumerable<ScatterplotViewModel> result = await _redisCache.GetScatterplotData();

            //Assert
            Assert.AreEqual(0,result.Count());

        }

        //Tests that if the ScatterplotViewModel data 
        //is in the redis database (i.e keys could not be found)
        //the list of ScatterplotViewModel data is returned
        [TestMethod]
        public async Task GetScatterplotDataReturnsDataInCache()
        {
            //Arrange
            SetupListOfKeys("ScatterplotViewModel", 3);


            //Create and return a list of scatterplotViewModel
            //when GetAllAsync is called
            var lstScatterplotViewModel = new Dictionary<String,ScatterplotViewModel>
            {
                {"ScatterplotViewModel2", new ScatterplotViewModel{URN = 2 , SCHNAME = "Test 2"} },
                {"ScatterplotViewModel3", new ScatterplotViewModel{URN = 3, SCHNAME = "Test 3"} },
                {"ScatterplotViewModel1", new ScatterplotViewModel{URN = 1 , SCHNAME = "Test 1"} }
            };

            //Gets first school name when list is sorted in alphabetical order
            var firstSchoolinLst = lstScatterplotViewModel.OrderBy(s => s.Value.SCHNAME).First().Value.SCHNAME;

            var taskLstScatterplotViewModel = Task.FromResult<IDictionary<String,ScatterplotViewModel>>(lstScatterplotViewModel);
            _redisDatabase.Setup(m => m.GetAllAsync<ScatterplotViewModel>(It.IsAny<List<string>>()))
                           .Returns(taskLstScatterplotViewModel);

            SetupRedisCacheClient();

            //Act
            IEnumerable<ScatterplotViewModel> result = await _redisCache.GetScatterplotData();

            //Assert
            //Number of items retrieved from cache is correct
            Assert.AreEqual(lstScatterplotViewModel.Count(), result.Count());

            //List returned from cache is sorted in order
            Assert.AreEqual(firstSchoolinLst, result.First().SCHNAME);
        }


        //Tests that if the TableViewModelAll data 
        //are not in the redis database (i.e keys could not be found)
        //an empty list is returned
        [TestMethod]
        public async Task GetTableDataAllReturnsEmptyListIfItemsAreNotInCache()
        {
            //Arrange
            SetupEmptyMockCache();

            //Act
            IEnumerable<TableViewModelAll> result = await _redisCache.GetTableDataAll();

            //Assert
            Assert.AreEqual(0, result.Count());

        }

        //Tests that if the TableViewModelAll data 
        //is in the redis database
        //the list of TableViewModelAll data is returned
        [TestMethod]
        public async Task GetTableDataAllReturnsDataInCache()
        {
            //Arrange
            SetupListOfKeys("TableViewModelAll", 3);


            //Create and return a list of TableViewModelAll
            //when GetAllAsync is called
            var lstTableViewModel = new Dictionary<String, TableViewModelAll>
            {
                {"TableViewModelAll3", new TableViewModelAll{URN = 3 , SCHNAME = "Test 3"} },
                {"TableViewModelAll2", new TableViewModelAll{URN = 2, SCHNAME = "Test 2"} },
                {"TableViewModelAll1", new TableViewModelAll{URN = 1 , SCHNAME = "Test 1"} }
            };

            var taskLstTableViewModel = Task.FromResult<IDictionary<String, TableViewModelAll>>(lstTableViewModel);
            _redisDatabase.Setup(m => m.GetAllAsync<TableViewModelAll>(It.IsAny<List<string>>()))
                           .Returns(taskLstTableViewModel);

            SetupRedisCacheClient();

            //Gets first school name when list is sorted in alphabetical order
            var firstSchoolinLst = lstTableViewModel.OrderBy(s => s.Value.SCHNAME).First().Value.SCHNAME;

            //Act
            IEnumerable<TableViewModelAll> result = await _redisCache.GetTableDataAll();

            //Assert
            //Number of items retrieved from cache is correct
            Assert.AreEqual(lstTableViewModel.Count(), result.Count());

            //List returned from cache is sorted in order
            Assert.AreEqual(firstSchoolinLst, result.First().SCHNAME);

        }

        //Tests that if the TableViewModelDisadvantaged data 
        //are not in the redis database (i.e keys could not be found)
        //an empty list is returned
        [TestMethod]
        public async Task GetTableDataDisadvantagedReturnsEmptyListIfItemsAreNotInCache()
        {
            //Arrange
            SetupEmptyMockCache();

            //Act
            IEnumerable<TableViewModelDisadvantaged> result = await _redisCache.GetTableDataDisadvantaged();

            //Assert
            Assert.AreEqual(0, result.Count());

        }


        //Tests that if the TableViewModelDisadvantaged data 
        //is in the redis database
        //the list of TableViewModelDisadvantaged data is returned
        [TestMethod]
        public async Task GetTableDataDisadvantagedReturnsDataInCache()
        {
            //Arrange
            SetupListOfKeys("TableViewModelDisadvantaged", 3);


            //Create and return a list of TableViewModelAll
            //when GetAllAsync is called
            var lstTableViewModel = new Dictionary<String, TableViewModelDisadvantaged>
            {
                {"TableViewModelDisadvantaged3", new TableViewModelDisadvantaged{URN = 3 , SCHNAME = "Test 3"} },
                {"TableViewModelDisadvantaged2", new TableViewModelDisadvantaged{URN = 2, SCHNAME = "Test 2"} },
                {"TableViewModelDisadvantaged1", new TableViewModelDisadvantaged{URN = 1 , SCHNAME = "Test 1"} }
            };

            var taskLstTableViewModel = Task.FromResult<IDictionary<String, TableViewModelDisadvantaged>>(lstTableViewModel);
            _redisDatabase.Setup(m => m.GetAllAsync<TableViewModelDisadvantaged>(It.IsAny<List<string>>()))
                           .Returns(taskLstTableViewModel);

            SetupRedisCacheClient();

            //Gets first school name when list is sorted in alphabetical order
            var firstSchoolinLst = lstTableViewModel.OrderBy(s => s.Value.SCHNAME).First().Value.SCHNAME;

            //Act
            IEnumerable<TableViewModelDisadvantaged> result = await _redisCache.GetTableDataDisadvantaged();

            //Assert
            //Number of items retrieved from cache is correct
            Assert.AreEqual(lstTableViewModel.Count(), result.Count());

            //List returned from cache is sorted in order
            Assert.AreEqual(firstSchoolinLst, result.First().SCHNAME);

        }


        //Tests that GetNationalTableDataAll returns a null object
        //if national data is not in the redis database (i.e key could not be found)
        [TestMethod]
        public async Task GetNationalTableDataAllReturnsEmptyObjectIfDataIsNotInCache()
        {
            //Arrange
            SetupRedisCacheClient();

            //Act
            TableViewModelAll result = await _redisCache.GetNationalTableDataAll();

            //Assert
            Assert.IsNull(result);

        }

        //Tests that GetNationalTableDataAll
        //returns an TableViewModelAll object
        //if national data is in the redis database
        [TestMethod]
        public async Task GetNationalTableDataAllReturnsDataInCache()
        {
            //Arrange
            SetupNationalMockCache();

            //Act
            var result = await _redisCache.GetNationalTableDataAll();

            //Assert
            Assert.IsNotNull(result);

        }

        //Tests that GetNationalTableDataDisadvantaged returns a null object
        //if national data is not in the redis database (i.e key could not be found)
        [TestMethod]
        public async Task GetNationalTableDataDisadvantagedReturnsEmptyObjectIfDataIsNotInCache()
        {
            //Arrange
            SetupEmptyMockCache();

            //Act
            TableViewModelDisadvantaged result = await _redisCache.GetNationalTableDataDisadvantaged();

            //Assert
            Assert.IsNull(result);

        }

        //Tests that GetNationalTableDataDisadvantaged
        //returns an TableViewModelDisadvantaged object
        //if national data is in the redis database
        [TestMethod]
        public async Task GetNationalTableDataDisadvantagedReturnsDataInCache()
        {
            //Arrange
            SetupNationalMockCache();

            //Act
            var result = await _redisCache.GetNationalTableDataDisadvantaged();

            //Assert
            Assert.IsNotNull(result);

        }


        //Create and return an empty list of keys
        //when SearchKeysAsync is called
        //in the get methods
        [Ignore]
        public void SetupEmptyMockCache()
        {
            //Arrange

            //When SearchKeysAsync is called it should return an empty list of keys
            var emptyLstOfKeys = Task.FromResult<IEnumerable<String>>(null);
            _redisDatabase.Setup(m => m.SearchKeysAsync(It.IsAny<string>())).Returns(emptyLstOfKeys);

            //When ExistsAsync is called it should return false
            var taskFalse = Task.FromResult<bool>(false);
            _redisDatabase.Setup(m => m.ExistsAsync(It.IsAny<string>(), It.IsAny<CommandFlags>())).Returns(taskFalse);
            SetupRedisCacheClient();
            
        }

        //Setup Mock redisCacheClient
        [Ignore]
        public void SetupRedisCacheClient()
        {
            //Arrange
            _redisCacheClient = new Mock<IRedisCacheClient>();
            _redisCacheClient.Setup(m => m.Db0).Returns(_redisDatabase.Object);

            _redisCache = new RedisCache(_redisCacheClient.Object);
        }

        //Create and return a mock list of keys
        //when SearchKeysAsync is called
        [Ignore]
        public void SetupListOfKeys(string keyStartsWith, int noOfKeys)
        {
            var LstOfKeys = new List<String>();

            for (int i = 1; i == noOfKeys; i++)
            {
                LstOfKeys.Add(keyStartsWith + i);
            }

            var taskLstOfKeys = Task.FromResult<IEnumerable<String>>(LstOfKeys);

            _redisDatabase.Setup(m => m.SearchKeysAsync(It.IsAny<string>())).Returns(taskLstOfKeys);
        }


        //Setup ExistsAsync and GetAsync 
        //when the GetNational methods are called
        [Ignore]
        public void SetupNationalMockCache()
        {
            //Arrange

            //When ExistsAsync is called it should return true
            var taskFalse = Task.FromResult<bool>(true);
            _redisDatabase.Setup(m => m.ExistsAsync(It.IsAny<string>(), It.IsAny<CommandFlags>()))
                .Returns(taskFalse);

            //When GetAsync is called it should return TableViewModelAll object
            var tableViewModelAll = Task.FromResult<TableViewModelAll>( new TableViewModelAll());
            _redisDatabase.Setup(m => m.GetAsync<TableViewModelAll>(It.IsAny<string>(), It.IsAny<CommandFlags>()))
                           .Returns(tableViewModelAll);

            //When GetAsync is called it should return TableViewModelDisadvantaged object
            var tableViewModelDisadvantaged = Task.FromResult<TableViewModelDisadvantaged>(new TableViewModelDisadvantaged());
            _redisDatabase.Setup(m => m.GetAsync<TableViewModelDisadvantaged>(It.IsAny<string>(), It.IsAny<CommandFlags>()))
                           .Returns(tableViewModelDisadvantaged);


            SetupRedisCacheClient();

        }
    }
}
