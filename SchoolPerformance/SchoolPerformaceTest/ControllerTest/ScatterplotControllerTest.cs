using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Moq;
using SchoolPerformance.Controllers;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using SchoolPerformance.ViewModels;
using System.Threading.Tasks;
using SchoolPerformance.Cache;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class ScatterplotControllerTest
    {
        Mock<ISchoolPerformanceRepository<SchoolResult>> _mockSchoolResult;
        Mock<IRedisCache> _mockRedisCache;
        ScatterplotController _controller;
        Task<IEnumerable<ScatterplotViewModel>> _scatterplotViewModelLst;
        Task<ScatterplotViewModel> _nationalScatterplotViewModel;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Instantiate the controller class by mocking the repository
            _mockSchoolResult = new Mock<ISchoolPerformanceRepository<SchoolResult>>();

            //Mock the redis cache
            _mockRedisCache = new Mock<IRedisCache>();

            _scatterplotViewModelLst = Task.FromResult<IEnumerable<ScatterplotViewModel>>(new List<ScatterplotViewModel>());

            _nationalScatterplotViewModel = Task.FromResult<ScatterplotViewModel>(new ScatterplotViewModel());

            _mockRedisCache.Setup(m => m.GetScatterplotData()).Returns(_scatterplotViewModelLst);

            _mockRedisCache.Setup(m => m.GetNationalScatterplotData()).Returns(_nationalScatterplotViewModel);

            _controller = new ScatterplotController(_mockSchoolResult.Object, _mockRedisCache.Object);
        }

        //Checks Scatterplot view is rendered
        //with an IEnumerable list of data of type ScatterplotViewModel
        [TestMethod]
        public async Task IndexReturnsHomePageWithScatterplotViewModel()
        {

            // Act and Assert
            var controller = await _controller.Index();
            controller.Should()
                .BeViewResult().WithDefaultViewName();

            var res = controller.Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<ScatterplotListViewModel>().Subject;
        }

    }
}