using System;
using System.Collections.Generic;
using System.Text;
using Library.Core.ExtensionMethods;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{

    /// <summary>
    /// Unit test to test byte array Extension Methods
    /// </summary>
    public class ByteArrayExtensionTest
    {

        [InlineData("5465737456616c7565", true)]
        [InlineData("5465737456616C7565", false)]
        [Theory(DisplayName = "Unit test to ensure a byte array can convert to a hexadecimal in lowercase and uppercase")]
        public void ByteArrayToHexadecimalLowerCaseTest1(string expectedResult, bool toLowerCase)
        {
            //now make sure nothing has changed
            Assert.Equal(expectedResult, new UTF8Encoding().GetBytes("TestValue").ToByteArrayToHexadecimalString(toLowerCase));
        }

    }

}
