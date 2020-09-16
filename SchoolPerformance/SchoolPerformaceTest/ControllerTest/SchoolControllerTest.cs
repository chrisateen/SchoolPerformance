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
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SchoolPerformanceTest.ControllerTest
{
    [TestClass]
    public class SchoolControllerTest
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
        public async Task IndexReturnsPageWithSchoolViewModel()
        {
            // Act and Assert
            var controller = await _controller.Index(1);

            controller.Should()
                .BeViewResult().WithDefaultViewName();

            var res = controller.Should()
                .BeOfType<ViewResult>().Subject;

            var test = res.Model.Should()
                .BeAssignableTo<SchoolViewModel>().Subject;
        }

        //Checks error page is displayed if the user enters a school id 
        //that does not exist
        [TestMethod]
        public async Task IndexReturnsPageErrorIfSchoolDoesNotExist()
        {
            // Act and Assert
            var controller = await _controller.Index(3);

            controller.Should()
                .BeViewResult().WithViewName("SchoolNotFound");

            var res = controller.Should()
                .BeOfType<ViewResult>().Subject;
        }

        //Check that call to school action method redirects to index action method
        [TestMethod]
        public void SchoolActionMethodRedirectsToIndex()
        {
            //Act
            var controller = _controller.School(1);

            //Assert
            controller.Should()
                .BeRedirectToActionResult().WithActionName("Index");
        }

        //Setup mock objects to be returned 
        //when methods from Repository is called
        [Ignore]
        public void SetupRepository()
        {
            //Mock SchoolPerformanceRepository
            _mockSchoolResult = new Mock<ISchoolPerformanceRepository<SchoolResult>>();
            _mockSchoolContextual = new Mock<ISchoolPerformanceRepository<SchoolContextual>>();

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
                It.IsAny<Expression<Func<SchoolResult, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolResult>>(results.Where(r => r.URN == 9)));

            //When the GetNational is called return contextuals 
            //with national data only
            _mockSchoolContextual.Setup(m => m.GetNational(
                It.IsAny<Expression<Func<SchoolContextual, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolContextual>, IOrderedQueryable<SchoolContextual>>>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolContextual>>(contextuals.Where(r => r.URN == 9)));

            //When the Get method is called return contextuals
            //excluding national data
            _mockSchoolContextual.Setup(m => m.Get(
                It.IsAny<Expression<Func<SchoolContextual, bool>>>(),
                It.IsAny<Func<IQueryable<SchoolContextual>, IOrderedQueryable<SchoolContextual>>>(),
                It.IsAny<Expression<Func<SchoolContextual, object>>[]>()
                ))
                .Returns(Task.FromResult<IEnumerable<SchoolContextual>>(contextuals.Where(r => r.URN != 9)));

            //When the GetByUrnOrLAESATB is called return get data where URN matches
            _mockSchoolContextual.Setup(m => m.GetByUrnOrLAESATB(
                It.IsAny<int>(),
                It.IsAny<Expression<Func<SchoolContextual, bool>>>(),
                It.IsAny<Expression<Func<SchoolContextual, object>>[]>()
                ))
                .Returns(
                (int x,
                Expression<Func<SchoolContextual, bool>> y, 
                Expression<Func<SchoolContextual, object>>[] z) =>
                Task.FromResult<IEnumerable<SchoolContextual>>(contextuals.Where(r => r.URN == x)));

            _mockSchoolResult.Setup(m => m.GetByUrnOrLAESATB(
               It.IsAny<int>(),
               It.IsAny<Expression<Func<SchoolResult, bool>>>(),
               It.IsAny<Expression<Func<SchoolResult, object>>[]>()
               ))
               .Returns(
                (int x, 
                Expression<Func<SchoolResult, bool>> y, 
                Expression<Func<SchoolResult, object>>[] z) => 
               Task.FromResult<IEnumerable<SchoolResult>>(results.Where(r => r.URN == x)));

            _controller = new SchoolController(_mockSchoolResult.Object, _mockSchoolContextual.Object);

        }
    }
}
