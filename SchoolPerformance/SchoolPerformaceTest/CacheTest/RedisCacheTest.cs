using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolPerformance.Cache;
using SchoolPerformance.ViewModels;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPerformaceTest.CacheTest
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

        //Tests that if items are not in the redis database (i.e keys could not be found)
        //an empty list is returned
        [TestMethod]
        public async Task getScatterplotDataReturnsEmptyListIfItemsAreNotInCache()
        {
            //Arrange
            //Create and return an empty list of keys
            //when SearchKeysAsync is called
            var emptyLstOfKeys = Task.FromResult<IEnumerable<String>>(null);
            _redisDatabase.Setup(m => m.SearchKeysAsync(It.IsAny<string>())).Returns(emptyLstOfKeys);

            _redisCacheClient = new Mock<IRedisCacheClient>();
            _redisCacheClient.Setup(m => m.Db0).Returns(_redisDatabase.Object);

            _redisCache = new RedisCache(_redisCacheClient.Object);

            //Act
            IEnumerable<ScatterplotViewModel> result = await _redisCache.getScatterplotData();

            //Assert
            Assert.AreEqual(0,result.Count());

        }

        //Tests that if items are not in the redis database (i.e keys could not be found)
        //an empty list is returned
        [TestMethod]
        public async Task getScatterplotDataReturnsDataInCache()
        {
            //Arrange
            //Create and return a mock list of keys
            //when SearchKeysAsync is called
            var LstOfKeys = new List<String>
            {
                "ScatterplotViewModel1","ScatterplotViewModel2","ScatterplotViewModel3"
            };

            var taskLstOfKeys = Task.FromResult<IEnumerable<String>>(LstOfKeys);
            _redisDatabase.Setup(m => m.SearchKeysAsync(It.IsAny<string>())).Returns(taskLstOfKeys);


            //Create and return a list of scatterplotViewModel
            //when GetAllAsync is called
            var lstScatterplotViewModel = new Dictionary<String,ScatterplotViewModel>
            {
                {"ScatterplotViewModel2", new ScatterplotViewModel{URN = 2 , SCHNAME = "Test 2"} },
                {"ScatterplotViewModel3", new ScatterplotViewModel{URN = 3, SCHNAME = "Test 3"} },
                {"ScatterplotViewModel1", new ScatterplotViewModel{URN = 1 , SCHNAME = "Test 1"} }
            };

            var taskLstScatterplotViewModel = Task.FromResult<IDictionary<String,ScatterplotViewModel>>(lstScatterplotViewModel);
            _redisDatabase.Setup(m => m.GetAllAsync<ScatterplotViewModel>(It.IsAny<List<string>>()))
                           .Returns(taskLstScatterplotViewModel);

            _redisCacheClient = new Mock<IRedisCacheClient>();
            _redisCacheClient.Setup(m => m.Db0).Returns(_redisDatabase.Object);

            _redisCache = new RedisCache(_redisCacheClient.Object);

            //Act
            IEnumerable<ScatterplotViewModel> result = await _redisCache.getScatterplotData();

            //Assert
            Assert.AreEqual(lstScatterplotViewModel.Count(), result.Count());

        }
    }
}
