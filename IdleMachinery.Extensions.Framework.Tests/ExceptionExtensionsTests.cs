﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleMachinery.Extensions.Framework.Tests
{
    [TestClass]
    public class ExceptionExtensionsTests
    {
        public double Divide(int amount, int by)
        {
            try
            {
                return amount / by;
            }
            catch (Exception ex)
            {
                var invalidOpEx = new InvalidOperationException("Invalid operation", ex);
                var message = string.Format("Divide failed - amount: {0}, by: {1}", amount, by);
                throw new ApplicationException(message, invalidOpEx);
            }
        }

        [TestMethod]
        public void DivideBy1()
        {
            // Arrange
            var expected = 10;

            // Act
            var actual = Divide(10, 1);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ExceptionFullMessage()
        {
            // Arrange
            var nl = Environment.NewLine;
            var expected =
                $"Divide failed - amount: 10, by: 0{nl}" +
                $"Invalid operation{nl}" +
                $"Attempted to divide by zero.{nl}";
                               
            try
            {
                // Act
                Divide(10, 0);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                var actual = ex.FullMessage();

                //Console.WriteLine(ex.Message);
                //Console.WriteLine(ex.ToString());
                Console.WriteLine(actual);

                //Assert
                Assert.AreEqual(expected, actual);
                Assert.IsInstanceOfType(ex, typeof(ApplicationException));
            }
        }

        [TestMethod]
        public void HasInnerException()
        {
            // Arrange            
            var expected = true;

            try
            {
                // Act
                Divide(10, 0);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                var actual = ex.HasInnerException();

                //Assert
                Assert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void GetExceptionStack()
        {
            // Arrange            
            var expected = 3;

            try
            {
                // Act
                Divide(10, 0);
                Assert.Fail("Should throw exception");
            }
            catch (Exception ex)
            {
                var exStack = ex.GetExceptionStack().ToList();
                var actual = exStack.Count;

                //Assert
                Assert.AreEqual(expected, actual);
                Assert.IsInstanceOfType(exStack[0], typeof(ApplicationException));
            }
        }
    }
}
