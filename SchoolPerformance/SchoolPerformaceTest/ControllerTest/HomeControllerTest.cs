using FluentAssertions;
using FluentAssertions.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest.ControllerTest
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexReturnsHomePage()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            controller.Index().Should()
                .BeViewResult().WithDefaultViewName();
        }
    }
}
