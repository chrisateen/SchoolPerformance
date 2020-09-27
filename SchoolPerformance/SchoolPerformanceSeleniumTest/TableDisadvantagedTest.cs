using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SchoolPerformanceTestSelenium
{
    [TestClass]
    public class TableDisadvantagedTest
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        [TestInitialize]
        public void Setup()
        {
            _driver = new ChromeDriver();

            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));

            //Go to the homepage
            _driver.Navigate().GoToUrl("https://localhost:44382");

            //Go to the Table page
            _driver.Navigate().GoToUrl("https://localhost:44382/Tables/Disadvantaged");

        }

        [TestCleanup]
        public void TearDown()
        {
            _driver.Quit();
            _driver.Dispose();
        }

        //Test that all records get exported
        [TestMethod]
        public void ExportAllReocrdsInTable()
        {
            //Wait until records have been loaded
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.LinkText("1")));

            //Click on the export all button
            _driver.FindElement(By.LinkText("Export All Records"))
                    .Click();

            //Export all records to csv
            _driver.FindElement(By.LinkText("CSV"))
                    .Click();

            Thread.Sleep(500);

            var fileRows = CountCSVRows();

            //Use the table info to get the number of records
            var tableInfo = _driver.FindElement(By.Id("resultsTable_info"));

            var tableTotalCount = tableInfo.GetAttribute("innerHTML").Split(" ")[5];

            var total = Int32.Parse(tableTotalCount, NumberStyles.AllowThousands);
            
            //Check that all records were exported
            Assert.AreEqual(total, fileRows);
        }


        //Test that current page only get exported
        [TestMethod]
        public void ExportCurrentPageRecordsInTable()
        {
            //Css selector of the search box in the table
            var cssElm = "div.dataTables_wrapper div.dataTables_filter input";

            //Wait until records have been loaded
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.LinkText("1")));

            //Get search box in table
            var search = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(cssElm)));

            search.SendKeys("Harris");

            //Click on the export current page button
            _driver.FindElement(By.LinkText("Export Current Page"))
                    .Click();

            //Export records to csv
            _driver.FindElement(By.LinkText("CSV"))
                    .Click();

            Thread.Sleep(500);

            var fileRows = CountCSVRows();

            //Use the table info to get the number of records
            var tableInfo = _driver.FindElement(By.Id("resultsTable_info"));

            var tableTotalCount = tableInfo.GetAttribute("innerHTML").Split(" ")[3];

            var total = Int32.Parse(tableTotalCount, NumberStyles.AllowThousands);

            //Check that only records on the page were exported
            Assert.AreEqual(total, fileRows);
        }

        //Test that all records that meet a condition gets exported
        [TestMethod]
        public void ExportFilteredRecordsInTable()
        {
            //Css selector of the search box in the table
            var cssElm = "div.dataTables_wrapper div.dataTables_filter input";

            //Wait until records have been loaded
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.LinkText("1")));

            //Get search box in table
            var search = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(cssElm)));

            search.SendKeys("Harris");

            //Click on the export current page button
            _driver.FindElement(By.LinkText("Export Filtered Records"))
                    .Click();

            //Export records to csv
            _driver.FindElement(By.LinkText("CSV"))
                    .Click();

            Thread.Sleep(500);

            var fileRows = CountCSVRows();

            //Use the table info to get the number of records
            var tableInfo = _driver.FindElement(By.Id("resultsTable_info"));

            var tableTotalCount = tableInfo.GetAttribute("innerHTML").Split(" ")[5];

            var total = Int32.Parse(tableTotalCount, NumberStyles.AllowThousands);

            //Check that only records on the page were exported
            Assert.AreEqual(total, fileRows);
        }


        //Tests that one can click on the school name 
        //on the table to get the school details
        [TestMethod]
        public void ClickSchoolNameToGetSchoolDetails()
        {

            //Css selector of the search box in the table
            var cssElm = "div.dataTables_wrapper div.dataTables_filter input";

            //Get search box in table
            var search = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.CssSelector(cssElm)));

            search.SendKeys("Brentside High School");

            //Get and click on 
            var link = _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.LinkText("Brentside High School")));

            link.Click();

            string Url = _driver.Url;

            Assert.AreEqual("https://localhost:44382/School/101939", Url);

        }

        //Count number of rows in latest export file
        [Ignore]
        public int CountCSVRows()
        {
            
            var rowCount = 0;

            var folder = new DirectoryInfo(@"C:\Users\no_ot\Downloads");

            //Get the latest Data Export csv file
            var csvFile = folder.GetFiles().OrderByDescending(f => f.LastWriteTime)
                .Where(f => f.Name.Contains("Data export") && f.Extension == ".csv").First();

            //Count no of rows in the file
            using (var reader = new StreamReader(csvFile.FullName))
            {
                var lines = File.ReadAllLines(csvFile.FullName);
                
                //Total no of rows minus header row
                rowCount = lines.Length - 1;
            }

            return rowCount;
        }

    }
}
