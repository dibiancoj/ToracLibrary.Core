using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.ExtensionMethods
{

    /// <summary>
    /// Extension Methods For Byte Arrays
    /// </summary>
    public static class ByteArrayExtensionMethods
    {

        #region String To Hexadecimal

        /// <summary>
        /// Converts a byte array which is hashed to a string for encryption
        /// </summary>
        /// <param name="securityBytes">bytes to convert</param>
        /// <param name="toLowerCaseHash">To lower or uppcase hash. Will convert everything to uppercase if false</param>
        /// <returns>string which represents the bytes</returns>
        public static string ToByteArrayToHexadecimalString(this IEnumerable<byte> securityBytes, bool toLowerCaseHash)
        {
            //create a new string builder
            var builder = new StringBuilder();

            //format to use
            string formatToUse = toLowerCaseHash ? "x2" : "X2";

            //loop through the bytes
            foreach (var bytesToWrite in securityBytes)
            {
                //append it (x2 pushed to hexidecimal uppercase)
                builder.Append(bytesToWrite.ToString(formatToUse));
            }

            //return the string
            return builder.ToString();
        }

        #endregion

    }

}
