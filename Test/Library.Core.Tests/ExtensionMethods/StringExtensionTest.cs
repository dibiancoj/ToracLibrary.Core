using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Library.Core.ExtensionMethods;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{
    /// <summary>
    /// Unit test to String Extension Methods
    /// </summary>
    public class StringExtensionTest
    {

        #region Contains

        #region Contains Off Of String

        [InlineData("Jason", "JASON")]
        [InlineData("JASON", "JASON")]
        [InlineData("jason2", "JASON")]
        [Theory(DisplayName = "Test string contains with string comparison (true result)")]
        public void StringContainsTrueTest1(string valueToTest, string containsValueToTest)
        {
            Assert.True(valueToTest.Contains(containsValueToTest, StringComparison.OrdinalIgnoreCase));
        }

        [InlineData("test 123", "JASON")]
        [InlineData("123", "Bla123")]
        [Theory(DisplayName = "Test string contains with string comparison (false result)")]
        public void StringContainsFalseTest1(string valueToTest, string containsValueToTest)
        {
            Assert.False(valueToTest.Contains(containsValueToTest, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Contains Off Of IEnumerable

        [InlineData(true, "JASON", StringComparison.OrdinalIgnoreCase)]
        [InlineData(false, "JASONABC", StringComparison.OrdinalIgnoreCase)]

        [InlineData(false, "JASON", StringComparison.Ordinal)]
        [InlineData(true, "jason1", StringComparison.Ordinal)]
        [Theory(DisplayName = "Unit test ienumerable of string contains with string comparison")]
        public void IEnumerableStringContainsTest1(bool containsValueExpectedResult, string stringToFind, StringComparison stringComparison)
        {
            Assert.Equal(containsValueExpectedResult, new string[] { "jason1", "jason2", "jason3" }.Contains(stringToFind, stringComparison));
        }

        #endregion

        #endregion

        #region String Is Null Or Empty - Instance

        [InlineData("Test")]
        [InlineData("123")]
        [InlineData("12345")]
        [Theory(DisplayName = "Test if a string has a value in a string instance extension method (true result)")]
        public void HasValueTrueResultTest1(string valueToTest)
        {
            Assert.True(valueToTest.HasValue());
        }

        [InlineData("")]
        [InlineData((string)null)]
        [Theory(DisplayName = "Test if a string has a value in a string instance extension method (false result)")]
        public void HasValueFalseResultTest1(string valueToTest)
        {
            Assert.False(valueToTest.HasValue());
        }

        [InlineData(" ")]
        [InlineData("123")]
        [InlineData("123 456")]
        [InlineData("a")]
        [Theory(DisplayName = "Test is a string is null or empty in a string instance extension method (not null result)")]
        public void NullOrEmptyNotNullTestTest1(string valueToTest)
        {
            Assert.False(valueToTest.IsNullOrEmpty());
        }

        [InlineData("")]
        [InlineData((string)null)]
        [Theory(DisplayName = "Test is a string is null or empty in a string instance extension method  (null result)")]
        public void NullOrEmptyNullTestTest1(string valueToTest)
        {
            Assert.True(valueToTest.IsNullOrEmpty());
        }

        #endregion

        #region Format USA Phone Number

        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("9145552235", "(914) 555-2235")]
        [InlineData("914552", "914552")]
        [InlineData("914555)235", "914555)235")]
        [InlineData("(914) 555-7777", "(914) 555-7777")]
        [InlineData("(914)5557777", "(914) 555-7777")]
        [Theory(DisplayName = "Unit test for formatting a string to a usa phone number")]
        public void FormatUSAPhoneNumberTest1(string valueToTest, string shouldBeValue)
        {
            Assert.Equal(shouldBeValue, valueToTest.ToUSAPhoneNumber());
        }

        #endregion

        #region Format USA Zip Code

        [InlineData("", "")]
        [InlineData("10583", "10583")]
        [InlineData("1058322", "1058322")]
        [InlineData("105832233", "10583-2233")]
        [Theory(DisplayName = "Unit test for formatting a string to a usa zip code")]
        public void FormatUSAZipCodeTest1(string ValueToTest, string shouldBeValue)
        {
            Assert.Equal(shouldBeValue, ValueToTest.ToUSAZipCode());
        }

        #endregion

        #region Is Valid Email

        [InlineData("jason")]
        [InlineData("jason.com@")]
        [InlineData("jason@")]
        [InlineData("j ason@aol.com")]
        [InlineData("jason@com.")]
        [InlineData("@jasoncom.")]
        [Theory(DisplayName = "Test is a string is a valid email address (this test should be for invalid email address)")]
        public void IsValidEmailTestNegativeResultTest1(string emailValueToTest)
        {
            //should be invalid email adddress
            Assert.False(emailValueToTest.IsValidEmailAddress());
        }

        [InlineData("jason@aol.com")]
        [InlineData("JoeJ@gmail.com")]
        [InlineData("jason@my.torac.com")] //sub domain test
        [Theory(DisplayName = "Test is a string is a valid email address (this test should be for correct email address)")]
        public void IsValidEmailTestPostiveResultTest1(string emailValueToTest)
        {
            //should be valid email adddress
            Assert.True(emailValueToTest.IsValidEmailAddress());
        }

        #endregion

        #region Replace Case Sensitive

        [InlineData("jason", "ASON", "j")]
        [InlineData("jason", "yay", "jason")]
        [Theory(DisplayName = "Test the replacement of characters ignoring case")]
        public void StringReplaceCaseSensitiveTest1(string testString, string textToReplace, string shouldBeValue)
        {
            Assert.Equal(shouldBeValue, testString.ReplaceCaseInSensitiveString(textToReplace, string.Empty));
        }

        #endregion

        #region String Right

        [InlineData(2, "on")]
        [InlineData(4, "ason")]
        [Theory(DisplayName = "Test the string right function")]
        public void StringRightTest1(int howManyToTake, string expectedResult)
        {
            //test string
            const string testString = "jason";

            //check the values
            Assert.Equal(expectedResult, testString.Right(howManyToTake));
            Assert.Equal(expectedResult, testString.Right(howManyToTake));
        }

        #endregion

        #region Trim Handle Null

        [InlineData(" Jason Test 1 ", "Jason Test 1")]
        [InlineData(" Jason Test 2 ", "Jason Test 2")]
        [InlineData(" Jason Test 3  ", "Jason Test 3")]
        [InlineData(null, "")]
        [Theory(DisplayName = "Check trim with null check with no replacement value")]
        public void TrimHandleNullNoReplacementTest1(string stringToTrim, string expectedResult)
        {
            Assert.Equal(expectedResult, stringToTrim.TrimHandleNull());
        }

        [InlineData(" Jason Test 1 ", "Jason Test 1")]
        [InlineData(" Jason Test 2 ", "Jason Test 2")]
        [InlineData(" Jason Test 3  ", "Jason Test 3")]
        [InlineData("", "This Is Null")]
        [InlineData(null, "This Is Null")]
        [Theory(DisplayName = "Check trim with null check with with replacement value")]
        public void TrimHandleNullWithReplacementTest2(string stringToTrim, string expectedResult)
        {
            Assert.Equal(expectedResult, stringToTrim.TrimHandleNull("This Is Null"));
        }

        #endregion

        #region String To Byte Array - Ascii

        [Fact(DisplayName = "Test to make sure a string to a byte array is correct")]
        public void StringToByteArrayAsciiTest1()
        {
            //loop through the elements to test using the helper method for this
            Assert.Equal(new byte[] { 106, 97, 115, 111, 110 }, "jason".ToAsciiByteArray());
        }

        [Fact(DisplayName = "Test to make sure a string to a byte array is correct")]
        public void StringToByteArrayAsciiTest2()
        {
            //loop through the elements to test using the helper method for this
            Assert.Equal(new byte[] { 106, 97, 115, 111, 110, 50 }, "jason2".ToAsciiByteArray());
        }

        #endregion

        #region String To Byte Array - Utf-8

        [Fact(DisplayName = "Test to make sure a string to a byte array is correct")]
        public void StringToByteArrayUtf8Test1()
        {
            //loop through the elements to test using the helper method for this
            Assert.Equal(new byte[] { 106, 97, 115, 111, 110 }, "jason".ToUtf8ByteArray());
        }

        [Fact(DisplayName = "Test to make sure a string to a byte array is correct")]
        public void StringToByteArrayUtf8Test2()
        {
            //loop through the elements to test using the helper method for this
            Assert.Equal(new byte[] { 106, 97, 115, 111, 110, 50 }, "jason2".ToUtf8ByteArray());
        }

        #endregion

        #region Indexes Of All Lazy

        [Fact(DisplayName = "Test to make sure the index of all returns the correct value")]
        public void IndexesOfAllLazyTest1()
        {
            //test string to look through
            string testLookThroughString = "<html><img src='relative/test.jpg' /><img src='www.google.com' /></html>";

            //go run the method
            var results = testLookThroughString.IndexesOfAllLazy("src='", StringComparison.OrdinalIgnoreCase).ToArray();

            //test this value
            Assert.Equal(2, results.Count());

            //test the indexes
            Assert.Contains(results, x => x == 11);
            Assert.Contains(results, x => x == 42);
        }

        [Fact(DisplayName = "Test to make sure the index of all returns the correct value when there are no matches found")]
        public void IndexesOfAllLazyTestWithNoMatches()
        {
            //test string to look through
            string TestLookThroughString = "Test 123";

            //go run the method
            Assert.Empty(TestLookThroughString.IndexesOfAllLazy("Fact", StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Surround With

        [InlineData("Jason", "?Jason?", "?")]
        [InlineData("Test", "!Test!", "!")]
        [Theory(DisplayName = "Test to make sure the SurroundWith works")]
        public void SurroundWithTest1(string valueToTest, string shouldBeValue, string valueToSurroundWith)
        {
            Assert.Equal(shouldBeValue, valueToTest.SurroundWith(valueToSurroundWith));
        }

        [InlineData("Jason", @"""Jason""")]
        [InlineData("Test", @"""Test""")]
        [Theory(DisplayName = "Test to make sure the SurroundWithQuotes works")]
        public void SurroundWithQuotesTest1(string valueToTest, string shouldBeValue)
        {
            Assert.Equal(shouldBeValue, valueToTest.SurroundWithQuotes());
        }

        #endregion

        #region To Base 64 Decode

        [InlineData("Test12345hts")]
        [InlineData("456vsdfidsajf sdfds")]
        [Theory(DisplayName = "Test to make sure the Base 64 Encoding Works works")]
        public void Base64Encoded(string valueToTest)
        {
            Assert.Equal(valueToTest, valueToTest.ToBase64Encode().ToBase64Decode());
        }

        #endregion

        #region Remove Spaces

        [InlineData("Test123", "Test123")]
        [InlineData("Test 1 2 3 4 5", "Test12345")]
        [InlineData("Test 1 2 345", "Test12345")]
        [Theory(DisplayName = "Test to make sure the spaces are removed")]
        public void RemoveSpaces(string valueToTest, string expectedValue)
        {
            Assert.Equal(expectedValue, valueToTest.RemoveSpaces());
        }

        #endregion

    }
}
