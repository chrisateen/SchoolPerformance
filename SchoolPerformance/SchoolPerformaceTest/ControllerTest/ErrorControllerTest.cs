using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Controllers;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class ErrorControllerTest
    {
        private ILogger<ErrorController> _logger;
        private IHttpContextAccessor _httpContextAccessor;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Arrange
            _logger = new NullLogger<ErrorController>();

            _httpContextAccessor = new HttpContextAccessor();

            _httpContextAccessor.HttpContext = new DefaultHttpContext();

            _httpContextAccessor.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
            {
                OriginalPath = "Test"
            });

        }

        //Checks 404 errors are handled
        [TestMethod]
        public void Error404ReturnsPageNotFound()
        {

            var controller = new ErrorController(_logger, _httpContextAccessor);

            // Act and Assert
            controller.Error(404).Should()
                .BeViewResult().WithViewName("PageNotFound");
        }

        //Checks other errors are handled
        [TestMethod]
        public void ErrorReturnsErrorPage()
        {
            //Arrange
            var controller = new ErrorController(_logger,_httpContextAccessor);

            // Act and Assert
            controller.Error(500).Should()
                .BeViewResult().WithViewName("Error");
        }

    }
}
