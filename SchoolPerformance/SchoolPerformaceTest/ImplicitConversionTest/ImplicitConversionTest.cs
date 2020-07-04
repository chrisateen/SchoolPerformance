using Microsoft.VisualStudio.TestTools.UnitTesting;
using SchoolPerformance.Models;
using SchoolPerformance.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest.ImplicitConversionTest
{
    [TestClass]
    public class ImplicitConversionTest
    {
        [TestMethod]
        public void ResultModeltoScatterplotViewModelConversion()
        {
            //Arrange
            SchoolResult result = new SchoolResult
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
                    URN = 1 , 
                    LA = 100, 
                    ESTAB = 1000,
                    SCHNAME = "Test School"
                }
            };

            //Act
            ScatterplotViewModel resultViewModel = result;

            //Assert

            //Checks the Schoolresult object gets converted to ScatterplotViewModel
            Assert.IsNotNull(resultViewModel);

            //Checks the School name in the school model is included
            //when converting from SchoolResult to ScatterplotViewModel
            Assert.AreEqual(result.School.SCHNAME, resultViewModel.SCHNAME);
        }
    }
}
