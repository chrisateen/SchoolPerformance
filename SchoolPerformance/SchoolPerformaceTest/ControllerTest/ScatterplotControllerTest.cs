﻿using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class ScatterplotControllerTest
    {
        private Mock<ISchoolPerformanceRepository<SchoolResult>> _mockSchoolResult;
        private Mock<IRedisCache> _mockRedisCache;
        private ScatterplotController _controller;
        private Task<IEnumerable<ScatterplotViewModel>> _scatterplotViewModelLst;
        private Task<ScatterplotViewModel> _nationalScatterplotViewModel;
        private ILogger<ScatterplotController> _logger;

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

            _logger = new NullLogger<ScatterplotController>();

            _controller = new ScatterplotController(_mockSchoolResult.Object, _mockRedisCache.Object, _logger);
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