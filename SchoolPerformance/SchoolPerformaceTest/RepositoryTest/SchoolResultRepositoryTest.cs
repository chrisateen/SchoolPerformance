using SchoolPerformance.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace SchoolPerformaceTest
{
    [TestClass]
    public class SchoolResultRepositoryTest
    {
        private SchoolPerformanceContext _context;

        private ISchoolResultRepository<SchoolResult> _repositorySchoolResult;

        private List<SchoolResult> _schoolResults;
        private List<SchoolResult> _schoolResults2;

        //Arange
        [TestInitialize]
        public void Setup()
        {
            //Create an InMemory Sqlite Database for testing
            var connection = new InMemorySqliteConnection();
            _context = connection._context;
            
            //Create the repository class that will be tested
            _repositorySchoolResult = new SchoolResultRepository<SchoolResult>(_context);

            //Mock data
            SetData();

            //Remove existing seeded data
            ClearSeedData();
        }

        //Tests remove method removes any school with no result
        [TestMethod] 
        public void removeAllNullResults()
        {
            //Add and save the mock data to the context
            _schoolResults.ForEach(x => _context.SchoolResult.Add(x));
            _context.SaveChanges();

            //Act
            var schoolResultsLst = _repositorySchoolResult.removeNullResults();

            //Assert
            Assert.AreEqual(2, schoolResultsLst.Count());

        }

        //Tests remove method does not remove schools 
        //with only some results being null
        [TestMethod]
        public void removeNullResultsDoesNotRemoveParticalNullResults()
        {
            //Remove previous mock data that was added
            _schoolResults.ForEach(x => _context.SchoolResult.Remove(x));

            //Add and save the mock data to the context
            _schoolResults2.ForEach(x => _context.SchoolResult.Add(x));
            _context.SaveChanges();

            //Act
            var schoolResultsLst = _repositorySchoolResult.removeNullResults();

            //Assert
            Assert.AreEqual(3, schoolResultsLst.Count());

        }



        //Create mock data
        [Ignore]
        public void SetData()
        {

            _schoolResults = new List<SchoolResult>
            {
                new SchoolResult { 
                    URN = 2, PTFSM6CLA1A = 0.43, ATT8SCR = 40, 
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.6, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35},
                 new SchoolResult {
                    URN = 1, PTFSM6CLA1A = 0.43, ATT8SCR = 40,
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.6, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35},
                   new SchoolResult {
                    URN = 3, PTFSM6CLA1A = null, ATT8SCR = null,
                    ATT8SCR_FSM6CLA1A = null, ATT8SCR_NFSM6CLA1A = null,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = null, P8MEA_NFSM6CLA1A = null, PTL2BASICS_94 = null,
                    PTFSM6CLA1ABASICS_94 = null, PTNOTFSM6CLA1ABASICS_94 = null, PTL2BASICS_95 = null,
                    PTFSM6CLA1ABASICS_95 = null, PTNOTFSM6CLA1ABASICS_95 = null}
            };

            _schoolResults2 = new List<SchoolResult>
            {
                new SchoolResult {
                    URN = 2, PTFSM6CLA1A = 0.43, ATT8SCR = 40,
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.6, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35},
                 new SchoolResult {
                    URN = 1, PTFSM6CLA1A = 0.43, ATT8SCR = 40,
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = null,
                    P8MEA=null, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = null, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = null, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35},
                   new SchoolResult {
                    URN = 3, PTFSM6CLA1A = null, ATT8SCR = null,
                    ATT8SCR_FSM6CLA1A = null, ATT8SCR_NFSM6CLA1A = null,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = null, P8MEA_NFSM6CLA1A = null, PTL2BASICS_94 = null,
                    PTFSM6CLA1ABASICS_94 = null, PTNOTFSM6CLA1ABASICS_94 = null, PTL2BASICS_95 = null,
                    PTFSM6CLA1ABASICS_95 = null, PTNOTFSM6CLA1ABASICS_95 = null}
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
