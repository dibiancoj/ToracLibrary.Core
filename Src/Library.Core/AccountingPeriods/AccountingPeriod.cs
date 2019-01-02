using Library.Core.AccountingPeriods.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Library.Core.AccountingPeriods
{

    /// <summary>
    /// Holds a common class to build, use, and manipulate accounting periods. Accounting periods are YearMonth int's.
    /// </summary>
    /// <remarks>Class Is Immutable</remarks>
    public class AccountingPeriod
    {

        #region Constructor

        /// <summary>
        /// Constructor using a seperate month and year variables
        /// </summary>
        /// <param name="monthToSet">Month To Set.ie 1 = January, 2 = Feb.</param>
        /// <param name="yearToSet">Year To Set</param>
        public AccountingPeriod(int monthToSet, int yearToSet)
        {
            //go validate that the month is legit (will throw an error if it fails)
            ValidateMonth(monthToSet);

            //set the variables
            Month = monthToSet;
            Year = yearToSet;
        }

        /// <summary>
        /// Constructor using a YearMonth format to set the model from
        /// </summary>
        /// <param name="accountingPeriodToSet"></param>
        public AccountingPeriod(int accountingPeriodToSet)
        {
            //go validate the accounting period
            ValidateAccountingPeriod(accountingPeriodToSet);

            //we are all good set the variables (first push the int to a string
            var periodInStringFormat = accountingPeriodToSet.ToString().AsSpan();

            //grab the month now and set it
            Month = GetMonthFromAccountingPeriod(periodInStringFormat);

            //grab the year now and set it
            Year = GetYearFromAccountingPeriod(periodInStringFormat);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Month - ie 1 = January, 2 = Feb.
        /// </summary>
        public int Month { get; }

        /// <summary>
        /// Year
        /// </summary>
        public int Year { get; }

        #endregion

        #region Validation

        /// <summary>
        /// Validates that the accounting period in an integer is legal
        /// </summary>
        /// <param name="accountingPeriod">Accounting Period To Validate. Will Throw An Error If It Doesn't Match The Criteria</param>
        private static void ValidateAccountingPeriod(int accountingPeriod)
        {
            //throw the accounting period to a string so we don't have to cast it twice
            string AccountingPeriodToString = accountingPeriod.ToString(CultureInfo.InvariantCulture);

            //make sure it's 6 characters
            if (AccountingPeriodToString.Length != 6)
            {
                //not the correct length
                throw new AccountingPeriodYearOutOfRangeException(accountingPeriod);
            }

            //go validate that the month number is legit (we can't call period breakdown otherwise we will get a circular reference)
            ValidateMonth(GetMonthFromAccountingPeriod(AccountingPeriodToString));
        }

        /// <summary>
        /// Validates that the accounting period month passed in, is a valid number
        /// </summary>
        /// <param name="monthToTest">Month To Test</param>
        private static void ValidateMonth(int monthToTest)
        {
            //make sure the month are within the expected range
            if (monthToTest < 1 || monthToTest > 12)
            {
                //the month is either earlier than Jan or later then Dec.
                throw new AccountingPeriodMonthOutOfRangeException(monthToTest);
            }
        }

        #endregion

        #region Building An Accounting Period

        /// <summary>
        /// Gets the year from an accounting period
        /// </summary>
        /// <param name="accountingPeriodString">Accounting Period.ToString()</param>
        /// <returns>Year Part Of The Accounting Period String</returns>
        private static int GetYearFromAccountingPeriod(ReadOnlySpan<char> accountingPeriodString)
        {
            //grab the sub string then build the int and return it
            return int.Parse(accountingPeriodString.Slice(0, 4));
        }

        /// <summary>
        /// Gets the month from an accounting period
        /// </summary>
        /// <param name="accountingPeriodString">Accounting Period</param>
        /// <returns>Month Part Of The Accounting Period String</returns>
        private static int GetMonthFromAccountingPeriod(ReadOnlySpan<char> accountingPeriodString)
        {
            //grab the sub string then build the int and return it
            return int.Parse(accountingPeriodString.Slice(4, 2));
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Converts an accounting period to a date time object. Will set it to the first day of that month
        /// </summary>
        /// <param name="accountingPeriod">Accounting Period To Convert</param>
        /// <returns>Date Time Object</returns>
        public static DateTime PeriodToDateTime(int accountingPeriod)
        {
            //first validate the accounting period
            ValidateAccountingPeriod(accountingPeriod);

            //go get the break down of this accounting period
            var breakdown = new AccountingPeriod(accountingPeriod);

            //go convert it and return it
            return new DateTime(breakdown.Year, breakdown.Month, 1);
        }

        /// <summary>
        /// Converts a date to an accounting period object
        /// </summary>
        /// <param name="dateToConvert">Date to convert</param>
        /// <returns>Accounting Period</returns>
        public static int DateTimeToPeriod(DateTime dateToConvert)
        {
            return new AccountingPeriod(dateToConvert.Month, dateToConvert.Year).ToAccountingPeriod();
        }

        /// <summary>
        /// Pushes the current model to an integer accounting period
        /// </summary>
        /// <returns>accounting period in an integer format</returns>
        public int ToAccountingPeriod()
        {
            //we need to basically send it into the string builder as
            //YYYYmm
            return int.Parse($"{Year}{Month.ToString("D2")}");
        }

        #endregion

        #region Adding - Subtracting Periods

        /// <summary>
        /// Increments a period. Can add or subtract however many periods that is passed in
        /// </summary>
        /// <param name="accountingPeriod">Accounting period to add too</param>
        /// <param name="howManyPeriodsToAdd">How many periods to add. You can pass in -1 to subtract 1 period (will handle that)</param>
        /// <returns>New Period</returns>
        public static int IncrementPeriod(int accountingPeriod, int howManyPeriodsToAdd)
        {
            //first validate that the accounting period is legit (will throw an error if it fails)
            ValidateAccountingPeriod(accountingPeriod);

            //let's split this out first
            var splitOutPeriod = new AccountingPeriod(accountingPeriod);

            //are we adding or subtracting
            if (howManyPeriodsToAdd > 0)
            {
                //use the add period method
                return AddPeriod(splitOutPeriod, howManyPeriodsToAdd).ToAccountingPeriod();
            }

            //are we subtracting periods?
            if (howManyPeriodsToAdd < 0)
            {
                //we are subtracting
                return SubtractPeriod(splitOutPeriod, howManyPeriodsToAdd).ToAccountingPeriod();
            }

            //HowManyPeriodsToAdd = 0...just return whatever was passed in
            return accountingPeriod;
        }

        #region Calculations

        /// <summary>
        /// Increments the accounting period by HowManyPeriodsToAdd periods.
        /// </summary>
        /// <param name="accountingPeriodToAddTo">Accounting period to add to</param>
        /// <param name="howManyPeriodsToAdd">How many periods to add. You can pass in -1 to subtract 1 period (will handle that)</param>
        /// <returns>New Period</returns>
        private static AccountingPeriod AddPeriod(AccountingPeriod accountingPeriodToAddTo, int howManyPeriodsToAdd)
        {
            //if we are are not adding then throw an error
            if (howManyPeriodsToAdd <= 0)
            {
                //throw an error because we aren't adding a period
                throw new ArgumentOutOfRangeException("Can't Use Add Period When You Want To Subtract A Period");
            }

            //since Accounting Period is immutable let's create a working month and a working year
            var workingMonth = accountingPeriodToAddTo.Month;

            //grab the year now
            var workingYear = accountingPeriodToAddTo.Year;

            //we want to loop through for each period we want to add
            for (int i = 0; i < howManyPeriodsToAdd; i++)
            {
                //are we up to december?
                if (workingMonth == 12)
                {
                    //we need to go to the next year
                    workingYear += 1;

                    //set the month to january
                    workingMonth = 1;
                }
                else
                {
                    //we just need to increment the month (because we aren't in Dec)
                    workingMonth += 1;
                }
            }

            //we just need to increment the month
            return new AccountingPeriod(workingMonth, workingYear);
        }

        /// <summary>
        /// subtracts the accounting period by HowManyPeriodsToAdd periods.
        /// </summary>
        /// <param name="accountingPeriodToSubtractTo">Accounting period to subtract to</param>
        /// <param name="howManyPeriodsToSubtract">How many periods to subtract. You can pass in -1 to subtract 1 period (will handle that)</param>
        /// <returns>New Period</returns>
        private static AccountingPeriod SubtractPeriod(AccountingPeriod accountingPeriodToSubtractTo, int howManyPeriodsToSubtract)
        {
            //if we are are not adding then throw an error
            if (howManyPeriodsToSubtract >= 0)
            {
                //throw an error because we aren't adding a period
                throw new ArgumentOutOfRangeException("Can't Use Subtract Period When You Want To Add A Period");
            }

            //since Accounting Period is immutable let's create a working month and a working year
            var workingMonth = accountingPeriodToSubtractTo.Month;

            //grab the year now
            var workingYear = accountingPeriodToSubtractTo.Year;

            //we want to loop through for each period we want to add
            for (int i = 0; i > howManyPeriodsToSubtract; i--)
            {
                //are we up to january?
                if (workingMonth == 1)
                {
                    //we need to go to the previous year
                    workingYear -= 1;

                    //set the month to december
                    workingMonth = 12;
                }
                else
                {
                    //we just need to subtract the month (because we aren't in january)
                    workingMonth -= 1;
                }
            }

            //we just need to increment the month
            return new AccountingPeriod(workingMonth, workingYear);
        }

        #endregion

        #endregion

    }

}
