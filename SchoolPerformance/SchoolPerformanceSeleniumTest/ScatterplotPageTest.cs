using OpenQA.Selenium;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Support.Extensions;

namespace SchoolPerformaceTestSelenium
{
    [TestClass]
    public class ScatterplotPageTest
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();

            //Go to the homepage
            _driver.Navigate().GoToUrl("https://localhost:44382");

            //Go to the Scatterplot page
            _driver.Navigate().GoToUrl("https://localhost:44382/Scatterplot");
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }
        
        //Tests Y axis label on the graph changes when a user
        //selects a different measure
        [TestMethod]
        public void ChangeScatterplotYAxis()
        {
           var measureSelection = "Attainment 8 Score non-disadvantaged pupils";
            
            //Get dropdown list
           var dropDwnElm =  _driver.FindElement(By.Id("measure"));

           SelectElement measureLst = new SelectElement(dropDwnElm);

            //Select the attainment 8 non-disadvantaged option
            measureLst.SelectByText(measureSelection);
            _driver.FindElement(By.Id("btn-measure"))
                   .Click();

            //Get the y axis label using javascript
            var javaScript = "var yAxis=Chart.instances[0].options.scales.yAxes[0].scaleLabel.labelString; return yAxis";
            string result = _driver.ExecuteJavaScript<string>(javaScript);


            //Check that the y axis label has updated to the new measure
            Assert.AreEqual(measureSelection, result);

        }

        //Tests the legend changes when a user
        //selects a different school
        [TestMethod]
        public void SearchSchoolInScatterplot()
        {
            //Search for a school that exists using the search box
            _driver.FindElement(By.Id("inputSchool"))
                    .SendKeys("140958");

           
            _driver.FindElement(By.Id("btn-SchoolSearch"))
                   .Click();

            //Get the legend for the single black point using javascript
            var javaScript = "var schoolLabel=Chart.instances[0].data.datasets[0].label; return schoolLabel";
            string result = _driver.ExecuteJavaScript<string>(javaScript);


            //Check that the legend matches the school name searched for
            Assert.AreEqual("Eden Girls' School Coventry", result);

        }

        //Tests hat an alert is shown if school cannot be found
        [TestMethod]
        public void AlertShownIfSchoolCannotBeFound()
        {
            //Search for a school that doesn't exist
            _driver.FindElement(By.Id("inputSchool"))
                    .SendKeys("10");


            _driver.FindElement(By.Id("btn-SchoolSearch"))
                   .Click();

            //Store if alert was shown
            Boolean alertShown;

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            //Check if Alert is present after 10 seconds
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                alertShown = true;
            }
            catch (TimeoutException)
            {
                alertShown = false;
            }

            //Check if alert was shown
            Assert.IsTrue(alertShown);
        }

        //Tests that the show/hide national button
        //shows/hide national label
        [TestMethod]
        public void ToggleNationalLabel()
        {
            //Get the national label button
            var btn = _driver.FindElement(By.Id("toogleNationalLabel"));

            var xNational = false;
            var yNational = false;
            
            //Javascript to get whether the national labels are showing
            var xBtnScript = "var yNational=Chart.instances[0].options.annotation.annotations[0].label.enabled; return yNational";
            var yBtnScript = "var xNational=Chart.instances[0].options.annotation.annotations[1].label.enabled; return xNational";

            var btnTxt = btn.GetAttribute("value");

            //Toggle button
            for(int i = 0; i<2; i++)
            {
                //Click the show national label button
                btn.Click();

                //Get whether the national labels are showing
                var newXNational = _driver.ExecuteJavaScript<bool>(xBtnScript);
                var newYNational = _driver.ExecuteJavaScript<bool>(yBtnScript);

                var newBtnTxt = btn.GetAttribute("value");

                //Check that national label has changed to on/off
                Assert.AreNotEqual(xNational, newXNational);
                Assert.AreNotEqual(yNational, newYNational);

                //Check button text has changed
                Assert.AreNotEqual(btnTxt,newBtnTxt);

                xNational = newXNational;
                yNational = newYNational;
                btnTxt = newBtnTxt;
            }


        }
    }
}
