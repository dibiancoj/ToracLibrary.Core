using Library.Core.DateTimeHelpers;
using Library.Core.DateTimeHelpers.BusinessHours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Core.Tests.DateTimeHelpers
{

    /// <summary>
    /// Unit test to test date specific functionality
    /// </summary>
    public class DateTimeTest
    {

        #region Months Between 2 Dates

        [Fact(DisplayName = "Test how many months between 2 dates")]
        public void HowManyMonthsBetween2DatesTest1()
        {
            Assert.Equal(1.129032258064516, DateTimeCalculations.HowManyMonthsBetween2Dates(new DateTime(2012, 12, 1), new DateTime(2013, 1, 5)));
            Assert.Equal(1, DateTimeCalculations.HowManyMonthsBetween2Dates(new DateTime(2012, 12, 1), new DateTime(2013, 1, 1)));
            Assert.Equal(12, DateTimeCalculations.HowManyMonthsBetween2Dates(new DateTime(2012, 12, 1), new DateTime(2013, 12, 1)));

            //make sure this results in an error
            Assert.Throws<ArgumentOutOfRangeException>(() => DateTimeCalculations.HowManyMonthsBetween2Dates(new DateTime(2015, 1, 2), new DateTime(2015, 1, 1)));
        }

        #endregion

        #region Business Hours

        #region Constants

        /// <summary>
        /// Start hour of the work day
        /// </summary>
        private const int StartWorkDayHour = 8;

        /// <summary>
        /// End hour of the work day
        /// </summary>
        private const int EndWorkDayHour = 17;

        /// <summary>
        /// How long is a normal work day
        /// </summary>
        private const int HowLongIsWorkDayInHour = 9;

        #endregion

        /// <summary>
        /// Builds holidays
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<BusinessHourHoliday> BuildHolidaysLazy()
        {
            //just return 1 holiday
            yield return new BusinessHourHoliday(new DateTime(2014, 7, 4), new DateTime(2014, 7, 5));
        }

        [Fact(DisplayName = "Test method for business hours, going forwards")]
        public void BusinessHoursPositiveNumbersTest1()
        {
            //grab the holidays to test with
            var holidaysToTestWith = BuildHolidaysLazy().ToArray();

            //test 1 hour
            Assert.Equal(1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 8, 0, 0), new DateTime(2014, 1, 6, 9, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test 1 hour with 30: after destination
            Assert.Equal(1.5, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 8, 0, 0), new DateTime(2014, 1, 6, 9, 30, 0), StartWorkDayHour, EndWorkDayHour));

            //test 1 full day
            Assert.Equal(HowLongIsWorkDayInHour, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 8, 0, 0), new DateTime(2014, 1, 6, 17, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test 1 full day after business hours have ended
            Assert.Equal(HowLongIsWorkDayInHour, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 8, 0, 0), new DateTime(2014, 1, 6, 20, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test 2 full day after business hours have ended
            Assert.Equal(HowLongIsWorkDayInHour * 2, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 8, 0, 0), new DateTime(2014, 1, 7, 20, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test weekends
            Assert.Equal(HowLongIsWorkDayInHour, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 10, 8, 0, 0), new DateTime(2014, 1, 11, 20, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test weekends (sat)
            Assert.Equal(HowLongIsWorkDayInHour, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 10, 8, 0, 0), new DateTime(2014, 1, 12, 20, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test weekends (sun)
            Assert.Equal(HowLongIsWorkDayInHour * 2, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 10, 8, 0, 0), new DateTime(2014, 1, 13, 20, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test holidays (no days)
            Assert.Equal(0, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 4, 8, 0, 0), new DateTime(2014, 7, 5, 8, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (1 day)
            Assert.Equal(HowLongIsWorkDayInHour, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 3, 8, 0, 0), new DateTime(2014, 7, 5, 8, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (after hour on last day day)
            Assert.Equal(0, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 3, EndWorkDayHour + 1, 0, 0), new DateTime(2014, 7, 5, 8, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (holiday on friday with weekend)
            Assert.Equal(HowLongIsWorkDayInHour - 1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 3, 9, 0, 0), new DateTime(2014, 7, 6, 8, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (holiday on friday with weekend and then next week)
            Assert.Equal(HowLongIsWorkDayInHour * 2, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 3, StartWorkDayHour, 0, 0), new DateTime(2014, 7, 8, StartWorkDayHour, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

        }

        [Fact(DisplayName = "Test the business hours, going backwards")]
        public void BusinessHoursNegativeNumbersTest1()
        {
            //grab the holidays to test with
            var holidaysToTestWith = BuildHolidaysLazy().ToArray();

            //test 1 hour
            Assert.Equal(1 * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 9, 0, 0), new DateTime(2014, 1, 6, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test 1 hour with 30: after destination
            Assert.Equal(1.5 * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 9, 30, 0), new DateTime(2014, 1, 6, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test 1 full day
            Assert.Equal(HowLongIsWorkDayInHour * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 17, 0, 0), new DateTime(2014, 1, 6, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test 1 full day after business hours have ended
            Assert.Equal(HowLongIsWorkDayInHour * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 6, 20, 0, 0), new DateTime(2014, 1, 6, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test 2 full day after business hours have ended
            Assert.Equal((HowLongIsWorkDayInHour * 2) * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 7, 20, 0, 0), new DateTime(2014, 1, 6, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test weekends
            Assert.Equal(HowLongIsWorkDayInHour * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 11, 20, 0, 0), new DateTime(2014, 1, 10, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test weekends (sat)
            Assert.Equal(HowLongIsWorkDayInHour * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 12, 20, 0, 0), new DateTime(2014, 1, 10, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test weekends (sun)
            Assert.Equal((HowLongIsWorkDayInHour * 2) * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 1, 13, 20, 0, 0), new DateTime(2014, 1, 10, 8, 0, 0), StartWorkDayHour, EndWorkDayHour));

            //test holidays (no days)
            Assert.Equal(0 * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 5, 8, 0, 0), new DateTime(2014, 7, 4, 8, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (1 day)
            Assert.Equal(HowLongIsWorkDayInHour * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 5, 8, 0, 0), new DateTime(2014, 7, 3, 8, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (after hour on last day day)
            Assert.Equal(0 * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 5, 8, 0, 0), new DateTime(2014, 7, 3, EndWorkDayHour + 1, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (holiday on friday with weekend)
            Assert.Equal((HowLongIsWorkDayInHour - 1) * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 6, 8, 0, 0), new DateTime(2014, 7, 3, 9, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));

            //test holidays (holiday on friday with weekend and then next week)
            Assert.Equal((HowLongIsWorkDayInHour * 2) * -1, BusinessHoursCalculator.BusinessHoursBetweenDates(new DateTime(2014, 7, 8, StartWorkDayHour, 0, 0), new DateTime(2014, 7, 3, StartWorkDayHour, 0, 0), StartWorkDayHour, EndWorkDayHour, holidaysToTestWith));
        }

        #endregion

        #region Quarter Time Period

        //1st quarter
        [InlineData(2014, 1, 1)]
        [InlineData(2014, 2, 1)]
        [InlineData(2014, 3, 1)]

        //2nd quarter
        [InlineData(2014, 4, 2)]
        [InlineData(2014, 5, 2)]
        [InlineData(2014, 6, 2)]

        //3rd quarter
        [InlineData(2014, 7, 3)]
        [InlineData(2014, 8, 3)]
        [InlineData(2014, 9, 3)]

        //4th quarter
        [InlineData(2014, 10, 4)]
        [InlineData(2014, 11, 4)]
        [InlineData(2014, 12, 4)]
        [Theory(DisplayName = "Test which quarter this date is in")]
        public void QuarterTimePeriodTest1(int year, int month, int expectedQuarter)
        {
            Assert.Equal(expectedQuarter, DateTimeCalculations.QuarterIsInTimePeriod(new DateTime(year, month, 1)));
        }

        #endregion

        #region Age

        [InlineData(-10, 0, 10)]
        [InlineData(-10, 1, 9)]
        [InlineData(-10, -1, 10)]
        [InlineData(-20, -1, 20)]
        [InlineData(-25, -1, 25)]
        [Theory(DisplayName = "Test Age")]
        public void AgeTest1(int addYears, int addDays, int expectedAge)
        {
            Assert.Equal(expectedAge, DateTimeCalculations.CalculateAge(DateTime.Now.AddYears(addYears).AddDays(addDays)));
        }

        #endregion

    }

}
