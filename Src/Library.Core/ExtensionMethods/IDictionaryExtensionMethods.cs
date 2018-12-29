using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Core.ExtensionMethods
{

    /// <summary>
    /// Extension Methods For IDictionary
    /// </summary>
    public static class IDictionaryExtensionMethods
    {

        //I was going to add AddOrUpdate but you can just use the dictionary init to do that. ie: MyDictionary["Key"] = AddOrUpdateObject
        //TryGet and TryAdd is already an extension method in dot net core. They weren't brought over

        /// <summary>
        /// try to get the value from the dictionary first. if not found then go create it. Useful for cache like dictionary. Currently its in the concurrent dictionary
        /// </summary>
        /// <typeparam name="TKey">Type Of The Key Of The Dictionary</typeparam>
        /// <typeparam name="TValue">Type Of The Value Of The Dictionary</typeparam>
        /// <param name="dictionaryToUse">Dictionary to try to get the item or add the item</param>
        /// <param name="keyToTryToRetrieve">Key to try to retrieve with</param>
        /// <param name="valueCreator">func that creates the value if not found. This value will be inserted into the dictionary after it is created, then returned</param>
        /// <returns>TValue. Either from dictionary lookup or from the value creator</returns>
        public static TValue GetOrAdd<TValue, TKey>(this IDictionary<TKey, TValue> dictionaryToUse, TKey keyToTryToRetrieve, Func<TValue> valueCreator)
        {
            //go try to find the object in the dictionary
            if (dictionaryToUse.TryGetValue(keyToTryToRetrieve, out var valueToTryToFetch))
            {
                //we found the item in the dictionary, return it
                return valueToTryToFetch;
            }

            //we don't have it...so go create it
            TValue createdValue = valueCreator();

            //put it in the dictionary
            dictionaryToUse.Add(keyToTryToRetrieve, createdValue);

            //return the value now
            return createdValue;
        }

    }

}
