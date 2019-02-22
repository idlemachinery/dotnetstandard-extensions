using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;

namespace IdleMachinery.Extensions.Core.Tests
{
    [TestClass]
    public class RegexTests
    {
        [TestMethod]
        public void RegexEmail_Core()
        {
            // Arrange            
            var pattern = RegexPatterns.Email;
            var regex = new Regex(pattern);

            // Assert

            // bad emails     
            Assert.AreEqual(false, regex.IsMatch("joe")); // should fail
            Assert.AreEqual(false, regex.IsMatch("joe@home")); // should fail
            Assert.AreEqual(false, regex.IsMatch("a@b.c")); // should fail because .c is only one character but must be 2-4 characters
            Assert.AreEqual(false, regex.IsMatch("joe-bob[at]home.com")); // should fail because [at] is not valid
            Assert.AreEqual(false, regex.IsMatch("joe@his.home.place")); // should fail because place is 5 characters but must be 2-4 characters
            Assert.AreEqual(false, regex.IsMatch("joe.@bob.com")); // should fail because there is a dot at the end of the local-part
            Assert.AreEqual(false, regex.IsMatch(".joe@bob.com")); // should fail because there is a dot at the beginning of the local-part
            Assert.AreEqual(false, regex.IsMatch("john..doe@bob.com")); // should fail because there are two dots in the local-part
            Assert.AreEqual(false, regex.IsMatch("john.doe@bob..com")); // should fail because there are two dots in the domain
            Assert.AreEqual(false, regex.IsMatch("joe<>bob@bob.come")); // should fail because <> are not valid
            Assert.AreEqual(false, regex.IsMatch("joe@his.home.com.")); // should fail because it can't end with a period
            Assert.AreEqual(false, regex.IsMatch("a@10.1.100.1a"));  // Should fail because of the extra character
            //Assert.AreEqual(false, regex.IsMatch("joe@bob.com1")); // Should fail but doesn't

            // good emails
            Assert.AreEqual(true, regex.IsMatch("joe@home.org"));
            Assert.AreEqual(true, regex.IsMatch("joe@joebob.name"));
            Assert.AreEqual(true, regex.IsMatch("joe&bob@bob.com"));
            Assert.AreEqual(true, regex.IsMatch("~joe@bob.com"));
            Assert.AreEqual(true, regex.IsMatch("joe$@bob.com"));
            Assert.AreEqual(true, regex.IsMatch("joe+bob@bob.com"));
            Assert.AreEqual(true, regex.IsMatch("joe-bob@bob.com"));
            Assert.AreEqual(true, regex.IsMatch("joe_bob@bob.com"));
            Assert.AreEqual(true, regex.IsMatch("o'reilly@there.com"));
            Assert.AreEqual(true, regex.IsMatch("joe@home.com"));
            Assert.AreEqual(true, regex.IsMatch("joe.bob@home.com"));
            Assert.AreEqual(true, regex.IsMatch("joe@his.home.com"));
            Assert.AreEqual(true, regex.IsMatch("a@abc.org"));
            Assert.AreEqual(true, regex.IsMatch("a@192.168.0.1"));
            Assert.AreEqual(true, regex.IsMatch("a@10.1.100.1"));
        }

        [TestMethod]
        public void RegexPhone_Core()
        {
            // Arrange            
            var pattern = RegexPatterns.Phone;
            var regex = new Regex(pattern);

            // Assert

            // bad numbers
            Assert.AreEqual(false, regex.IsMatch("926 3 4"));
            Assert.AreEqual(false, regex.IsMatch("8 800 600-APPLE"));
            Assert.AreEqual(false, regex.IsMatch("not a phone number"));
            Assert.AreEqual(false, regex.IsMatch("-7(926)1234567"));

            // good numbers
            Assert.AreEqual(true, regex.IsMatch("+42 555.123.4567"));
            Assert.AreEqual(true, regex.IsMatch("+1-(800)-123-4567"));
            Assert.AreEqual(true, regex.IsMatch("+7 555 1234567"));
            Assert.AreEqual(true, regex.IsMatch("+15551234567"));
            Assert.AreEqual(true, regex.IsMatch("+7(926)1234567"));
            Assert.AreEqual(true, regex.IsMatch("1-234-567-8901 ext1234"));
            Assert.AreEqual(true, regex.IsMatch("(926) 1234567"));
            Assert.AreEqual(true, regex.IsMatch("+79261234567"));
            Assert.AreEqual(true, regex.IsMatch("926 1234567"));
            Assert.AreEqual(true, regex.IsMatch("9261234567"));
            Assert.AreEqual(true, regex.IsMatch("1234567"));
            Assert.AreEqual(true, regex.IsMatch("1-234-567-8901 ext. 1234"));
            Assert.AreEqual(true, regex.IsMatch("123-4567"));
            Assert.AreEqual(true, regex.IsMatch("123-89-01"));
            Assert.AreEqual(true, regex.IsMatch("495 1234567"));
            Assert.AreEqual(true, regex.IsMatch("469 123 45 67"));
            Assert.AreEqual(true, regex.IsMatch("89261234567"));
            Assert.AreEqual(true, regex.IsMatch("8 (926) 1234567"));
            Assert.AreEqual(true, regex.IsMatch("926.123.4567"));
            Assert.AreEqual(true, regex.IsMatch("415-555-1234"));
            Assert.AreEqual(true, regex.IsMatch("650-555-2345"));
            Assert.AreEqual(true, regex.IsMatch("(416)555-3456"));
            Assert.AreEqual(true, regex.IsMatch("(416)555-3456 x1234"));
            Assert.AreEqual(true, regex.IsMatch("202 555 4567"));
            Assert.AreEqual(true, regex.IsMatch("4035555678"));
            Assert.AreEqual(true, regex.IsMatch("1 416 555 9292"));
            Assert.AreEqual(true, regex.IsMatch("1 (800) 123-3455"));
        }

        [TestMethod]
        public void RegexWildcard_Core()
        {
            // Arrange
            var testString = "ate Ape are";
            var fullWildcard = new Wildcard("* are*");
            var partialWildcard = new Wildcard("a?e", RegexOptions.IgnoreCase, false, false);
            var expected_full_IsMatch = true;
            var expected_full_NumMatches = 1;
            var expected_part_IsMatch = true;
            var expected_part_NumMatches = 3;
            var expected_part_2ndMatch = "Ape";

            Console.WriteLine(fullWildcard.ToString());
            Console.WriteLine(partialWildcard.ToString());

            // Act
            var fullMatches = fullWildcard.Matches(testString);
            var actual_full_IsMatch = fullWildcard.IsMatch(testString);
            var actual_full_NumMatches = fullMatches.Count;

            var partialMatches = partialWildcard.Matches(testString);
            var actual_part_IsMatch = partialWildcard.IsMatch(testString);
            var actual_part_NumMatches = partialMatches.Count;
            var actual_part_2ndMatch = partialMatches[1].ToString();

            // Assert
            Assert.AreEqual(expected_full_IsMatch, actual_full_IsMatch);
            Assert.AreEqual(expected_full_NumMatches, actual_full_NumMatches);
            Assert.AreEqual(expected_part_IsMatch, actual_part_IsMatch);
            Assert.AreEqual(expected_part_NumMatches, actual_part_NumMatches);
            Assert.AreEqual(expected_part_2ndMatch, actual_part_2ndMatch);
        }
    }
}
