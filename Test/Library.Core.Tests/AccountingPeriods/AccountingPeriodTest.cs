using Library.Core.AccountingPeriods;
using Library.Core.AccountingPeriods.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Library.Core.Tests.AccountingPeriods
{

    /// <summary>
    /// Unit tests for accounting period
    /// </summary>
    public class AccountingPeriodTest
    {

        #region Main Tests

        [InlineData(201412, 12, 2014)]
        [InlineData(201301, 01, 2013)]
        [InlineData(201306, 06, 2013)]
        [Theory(DisplayName = "Test builds an accounting period object and verifies its correct")]
        public void BuildAccountingPeriodObjectIntConstructorTest1(int accountingPeriodToTest, int shouldBeMonth, int shouldBeYear)
        {
            //go run the method and get the results
            var Result = new AccountingPeriod(accountingPeriodToTest);

            //test the month and year
            Assert.Equal(shouldBeMonth, Result.Month);
            Assert.Equal(shouldBeYear, Result.Year);

            //make sure the ToAccountingPeriod Works
            Assert.Equal(Result.ToAccountingPeriod(), accountingPeriodToTest);
        }

        [InlineData(12, 2014, 201412)]
        [InlineData(01, 2013, 201301)]
        [InlineData(06, 2013, 201306)]
        [Theory(DisplayName = "Test builds an accounting period object and verifies its correct")]
        public void BuildAccountingPeriodObjectSeperateMonthYearConstructorTest2(int monthToTest, int yearToTest, int shouldBeAccountingPeriod)
        {
            //grab the result
            var Result = new AccountingPeriod(monthToTest, yearToTest);

            //test the month and year
            Assert.Equal(monthToTest, Result.Month);
            Assert.Equal(yearToTest, Result.Year);

            //make sure the ToAccountingPeriod Works
            Assert.Equal(Result.ToAccountingPeriod(), shouldBeAccountingPeriod);
        }

        [InlineData(201412, 1, 201501)]
        [InlineData(201407, 1, 201408)]
        [InlineData(201401, 1, 201402)]
        [InlineData(201401, 2, 201403)]
        [InlineData(201401, 12, 201501)]
        [InlineData(201412, 3, 201503)]
        [Theory(DisplayName = "Test how we add periods")]
        public void IncrementPeriodTest1(int accountingPeriodToTest, int incrementBy, int shouldBeAccoutingPeriod)
        {
            //run the test and make sure everything equals out
            Assert.Equal(shouldBeAccoutingPeriod, AccountingPeriod.IncrementPeriod(accountingPeriodToTest, incrementBy));
        }

        [InlineData(201508, -1, 201507)]
        [InlineData(201501, -1, 201412)]
        [InlineData(201412, -1, 201411)]
        [InlineData(201412, -12, 201312)]
        [InlineData(201408, -2, 201406)]
        [Theory(DisplayName = "Test how we subtract periods")]
        public void DecreasePeriodTest1(int accountingPeriodToTest, int incrementBy, int shouldBeAccoutingPeriod)
        {
            //run the test and make sure everything equals out
            Assert.Equal(shouldBeAccoutingPeriod, AccountingPeriod.IncrementPeriod(accountingPeriodToTest, incrementBy));
        }

        [InlineData(201412, 12, 2014)]
        [Theory(DisplayName = "Test how we convert a period to a date")]
        public void ConvertPeriodToDateTest1(int accountingPeriodToTest, int shouldBeMonthDate, int shouldBeYearDate)
        {
            //make sure the date equals what is being returned
            Assert.Equal(new DateTime(shouldBeYearDate, shouldBeMonthDate, 1), AccountingPeriod.PeriodToDateTime(accountingPeriodToTest));
        }

        [InlineData(12, 2014, 201412)]
        [InlineData(11, 2014, 201411)]
        [InlineData(11, 2013, 201311)]
        [Theory(DisplayName = "Test a date to a period")]
        public void ConvertDateToPeriodTest1(int testMonthDate, int testYearDate, int shouldBeAccountingPeriod)
        {
            //make sure we get back the correct accounting period
            Assert.Equal(shouldBeAccountingPeriod, AccountingPeriod.DateTimeToPeriod(new DateTime(testYearDate, testMonthDate, 1)));
        }

        #endregion

        #region Validation Tests

        [InlineData(20140000)]
        [Theory(DisplayName = "Make sure we get the year out of range exception for a parameter that has a bad year")]
        public void ValidateAccountingPeriodOutOfRangeExceptionTest1(int accountingPeriodToTest)
        {
            Assert.Throws<AccountingPeriodYearOutOfRangeException>(() => new AccountingPeriod(accountingPeriodToTest));
        }

        [InlineData(201400)]
        [InlineData(201413)]
        [Theory(DisplayName = "Make sure we get the month out of range exception for a parameter that has a bad year")]
        public void ValidateAccountingPeriodOutOfRangeExceptionTest2(int accountingPeriodToTest)
        {
            Assert.Throws<AccountingPeriodMonthOutOfRangeException>(() => new AccountingPeriod(accountingPeriodToTest));
        }

        #endregion

    }

}
