using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformanceTestSelenium
{
    [TestClass]
    public class SideBarTest
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();

            //Go to the homepage
            _driver.Navigate().GoToUrl("https://localhost:44382");

        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        [TestMethod]
        public void HomeSideBarLinkReturnsHomePage()
        {

            //Click on the home link in the side bar
            _driver.FindElement(By.Id("home-sidebar-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/", Url);
        }

        [TestMethod]
        public void ScatterplotSideBarLinkReturnsScatterplotPage()
        {

            //Click on the Scatterplot link in the side bar
            _driver.FindElement(By.Id("scatterplot-sidebar-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Scatterplot", Url);
        }

        [TestMethod]
        public void TableAllSideBarLinkReturnsTableAllPage()
        {
            _driver.FindElement(By.Id("table-sidebar-link"))
                   .Click();

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //Click on the Table all link in the side bar
            _driver.FindElement(By.Id("tableall-sidebar-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Tables", Url);
        }

        [TestMethod]
        public void TableDisadvantagedSideBarLinkReturnsTableDisadvantagedPage()
        {
            _driver.FindElement(By.Id("table-sidebar-link"))
                   .Click();


            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);


            //Click on the Table disadvantaged link in the side bar
            _driver.FindElement(By.Id("tabledisadvantaged-sidebar-link"))
                    .Click();


            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Tables/Disadvantaged", Url);
        }
    }
}
