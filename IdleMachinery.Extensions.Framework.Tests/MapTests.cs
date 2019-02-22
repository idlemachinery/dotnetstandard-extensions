using System;
using IdleMachinery.Extensions.Mapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleMachinery.Extensions.Framework.Tests
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }

    public class UserModel
    {
        // Change this to FullName to break WithStringReflection
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
    }

    [TestClass]
    public class MapTests
    {
        [TestMethod]
        public void MapWithStringReflection()
        {
            var source = new User
            {
                FirstName = "Elton",
                LastName = "Stoneman",
                BirthDate = new DateTime(1980, 1, 1)
            };

            var map = new Map<User, UserModel>(source);
            map.Populate("DateOfBirth", "BirthDate")
                .Populate("Name", s => s.FirstName + " " + s.LastName)
               .Populate("Age", s => (int)DateTime.Today.Subtract(s.BirthDate).TotalDays / 365);

            var target = map.Target;

            var expected = DateTime.Now.Year - source.BirthDate.Year;

            Assert.AreEqual("Elton Stoneman", target.Name);
            Assert.AreEqual(source.BirthDate, target.DateOfBirth);
            Assert.AreEqual(expected, target.Age);
        }

        [TestMethod]
        public void MapWithExpressionReflection()
        {
            var source = new User
            {
                FirstName = "Elton",
                LastName = "Stoneman",
                BirthDate = new DateTime(1980, 1, 1)
            };

            var map = new Map<User, UserModel>(source);
            map.Populate(t => t.DateOfBirth, s => s.BirthDate)
               .Populate(t => t.Name, s => s.FirstName + " " + s.LastName)
               .Populate(t => t.Age, s => (int)DateTime.Today.Subtract(s.BirthDate).TotalDays / 365);

            var target = map.Target;

            var expected = DateTime.Now.Year - source.BirthDate.Year;

            Assert.AreEqual("Elton Stoneman", target.Name);
            Assert.AreEqual(source.BirthDate, target.DateOfBirth);
            Assert.AreEqual(expected, target.Age);
        }
    }
}
