using Library.Core.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Xunit;

namespace Library.Core.Tests.ExtensionMethods
{

    /// <summary>
    /// Unit test to test XElement Extension Methods
    /// </summary>
    public class XElementExtensionTest
    {

        #region Query With A Namespace

        #region Static Properties

        private static readonly XNamespace NamespaceToUse = "http://example.com/name";
        private static readonly XNamespace SchemaNamespaceToUse = "http://www.w3.org/2001/XMLSchema-instance";
        private const string IdAttributeName = "Id";

        #endregion

        [Fact(DisplayName = "Query Namespace with a single result")]
        public void QueryElementWithNamespaceTest1()
        {
            //row to test 
            var rowToTest = BuildXElementWithNamespace(1);

            //make sure this returns null because we are querying without a namespace
            Assert.True(rowToTest.Element("Example") == null);

            //now query our extension method which uses namespace
            var result = rowToTest.Element(NamespaceToUse, "Example");

            //make sure its not null
            Assert.False(result == null);

            //make sure the id is 0
            Assert.Equal("0", result.Attribute(IdAttributeName).Value);
        }

        [Fact(DisplayName = "Query Namespace with multiple nodes")]
        public void QueryElementsWithNamespaceTest1()
        {
            //how many to build
            const int howManyToBuild = 5;

            //row to test 
            var rowToTest = BuildXElementWithNamespace(howManyToBuild);

            //make sure this returns null because we are querying without a namespace
            Assert.True(rowToTest.Element("Example") == null);

            //now query our extension method which uses namespace
            var result = rowToTest.Elements(NamespaceToUse, "Example").ToArray();

            //make sure its not null
            Assert.False(result == null);

            //loop through the records
            for (int i = 0; i < howManyToBuild; i++)
            {
                //make sure the id the corresponding i
                Assert.Equal(i.ToString(), result[i].Attribute(IdAttributeName).Value);
            }
        }

        #region Framework

        /// <summary>
        /// Build a set of xelements with a namespace
        /// </summary>
        /// <param name="howManyRecordsToBuild">How many records to build</param>
        /// <returns>Root Element with child elements in it</returns>
        private static XElement BuildXElementWithNamespace(int howManyRecordsToBuild)
        {
            //<name:Example xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:name="http://example.com/name" Id="#"></name:Example>

            //root element
            var rootElement = new XElement("root");

            //loop through the rows
            for (int i = 0; i < howManyRecordsToBuild; i++)
            {
                rootElement.Add(new XElement(NamespaceToUse + "Example",
                            new XAttribute(IdAttributeName, i),
                            new XAttribute(XNamespace.Xmlns + "name", NamespaceToUse),
                            new XAttribute(XNamespace.Xmlns + "xsi", SchemaNamespaceToUse)));
            }

            //return the root
            return rootElement;
        }

        #endregion

        #endregion

        #region Remove Blank Elements

        /// <summary>
        /// Unit test to remove blank elements from XElement. 
        /// </summary>
        [Fact(DisplayName = "Should be no nodes removed since the element is not empty")]
        public void RemoveBlankElementsTest1()
        {
            //declare the xml so we can test it later
            const string testXml = "<root> " +
                                   "<jason id=\"2\">Jason</jason> " +
                                   "</root>";

            //throw the xml into an xelement
            var xElementToTest = XElement.Parse(testXml);

            //let's remove the blank (which there shouldn't be any of them)
            xElementToTest.RemoveBlankElements();

            //now make sure nothing has changed
            Assert.Equal(XElement.Parse(testXml).ToString(), xElementToTest.ToString());
        }

        /// <summary>
        /// Unit test to remove blank elements from XElement. 
        /// </summary>
        [Fact(DisplayName = "Remove 1 node since the element is empty")]
        public void RemoveBlankElementsTest2()
        {
            //declare the xml so we can test it later
            const string testXml = "<root> " +
                                   "<jason id=\"2\"></jason> " +
                                   "</root>";

            //throw the xml into an xelement
            var xElementToTest = XElement.Parse(testXml);

            //let's remove the blank
            xElementToTest.RemoveBlankElements();

            //now make sure the jason node is gone
            Assert.Equal(XElement.Parse("<root />").ToString(), xElementToTest.ToString());
        }

        /// <summary>
        /// Unit test to remove blank elements from XElement. 
        /// </summary>
        [Fact(DisplayName = "Remove 1 node when you have multiple nodes in the document")]
        public void RemoveBlankElementsTest3()
        {
            //declare the xml so we can test it later
            const string testXml = "<root> " +
                                   "<jason id=\"1\">jason1</jason> " +
                                   "<jason id=\"2\"></jason> " +
                                   "<jason id=\"3\">jason3</jason> " +
                                   "</root>";

            //throw the xml into an xelement
            var xElementToTest = XElement.Parse(testXml);

            //let's remove the blank (which there should be only id = 2 removed)
            xElementToTest.RemoveBlankElements();

            //now make sure the jason node is gone
            Assert.Equal(XElement.Parse("<root>" +
                                           "<jason id=\"1\">jason1</jason>" +
                                           "<jason id=\"3\">jason3</jason>" +
                                           "</root>").ToString(), xElementToTest.ToString());
        }

        /// <summary>
        /// Unit test to remove blank elements from XElement. 
        /// </summary>
        [Fact(DisplayName = "Remove multiple nodes when the document has multiple nodes")]
        public void RemoveBlankElementsTest4()
        {
            //declare the xml so we can test it later
            const string testXml = "<root> " +
                                   "<jason id=\"1\"></jason> " +
                                   "<jason id=\"2\"></jason> " +
                                   "<jason id=\"3\"></jason> " +
                                   "<subNode></subNode> " +
                                   "</root>";

            //throw the xml into an xelement
            var xElementToTest = XElement.Parse(testXml);

            //let's remove the blank (which there shouldn't be any of them)
            xElementToTest.RemoveBlankElements();

            //now make sure the all the nodes are gone
            Assert.Equal(XElement.Parse("<root />").ToString(), xElementToTest.ToString());
        }

        /// <summary>
        /// Unit test to remove blank elements from XElement. 
        /// </summary>
        [Fact(DisplayName = "Mix and match element names")]
        public void RemoveBlankElementsTest5()
        {
            //declare the xml so we can test it later
            const string testXml = "<root> " +
                                   "<jason id=\"1\">jason1</jason> " +
                                   "<jason id=\"2\"></jason> " +
                                   "<jason id=\"3\"></jason> " +
                                   "<subNode>" +
                                   "<SubNodeItem>s1</SubNodeItem>" +
                                   "<SubNodeItem></SubNodeItem>" +
                                   "</subNode> " +
                                   "</root>";

            //throw the xml into an xelement
            var xElementToTest = XElement.Parse(testXml);

            //let's remove the blank (which there shouldn't be any of them)
            xElementToTest.RemoveBlankElements();

            //now make sure the jason node is gone
            Assert.Equal(XElement.Parse("<root>" +
                                           "<jason id=\"1\">jason1</jason> " +
                                            "<subNode>" +
                                              "<SubNodeItem>s1</SubNodeItem>" +
                                            "</subNode> " +
                                           "</root>").ToString(), xElementToTest.ToString());
        }

        #endregion

    }

}
