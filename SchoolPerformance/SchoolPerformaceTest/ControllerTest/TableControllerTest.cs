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

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class TableControllerTest
    {
        Mock<ISchoolResultRepository<SchoolResult>> _mockSchoolResult;
        TableController _controller;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Instantiate the controller class by mocking the repository
            _mockSchoolResult = new Mock<ISchoolResultRepository<SchoolResult>>();
            _controller = new TableController(_mockSchoolResult.Object);
        }

        //Checks Table view is rendered
        //with an IEnumerable list of data of type TableViewModel
        [TestMethod]
        public void IndexReturnsHomePageWithScatterplotViewModel()
        {

            // Act and Assert
            _controller.Index().Should()
                .BeViewResult().WithDefaultViewName();

            var res = _controller.Index().Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<IEnumerable<TableViewModel>>().Subject;
        }
    }
}