using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest
{
    static class MockData
    {
        /// <summary>
        /// Create a mock SchoolResult object
        /// </summary>
        /// <param name="includeSchool">If a School object should be included in SchoolResult</param>
        /// <returns> Mock SchoolResult object</returns>
        public static SchoolResult GetSchoolResult(bool includeSchool)
        {
            var result = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28
            };

            var School = new School
            {
                URN = 1,
                LA = 100,
                ESTAB = 1000,
                SCHNAME = "Test School"
            };

            //Include school object if required
            if (includeSchool == true)
            {
                result.School = School;
            }

            return result;
        }

        /// <summary>
        /// Create a mock list of SchoolResult objects
        /// </summary>
        /// <param name="national">If mock object representing national data should be included </param>
        /// <returns>A list of mock SchoolResult objects</returns>
        public static List<SchoolResult> GetSchoolResultList(bool national)
        {
            List<SchoolResult> results = new List<SchoolResult>();
            SchoolResult result1 = new SchoolResult
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 1,
                    LA = 100,
                    ESTAB = 1000,
                    SCHNAME = "Test School 1"
                }
            };

            SchoolResult result2 = new SchoolResult
            {
                URN = 2,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 2,
                    LA = 200,
                    ESTAB = 2000,
                    SCHNAME = "Test School 2"
                }
            };

            SchoolResult nationalResult = new SchoolResult
            {
                URN = 9,
                ACADEMICYEAR = 2019,
                PTFSM6CLA1A = 0.5,
                ATT8SCR = 40,
                ATT8SCR_FSM6CLA1A = 38,
                ATT8SCR_NFSM6CLA1A = 42,
                P8MEA = 0.00,
                P8MEA_FSM6CLA1A = -0.01,
                P8MEA_NFSM6CLA1A = 0.00,
                PTL2BASICS_94 = 0.55,
                PTFSM6CLA1ABASICS_94 = 0.54,
                PTNOTFSM6CLA1ABASICS_94 = 0.56,
                PTL2BASICS_95 = 0.25,
                PTFSM6CLA1ABASICS_95 = 0.22,
                PTNOTFSM6CLA1ABASICS_95 = 0.28,
                School = new School
                {
                    URN = 9,
                    SCHNAME = ""
                }
            };

            results.Add(result1);
            results.Add(result2);

            //Include mock object representing national data if required
            if(national == true)
            {
                results.Add(nationalResult);
            }

            return results;
        }

        /// <summary>
        /// Create a mock list of SchoolContextual objects
        /// </summary>
        /// <param name="national">If mock object representing national data should be included </param>
        /// <returns>A list of mock SchoolContextual objects</returns>
        public static List<SchoolContextual> GetSchoolContextualList(bool national)
        {
            List<SchoolContextual> contextuals = new List<SchoolContextual>();

            SchoolContextual school1 = new SchoolContextual
            {
                URN = 1,
                ACADEMICYEAR = 2019,
                School = new School
                {
                    URN = 1,
                    LA = 100,
                    ESTAB = 1000,
                    SCHNAME = "Test School 1"
                }
            };

            SchoolContextual school2 = new SchoolContextual
            {
                URN = 2,
                ACADEMICYEAR = 2019,
                School = new School
                {
                    URN = 2,
                    LA = 200,
                    ESTAB = 2000,
                    SCHNAME = "Test School 2"
                }
            };

            SchoolContextual nationalContextual = new SchoolContextual
            {
                URN = 9,
                ACADEMICYEAR = 2019,
                School = new School
                {
                    URN = 9,
                    SCHNAME = ""
                }
            };

            contextuals.Add(school1);
            contextuals.Add(school2);

            //Include mock object representing national data if required
            if (national == true)
            {
                contextuals.Add(nationalContextual);
            }


            return contextuals;

        }
    }
}
