using CsvHelper.Configuration;
using LoadData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolPerformaceTest
{
    [TestClass]
    public class NullIntConverterTest
    {

        //Tests NullIntConverter is able to convert an int of type string to an integer
        [TestMethod]
        public void NullIntConverterConvertsToInt()
        {
            //Arrange
            var converter = new NullIntConverter();

            //Act
            var num = "100";

            //Assert
            Assert.AreEqual(Convert.ToInt32(num),converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullIntConverter is able to handle an empty string
        [TestMethod]
        public void NullIntConverterConvertsEmptyString()
        {
            //Arrange
            var converter = new NullIntConverter();

            //Act
            var num = "";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullIntConverter converts NE values to null
        [TestMethod]
        public void NullIntConverterConvertsNE()
        {
            //Arrange
            var converter = new NullIntConverter();

            //Act
            var num = "NE";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullIntConverter converts NA values to null
        [TestMethod]
        public void NullIntConverterConvertsNA()
        {
            //Arrange
            var converter = new NullIntConverter();

            //Act
            var num = "NA";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullIntConverter converts NP values to null
        [TestMethod]
        public void NullIntConverterConvertsNP()
        {
            //Arrange
            var converter = new NullIntConverter();

            //Act
            var num = "NP";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullIntConverter converts SUPP values to null
        [TestMethod]
        public void NullIntConverterConvertsSUPP()
        {
            //Arrange
            var converter = new NullIntConverter();

            //Act
            var num = "SUPP";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }

        //Tests NullIntConverter converts LOWCOV values to null
        [TestMethod]
        public void NullIntConverterConvertsLOWCOV()
        {
            //Arrange
            var converter = new NullIntConverter();

            //Act
            var num = "LOWCOV";

            //Assert
            Assert.IsNull(converter.ConvertFromString(num, null, new MemberMapData(null)));
        }
    }
}
