using CsvHelper.Configuration;
using LoadData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest
{
    [TestClass]
    public class NullURNAndLAESTABConverterTest
    {

        //Tests NullURNAndLAESTABConverter is able to convert an int of type string to an integer
        [TestMethod]
        public void NullURNAndLAESTABConverterReturnsStrings()
        {
            //Arrange
            var converter = new NullURNAndLAESTABConverter();

            //Act
            var num = "123456";

            //Assert
            Assert.AreEqual(Convert.ToInt32(num),converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullURNAndLAESTABConverterTest handles an empty string by returning zero
        [TestMethod]
        public void NullURNAndLAESTABConverterConvertsEmptyString()
        {
            //Arrange
            var converter = new NullURNAndLAESTABConverter();

            //Act
            var str = "";

            //Assert
            Assert.AreEqual(0, converter.ConvertFromString(str, null, new MemberMapData(null)));
        }

        
    }
}
