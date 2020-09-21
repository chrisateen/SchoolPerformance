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
    }
}
