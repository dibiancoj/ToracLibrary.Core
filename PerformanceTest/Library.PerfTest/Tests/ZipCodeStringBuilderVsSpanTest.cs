using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Library.PerfTest.Interface;

namespace Library.PerfTest.Tests
{
    [InProcess]
    [MemoryDiagnoser]
    public class ZipCodeStringBuilderVsSpanTest : IPerformanceTest
    {

        public string Command => "Zip";

        public string Description => "Zip Code Format - Substring Vs Span";

        [Params("107101234", "10583", "1234567", "107111234")]
        public string ZipFormat { get; set; }

        [Benchmark(Baseline = true)]
        public string WithSubString()
        {
            //if the zip code is null then just return it right away
            if (string.IsNullOrEmpty(ZipFormat))
            {
                //zip code is null / blank...just return the string that was passed in
                return ZipFormat;
            }

            if (ZipFormat.Length == 5)
            {
                //if its just the 5 character version, then return just the item passed in
                return ZipFormat;
            }

            if (ZipFormat.Length != 9)
            {
                //if the length is not 9 then return it...we need 9 characters
                return ZipFormat;
            }

            //we have 9 characters, create the instance of the string builder becauase we need it (init the capacity to reduce memory just a tag)
            var formattedZipCode = new StringBuilder();

            //Add the first 5 characters
            formattedZipCode.Append(ZipFormat.Substring(0, 5));

            //add the dash
            formattedZipCode.Append("-");

            //return the last 4 digits
            formattedZipCode.Append(ZipFormat.Substring(5, 4));

            //return the formatted zip code
            return formattedZipCode.ToString();
        }


        [Benchmark]
        public string WithSpan()
        {
            //if the zip code is null then just return it right away
            if (string.IsNullOrEmpty(ZipFormat))
            {
                //zip code is null / blank...just return the string that was passed in
                return ZipFormat;
            }

            if (ZipFormat.Length == 5)
            {
                //if its just the 5 character version, then return just the item passed in
                return ZipFormat;
            }

            if (ZipFormat.Length != 9)
            {
                //if the length is not 9 then return it...we need 9 characters
                return ZipFormat;
            }

            var rawZipInSpan = ZipFormat.AsSpan();

            //we have 9 characters, create the instance of the string builder becauase we need it (init the capacity to reduce memory just a tag)
            var formattedZipCode = new StringBuilder();

            //Add the first 5 characters
            formattedZipCode.Append(rawZipInSpan.Slice(0, 5));

            //add the dash
            formattedZipCode.Append("-");

            //return the last 4 digits
            formattedZipCode.Append(rawZipInSpan.Slice(5, 4));

            //return the formatted zip code
            return formattedZipCode.ToString();
        }

    }
}
