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

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class ScatterplotControllerTest
    {
        Mock<ISchoolResultRepository<SchoolResult>> _mockSchoolResult;
        ScatterplotController _controller;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            _mockSchoolResult = new Mock<ISchoolResultRepository<SchoolResult>>();
            _controller = new ScatterplotController(_mockSchoolResult.Object);
        }

        [TestMethod]
        public void IndexReturnsHomePageWithResultModel()
        {

            // Act and Assert
            _controller.Index().Should()
                .BeViewResult().WithDefaultViewName();

            var res = _controller.Index().Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<IEnumerable<SchoolResult>>().Subject;
        }
    }
}