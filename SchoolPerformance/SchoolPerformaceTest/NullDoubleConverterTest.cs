using CsvHelper.Configuration;
using LoadData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest
{
    [TestClass]
    public class NullDoubleConverterTest
    {

        //Tests NullDoubleConverter is able to convert a double of type string to a double
        [TestMethod]
        public void NullDoubleConverterConvertsToDouble()
        {
            //Arrange
            var converter = new NullDoubleConverter();

            //Act
            var num = "1.12";

            //Assert
            Assert.AreEqual(Convert.ToDouble(num),converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullDoubleConverter is able to handle an empty string
        [TestMethod]
        public void NullDoubleConverterConvertsEmptyString()
        {
            //Arrange
            var converter = new NullDoubleConverter();

            //Act
            var num = "";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullDoubleConverter converts NE values to null
        [TestMethod]
        public void NullDoubleConverterConvertsNE()
        {
            //Arrange
            var converter = new NullDoubleConverter();

            //Act
            var num = "NE";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }
    }
}
