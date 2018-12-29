using System;
using System.Collections.Generic;
using System.Text;
using Library.Core.ExtensionMethods;
using Library.Core.Tests.Framework;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{

    /// <summary>
    /// Unit test to Object Extension Methods
    /// </summary>
    public class ObjectExtensionTest
    {

        #region Framework

        public abstract class MyObject
        {
            public const int MyValue = 1;

            public int MyValueGetter
            {
                get { return MyValue; }
            }
        }

        public class MyDerivedObject : MyObject
        {
        }

        #endregion

        #region Cast Unit Tests

        [Fact(DisplayName = "Try to convert a class to something else")]
        public void ObjectCastTest1()
        {
            Assert.Equal(MyObject.MyValue, new MyDerivedObject().Cast<MyObject>().MyValueGetter);
        }

        [Fact(DisplayName = "Try to convert a class to something that isn't castable")]
        public void ObjectCastToNullTest1()
        {
            Assert.Throws<InvalidCastException>(() => DummyObject.CreateDummyRecord().Cast<MyObject>());
        }

        #endregion

        #region As Unit Tests

        [Fact(DisplayName = "Try to convert a class to something else")]
        public void ObjectAsTest1()
        {
            Assert.Equal(MyObject.MyValue, new MyDerivedObject().As<MyObject>().MyValueGetter);
        }

        [Fact(DisplayName = "Try to convert a class to something that isn't castable")]
        public void ObjectAsToNullTest1()
        {
            Assert.Null(DummyObject.CreateDummyRecord().As<MyObject>()?.MyValueGetter);
        }

        #endregion

        #region Is Unit Tests

        [Fact(DisplayName = "Try to convert a class that is derived from another. Should return true")]
        public void ObjectIsTest1()
        {
            Assert.True(new MyDerivedObject().Is<MyObject>());
        }

        [Fact(DisplayName = "Try to check a class that isn't castable. Should return false since it's not the same or derived type")]
        public void ObjectIsToNullTest1()
        {
            Assert.False(DummyObject.CreateDummyRecord().Is<MyObject>());
        }

        #endregion

        #region Single Object To Array Types

        [Fact(DisplayName = "Unit test to create an IEnumerable from a single object")]
        public void SingleObjectToIEnumerableTest1()
        {
            //make sure we only have 1 record. This should prove it's in a form of ienumerable
            Assert.Single(DummyObject.CreateDummyRecord().ToIEnumerableLazy());
        }

        [Fact(DisplayName = "Unit test to create an IList from a single object")]
        public void SingleObjectToListTest1()
        {
            //grab a single record and push to an ienumerable
            var iListBuiltFromSingleObject = DummyObject.CreateDummyRecord().ToIList();

            //make sure we only have 1 record. This should prove it's in a form of ienumerable
            Assert.Equal(1, iListBuiltFromSingleObject.Count);

            //add another record so we can make sure it increments
            iListBuiltFromSingleObject.Add(DummyObject.CreateDummyRecord());

            //check the count
            Assert.Equal(2, iListBuiltFromSingleObject.Count);
        }

        #endregion

    }

}
