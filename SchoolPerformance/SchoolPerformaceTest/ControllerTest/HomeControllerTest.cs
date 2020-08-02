﻿using FluentAssertions;
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
    public class HomeControllerTest
    {
        //Checks Home view is rendered
        [TestMethod]
        public void IndexReturnsHomePage()
        {
            //Arrange
            var controller = new HomeController();

            // Act and Assert
            controller.Index().Should()
                .BeViewResult().WithDefaultViewName();
        }
    }
}
