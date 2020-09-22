using OpenQA.Selenium;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;

namespace SchoolPerformaceTestSelenium
{
    [TestClass]
    public class HomePageTest
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();
        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [TestMethod]
        public void SearchValidSchoolReturnsSchoolPage()
        {
            //Go to the homepage
            _driver.Navigate().GoToUrl("https://localhost:44382");

            //Search for a school that exists using the search box in the home page
            _driver.FindElement(By.Id("homepage-search"))
                    .SendKeys("146255");

            //Press the search button
            _driver.FindElement(By.Id("homepage-search-btn")).Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/School/146255", Url);
        }

        [TestMethod]
        public void LinktoScatterplotPageIsWorking()
        {
            //Go to the homepage
            _driver.Navigate().GoToUrl("https://localhost:44382");

            //Click on the Scatterplot page link
            _driver.FindElement(By.Id("scatterplot-home-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Scatterplot", Url);
        }

        [TestMethod]
        public void LinktoTableAllPageIsWorking()
        {
            //Go to the homepage
            _driver.Navigate().GoToUrl("https://localhost:44382");

            //Click on the results table all link
            _driver.FindElement(By.Id("results-all-home-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Tables", Url);
        }

        [TestMethod]
        public void LinktoTableDisadvantagedPageIsWorking()
        {
            //Go to the homepage
            _driver.Navigate().GoToUrl("https://localhost:44382");

            //Click on the results table disadvantaged link
            _driver.FindElement(By.Id("results-disadvantaged-home-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Tables/Disadvantaged", Url);
        }
    }
}
