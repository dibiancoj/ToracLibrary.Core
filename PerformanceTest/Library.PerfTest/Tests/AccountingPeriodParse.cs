using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Library.PerfTest.Interface;

namespace Library.PerfTest.Tests
{
    [InProcess]
    [MemoryDiagnoser]
    public class AccountingPeriodParse : IPerformanceTest
    {

        public string Command => "AP";

        public string Description => "Accounting Period Parse - Substring Vs Span";

        [Params(201801)]
        public int AccountPeriod { get; set; }

        [Benchmark(Baseline = true)]
        public int WithSubString()
        {
            //we are all good set the variables (first push the int to a string
            var periodInStringFormat = AccountPeriod.ToString();

            //grab the month now and set it
            var month = GetMonthFromAccountingPeriod(periodInStringFormat);

            //grab the year now and set it
            var year = GetYearFromAccountingPeriod(periodInStringFormat);

            return month + year;
        }

        private static int GetYearFromAccountingPeriod(string accountingPeriodString)
        {
            return int.Parse(accountingPeriodString.Substring(0, 4));
        }

        private static int GetMonthFromAccountingPeriod(string accountingPeriodString)
        {
            return int.Parse(accountingPeriodString.Substring(4, 2));
        }


        [Benchmark]
        public int WithSpan()
        {
            //we are all good set the variables (first push the int to a string
            var periodInStringFormat = AccountPeriod.ToString().AsSpan();

            //grab the month now and set it
            var month = GetYearFromAccountingPeriodSpan(periodInStringFormat);

            //grab the year now and set it
            var year = GetMonthFromAccountingPeriodSpan(periodInStringFormat);

            return month + year;
        }

        private static int GetYearFromAccountingPeriodSpan(ReadOnlySpan<char> accountingPeriodString)
        {
            return int.Parse(accountingPeriodString.Slice(0, 4));
        }

        private static int GetMonthFromAccountingPeriodSpan(ReadOnlySpan<char> accountingPeriodString)
        {
            return int.Parse(accountingPeriodString.Slice(4, 2));
        }

    }
}
