using CsvHelper.Configuration;
using LoadData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest.LoadDataTest
{
    [TestClass]
    public class NullStringConverterTest
    {

        //Tests NullStringConverter handles strings
        [TestMethod]
        public void NullStringConverterReturnsStrings()
        {
            //Arrange
            var converter = new NullStringConverter();

            //Act
            var str = "Hello World !";

            //Assert
            Assert.AreEqual(str,converter.ConvertFromString(str, null, new MemberMapData(null)));
        }

        //Tests NullStringConverter is able to handle an empty string
        [TestMethod]
        public void NullStringConverterConvertsEmptyString()
        {
            //Arrange
            var converter = new NullStringConverter();

            //Act
            var str = "";

            //Assert
            Assert.AreEqual(str, converter.ConvertFromString(str, null, new MemberMapData(null)));
        }

        
    }
}
