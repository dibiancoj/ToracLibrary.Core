using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.AccountingPeriods.Exceptions
{

    /// <summary>
    /// Holds the exception when the accounting period month does not match the expected format
    /// </summary>
    /// <remarks>Class Is Immutable</remarks>
    public class AccountingPeriodMonthOutOfRangeException : Exception
    {

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="monthThatFailedValidationToSet">Month Value That Failed Validation</param>
        public AccountingPeriodMonthOutOfRangeException(int monthThatFailedValidationToSet)
        {
            //set the property
            MonthThatFailedValidation = monthThatFailedValidationToSet;
        }

        #endregion

        #region Readonly Properties

        /// <summary>
        /// Month value that failed validation
        /// </summary>
        public int MonthThatFailedValidation { get; }

        #endregion

        #region Overload Methods

        /// <summary>
        /// Override the ToString() method for this custom exception
        /// </summary>
        /// <returns>string output to display</returns>
        public override string ToString()
        {
            return "Accounting Period Month Is Not In The Correct Format. Year Should Be MM. Month That Passed Validation Is = " + MonthThatFailedValidation;
        }

        #endregion

    }

}
