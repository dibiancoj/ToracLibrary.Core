using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.ExtensionMethods
{

    /// <summary>
    /// Extension methods for date time variables
    /// </summary>
    public static class DateTimeExtensionMethods
    {

        /// <summary>
        /// Evalulate if the date time is between the start and end date. Essentially ValueToEvaluate >= BeginningStartDate && ValueToEvaluate < EndStartDate
        /// </summary>
        /// <param name="valueToEvaluate">Value to determine if its between the 2 time periods specified in BeginningStartDate and EndStartDate</param>
        /// <param name="beginningStartDate">Start date range</param>
        /// <param name="endStartDate">End date to range</param>
        /// <returns>True if the ValueToEvaluate is between the specified date range</returns>
        public static bool IsBetween(this DateTime valueToEvaluate, DateTime beginningStartDate, DateTime endStartDate)
        {
            //make sure the start is before the end
            if (endStartDate < beginningStartDate)
            {
                throw new ArgumentOutOfRangeException(nameof(beginningStartDate), "Start Date Is After End Date");
            }

            //is the value between the 2 dates passed in
            return valueToEvaluate >= beginningStartDate && valueToEvaluate < endStartDate;
        }

    }

}
