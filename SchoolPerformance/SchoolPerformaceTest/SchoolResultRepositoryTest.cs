using SchoolPerformance.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Repository;

namespace SchoolPerformaceTest
{
    [TestClass]
    public class SchoolResultRepositoryTest
    {
        private SchoolPerformanceContext _context;

        private ISchoolResultRepository<School> _repository;

        private List<School> _schools;
        private List<SchoolResult> _schoolResults;

        [TestInitialize]
        public void Setup()
        {
            //Arange
            var connection = new InMemorySqliteConnection();
            _context = connection._context;
            _repository = connection._repository;

            _schools = new List<School>
            {
                new School { URN = 1, LAESTAB = 1,SCHNAME = "Test 1" },
                new School { URN = 2, LAESTAB = 2,SCHNAME = "Test 2" }
            };

            _schoolResults = new List<SchoolResult>
            {
                new SchoolResult { URN = 1, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.5 },
                new SchoolResult{ URN = 1, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.51 },
                new SchoolResult { URN = 2, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.62 },
                new SchoolResult { URN = 2, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.68 }
            };

            //Act    
            _schools.ForEach(x => _context.School.Add(x));
            _schoolResults.ForEach(x => _context.SchoolResult.Add(x));
            _context.SaveChanges();
        }

        [TestMethod]
        public void Task_Add_With_Relation()
        {
            
            //Assert   
            var schoolLst = _repository.GetAll(x => x.OrderBy(n => n.SCHNAME), x => x.SchoolResults);
            var resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            Assert.AreEqual(_schoolResults.Count(), resultCount);
        }


    }
}
