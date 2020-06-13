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
        //Properties to hold mock data
        IQueryable<School> _school { get; set; }
        IQueryable<SchoolDetails> _schoolDetails { get; set; }
        IQueryable<SchoolContextual> _schoolContextual { get; set; }
        IQueryable<SchoolResult> _schoolResult { get; set; }

        //Properties to hold mock DbSet
        Mock<DbSet<School>> _mockSchoolDbSet { get; set; }
        Mock<DbSet<SchoolContextual>> _mockSchoolContextualDbSet { get; set; }
        Mock<DbSet<SchoolDetails>> _mockSchoolDetailsDbSet { get; set; }
        Mock<DbSet<SchoolResult>> _mockSchoolResultDbSet { get; set; }

        //Properties to hold mock DbContext
        Mock<SchoolPerformanceContext> _mockSchoolPerformanceDbContext { get; set; }

        ISchoolResultRepository<School> _service { get; set; }

        [TestInitialize]
        public void Setup()
        {
            createData();
            setupDbContext();
            _service = new SchoolResultRepository<School>(_mockSchoolPerformanceDbContext.Object);
        }

        [Ignore]
        public void createData()
        {
            //Mock data
            _school = new List<School>
            {
                new School { URN = 1, LAESTAB = 1,SCHNAME = "Test 1" },
                new School { URN = 2, LAESTAB = 2,SCHNAME = "Test 2" }
            }.AsQueryable();

            _mockSchoolDbSet = _school.setupDbSet();

            _schoolDetails = new List<SchoolDetails>
            {
                new SchoolDetails { URN = 1, GENDER = "Mixed" },
                new SchoolDetails { URN = 2, GENDER = "Mixed" }
            }.AsQueryable();

            _mockSchoolDetailsDbSet = _schoolDetails.setupDbSet();

            _schoolContextual = new List<SchoolContextual>
            {
                new SchoolContextual { URN = 1, ACADEMICYEAR = 2018, NOR = 1200 },
                new SchoolContextual { URN = 1, ACADEMICYEAR = 2019, NOR = 1100 },
                new SchoolContextual { URN = 2, ACADEMICYEAR = 2018, NOR = 900 },
                new SchoolContextual { URN = 2, ACADEMICYEAR = 2019, NOR = 1000 }
            }.AsQueryable();

            _mockSchoolContextualDbSet = _schoolContextual.setupDbSet();

            _schoolResult = new List<SchoolResult>
            {
                new SchoolResult { URN = 1, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.5 },
                new SchoolResult{ URN = 1, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.51 },
                new SchoolResult { URN = 2, ACADEMICYEAR = 2018, PTL2BASICS_94 = 0.62 },
                new SchoolResult { URN = 2, ACADEMICYEAR = 2019, PTL2BASICS_94 = 0.68 }
            }.AsQueryable();

            _mockSchoolResultDbSet = _schoolResult.setupDbSet();
        }

        [Ignore]
        public void setupDbContext()
        {
            _mockSchoolPerformanceDbContext = new Mock<SchoolPerformanceContext>();
            _mockSchoolPerformanceDbContext
                .Setup(c => c.Set<School>())
                .Returns(_mockSchoolDbSet.Object);
            _mockSchoolPerformanceDbContext
               .Setup(c => c.Set<SchoolContextual>())
               .Returns(_mockSchoolContextualDbSet.Object);
            _mockSchoolPerformanceDbContext
              .Setup(c => c.Set<SchoolDetails>())
              .Returns(_mockSchoolDetailsDbSet.Object);
            _mockSchoolPerformanceDbContext
              .Setup(c => c.Set<SchoolResult>())
              .Returns(_mockSchoolResultDbSet.Object);
        }

        //Tests that the get() method in the repository class returns the 2 mock _school data
        [TestMethod]
        public void RepositoryGetMethodReturnsSchools()
        {
            var schools = _service.Get();
            var schoolsLstSize = schools.Count();
            
            Assert.AreEqual(_school.Count(), schoolsLstSize);
        }

        //Tests that the get() method with parameters in the repository class returns the 2 mock _school data
        [TestMethod]
        public void RepositoryGetMethodwithConditionsReturnsSchools()
        {
            //Conditions are ignored
            var schools = _service.Get(x => x.URN == 1,
                x => x.OrderBy(s => s.SCHNAME),
                x => x.SchoolResults
                );
            var schoolsLstSize = schools.Count();

            Assert.AreEqual(_school.Count(), schoolsLstSize);
        }


    }
}
