using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformanceTestSelenium
{
    [TestClass]
    public class NavBarTest
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
        public void HomeNavLinkReturnsHomePage()
        {
            
            //Click on the home link in the nav bar
            _driver.FindElement(By.Id("home-navbar-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/", Url);
        }

        [TestMethod]
        public void AboutNavLinkReturnsAboutPage()
        {

            //Click on the about link in the nav bar
            _driver.FindElement(By.Id("about-navbar-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Home/About", Url);
        }

        [TestMethod]
        public void FAQNavLinkReturnsFAQPage()
        {

            //Click on the FAQ link in the nav bar
            _driver.FindElement(By.Id("faq-navbar-link"))
                    .Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/Home/FAQ", Url);
        }
    }
}
