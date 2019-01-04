using Library.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Core.DateTimeHelpers.BusinessHours
{

    /// <summary>
    /// Calculates Business Hours Between 2 Dates
    /// </summary>
    public static class BusinessHoursCalculator
    {

        #region Public Methods

        /// <summary>
        /// Calculates The Number Of Business Hours Between 2 Dates.
        /// </summary>
        /// <param name="startDate">Start Date To Calculate From</param>
        /// <param name="endDate">End Date To Calculate To</param>
        /// <param name="businessDayStartHour">Hour Of When The Business Day Starts (24 Hour Clock - ie. 1pm is 13)</param>
        /// <param name="businessDayEndHour">Hour Of When The Business Day Ends (24 Hour Clock - ie. 1pm is 13)</param>
        /// <returns>Number Of Business Hours. Supports Going Backwards</returns>
        public static double BusinessHoursBetweenDates(DateTime startDate, DateTime endDate, int businessDayStartHour, int businessDayEndHour)
        {
            //use the helper method
            return BusinessHoursBetweenDatesHelper(startDate, endDate, businessDayStartHour, businessDayEndHour, null);
        }

        /// <summary>
        /// Calculates The Number Of Business Hours Between 2 Dates When You Want To Account For Holidays
        /// </summary>
        /// <param name="startDate">Start Date To Calculate From</param>
        /// <param name="endDate">End Date To Calculate To</param>
        /// <param name="businessDayStartHour">Hour Of When The Business Day Starts (24 Hour Clock - ie. 1pm is 13)</param>
        /// <param name="BusinessDayEndHour">Hour Of When The Business Day Ends (24 Hour Clock - ie. 1pm is 13)</param>
        /// <param name="holidayListing">Holiday Listing. This Works With Only Dates. Don't Specify July 4th at 4pm. There Is A Holiday Length For Half Days</param>
        /// <returns>Number Of Business Hours. Supports Going Backwards</returns>
        public static double BusinessHoursBetweenDates(DateTime startDate, DateTime endDate, int businessDayStartHour, int businessDayEndHour, IEnumerable<BusinessHourHoliday> holidayListing)
        {
            //use the helper method
            return BusinessHoursBetweenDatesHelper(startDate, endDate, businessDayStartHour, businessDayEndHour, holidayListing);
        }

        #endregion

        #region Private Static Helper Methods

        /// <summary>
        /// Calculates The Number Of Business Hours Between 2 Dates
        /// </summary>
        /// <param name="startDate">Start Date To Calculate From</param>
        /// <param name="endDate">End Date To Calculate To</param>
        /// <param name="businessDayStartHour">Hour Of When The Business Day Starts (24 Hour Clock - ie. 1pm is 13)</param>
        /// <param name="businessDayEndHour">Hour Of When The Business Day Ends (24 Hour Clock - ie. 1pm is 13)</param>
        /// <param name="holidayListing">Holiday Listing. This Works With Only Dates. Don't Specify July 4th at 4pm. There Is A Holiday Length For Half Days</param>
        /// <returns>Number Of Business Hours. Supports Going Backwards</returns>
        private static double BusinessHoursBetweenDatesHelper(DateTime startDate, DateTime endDate, int businessDayStartHour, int businessDayEndHour, IEnumerable<BusinessHourHoliday> holidayListing)
        {
            //make sure the start hour and end hour are between and 1 and 24
            if (!HourPassesValidation24HourValue(businessDayStartHour))
            {
                //start hour is not between 1 and 24
                throw new ArgumentOutOfRangeException(nameof(businessDayStartHour), "Start Hour Must Be Between 0-24 (Hour Value In A Day)");
            }

            //make sure the start hour and end hour are between and 1 and 24
            if (!HourPassesValidation24HourValue(businessDayEndHour))
            {
                //end hour is not between 1 and 24
                throw new ArgumentOutOfRangeException(nameof(businessDayEndHour), "End Hour Must Be Between 0-24 (Hour Value In A Day)");
            }

            //make sure the end hour is not earlier then the start hour
            if (businessDayEndHour < businessDayStartHour)
            {
                //throw an out of range argument exception because the end of the day needs to be after the start of the day
                throw new ArgumentOutOfRangeException(nameof(businessDayEndHour), "Can't Be Before " + nameof(businessDayStartHour));
            }

            //are we going backwards? (start date is after end date)
            bool areWeGoingBackwards = false;

            //holds the start date so we can always go forwards
            DateTime startDateConverted = startDate;

            //holds the end date so we can always go forwards
            DateTime endDateConverted = endDate;

            //holds the trim down version of the holiday list for this period (using an array to speed it up - instead of IEnumerable)
            BusinessHourHoliday[] holidayListingForThisPeriod = null;

            //let's check if we need to go backwards
            if (startDateConverted > endDateConverted)
            {
                //we need to go backwards flip the flag and the dates
                areWeGoingBackwards = true;

                //now flip the dates
                startDateConverted = endDate;
                endDateConverted = startDate;
            }

            //do we have any holidays? if so, grab just the holidays for this period
            if (holidayListing.AnyWithNullCheck())
            {
                //go grab the holidays for this period
                holidayListingForThisPeriod = holidayListing.Where(x => x.StartDateOfHoliday.Date >= startDateConverted.Date &&
                                                                        x.EndDateOfHoliday.Date <= endDateConverted.Date).ToArray();
            }

            //holds the working date which we will keep incrementing
            DateTime workingDate = startDateConverted;

            //holds the hour count that we will return
            double workBusinessHourCount = 0;

            //loop until we get the end of the time
            while (workingDate < endDateConverted)
            {
                //is it the weekend
                if (workingDate.DayOfWeek == DayOfWeek.Saturday || workingDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    //its the weekend...just fast foward to the Monday
                    while (workingDate.DayOfWeek == DayOfWeek.Saturday || workingDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        //fast foward to the next day
                        workingDate = GetNextDayAtStartOfBusinessDay(workingDate, businessDayStartHour);
                    }
                }

                //is it a holiday
                else if (holidayListingForThisPeriod.AnyWithNullCheck(x => workingDate.IsBetween(x.StartDateOfHoliday, x.EndDateOfHoliday)))
                {
                    //it's a working holiday...so don't increment it and increase the working date
                    workingDate = workingDate.AddHours(1);
                }

                //is it a work hour (between 8 and 7 pm)
                else if (workingDate.Hour >= businessDayStartHour && workingDate.Hour < businessDayEndHour)
                {
                    //are we less then an hour from the end
                    double WorkingDateFromEnd = endDateConverted.Subtract(workingDate).TotalHours;

                    //is it a full hour?
                    if (WorkingDateFromEnd >= 1)
                    {
                        //we are a full hour
                        workBusinessHourCount += 1;
                    }
                    else
                    {
                        //we are less then an hour so increment the different
                        workBusinessHourCount += WorkingDateFromEnd;
                    }

                    //add an hour to the working date
                    workingDate = workingDate.AddHours(1);
                }
                else
                {
                    //we are at a non business hour...let's fast foward to the next day at 8am
                    workingDate = GetNextDayAtStartOfBusinessDay(workingDate, businessDayStartHour);
                }
            }

            //if we are going to backwards then we want to make it a negative number
            if (areWeGoingBackwards)
            {
                //we need to flip the sign because we are going backwards
                workBusinessHourCount *= -1;
            }

            //now return the count
            return workBusinessHourCount;
        }

        /// <summary>
        /// Fast Forward The Date To The Next Day At The Start Of The Business Day
        /// </summary>
        /// <param name="dateToGetNextDay">Date To Get The Next Business Day</param>
        /// <param name="startBusinessHour">Start Business Hour</param>
        /// <returns>Will Return The Next Day Of The Date Passed In At The Start Business Hour Time</returns>
        private static DateTime GetNextDayAtStartOfBusinessDay(DateTime dateToGetNextDay, int startBusinessHour)
        {
            //add a day to the next day (we will set 8 am of this date below)
            DateTime nextDay = dateToGetNextDay.AddDays(1);

            //we are at a non business hour...let's fast foward to the next day at 8am
            return new DateTime(nextDay.Year, nextDay.Month, nextDay.Day, startBusinessHour, 0, 0);
        }

        /// <summary>
        /// Method validates that the hour passed in is between 0-24 (hours in a day)
        /// </summary>
        /// <param name="hourToCheck">Hour to check</param>
        /// <returns>If it passes validation</returns>
        private static bool HourPassesValidation24HourValue(int hourToCheck)
        {
            //is it between 0 and 24 hour.
            return hourToCheck >= 0 && hourToCheck < 24;
        }

        #endregion

    }

}
