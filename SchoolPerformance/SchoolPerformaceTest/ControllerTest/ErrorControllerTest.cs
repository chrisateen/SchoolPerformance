using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
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
        //Checks 404 errors are handled
        [TestMethod]
        public void Error404ReturnsPageNotFound()
        {
            //Arrange
            var controller = new ErrorController();

            // Act and Assert
            controller.Index(404).Should()
                .BeViewResult().WithViewName("PageNotFound");
        }

        //Checks other errors are handled
        [TestMethod]
        public void ErrorReturnsErrorPage()
        {
            //Arrange
            var controller = new ErrorController();

            // Act and Assert
            controller.Index(1).Should()
                .BeViewResult().WithViewName("Error");
        }

    }
}
