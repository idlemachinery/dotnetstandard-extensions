using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace IdleMachinery.Extensions.Core.Tests
{
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void StartAndEndOfYear_Core()
        {
            // Arrange
            var date = new DateTime(2020, 2, 15);
            var expected_start = new DateTime(2020, 1, 1);
            var expected_end = new DateTime(2020, 12, 31);

            // Act
            var actual_start = date.StartOfYear();
            var actual_end = date.EndOfYear();

            // Assert
            Assert.AreEqual(expected_start, actual_start);
            Assert.AreEqual(expected_end, actual_end);
        }

        [TestMethod]
        public void StartAndEndOfMonth_Core()
        {
            // Arrange
            var date = new DateTime(2020, 2, 15);
            var expected_start = new DateTime(2020, 2, 1);
            var expected_end = new DateTime(2020, 2, 29);

            // Act
            var actual_start = date.StartOfMonth();
            var actual_end = date.EndOfMonth();

            // Assert
            Assert.AreEqual(expected_start, actual_start);
            Assert.AreEqual(expected_end, actual_end);
        }

        [TestMethod]
        public void SameDayLastYear_Core()
        {
            // Arrange
            var dates = new List<string>() { "2/28/1900", "3/1/1900", "2/29/2000", "3/1/2000", "2/28/2015", "3/1/2015", "2/29/2016", "3/1/2016", "2/28/2017", "3/1/2017" };
            var expected = new List<DateTime>()
            {
                new DateTime(1899, 3, 1),
                new DateTime(1899, 3, 2),
                new DateTime(1999, 3, 2),
                new DateTime(1999, 3, 3),
                new DateTime(2014, 3, 1),
                new DateTime(2014, 3, 2),
                new DateTime(2015, 3, 2),
                new DateTime(2015, 3, 3),
                new DateTime(2016, 3, 1),
                new DateTime(2016, 3, 2)
            };

            // Act
            var actual = new List<DateTime>();
            foreach (var dateStr in dates)
            {
                var date = DateTime.Parse(dateStr);
                Console.WriteLine("Original Date: " + date.ToLongDateString());
                Console.WriteLine("Is Leap Year: " + date.IsLeapYear().ToString());
                var sameDay = date.SameDayLastYear();
                Console.WriteLine("Last Year Date: " + sameDay.ToLongDateString());
                Console.WriteLine("--------------------------------------------");
                actual.Add(sameDay);
            }

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetQuarter_Core()
        {
            // Quarters start in April

            // Arrange
            var date = new DateTime(2020, 9, 15);
            var expected = 2;

            // Act
            var actual = date.GetQuarter();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsLeapYear_Core()
        {
            // Arrange
            // Act
            // Assert
            Assert.AreEqual(true, new DateTime(2000, 1, 1).IsLeapYear());
            Assert.AreEqual(false, new DateTime(2015, 1, 1).IsLeapYear());
            Assert.AreEqual(true, new DateTime(2016, 1, 1).IsLeapYear());
            Assert.AreEqual(false, new DateTime(2017, 1, 1).IsLeapYear());
            Assert.AreEqual(false, new DateTime(2018, 1, 1).IsLeapYear());
            Assert.AreEqual(false, new DateTime(2019, 1, 1).IsLeapYear());
            Assert.AreEqual(true, new DateTime(2020, 1, 1).IsLeapYear());
        }

        [TestMethod]
        public void ToSQLDateTimeString_Core()
        {
            // Arrange
            var date = new DateTime(2019, 1, 31, 14, 7, 5, 35);
            DateTime? date_null = null;
            var expected = "2019-01-31 14:07:05.035";
            var expected_null = string.Empty;

            // Act
            var actual = date.ToSQLDateTimeString();
            var actual_null = date_null.ToSQLDateTimeString();

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected_null, actual_null);

        }

        [TestMethod]
        public void ToSQLDateString_Core()
        {
            // Arrange
            var date = new DateTime(2019, 1, 31, 14, 7, 5, 35);
            DateTime? date_null = null;
            var expected = "2019-01-31";
            var expected_null = string.Empty;

            // Act
            var actual = date.ToSQLDateString();
            var actual_null = date_null.ToSQLDateString();

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected_null, actual_null);

        }

        [TestMethod]
        public void ToShortString_Core()
        {
            // Arrange
            var date = new DateTime(2019, 1, 31, 14, 7, 5, 35);
            var expected = "1/31/2019 2:07 PM";

            // Act
            var actual = date.ToShortString();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ToTimeZone_Core()
        {
            // Arrange
            var date = new DateTime(2019, 1, 31, 14, 7, 5, 35);
            DateTime? date_null = null;
            var expected = new DateTime(2019, 1, 31, 6, 7, 5, 35);
            DateTime? expected_null = null;
            var expected_unknown = date;

            // Act
            var actual = date.ToTimeZone("Pacific Standard Time");
            var actual_null = date_null.ToTimeZone("Pacific Standard Time");
            var actual_unknown = date.ToTimeZone("lkjsgkljs");

            Console.WriteLine("\nLIST OF TIMEZONE IDS");
            Console.WriteLine("--------------------------------------------");
            foreach (TimeZoneInfo z in TimeZoneInfo.GetSystemTimeZones())
                Console.WriteLine(z.Id);

            // Assert
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected_null, actual_null);
            Assert.AreEqual(expected_unknown, actual_unknown);
        }

        [TestMethod]
        public void EachDay_Core()
        {
            // Arrange
            var date = new DateTime(2019, 1, 15);
            var thru = new DateTime(2019, 1, 25);
            var expected = new List<DateTime>();
            for (int i = 15; i <= 25; i++)
            {
                expected.Add(new DateTime(2019, 1, i));
            }

            // Act
            var actual = date.EachDay(thru).ToList();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetHoliday_Core()
        {
            // Arrange
            var expected_mlk = "Martin Luther King, Jr. Day";
            var expected_labor_day = "Labor Day";
            var expected_veterans_day = "Veterans Day";
            var expected_april_fools = "None";
            var expected_non_holiday = "None";

            // Act
            var actual_mlk = new DateTime(2019, 1, 18).GetHoliday();
            var actual_labor_day = new DateTime(2019, 9, 5).GetHoliday();
            var actual_veterans_day = new DateTime(2019, 11, 11).GetHoliday();
            var actual_april_fools = new DateTime(2019, 4, 1).GetHoliday();
            var actual_non_holiday = new DateTime(2019, 8, 15).GetHoliday();

            // Assert
            Assert.AreEqual(true, new DateTime(2019, 2, 15).IsHoliday());
            Assert.AreEqual(expected_mlk, actual_mlk);
            Assert.AreEqual(expected_labor_day, actual_labor_day);
            Assert.AreEqual(expected_veterans_day, actual_veterans_day);
            Assert.AreEqual(expected_april_fools, actual_april_fools);
            Assert.AreEqual(expected_non_holiday, actual_non_holiday);
        }
    }
}
