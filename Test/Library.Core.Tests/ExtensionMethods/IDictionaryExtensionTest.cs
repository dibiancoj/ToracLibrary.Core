using System;
using System.Collections.Generic;
using System.Text;
using Library.Core.ExtensionMethods;
using Library.Core.Tests.Framework;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{

    /// <summary>
    /// Unit test to IDictionary Extension Methods
    /// </summary>
    public class IDictionaryExtensionTest
    {

        #region Get Or Add

        [Fact(DisplayName = "Unit test for GetOrAdd to a dictionary")]
        public void GetOrAddTest1()
        {
            //how many times this has been created
            int howManyTimesCreated = 0;

            //value to use to test
            const int uniqueId = 9999;

            //create a test dictionary which we will use
            var testDictionary = new Dictionary<int, DummyObject>();

            //try to get it. It shouldn't be found...so we will return the creator
            var Result = testDictionary.GetOrAdd(uniqueId, () =>
            {
                //increase the tally
                howManyTimesCreated++;

                //return the object
                return new DummyObject(uniqueId, uniqueId.ToString());
            });

            //test the entry
            Assert.Equal(uniqueId, testDictionary[uniqueId].Id);

            //now make sure if we try to add the same item that we don't throw the exception
            Assert.Equal(uniqueId, testDictionary.GetOrAdd(uniqueId, () => throw new IndexOutOfRangeException("This shouldn't Be Called")).Id);

            //make sure we only every call the method once
            Assert.Equal(1, howManyTimesCreated);
        }

        #endregion

    }

}
