using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleMachinery.Extensions.Framework.Tests
{
    [TestClass]
    public class NumberExtensionsTests
    {
        [TestMethod]
        public void IsNumericDatatype()
        {
            // Arrange
            object numObject = 12;
            var num = 12;
            decimal dec = 13.5M;
            var str = "";
            Type type = typeof(object);
            var numType = typeof(byte);

            // Assert
            Assert.AreEqual(true, numObject.IsNumericDatatype());
            Assert.AreEqual(true, num.IsNumericDatatype());
            Assert.AreEqual(true, dec.IsNumericDatatype());
            Assert.AreEqual(false, str.IsNumericDatatype());
            Assert.AreEqual(false, type.IsNumericDatatype());
            Assert.AreEqual(true, numType.IsNumericDatatype());
        }

        [TestMethod]
        public void ToBooleanAndConversions()
        {
            // Arrange           
            bool? Null = null;
            var trueStrings = new[] { "T", "True", "y", "YeS", "1" };
            var falseStrings = new[] { "f", "FaLSE", "N", "no", "0" };
            var nonBoolString = "sdfgsdg";

            // Assert
            foreach (var boolStr in trueStrings)
            {
                Assert.AreEqual(true, boolStr.ToBoolean());
            }
            foreach (var boolStr in falseStrings)
            {
                Assert.AreEqual(false, boolStr.ToBoolean());
            }
            Assert.AreEqual(null, nonBoolString.ToBoolean());

            Assert.AreEqual("Y", true.ToYN());
            Assert.AreEqual("N", false.ToYN());
            Assert.AreEqual(string.Empty, Null.ToYN());
            Assert.AreEqual("Yes", true.ToYesNo());
            Assert.AreEqual("No", false.ToYesNo());
            Assert.AreEqual(string.Empty, Null.ToYesNo());
            Assert.AreEqual("True", true.ToTrueFalse());
            Assert.AreEqual("False", false.ToTrueFalse());
            Assert.AreEqual(string.Empty, Null.ToTrueFalse());
            Assert.AreEqual("T", true.ToTF());
            Assert.AreEqual("F", false.ToTF());
            Assert.AreEqual(string.Empty, Null.ToTF());
            Assert.AreEqual("1", true.To1or0());
            Assert.AreEqual("0", false.To1or0());
            Assert.AreEqual(string.Empty, Null.To1or0());
            Assert.AreEqual(1, true.ToInt());
            Assert.AreEqual(0, false.ToInt());
            Assert.AreEqual(null, Null.ToInt());
            Assert.AreEqual((byte)1, true.ToByte());
            Assert.AreEqual((byte)0, false.ToByte());
            Assert.AreEqual(null, Null.ToByte());
        }
    }
}
