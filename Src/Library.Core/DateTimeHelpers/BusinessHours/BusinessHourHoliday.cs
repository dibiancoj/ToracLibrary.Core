using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.DateTimeHelpers.BusinessHours
{

    /// <summary>
    /// Holds The Records For Each Holiday
    /// </summary>
    /// <remarks>Class Is Immutable</remarks>
    public class BusinessHourHoliday
    {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="whatIsTheStartDateOfTheHoliday">Start Date Time Of The Holiday.</param>
        /// <param name="whatIsTheEndDateOfTheHoliday">End Date Time Of The Holiday</param>
        public BusinessHourHoliday(DateTime whatIsTheStartDateOfTheHoliday, DateTime whatIsTheEndDateOfTheHoliday)
        {
            //set the variables
            StartDateOfHoliday = whatIsTheStartDateOfTheHoliday;
            EndDateOfHoliday = whatIsTheEndDateOfTheHoliday;
        }

        #endregion

        #region Readonly Properties

        /// <summary>
        /// Start Date Of The Holiday
        /// </summary>
        public DateTime StartDateOfHoliday { get; }

        /// <summary>
        /// End Date Of The Holiday
        /// </summary>
        public DateTime EndDateOfHoliday { get; }

        #endregion

    }

}
