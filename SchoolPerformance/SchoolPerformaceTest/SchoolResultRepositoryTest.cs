using SchoolPerformance.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            
            //Create the repository class that will be tested
            _repository = new SchoolResultRepository<School>(_context); ;

            //Mock data
            _schools = new List<School>
            {
                new School { URN = 2, LAESTAB = 2,SCHNAME = "Test 1" },
                new School { URN = 1, LAESTAB = 1,SCHNAME = "Test 2" }
            };

            _schoolResults = new List<SchoolResult>
            {
                new SchoolResult { URN = 2, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.5 },
                new SchoolResult{ URN = 2, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.51 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.62 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.68 }
            };

            //Act    
            _schools.ForEach(x => _context.School.Add(x));
            _schoolResults.ForEach(x => _context.SchoolResult.Add(x));
            _context.SaveChanges();
        }

        //Tests Get method and GetAll method returns the expected records
        [TestMethod] 
        public void GetAllRecordsFromADbset()
        {
            //Assert   

            var schoolLst = _repository.GetAll();
            Assert.AreEqual(_schools.Count(), schoolLst.Count());

            schoolLst = _repository.Get();
            Assert.AreEqual(_schools.Count(), schoolLst.Count());
        }

        //Tests Get method and GetAll method returns the expected records
        //when another DbSet is included
        [TestMethod]
        public void GetAllRecordsFromMultipleDbset()
        {
            //Assert   

            var schoolLst = _repository.GetAll(null, x => x.SchoolResults);
            
            //Count number of results for all schools in test database 
            //to see if school results objects was included in the list
            var resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            Assert.AreEqual(_schoolResults.Count(), resultCount);

            schoolLst = _repository.Get(null, null, x => x.SchoolResults);
            resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            Assert.AreEqual(_schoolResults.Count(), resultCount);
        }

        //Tests when orderBy parameter is used with Get method and GetAll method
        //it returns the data in order
        [TestMethod]
        public void GetAllRecordsFromADbsetinOrder()
        {
            //Assert   

            //Gets the first school name in school using GetAll method when school is ordered by name
            var school = _repository.GetAll(x => x.OrderBy(n => n.SCHNAME)).First().SCHNAME;

            //Checks the first school name is the same as the first name in _school
            //when _school ordered by name
            Assert.AreEqual(_schools.OrderBy(n => n.SCHNAME).First().SCHNAME, school);

            //Gets the first school name in school using Get method when school is ordered by name
            school = _repository.Get(null,x => x.OrderBy(n => n.SCHNAME)).First().SCHNAME;

            Assert.AreEqual(_schools.OrderBy(n => n.SCHNAME).First().SCHNAME, school);
        }

        //Tests Get method and GetAll method returns the expected records
        //when a filter condition is specifed
        [TestMethod]
        public void FilterRecordsFromADbset()
        {
            //Assert

            var schoolLst = _repository.Get(x => x.SCHNAME == "Test 2").Count();
            Assert.AreEqual(_schools.Where(x => x.SCHNAME == "Test 2").Count(), schoolLst);
        }

        //Tests Get method returns the expected records
        //when a filter condition is specifed
        //and another DbSet is added
        [TestMethod]
        public void FilterRecordsFromADbsetWithMultipleDbsetIncluded()
        {
            //Assert

            var schoolLst = _repository.Get(x => x.SCHNAME == "Test 2",null,x => x.SchoolResults);

            //Count number of results for school name test 2 in the test database 
            //to see if school results objects was included in the list
            var resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            Assert.AreEqual(_schoolResults.Where(x => x.School.SCHNAME == "Test 2").Count(), resultCount);
        }
    }
}
