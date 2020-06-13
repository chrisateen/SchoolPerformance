using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SchoolPerformaceTest
{
    public class SchoolResultRepositoryTest
    {

        IQueryable<School> _school = new List<School>
        {
            new School { URN = 1, LAESTAB = 1,SCHNAME = "Test 1" },
            new School { URN = 2, LAESTAB = 2,SCHNAME = "Test 2" }
        }.AsQueryable();

        IQueryable<SchoolDetails> _schoolDetails = new List<SchoolDetails>
        {
            new SchoolDetails { URN = 1, GENDER = "Mixed" },
            new SchoolDetails { URN = 2, GENDER = "Mixed" }
        }.AsQueryable();

        [Fact]
        public void Test1()
        {

        }
    }
}
