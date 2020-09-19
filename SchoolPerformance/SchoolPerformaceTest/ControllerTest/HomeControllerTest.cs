using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
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
    public class HomeControllerTest
    {
        private ILogger<HomeController> _logger;
        public HomeController _controller;

        [TestInitialize]
        public void Setup()
        {
            //Arrange
            _logger = new NullLogger<HomeController>();
            _controller = new HomeController(_logger);
        }

        //Checks Home view is rendered
        [TestMethod]
        public void IndexReturnsHomePage()
        {
            // Act and Assert
            _controller.Index().Should()
                .BeViewResult().WithDefaultViewName();
        }

        //Checks FAQ view is rendered
        [TestMethod]
        public void FAQReturnsFAQPage()
        {
            // Act and Assert
            _controller.FAQ().Should()
                .BeViewResult().WithDefaultViewName();
        }

        //Checks About view is rendered
        [TestMethod]
        public void AboutReturnsAboutPage()
        {
            // Act and Assert
            _controller.About().Should()
                .BeViewResult().WithDefaultViewName();
        }
    }
}
