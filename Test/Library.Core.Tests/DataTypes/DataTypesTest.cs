using Library.Core.DataTypes;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Library.Core.Tests.DataTypes
{

    /// <summary>
    /// Unit test to test data types
    /// </summary>
    public class DataTypesTest
    {

        #region Primitive Types

        /// <summary>
        /// Test Primitive types. All these items should be found in the list
        /// </summary>
        [InlineData(typeof(string))]
        [InlineData(typeof(bool))]
        [InlineData(typeof(bool?))]
        [InlineData(typeof(Int16))]
        [InlineData(typeof(Int16?))]
        [InlineData(typeof(int))]
        [InlineData(typeof(int?))]
        [InlineData(typeof(Int64))]
        [InlineData(typeof(Int64?))]
        [InlineData(typeof(double))]
        [InlineData(typeof(double?))]
        [InlineData(typeof(float))]
        [InlineData(typeof(float?))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(decimal?))]
        [Theory]
        public void PrimitiveTypesTest1(Type TypeToTest)
        {
            //make sure this is in the list
            Assert.Contains(TypeToTest, PrimitiveTypes.PrimitiveTypeLookup);
        }

        /// <summary>
        /// Test Primitive types. All these items should NOT be found in the list
        /// </summary>
        [InlineData(typeof(IEnumerable<double>))]
        [InlineData(typeof(object))]
        [InlineData(typeof(List<double>))]
        [Theory]
        public void PrimitiveTypesTest2(Type TypeToTest)
        {
            //make sure this item is NOT in the list
            Assert.DoesNotContain(TypeToTest, PrimitiveTypes.PrimitiveTypeLookup);
        }

        #endregion

    }

}
