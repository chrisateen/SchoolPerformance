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

        [TestMethod] 
        public void GetAllRecordsFromADbset()
        {
            //Assert   

            //Get all records using GetAll method
            var schoolLst = _repository.GetAll();
            Assert.AreEqual(_schools.Count(), schoolLst.Count());

            //Get all records using Get method
            schoolLst = _repository.Get();
            Assert.AreEqual(_schools.Count(), schoolLst.Count());
        }

       
        [TestMethod]
        public void GetAllRecordsFromMultipleDbset()
        {
            //Assert   

            //Use GetAll method to get records 
            var schoolLst = _repository.GetAll(null, x => x.SchoolResults);
            
            //Count number of results for all schools in test database 
            //to see if school results objects was included in the list
            var resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            Assert.AreEqual(_schoolResults.Count(), resultCount);

            //Use Get method to get records 
            schoolLst = _repository.Get(null, null, x => x.SchoolResults);
            resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            Assert.AreEqual(_schoolResults.Count(), resultCount);
        }

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



    }
}
