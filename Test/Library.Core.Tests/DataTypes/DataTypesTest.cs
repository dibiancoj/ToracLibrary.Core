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
        [Theory(DisplayName = "Test Primitive types. All these items should be found in the list")]
        public void PrimitiveTypesTest1(Type typeToTest)
        {
            //make sure this is in the list
            Assert.Contains(typeToTest, PrimitiveTypes.PrimitiveTypeLookup);
        }

        [InlineData(typeof(IEnumerable<double>))]
        [InlineData(typeof(object))]
        [InlineData(typeof(List<double>))]
        [Theory(DisplayName = "Test Primitive types. All these items should NOT be found in the list")]
        public void PrimitiveTypesTest2(Type typeToTest)
        {
            //make sure this item is NOT in the list
            Assert.DoesNotContain(typeToTest, PrimitiveTypes.PrimitiveTypeLookup);
        }

    }

}
