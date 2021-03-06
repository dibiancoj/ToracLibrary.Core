﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.ExtensionMethods
{

    /// <summary>
    /// Extension Methods For Objects
    /// </summary>
    public static class ObjectExtensionMethods
    {

        #region Casting - Type Checking

        /// <summary>
        /// Generic way to cast an object to T. This way you don't need parenthesis when you need to cast it. Just a shorter sytnax instead of ((T)myobject).Field. 
        /// </summary>
        /// <typeparam name="TTo">Object type to convert to</typeparam>
        /// <param name="objectToConvert">Object to convert</param>
        /// <returns>TTo or throws an InvalidCastException if it can't cast it</returns>
        /// <exception cref="InvalidCastException">InvalidCastException if it the conversion is not successful</exception>
        public static TTo Cast<TTo>(this object objectToConvert)
        {
            return (TTo)objectToConvert;
        }

        /// <summary>
        /// Generic way to run object as...T. This way you don't need parenthesis when you need to cast it. Just a shorter sytnax instead of (myobject as T).Field. 
        /// </summary>
        /// <typeparam name="TTo">Object type to convert to</typeparam>
        /// <param name="objectToTest">Object to convert</param>
        /// <returns>TTo or null if it can't cast</returns>
        public static TTo As<TTo>(this object objectToTest)
            where TTo : class
        {
            return objectToTest as TTo;
        }

        /// <summary>
        /// Short hand syntax for the 'is' statement. Using a not equal with is results in extra noise. Adding or's and and's makes it even worse. This is just easier syntax so you don't need brackets.
        /// </summary>
        /// <typeparam name="TIsCheck">Type to check if the parameter passed in is derived or the actual object type.</typeparam>
        /// <param name="objectToEvaluate">Object to run a is against with the TIsCheckType</param>
        /// <returns>If the object passed in is derived or is the actual object type</returns>
        public static bool Is<TIsCheck>(this object objectToEvaluate)
        {
            return objectToEvaluate is TIsCheck;
        }

        #endregion

        #region Single Object To List

        /// <summary>
        /// Pushes a single object to IEnumerable of that object type (T)
        /// </summary>
        /// <typeparam name="T">Type Of The Item Passed In</typeparam>
        /// <param name="itemToPutInArray">Item To Push Into The IEnumerable</param>
        /// <returns>IEnumerable Of That Object Type, With The Item In The IEnumerable</returns>
        public static IEnumerable<T> ToIEnumerableLazy<T>(this T itemToPutInArray)
        {
            //just return this item
            yield return itemToPutInArray;
        }

        /// <summary>
        /// Pushes a single object to a list of that object type (T) and returns IList Of T
        /// </summary>
        /// <typeparam name="T">Type Of The Item Passed In</typeparam>
        /// <param name="itemToPutInArray">Item To Push Into The IEnumerable</param>
        /// <returns>IList Of That Object Type, With The Item In The IList</returns>
        public static IList<T> ToIList<T>(this T itemToPutInArray)
        {
            //instead of calling the overload which will create an iterator for no reason, we will just return a list
            return new List<T> { itemToPutInArray };
        }

        #endregion

    }

}
