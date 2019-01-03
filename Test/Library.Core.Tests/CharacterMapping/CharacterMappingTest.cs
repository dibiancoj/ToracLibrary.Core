using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using static Library.Core.CharacterMapping.Characters;

namespace Library.Core.Tests.CharacterMapping
{

    /// <summary>
    /// Unit test to test the character mapping functionality
    /// </summary>
    public class CharacterMappingTest
    {

        [Fact(DisplayName = "Make sure the numeric numbers return are correct")]
        public void NumericCharactersTest1()
        {
            //going to hard code this test
            var digitsToTest = AllNumberCharactersLazy().OrderBy(x => x).ToArray();

            //start testing this. going to do this manually to ensure everything is correct
            for (int i = 0; i < 10; i++)
            {
                //test the result
                Assert.Equal(i, digitsToTest[i]);
            }
        }

        [Fact(DisplayName = "Make sure the alphabet numbers return are correct")]
        public void AlphabetCharactersTest1()
        {
            //loop through all the characters and test it
            var resultOfCall = AllAlphaBetCharactersString();

            //holds an independent string incase characters.constant gets modified by accident
            const string alphabet = "abcdefghijklmnopqrstuvwxyz";

            //we are first going to test the constant in the characters.cs module to make sure we are in sync. 
            Assert.Equal(alphabet, AlphabetCharacters);

            //test all the characters now (we are not going to call Characters
            foreach (var requiredCharacter in alphabet)
            {
                //make sure its there
                Assert.Contains(requiredCharacter, resultOfCall);
            }
        }

    }

}
