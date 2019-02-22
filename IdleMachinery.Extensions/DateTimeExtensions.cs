using System.Collections.Generic;

namespace System
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Retrieves the DateTime for the first day of the year.
        /// </summary>
        /// <param name="date">An instance in time.</param>
        /// <returns></returns>
        public static DateTime StartOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 1, 1);
        }

        /// <summary>
        /// Retrieves the DateTime for the last day of the year.
        /// </summary>
        /// <param name="date">An instance in time.</param>
        /// <returns></returns>
        public static DateTime EndOfYear(this DateTime date)
        {
            return new DateTime(date.Year, 12, 31);
        }

        /// <summary>
        /// Retrieves the DateTime for the first day of a month specified by a DateTime instance.
        /// </summary>
        /// <param name="date">An instance in time.</param>
        /// <returns></returns>
        public static DateTime StartOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        /// <summary>
        /// Retrieves the DateTime for the last day of a month specified by a DateTime instance.
        /// </summary>
        /// <param name="date">An instance in time.</param>
        /// <returns></returns>
        public static DateTime EndOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        // TODO - document
        public static DateTime SameDayLastYear(this DateTime date)
        {
            // always subtract 364 days (7 days x 52 weeks) to get same day
            //  NOTE:  leap year has no affect on the weekday.  
            return date.AddDays(-364);
        }
               
        // TODO - document
        public static int GetQuarter(this DateTime date)
        {
            if (date.Month >= 4 && date.Month <= 6)
                return 1;
            else if (date.Month >= 7 && date.Month <= 9)
                return 2;
            else if (date.Month >= 10 && date.Month <= 12)
                return 3;
            else
                return 4;
        }

        // TODO - document
        public static bool IsLeapYear(this DateTime date)
        {
            //  How to determine whether a year is a leap year
            //  To determine whether a year is a leap year, follow these steps: 
            //      1.If the year is evenly divisible by 4, go to step 2.Otherwise, go to step 5.
            //      2.If the year is evenly divisible by 100, go to step 3. Otherwise, go to step 4. 
            //      3.If the year is evenly divisible by 400, go to step 4.Otherwise, go to step 5.
            //      4.The year is a leap year (it has 366 days). 
            //      5.The year is not a leap year(it has 365 days).

            // Formula to determine whether a year is a leap year
            //  Use the following formula to determine whether the year number that is entered 
            //  into a cell(in this example, cell A1) is a leap year: 
            // = IF(OR(MOD(A1, 400) = 0, AND(MOD(A1, 4) = 0, MOD(A1, 100) <> 0)), "Leap Year", "NOT a Leap Year")
            
            if (date.Year % 4 == 0)
            {
                if (date.Year % 100 == 0)
                {
                    if (date.Year % 400 == 0)
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            return false;
        }

        // TODO - document
        public static string ToSQLDateTimeString(this DateTime date)
        {            
            return date.ToString("yyyy-MM-dd HH:mm:ss.FFF");
        }

        // TODO - document
        public static string ToSQLDateTimeString(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToSQLDateTimeString() : string.Empty;
        }

        // TODO - document
        public static string ToSQLDateString(this DateTime date)
        {
            return date.ToString("yyyy-MM-dd");
        }

        // TODO - document
        public static string ToSQLDateString(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToSQLDateString() : string.Empty;
        }

        // TODO - document
        public static string ToShortString(this DateTime date)
        {
            return date.ToShortDateString() + " " + date.ToShortTimeString();
        }

        // TODO - document
        public static DateTime ToTimeZone(this DateTime date, string timeZoneId)
        {
            DateTime newTime = date;
            try
            {
                TimeZoneInfo timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                newTime = TimeZoneInfo.ConvertTimeFromUtc(date, timeZone);
                Console.WriteLine("The date and time are {0} {1}.",
                                  newTime,
                                  timeZone.IsDaylightSavingTime(newTime) ?
                                          timeZone.DaylightName : timeZone.StandardName);
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine("The registry does not define the Time zone. [" + timeZoneId + "]");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine("Registry data on the Time zone has been corrupted. [" + timeZoneId + "]");
            }
            return newTime;
        }

        // TODO - document
        public static DateTime? ToTimeZone(this DateTime? date, string timeZoneId)
        {
            return date.HasValue ? date.Value.ToTimeZone(timeZoneId) : date;
        }

        // TODO - document
        public static IEnumerable<DateTime> EachDay(this DateTime from, DateTime thru)
        {
            var dates = new List<DateTime>();
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                //yield return day;
                dates.Add(day);
            return dates;
        }

        private static readonly Dictionary<string, string> Holidays =
            new Dictionary<string, string>(){
                { "1/1", "New Year's Day" },
                { "1/18", "Martin Luther King, Jr. Day" },
                { "2/15", "George Washington’s Birthday" },
                { "5/30", "Memorial Day" },
                { "7/4", "Independence Day" },
                { "9/5", "Labor Day" },
                { "10/10", "Columbus Day" },
                { "11/11", "Veterans Day" },
                { "11/24", "Thanksgiving Day" },
                { "12/25", "Christmas Day" }
            };

        // TODO - document
        public static string GetHoliday(this DateTime date, Dictionary<string, string> holidays = null)
        {
            if (holidays == null) holidays = Holidays;
            return holidays.ContainsKey(date.ToString("M/d")) ? holidays[date.ToString("M/d")] : "None";
        }

        // TODO - document
        public static bool IsHoliday(this DateTime date, Dictionary<string, string> holidays = null)
        {
            return date.GetHoliday(holidays) != "None";
        }
    }
}
