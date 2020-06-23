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
            var connection = new InMemorySqliteConnection();
            _context = connection._context;
           // _context._modelBuilder.Seed();

        }

        [TestMethod]
        public void ContextSeededWithSchoolRecords()
        {

            Assert.AreNotEqual(0,_context.School.Count());
        }

        [TestMethod]
        public void ContextSeededWithSchoolContextualRecords()
        {

            Assert.AreNotEqual(0,_context.SchoolContextual.Count());
        }

        [TestMethod]
        public void ContextSeededWithSchoolDetailsRecords()
        {
            Assert.AreNotEqual(0,_context.SchoolDetails.Count());

        }

        [TestMethod]
        public void ContextSeededWithSchoolResultsRecords()
        {

            Assert.AreNotEqual(0,_context.SchoolResult.Count());
        }

        [TestMethod]
        public void SeededRecordsDoesNotHaveZeroURN()
        {
            Assert.AreEqual(0, _context.School.Where(x => x.URN == 0).Count());
            Assert.AreEqual(0, _context.SchoolContextual.Where(x => x.URN == 0).Count());
            Assert.AreEqual(0, _context.SchoolDetails.Where(x => x.URN == 0).Count());
            Assert.AreEqual(0, _context.SchoolResult.Where(x => x.URN == 0).Count());
        }

        [TestMethod]
        public void SeededRecordsContainsAcademicYear()
        {
            Assert.AreEqual(0, _context.School.Where(x => x.URN == 0).Count());
        }

    }
}
