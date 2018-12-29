using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Core.ExtensionMethods;
using Library.Core.Tests.Framework;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{
    /// <summary>
    /// Unit test to IEnumerable Extension Methods
    /// </summary>
    public class IEnumerableExtensionTest
    {

        #region UnTyped IEnumerable

        #region Count

        [Fact(DisplayName = "Unit test the basic functionality of count on an untyped IEnumerable")]
        public void IEnumerableCountTest1()
        {
            //go check the counts
            Assert.Equal(10, ((IEnumerable)DummyObject.CreateDummyListLazy(10).ToArray()).Count());
            Assert.Equal(21, ((IEnumerable)DummyObject.CreateDummyListLazy(21).ToArray()).Count());
        }

        #endregion

        #endregion

        #region Typed IEnumerable

        #region Any With Null Check Tests

        [Fact(DisplayName = "Unit test the no predicate version")]
        public void AnyWithNullCheckTest1()
        {
            //create a new null list that we will use to check
            List<int> listToTestWith = null;

            //check the null list
            Assert.False(listToTestWith.AnyWithNullCheck());

            //create a new list
            listToTestWith = new List<int>();

            //check if the object instance has any items
            Assert.False(listToTestWith.AnyWithNullCheck());

            //add an item to the list
            listToTestWith.Add(1);

            //do we see the 1 number
            Assert.True(listToTestWith.AnyWithNullCheck());

            //add another item
            listToTestWith.Add(2);

            //should see the 2 items
            Assert.True(listToTestWith.AnyWithNullCheck());

            //clear all the items
            listToTestWith.Clear();

            //should resolve to false
            Assert.False(listToTestWith.AnyWithNullCheck());
        }

        [Fact(DisplayName = "Unit test the version with the predicate")]
        public void AnyWithNullCheckPredicateTest1()
        {
            //create a new null list that we will use to check
            List<int> listToTestWith = null;

            //should return false since we don't have an instance of an object
            Assert.False(listToTestWith.AnyWithNullCheck(x => x == 5));

            //create an instance of the list now
            listToTestWith = new List<int>();

            //we still don't have any items in the list
            Assert.False(listToTestWith.AnyWithNullCheck(x => x == 5));

            //add an item now 
            listToTestWith.Add(1);

            //we should be able to find the == 1
            Assert.True(listToTestWith.AnyWithNullCheck(x => x == 1));

            //we don't have anything greater then 5
            Assert.False(listToTestWith.AnyWithNullCheck(x => x > 5));

            //add 2
            listToTestWith.Add(2);

            //should be able to find the 2
            Assert.True(listToTestWith.AnyWithNullCheck(x => x == 2));

            //shouldn't be able to find any numbers greater then 5
            Assert.False(listToTestWith.AnyWithNullCheck(x => x > 5));

            //clear the list
            listToTestWith.Clear();

            //we have no items because we just cleared the list
            Assert.False(listToTestWith.AnyWithNullCheck(x => x <= 5));
        }

        #endregion

        #region Empty If Null Tests

        [Fact(DisplayName = "Test an enumerable that is not empty. Should pass back the original enumerable")]
        public void EmptyIfNullWithEnumerableThatIsNotNullTest1()
        {
            //original item to test
            var originalEnumerable = new List<string> { "1", "2", "3" };

            //go use the helper to check the result
            Assert.Equal(originalEnumerable, originalEnumerable.EmptyIfNull());
        }

        [Fact(DisplayName = "Test an enumerable that is not empty. Should pass back the original enumerable")]
        public void EmptyIfNullWithEnumerableThatIsNullTest1()
        {
            //original item to test
            List<string> originalEnumerable = null;

            //go grab the result. (pass in empty enumerable...because the result should be empty)
            Assert.Equal(Enumerable.Empty<string>(), originalEnumerable.EmptyIfNull());
        }

        #endregion

        #region Any With Null Check Tests

        [Fact(DisplayName = "Unit test the Coalesce for a class")]
        public void CoalesceWithClassTest1()
        {
            DateTime? date1 = null;
            DateTime? date2 = new DateTime(2017, 1, 1);
            DateTime? date3 = new DateTime(2017, 12, 1);

            //date 2 is the first non null date
            Assert.Equal(date2, new DateTime?[] { date1, date2, date3 }.Coalesce());

            //3rd element is first non null
            Assert.Equal(date3, new DateTime?[] { date1, date1, date3 }.Coalesce());

            //all items are null...this should return null
            Assert.Null(new DateTime?[] { date1, date1, null }.Coalesce());
        }

        [Fact(DisplayName = "Unit test the Coalesce for a struct")]
        public void CoalesceWithStructTest1()
        {
            DateTime date1 = new DateTime(2017, 1, 1);
            DateTime date2 = DateTime.MinValue;
            DateTime date3 = new DateTime(2017, 12, 1);

            //date 2 is the first non default date
            Assert.Equal(date1, new DateTime[] { date1, date2, date3 }.Coalesce());

            //switch the order and make sure 2 is the returned value
            Assert.Equal(date3, new DateTime[] { date2, date3, date1 }.Coalesce());

            //all items are null...this should return null
            Assert.Equal(DateTime.MinValue, new DateTime[] { date2, date2, DateTime.MinValue }.Coalesce());
        }

        #endregion

        #region First Index Of Element

        [Fact(DisplayName = "Unit test the basic functionality of the first index")]
        public void FirstIndexOfElementTest1()
        {
            //create a dummy list to test
            var dummyCreatedList = DummyObject.CreateDummyListLazy(10).ToArray();

            //run the test below
            Assert.Equal(0, dummyCreatedList.FirstIndexOfElement(x => x.Id == 0));
            Assert.Equal(1, dummyCreatedList.FirstIndexOfElement(x => x.Id == 1));
            Assert.Equal(8, dummyCreatedList.FirstIndexOfElement(x => x.Id == 8));
            Assert.Equal(8, dummyCreatedList.FirstIndexOfElement(x => x.Description == "Test_8"));
            Assert.Null(dummyCreatedList.FirstIndexOfElement(x => x.Id == 1000));
        }

        #endregion

        #region Last Index Of Element

        [Fact(DisplayName = "Unit test the basic functionality of the last index")]
        public void LastIndexOfElementTest1()
        {
            //creat the id's that will have duplicates
            var duplicateIds = new int[] { 3, 4 };

            //duplicate text to use
            const string duplicateTextTag = "Duplicate";

            //create a dummy list to test and modify it so we have duplicate propertu values
            var dummyCreatedList = DummyObject.CreateDummyListLazy(10).Select((x, i) => new { MainObject = x, DuplicateText = (duplicateIds.Contains(i) ? duplicateTextTag : null) }).ToArray();

            //check the following tests

            //check the main object that we don't have any id's with 100
            Assert.Null(dummyCreatedList.LastIndexOfElement(x => x.MainObject.Id == 100));

            //now check the duplicate text value and make sure the last index is 4 (or the highest number in duplicate id)
            Assert.Equal(duplicateIds.Last(), dummyCreatedList.LastIndexOfElement(x => x.DuplicateText == duplicateTextTag));

            //let's try to find something with no duplicates
            Assert.Equal(0, dummyCreatedList.LastIndexOfElement(x => x.MainObject.Id == 0));
        }

        #endregion

        #region For Each

        [Fact(DisplayName = "Unit test the basic functionality of the foreach on ienumerable")]
        public void ForEachTest1()
        {
            //set the value we want to change too
            const string valueToSet = "Changed Value";

            //id values we want to change
            var idsToChange = new int[] { 1, 2 };

            //grab the dummy list
            var dummyCreatedList = DummyObject.CreateDummyListLazy(10).ToArray();

            //go change the value for the 2 items using foreach
            dummyCreatedList.Where(x => idsToChange.Contains(x.Id)).ForEach(x => x.Description = valueToSet);

            //let's loop through each item and make sure we have the correct value
            foreach (var ItemToTest in dummyCreatedList.Where(x => idsToChange.Contains(x.Id)))
            {
                //make sure it's the value
                Assert.Equal(valueToSet, ItemToTest.Description);
            }
        }

        #endregion

        #region Distinct By

        [Fact(DisplayName = "Unit test the basic functionality of the Distinct By")]
        public void DistinctByTest1()
        {
            //creat the id's that will have duplicates
            var duplicateIds = new int[] { 3, 4 };

            //duplicate text to use
            const string duplicateTextTag = "Duplicate";

            //create a dummy list to test and modify it so we have duplicate propertu values
            var dummyCreatedList = DummyObject.CreateDummyListLazy(10).Select((x, i) => new { MainObject = x, DuplicateText = (duplicateIds.Contains(i) ? duplicateTextTag : null) }).ToArray();

            //run a distinct by on the duplicate text
            var resultsOfDistinctBy = dummyCreatedList.DistinctByLazy(x => x.DuplicateText).OrderBy(x => x.MainObject.Id).ToArray();

            //check the results

            //make sure we have 2 distinct items
            Assert.Equal(2, resultsOfDistinctBy.Length);

            //make sure we only have 1 distint value for the duplicate tag (make sure it's truly distinct)
            Assert.Single(resultsOfDistinctBy.Where(x => x.DuplicateText == duplicateTextTag));

            //make sure the duplicate text matches what we set
            Assert.Equal(duplicateTextTag, resultsOfDistinctBy.FirstOrDefault(x => x.DuplicateText == duplicateTextTag).DuplicateText);
        }

        #endregion

        #region Chunk Up List

        /// <summary>
        /// Helper method to calculate how many items should go in a bucket
        /// </summary>
        /// <param name="ItemsToBuild">how many items to build. Total data set count</param>
        /// <param name="MaxItemsInABucket">the most amount of items in a bucket</param>
        /// <returns>how many buckets there should be</returns>
        private static int HowManyGroupsInAChunkedUpList(int ItemsToBuild, int MaxItemsInABucket)
        {
            //ceiling so we "round up"
            return Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(Convert.ToDouble(ItemsToBuild) / MaxItemsInABucket)));
        }

        [Fact(DisplayName = "Unit test the basic functionality of the chunk list for ienumerable")]
        public void ChunkUpListTest1()
        {
            //the results should be 
            //bucket 1 = 5 items
            //bucket 2 = 5 items
            //bucket 3 = 5 items
            //bucket 4 = 5 items

            //how many items to build
            const int itemsToBuild = 20;

            //how many items in a bucket
            const int maxItemsInABucket = 5;

            //grab the dummy list
            var dummyCreatedList = DummyObject.CreateDummyListLazy(itemsToBuild).ToArray();

            //let's chunk it up in slabs of 5
            var chunkedInto5ElementsPerGroup = dummyCreatedList.ChunkUpListItemsLazy(maxItemsInABucket).ToArray();

            //we should have an even 4 groups
            Assert.Equal(HowManyGroupsInAChunkedUpList(itemsToBuild, maxItemsInABucket), chunkedInto5ElementsPerGroup.Length);

            //should be an even 5 elements per group
            chunkedInto5ElementsPerGroup.ForEach(x => Assert.Equal(maxItemsInABucket, x.Count()));

            //let's make sure, the elements in each group are correct. we will just check the first element in each group
            for (int i = 0; i < chunkedInto5ElementsPerGroup.Length; i++)
            {
                Assert.Equal(i * maxItemsInABucket, chunkedInto5ElementsPerGroup[i].ElementAt(0).Id);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact(DisplayName = "Unit test the chunk list items when we have an even count")]
        public void ChunkUpListTest2()
        {
            //the results should be 
            //bucket 1 = 5 items
            //bucket 2 = 5 items
            //bucket 3 = 2 items

            //how many items to build
            const int itemsToBuild = 12;

            //how many items in a bucket
            const int maxItemsInABucket = 5;

            //grab the dummy list
            var dummyCreatedList = DummyObject.CreateDummyListLazy(12).ToArray();

            //let's chunk it up in slabs of 5 (we should have an extra 2 guys at the end)
            var chunkedInto5ElementsPerGroup = dummyCreatedList.ChunkUpListItemsLazy(5).ToArray();

            //we should have an even 3 groups
            Assert.Equal(HowManyGroupsInAChunkedUpList(itemsToBuild, maxItemsInABucket), chunkedInto5ElementsPerGroup.Length);

            //let's make sure, the elements in each group are correct. we will just check the first element in each group
            for (int i = 0; i < chunkedInto5ElementsPerGroup.Length; i++)
            {
                //group should have the following number of elements
                int ShouldBeXAmountOfElementsPerGroup = maxItemsInABucket;

                //let's make sure this group has the correct number (the last group should only have 2)
                if (i == (chunkedInto5ElementsPerGroup.Length - 1))
                {
                    //it's the last group, there should only be 2 items
                    ShouldBeXAmountOfElementsPerGroup = 2;
                }

                //check how many elements in the group
                Assert.Equal(ShouldBeXAmountOfElementsPerGroup, chunkedInto5ElementsPerGroup[i].Count());

                //now check the first element in the group
                Assert.Equal(i * 5, chunkedInto5ElementsPerGroup[i].ElementAt(0).Id);
            }
        }

        [Fact(DisplayName = "Chunk up List Should Raise Exception With Out Of Range")]
        public void ChunkUpListMaxNumberZeroRaiseException()
        {
            //grab the dummy list
            var dummyCreatedList = DummyObject.CreateDummyListLazy(2).ToArray();

            //these should throw an exception
            Assert.Throws<ArgumentOutOfRangeException>(() => dummyCreatedList.ChunkUpListItemsLazy(0).ToArray());
            Assert.Throws<ArgumentOutOfRangeException>(() => dummyCreatedList.ChunkUpListItemsLazy(-1).ToArray());

            //positive test so it shouldn't throw
            Assert.Equal(2, dummyCreatedList.ChunkUpListItemsLazy(1).SelectMany(x => x).Count());
        }

        #endregion

        #region Prepend and Append With Single Item

        [Fact(DisplayName = " Unit test for Prepend - single item first")]
        public void PrependSingleItemFirstTest1()
        {
            //go build the list
            var testList = DummyObject.CreateDummyListLazy(2).ToList();

            //go grab the single item
            var itemToAppend = DummyObject.CreateDummyRecord();

            //go build the result
            var result = testList.PrependItemLazy(itemToAppend).ToList();

            //make sure we have 3 items
            Assert.Equal(testList.Count + 1, result.Count);

            //use the concat operator to make sure we have the same results
            var shouldBeResult = new DummyObject[] { itemToAppend }.Concat(testList).ToList();

            //now go test all the items
            for (int i = 0; i < shouldBeResult.Count; i++)
            {
                //test the id
                Assert.Equal(shouldBeResult[i].Id, result[i].Id);

                //test the description
                Assert.Equal(shouldBeResult[i].Description, result[i].Description);
            }
        }

        [Fact(DisplayName = "Unit test for Append - list first")]
        public void AppendSingleItemFirstTest1()
        {
            //go build the list
            var testList = DummyObject.CreateDummyListLazy(2).ToList();

            //go grab the single item
            var itemToAppend = DummyObject.CreateDummyRecord();

            //go build the result
            var result = testList.AppendItemLazy(itemToAppend).ToList();

            //make sure we have 3 items
            Assert.Equal(testList.Count + 1, result.Count);

            //use the concat operator to make sure we have the same results
            var shouldBeResult = testList.Concat(new DummyObject[] { itemToAppend }).ToList();

            //now go test all the items
            for (int i = 0; i < shouldBeResult.Count; i++)
            {
                //test the id
                Assert.Equal(shouldBeResult[i].Id, result[i].Id);

                //test the description
                Assert.Equal(shouldBeResult[i].Description, result[i].Description);
            }
        }

        #endregion

        #endregion

    }
}
