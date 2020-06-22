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

            
        }

        [TestMethod]
        public void hasSchoolRecords()
        {
            _context._modelBuilder.Seed();
            Assert.IsNotNull(_context.School.Count());
        }


    }
}
