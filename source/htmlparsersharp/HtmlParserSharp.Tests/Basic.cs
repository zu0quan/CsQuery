﻿using System;
using System.Linq;
using CsQuery;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlParserSharp.Tests
{
    [TestClass]
    public class BigDom
    {
        static CQ Dom;

        /// <summary>
        /// This method ensures that the huge DOM gets parsed correctly by checking a few key selectors. The biggest factors that
        /// can cause problems are self-closing tags. See: TagHasImplicitClose function.
        /// 
        ///  Note that changing in that function
        ///  
        ///  case DomData.tagLI:
        ///     return newTagID == DomData.tagLI || newTagId == DomData.tagUL || newTagId == DomData.tagOL;
        ///     
        /// (e.g. checking for any other list type opener, instead of just the same one that's currently open, as it is now) causes
        /// the HTML5 spec document to parse differently than Chrome and these tests to fail. I am not sure why since I don't think that
        /// OL is a valid child of UL > LI or UL is a valid child of OL > LI. But apparently they do that in the document somewhere,
        /// e.g. open a differently-typed list as a child of an LI.
        /// 
        /// </summary>
        [ TestMethod]
        public void DomParsingTestWithNthChild()
        {

            // these values have been verified in Chrome with jQuery 1.7.2

            Assert.AreEqual(2704, Dom["div span:first-child"].Length);
            Assert.AreEqual(2517, Dom["div span:only-child"].Length);
            Assert.AreEqual(2, Dom["[type]"].Length);
            Assert.AreEqual(505, Dom["div:nth-child(2n+1)"].Length);
            Assert.AreEqual(13, Dom["div:nth-child(3)"].Length);
            Assert.AreEqual(534, Dom["div:nth-last-child(2n+1)"].Length);
            Assert.AreEqual(7, Dom["div:nth-last-child(3)"].Length);
            Assert.AreEqual(2605, Dom["div span:last-child"].Length);
        
        }

        [ TestMethod]
        public void AutoGeneratedTags()
        {

            // these values have been verified in Chrome with jQuery 1.7.2

            Assert.AreEqual(110, Dom["tbody"].Length);

        }


        [ClassInitialize]
        public static void ReadLargeDoc(TestContext context)
        {
            var parser = new CsQueryParser();
            //Dom = CQ.Create(parser.Parse("C:\\projects\\csharp\\csquery\\source\\CsQuery.Tests\\Resources\\html standard.htm"));
            Dom = CsQuery.Utility.Support.GetFile("HtmlParserSharp.Tests\\Resources\\html standard.htm");
        }

        
    }
}