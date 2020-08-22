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

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class ScatterplotControllerTest
    {
        Mock<ISchoolPerformanceRepository<SchoolResult>> _mockSchoolResult;
        Mock<IRedisCache> _mockRedisCache;
        ScatterplotController _controller;
        Task<IEnumerable<ScatterplotViewModel>> _scatterplotViewModelLst;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Instantiate the controller class by mocking the repository
            _mockSchoolResult = new Mock<ISchoolPerformanceRepository<SchoolResult>>();

            _mockRedisCache = new Mock<IRedisCache>();

            _scatterplotViewModelLst = Task.FromResult<IEnumerable<ScatterplotViewModel>>(new List<ScatterplotViewModel>());

            _mockRedisCache.Setup(m => m.getScatterplotData()).Returns(_scatterplotViewModelLst);
           
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
                .BeAssignableTo<IEnumerable<ScatterplotViewModel>>().Subject;
        }

    }
}