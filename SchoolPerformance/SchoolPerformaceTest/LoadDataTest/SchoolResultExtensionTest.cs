using SchoolPerformance.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.LoadData;

namespace SchoolPerformanceTest.LoadDataTest
{
    [TestClass]
    public class SchoolResultExtensionTest
    {
        private IEnumerable<SchoolResult> _schoolResults;
        private IEnumerable<SchoolResult> _schoolResults2;

        //Arrange
        //Create mock data
        [TestInitialize]
        public void Setup()
        {
            _schoolResults = new List<SchoolResult>
            {
                new SchoolResult {
                    URN = 2, PTFSM6CLA1A = 0.43, ATT8SCR = 40,
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.6, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35,
                    School = new School{URN = 2, LA = 10, ESTAB = 102}
                },
                 new SchoolResult {
                    URN = 1, PTFSM6CLA1A = 0.43, ATT8SCR = 40,
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.6, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35,
                    School = new School{URN = 1, LA = 10, ESTAB = 101}
                 },
                   new SchoolResult {
                    URN = 3, PTFSM6CLA1A = null, ATT8SCR = null,
                    ATT8SCR_FSM6CLA1A = null, ATT8SCR_NFSM6CLA1A = null,
                    P8MEA = null, P8MEA_FSM6CLA1A = null, P8MEA_NFSM6CLA1A = null, PTL2BASICS_94 = null,
                    PTFSM6CLA1ABASICS_94 = null, PTNOTFSM6CLA1ABASICS_94 = null, PTL2BASICS_95 = null,
                    PTFSM6CLA1ABASICS_95 = null, PTNOTFSM6CLA1ABASICS_95 = null,
                    School = new School{URN = 3, LA = 10, ESTAB = 103}
                   }
            };

            _schoolResults2 = new List<SchoolResult>
            {
                new SchoolResult {
                    URN = 2, PTFSM6CLA1A = 0.43, ATT8SCR = 40,
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = 40,
                    P8MEA=0.01, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = 0.01, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = 0.6, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35,
                    School = new School{URN = 2, LA = 10, ESTAB = 102}
                },
                 new SchoolResult {
                    URN = 1, PTFSM6CLA1A = 0.43, ATT8SCR = 40,
                    ATT8SCR_FSM6CLA1A = 38, ATT8SCR_NFSM6CLA1A = null,
                    P8MEA = null, P8MEA_FSM6CLA1A = 0.01, P8MEA_NFSM6CLA1A = null, PTL2BASICS_94 = 0.6,
                    PTFSM6CLA1ABASICS_94 = null, PTNOTFSM6CLA1ABASICS_94 = 0.6, PTL2BASICS_95 = 0.35,
                    PTFSM6CLA1ABASICS_95 = 0.35, PTNOTFSM6CLA1ABASICS_95 = 0.35,
                    School = new School{URN = 1, LA = 10, ESTAB = 101}
                 },
                   new SchoolResult {
                    URN = 3, PTFSM6CLA1A = null, ATT8SCR = null,
                    ATT8SCR_FSM6CLA1A = null, ATT8SCR_NFSM6CLA1A = null,
                    P8MEA = 0.01, P8MEA_FSM6CLA1A = null, P8MEA_NFSM6CLA1A = null, PTL2BASICS_94 = null,
                    PTFSM6CLA1ABASICS_94 = null, PTNOTFSM6CLA1ABASICS_94 = null, PTL2BASICS_95 = null,
                    PTFSM6CLA1ABASICS_95 = null, PTNOTFSM6CLA1ABASICS_95 = null,
                    School = new School{URN = 3, LA = 10, ESTAB = 103}
                   }
            };
        }

        //Tests remove method removes any school with no result
        [TestMethod] 
        public void removeAllNullResults()
        {

            //Act
            var schoolResultsLst = _schoolResults.RemoveNullResults();

            //Assert
            Assert.AreEqual(2, schoolResultsLst.Count());

        }

        //Tests remove method does not remove schools 
        //with only some results being null
        [TestMethod]
        public void removeNullResultsDoesNotRemoveParticalNullResults()
        {

            //Act
            var schoolResultsLst = _schoolResults2.RemoveNullResults();

            //Assert
            Assert.AreEqual(3, schoolResultsLst.Count());

        }

    }
}
