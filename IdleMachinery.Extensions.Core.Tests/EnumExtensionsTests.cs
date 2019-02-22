using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using componentModel = System.ComponentModel;

namespace IdleMachinery.Extensions.Core.Tests
{
    [TestClass]
    public class EnumExtensionsTests
    {
        public enum Module
        {
            [componentModel.Description("Introducing Extension Methods")]
            Intro,
            Advanced,
            Library = 99
        }

        [TestMethod]
        public void EnumGetName_Core()
        {
            // Arrange
            var intro = Module.Intro;
            var expected = "Intro";

            // Act
            var actual_ToString = intro.ToString();
            var actual_EnumGetName = Enum.GetName(typeof(Module), intro);
            var actual = intro.GetName();

            // Assert
            Assert.AreEqual(expected, actual_ToString);
            Assert.AreEqual(expected, actual_EnumGetName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EnumGetDescription_Core()
        {
            // Arrange
            var expected_Intro = "Introducing Extension Methods";
            var expected_Advanced = "Advanced";

            // Act
            var actual_Intro = Module.Intro.GetDescription();
            var actual_Advanced = Module.Advanced.GetDescription();

            // Assert
            Assert.AreEqual(expected_Intro, actual_Intro);
            Assert.AreEqual(expected_Advanced, actual_Advanced);
        }

        [TestMethod]
        public void EnumToInt_Core()
        {
            // Arrange
            var intro = Module.Intro;
            var expected_int = 0;
            var expected_string = "0";
            var expected_Library = 99;

            // Act
            var actual_int = intro.ToInt();
            var actual_string = intro.ToIntString();
            var actual_Library = Module.Library.ToInt();

            // Assert
            Assert.AreEqual(expected_int, actual_int);
            Assert.AreEqual(expected_string, actual_string);
            Assert.AreEqual(expected_Library, actual_Library);
        }

        [TestMethod]
        public void ToEnum_Core()
        {
            // Arrange
            var expected = Module.Intro;
            Module? expected_null = null;

            // Act
            var actual_int = 0.ToEnum<Module>();
            var actual_string = "Intro".ToEnum<Module>();
            var actual_unknown1 = 10.ToEnum<Module>();
            var actual_unknown2 = "10".ToEnum<Module>();
            var actual_unknown3 = "Recess".ToEnum<Module>();

            // Assert
            Assert.AreEqual(true, actual_int.HasValue);
            Assert.AreEqual(expected, actual_int);
            Assert.AreEqual(expected, actual_string);
            Assert.AreEqual(expected_null, actual_unknown1);
            Assert.AreEqual(expected_null, actual_unknown2);
            Assert.AreEqual(expected_null, actual_unknown3);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ToEnumError_Core()
        {
            var actual = "Intro".ToEnum<Decimal>();
            Assert.Fail("Should throw exception");
        }
    }
}
