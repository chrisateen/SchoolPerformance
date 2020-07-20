using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Moq;
using SchoolPerformance.Controllers;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using System.Collections.Generic;
using SchoolPerformance.ViewModels;
using System.Linq;
using System;
using System.Linq.Expressions;
using Newtonsoft.Json;

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class TableControllerTest
    {
        Mock<ISchoolPerformanceRepository<SchoolResult>> _mockSchoolResult;
        IEnumerable<SchoolResult> _results;
        TableController _controller;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Instantiate the controller class by mocking the repository
            _mockSchoolResult = new Mock<ISchoolPerformanceRepository<SchoolResult>>();

            //Mock data
            _results = new List<SchoolResult>
            {
                new SchoolResult { URN = 1, PTFSM6CLA1A = 0.2, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4,
                    School = new School{URN=1, SCHNAME = "Test 1"}
                },
                new SchoolResult { URN = 2, PTFSM6CLA1A = 0.43, ATT8SCR = 40, ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 43,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = -0.05, P8MEA_NFSM6CLA1A = 0.05, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.57, PTNOTFSM6CLA1ABASICS_94 = 0.63, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.32, PTNOTFSM6CLA1ABASICS_95 = 0.4,
                    School = new School{URN=2, SCHNAME = "Test 2"}
                }
            };

            //When the get all method is called return _results
            _mockSchoolResult.Setup(m => m.GetAll(
                It.IsAny<Func<IQueryable<SchoolResult>, IOrderedQueryable<SchoolResult>>>(),
                It.IsAny<Expression<Func<SchoolResult, object>>[]>()
                ))
                .Returns(_results).Verifiable();

            _controller = new TableController(_mockSchoolResult.Object);
        }

        //Checks Table view is rendered
        //with an object of type TableViewModel
        [TestMethod]
        public void IndexReturnsHomePageWithTableViewModel()
        {

            // Act and Assert
            _controller.Index().Should()
                .BeViewResult().WithDefaultViewName();

            var res = _controller.Index().Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<TableViewModel>().Subject;
        }

        //Checks when OnGet is called a JSON object is returned
        [TestMethod]
        public void OnGetReturnsJSONObject()
        {
            // Act and Assert
           var data = _controller.OnGet().Should()
                .BeJsonResult().Value;

        }

        //Checks when OnGet is called a JSON object is returned
        //with data from a list of TableViewModel
        [TestMethod]
        public void OnGetContainsListOfTableViewModel()
        {
            var data = _controller.OnGet().Should()
                .BeJsonResult().Value;

            //Get the data property from the JSON object 
            //which contains the list of TableViewModels
            var dataList = (List<TableViewModel>)data
                .GetType().GetProperty("data")
                .GetValue(data, null);

            //Check the JSON object returned
            //contains the mock data
           Assert.AreEqual(_results.Count(), dataList.Count());

        }
    }
}