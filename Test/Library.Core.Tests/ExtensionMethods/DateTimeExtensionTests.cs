using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Library.Core.ExtensionMethods;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{

    /// <summary>
    /// Unit test to Date Time Extension Methods
    /// </summary>
    public class DateTimeExtensionTests
    {

        public class DateTimeExtensionTestData : IEnumerable<object[]>
        {
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { true, new DateTime(2017, 1, 2), new DateTime(2017, 1, 1), new DateTime(2018, 1, 1) };
                yield return new object[] { true, new DateTime(2017, 1, 3), new DateTime(2017, 1, 1), new DateTime(2018, 1, 4) };

                yield return new object[] { false, new DateTime(2017, 1, 3), new DateTime(2017, 1, 5), new DateTime(2018, 1, 6) };
            }
        }

        [Theory(DisplayName = "Unit test for DateTime.Between")]
        [ClassData(typeof(DateTimeExtensionTestData))]
        public void DateTimeBetweenTest1(bool expectedIsBetweenTheDates, DateTime testDate, DateTime evaluateStartDate, DateTime evaluateEndDate)
        {
            Assert.Equal(expectedIsBetweenTheDates, testDate.IsBetween(evaluateStartDate, evaluateEndDate));
        }

        [Fact(DisplayName = "DateTime.Between should throw when end date is before start date")]
        public void EndDateBeforeStartDateShouldThrow()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new DateTime(2017, 1, 3).IsBetween(new DateTime(2017, 1, 2), new DateTime(2017, 1, 1)));
        }

    }

}
