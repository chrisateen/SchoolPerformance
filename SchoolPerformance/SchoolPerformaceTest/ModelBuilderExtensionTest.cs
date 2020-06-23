using LoadData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Models;
using SchoolPerformance.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace SchoolPerformaceTest
{
    [TestClass]
    public class ModelBuilderExtensionTest
    {
        public SchoolPerformanceContext _context;

        [TestInitialize]
        public void Setup()
        {
            //Create an InMemory Sqlite Database for testing
            var connection = new InMemorySqliteConnection();
            _context = connection._context;

        }

        //Tests School entity has been seeded with data
        [TestMethod]
        public void ContextSeededWithSchoolRecords()
        {

            Assert.AreNotEqual(0,_context.School.Count());
        }

        //Tests SchoolContextual entity has been seeded with data
        [TestMethod]
        public void ContextSeededWithSchoolContextualRecords()
        {

            Assert.AreNotEqual(0,_context.SchoolContextual.Count());
        }

        //Tests SchoolDetails entity has been seeded with data
        [TestMethod]
        public void ContextSeededWithSchoolDetailsRecords()
        {
            Assert.AreNotEqual(0,_context.SchoolDetails.Count());

        }

        //Tests SchoolResults entity has been seeded with data
        [TestMethod]
        public void ContextSeededWithSchoolResultsRecords()
        {

            Assert.AreNotEqual(0,_context.SchoolResult.Count());
        }

        //Tests that any records in the CSV file that does not have a URN i.e URN == 0
        //has been removed
        [TestMethod]
        public void SeededRecordsDoesNotHaveZeroURN()
        {
            Assert.AreEqual(0, _context.School.Where(x => x.URN == 0).Count());
            Assert.AreEqual(0, _context.SchoolContextual.Where(x => x.URN == 0).Count());
            Assert.AreEqual(0, _context.SchoolDetails.Where(x => x.URN == 0).Count());
            Assert.AreEqual(0, _context.SchoolResult.Where(x => x.URN == 0).Count());
        }

        //Tests that academic year has been added to all records 
        //in the SchoolContextual and SchoolResult Entity
        //as this information is added using code as it is not included in the csv files from the government
        [TestMethod]
        public void SeededRecordsContainsAcademicYear()
        {
            Assert.AreEqual(0, _context.SchoolContextual.Where(x => x.ACADEMICYEAR == 0).Count());
            Assert.AreEqual(0, _context.SchoolResult.Where(x => x.ACADEMICYEAR == 0).Count());
        }

        //Checks that schools in the school entity have data in the SchoolResult entity 
        //As only schools with an exam result should be seeded
        [TestMethod]
        public void AllSchoolsInSchoolEntityHaveResultsData()
        {
            Assert.AreEqual(0, _context.School.Where(x => x.SchoolResults == null).Count());
            Assert.AreEqual(0, _context.SchoolResult.Where(x => x.School == null).Count());
        }

        //Checks that schools in the SchoolContextual entity have data in the SchoolResult entity 
        //As only schools with an exam result should be seeded
        [TestMethod]
        public void AllSchoolsInSchoolContextualEntityHaveResultsData()
        {
            Assert.AreEqual(0, _context.SchoolContextual.Where(x => x.School.SchoolResults == null).Count());
            Assert.AreEqual(0, _context.SchoolResult.Where(x => x.School.SchoolContextuals == null).Count());
        }

        //Checks that schools in the SchoolDetails entity have data in the SchoolResult entity 
        //As only schools with an exam result should be seeded
        [TestMethod]
        public void AllSchoolsInSchoolDetailsEntityHaveResultsData()
        {
            Assert.AreEqual(0, _context.SchoolDetails.Where(x => x.School.SchoolResults == null).Count());
            Assert.AreEqual(0, _context.SchoolResult.Where(x => x.School.SchoolDetails == null).Count());
        }

    }
}
