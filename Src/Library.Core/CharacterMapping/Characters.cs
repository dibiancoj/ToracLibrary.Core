using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.CharacterMapping
{

    /// <summary>
    /// Holds a list for alphabet and numeric characters
    /// </summary>
    public static class Characters
    {

        #region Constants

        /// <summary>
        /// Holds the alphabet in a string. 
        /// </summary>
        public const string AlphabetCharacters = "abcdefghijklmnopqrstuvwxyz";

        #endregion

        #region Methods

        /// <summary>
        /// Returns the number characters in the english language
        /// </summary>
        /// <returns>all the numbers in the english language in an iterator</returns>
        public static IEnumerable<int> AllNumberCharactersLazy()
        {
            //loop through the numbers and yield them
            for (int i = 0; i < 10; i++)
            {
                //return i
                yield return i;
            }
        }


        public static IEnumerable<char> AllAlphaBetCharactersString() => AlphabetCharacters;

        /// <summary>
        /// Returns the alphabet characters in the english language
        /// </summary>
        /// <returns>all the alphabet characters in the english language</returns>
        public static ReadOnlySpan<char> AllAlphaBetCharacters() => AlphabetCharacters.AsSpan();

        #endregion

    }

}
