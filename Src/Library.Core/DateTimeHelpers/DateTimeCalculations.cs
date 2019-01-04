using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.DateTimeHelpers
{
    /// <summary>
    /// Calculations For Date Times
    /// </summary>
    public static class DateTimeCalculations
    {

        /// <summary>
        /// Get the number of months between 2 dates. Timespan won't give you months. You can use a timespan to get the number of days and assume its 30 days but that is not 100% accurate
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>How Many Months Between 2 Dates</returns>
        public static double HowManyMonthsBetween2Dates(DateTime startDate, DateTime endDate)
        {
            //Excel Formula To Validate
            //=(YEAR(EndDateCell)-YEAR(StartDateCell))*12+MONTH(EndDateCell)-MONTH(StartDateCell)

            //for the remainder 
            //=DAY(EndDateCell) (number of days in end date)
            //=DAY(DATE(YEAR(EndDateCell),MONTH(EndDateCell)+1,1)-1) (number of days in month)
            //=D19/D20 (Remainder Calculation)

            //validate that the start date is older than the end date
            if (startDate > endDate)
            {
                throw new ArgumentOutOfRangeException(nameof(startDate), "Start Date Can't Be After End Date");
            }

            //get how many years between the 2
            int yearDifference = endDate.Year - startDate.Year;

            //get the month difference between the 2
            int monthDifference = endDate.Month - startDate.Month;

            //multiple years by 12 months then add the month difference
            double workingFigure = (yearDifference * 12) + monthDifference;

            //add the number of months then the remainder of days (need to convert it to a double)
            //we subtract 1 because the 1st day of the month is essentially 0 remainder
            return workingFigure + (((double)endDate.Day - 1) / DateTime.DaysInMonth(endDate.Year, endDate.Month));
        }

        /// <summary>
        /// Calculate a persons age
        /// </summary>
        /// <param name="dateOfBirth">Person's date of birth. We will calculate age from this date</param>
        /// <returns>What is the current age of the person</returns>
        public static int CalculateAge(DateTime dateOfBirth)
        {
            //grab the date today
            var today = DateTime.Today;

            //grab the date of birth date
            var workingDateOfBirth = dateOfBirth.Date;

            //subtract the 2 years
            int ageInYears = today.Year - workingDateOfBirth.Year;

            //if today is less then the current year, then subtract 1 year because it isn't there birth date yet
            if (today < workingDateOfBirth.AddYears(ageInYears))
            {
                //subtract 1 year
                ageInYears--;
            }

            //return the age
            return ageInYears;
        }

        /// <summary>
        /// Figure out which quarter this time period falls in
        /// </summary>
        /// <param name="whichQuarterIsDateTimeIn">Date time to figure out which quarter this falls in</param>
        /// <returns>Which Quarter 1 through 4</returns>
        public static int QuarterIsInTimePeriod(DateTime whichQuarterIsDateTimeIn)
        {
            //determine which quarter by the month
            switch (whichQuarterIsDateTimeIn.Month)
            {
                case 1:
                case 2:
                case 3:
                    return 1;

                case 4:
                case 5:
                case 6:
                    return 2;

                case 7:
                case 8:
                case 9:
                    return 3;

                case 10:
                case 11:
                case 12:
                    return 4;

                default:
                    throw new ArgumentOutOfRangeException("Month Of WhichQuarterIsDateTimeIn Can't Be Found In Quarter Lookup");
            }
        }

    }

}
