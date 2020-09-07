using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolPerformance.Controllers;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using SchoolPerformance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPerformaceTest.ControllerTest
{
    class SchoolControllerTest
    {
        Mock<ISchoolPerformanceRepository<SchoolResult>> _mockSchoolResult;
        Mock<ISchoolPerformanceRepository<SchoolContextual>> _mockSchoolContextual;
        SchoolController _controller;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            SetupRepository();
        }

        //Checks School view is rendered
        //with an object of type SchoolViewModel
        [TestMethod]
        public void IndexReturnsPageWithSchoolViewModel()
        {
            // Act and Assert
            var res = _controller.Index(1).Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<SchoolViewModel>().Subject;
        }

        //Setup mock objects to be returned 
        //when methods from Repository is called
        [Ignore]
        public void SetupRepository()
        {
            //Mock SchoolPerformanceRepository
            _mockSchoolResult = new Mock<ISchoolPerformanceRepository<SchoolResult>>();

            //Mock data
            var results = MockData.GetSchoolResultList(true);
            var contextuals = MockData.GetSchoolContextualList(true);

            //When the Get method is called return results 
            //excluding national data
            _mockSchoolResult.Setup(m => m.Get(
                It.IsAny<Expression<Func<SchoolResult, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolResult>, IOrderedQueryable<SchoolResult>>>(),
                It.IsAny<Expression<Func<SchoolResult, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(results.Where(r => r.URN != 9)));


            //When the GetNational method is called return results 
            //with national data only
            _mockSchoolResult.Setup(m => m.GetNational(
                It.IsAny<Expression<Func<SchoolResult, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolResult>, IOrderedQueryable<SchoolResult>>>(),
                It.IsAny<Expression<Func<SchoolResult, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(results.Where(r => r.URN == 9)));

            //When the Get method is called return contextuals
            //excluding national data
            _mockSchoolContextual.Setup(m => m.Get(
                It.IsAny<Expression<Func<SchoolContextual, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolContextual>, IOrderedQueryable<SchoolContextual>>>(),
                It.IsAny<Expression<Func<SchoolContextual, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolContextual>>(contextuals.Where(r => r.URN != 9)));


            //When the GetNational is called return contextuals 
            //with national data only
            _mockSchoolContextual.Setup(m => m.GetNational(
                It.IsAny<Expression<Func<SchoolContextual, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolContextual>, IOrderedQueryable<SchoolContextual>>>(),
                It.IsAny<Expression<Func<SchoolContextual, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolContextual>>(contextuals.Where(r => r.URN == 9)));

            _controller = new SchoolController(_mockSchoolResult.Object, _mockSchoolContextual.Object);

        }
    }
}
