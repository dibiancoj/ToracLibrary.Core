using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Library.Core.ExtensionMethods
{
    public static class XElementExtensionMethods
    {

        #region Element Querying With A Namespace

        #region Public Methods

        /// <summary>
        /// Query an element with a namespace
        /// </summary>
        /// <param name="elementToQuery">Element to query</param>
        /// <param name="namespaceToUse">Namespace to use</param>
        /// <param name="nameToQuery">Name to query</param>
        /// <returns>Element found</returns>
        public static XElement Element(this XElement elementToQuery, XNamespace namespaceToUse, string nameToQuery)
        {
            //return the element with the namespace
            return elementToQuery.Element(namespaceToUse + nameToQuery);
        }

        /// <summary>
        /// Query an element with a namespace and return the elements
        /// </summary>
        /// <param name="elementToQuery">Element to query</param>
        /// <param name="namespaceToUse">Namespace to use</param>
        /// <param name="nameToQuery">Name to query</param>
        /// <returns>Elements found</returns>
        public static IEnumerable<XElement> Elements(this XElement elementToQuery, XNamespace namespaceToUse, string nameToQuery)
        {
            //return the element with the namespace
            return elementToQuery.Elements(namespaceToUse + nameToQuery);
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Query string value to query with a namespace
        /// </summary>
        /// <param name="namespaceToUse">Namespace to use</param>
        /// <param name="nameToQuery">Name to query</param>
        /// <returns>Name to use when we query</returns>
        private static XName NameToQueryWithNamespace(XNamespace namespaceToUse, string nameToQuery)
        {
            //just combine them
            return namespaceToUse + nameToQuery;
        }

        #endregion

        #endregion

        #region Remove Blank Elements

        /// <summary>
        /// Removes blank element's where there is no value
        /// </summary>
        /// <param name="xElementToRemoveBlanksFrom">XElement To Remove Blanks From</param>
        public static void RemoveBlankElements(this XElement xElementToRemoveBlanksFrom)
        {
            //i used this when i can't control the xml and i need to serialize it. 
            //so the xml is
            /*<root>
             * <item>jason</item>
             * <itemdate></itemdate>
             * </root>
             */

            //xml serialization can't handle nullable types. if the nil=true is there you don't need this. If it isn't there and you try to deserialize an item that is a blank string it will fail into a nullable type datetime?, bool?, decimal?, etc.
            //let's loop through all the descendants and where the value is null, remove it
            xElementToRemoveBlanksFrom.Descendants().Where(x => x.Value.IsNullOrEmpty()).Remove();
        }

        #endregion

    }
}
