using SchoolPerformance.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace SchoolPerformaceTest.RepositoryTest
{
    [TestClass]
    public class SchoolPerformanceRepositoryTest
    {
        private SchoolPerformanceContext _context;

        private ISchoolPerformanceRepository<School> _repositorySchool;
        private ISchoolPerformanceRepository<SchoolResult> _repositorySchoolResult;

        private List<School> _schools;
        private List<SchoolResult> _schoolResults;

        //Arrange
        [TestInitialize]
        public void Setup()
        {
            //Create an InMemory Sqlite Database for testing
            var connection = new InMemorySqliteConnection();
            _context = connection._context;
            
            //Create the repository class that will be tested
            _repositorySchool = new SchoolPerformanceRepository<School>(_context);
            _repositorySchoolResult = new SchoolPerformanceRepository<SchoolResult>(_context);

            //Mock data
            SetData();

            //Remove existing seeded data
            ClearSeedData();

            //Add and save the mock data to the context
            _schools.ForEach(x => _context.School.Add(x));
            _schoolResults.ForEach(x => _context.SchoolResult.Add(x));
            _context.SaveChanges();
        }

        //Tests Get method and GetAll method returns the expected records
        //and exclude the national data
        [TestMethod] 
        public void GetAllRecordsFromADbsetExcludingNational()
        {
              
            //Act
            var schoolLst = _repositorySchool.GetAll();

            //Assert
            Assert.AreEqual(
                _schools.Where(s => s.URN != 9).Count(), 
                schoolLst.Count()
                );

            //Act
            schoolLst = _repositorySchool.Get();

            //Assert
            Assert.AreEqual(
                _schools.Where(s => s.URN != 9).Count(), 
                schoolLst.Count()
                );
        }

        //Tests Get method and GetAll method returns the expected records
        //when another DbSet is included
        //National data should be excluded
        [TestMethod]
        public void GetAllRecordsFromMultipleDbset()
        {

            //Act
            var schoolLst = _repositorySchool.GetAll(null, x => x.SchoolResults);
            
            //Count number of results for all schools in test database 
            //to see if school results objects was included in the list
            var resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            //Assert
            Assert.AreEqual(_schoolResults.Where(s => s.URN != 9).Count(), resultCount);

            //Act
            schoolLst = _repositorySchool.Get(null, null, x => x.SchoolResults);
            resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();

            //Assert
            Assert.AreEqual(_schoolResults.Where(s => s.URN != 9).Count(), resultCount);
        }

        //Tests when orderBy parameter is used with Get method and GetAll method
        //it returns the data in order
        //National data should be excluded
        [TestMethod]
        public void GetAllRecordsFromADbsetinOrder()
        {

            //Act

            //Gets the first school name in school using GetAll method when school is ordered by name
            var school = _repositorySchool.GetAll(x => x.OrderBy(n => n.SCHNAME)).First().SCHNAME;

            //Assert

            //Checks the first school name is the same as the first name in _school
            //when _school ordered by name, excluding national
            Assert.AreEqual(
                _schools.Where(s => s.URN != 9).OrderBy(n => n.SCHNAME).First().SCHNAME, 
                school
                );

            //Act

            //Gets the first school name in school using Get method when school is ordered by name
            school = _repositorySchool.Get(null,x => x.OrderBy(n => n.SCHNAME)).First().SCHNAME;

            //Assert
            Assert.AreEqual(
                _schools.Where(s => s.URN != 9).OrderBy(n => n.SCHNAME).First().SCHNAME, 
                school
                );
        }

        //Tests Get method returns the expected records
        //when a filter condition is specified
        //National data should be excluded
        [TestMethod]
        public void FilterRecordsFromADbset()
        {
            
            //Act
            var schoolLst = _repositorySchool.Get(x => x.SCHNAME != "Test 2").Count();

            //Assert
            Assert.AreEqual(
                _schools.Where(x => x.SCHNAME != "Test 2" && x.URN != 9).Count(), 
                schoolLst
                );
        }

        //Tests Get method returns the expected records
        //when a filter condition is specified
        //and returns data in order when orderBy is specified
        //National data should be excluded
        [TestMethod]
        public void FilterRecordsFromADbsetAndOrder()
        {
            //Act

            //Gets the first school name in school using Get method
            //when data is filtered by PTL2BASICS_94 above 0.6 and ordered by PTL2BASICS_94
            //National results should be excluded
            var school = _repositorySchoolResult.Get(x => x.PTL2BASICS_94 >= 0.60, x => x.OrderBy(n => n.PTL2BASICS_94))
                                                .First().School.SCHNAME;

            //Assert
            Assert.AreEqual(
                _schoolResults.Where(x => x.PTL2BASICS_94 >= 0.60 && x.URN != 9)
                    .OrderBy(n => n.PTL2BASICS_94).First().School.SCHNAME, 
                school);
        }

        //Tests Get method returns the expected records
        //when a filter condition is specified
        //and another DbSet is added
        //National data should be excluded
        [TestMethod]
        public void FilterRecordsFromADbsetWithMultipleDbsetIncluded()
        {
            //Act
            var schoolLst = _repositorySchool.Get(x => x.SCHNAME != "Test 2",null,x => x.SchoolResults);

            //Count number of results for school name test 2 in the test database 
            //to see if school results objects was included in the list
            //National results should be excluded
            var resultCount = schoolLst.SelectMany(s => s.SchoolResults.Select(x => x.URN)).Count();


            //Assert
            Assert.AreEqual(
                _schoolResults.Where(x => x.School.SCHNAME != "Test 2" && x.URN !=9).Count(), 
                resultCount);
            
        }

        //Create mock data
        [Ignore]
        public void SetData()
        {
            _schools = new List<School>
            {
                new School { URN = 2, SCHNAME = "Test 1" },
                new School { URN = 1, SCHNAME = "Test 2" },
                new School { URN = 9, SCHNAME = ""}
            };

            _schoolResults = new List<SchoolResult>
            {
                new SchoolResult { URN = 2, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.5 },
                new SchoolResult{ URN = 2, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.51 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.62 },
                new SchoolResult { URN = 1, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.68 },
                new SchoolResult { URN = 9, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.62 },
                new SchoolResult { URN = 9, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.68 }
            };
        }

        //Any data that is seeded in the OnModelCreating method is removed
        [Ignore]
        public void ClearSeedData()
        {
            _context.Database.ExecuteSqlRaw("DELETE FROM SchoolResult");
            _context.Database.ExecuteSqlRaw("DELETE FROM SchoolContextual");
            _context.Database.ExecuteSqlRaw("DELETE FROM SchoolDetails");
            _context.Database.ExecuteSqlRaw("DELETE FROM School");
            _context.SaveChanges();
        }
    }
}
