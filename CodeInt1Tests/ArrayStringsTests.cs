using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeInt1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1.Tests
{
    [TestClass()]
    public class ArrayStringsTests
    {
        [TestMethod()]
        [Timeout(2000)]
        public void StringCharsUniqueTest()
        {
            Assert.IsFalse(ArrayStrings.StringCharsUnique("aaa"));
            Assert.IsFalse(ArrayStrings.StringCharsUnique("aab"));
            Assert.IsTrue(ArrayStrings.StringCharsUnique("abc"));
            Assert.IsTrue(ArrayStrings.StringCharsUnique(""));
        }

        [TestMethod()]
        [Timeout(2000)]
        public void StringCharsUniqueNoAddTest()
        {
            Assert.IsFalse(ArrayStrings.StringCharsUniqueNoAdd("aaa"));
            Assert.IsFalse(ArrayStrings.StringCharsUniqueNoAdd("aab"));
            Assert.IsTrue(ArrayStrings.StringCharsUniqueNoAdd("abc"));
            Assert.IsTrue(ArrayStrings.StringCharsUniqueNoAdd(""));
        }

        [TestMethod()]
        [Timeout(2000)]
        public void reverseCStrTest()
        {
            String t1 = "abcdefg";
            byte[] t1b = ASCIIEncoding.ASCII.GetBytes(t1);
            ArrayStrings.reverseCStr(t1b);
            String t1Final = ASCIIEncoding.ASCII.GetString(t1b);

            Assert.AreEqual(t1Final, "gfedcba");
        }

        [TestMethod()]
        [Timeout(2000)]
        public void CheckAnagramTest()
        {
            Assert.IsTrue(ArrayStrings.CheckAnagram("debit card", "bad credit"));
            Assert.IsTrue(ArrayStrings.CheckAnagram("dormitory", "dirtyroom"));
            Assert.IsFalse(ArrayStrings.CheckAnagram("dormitory", "dormitrory"));
        }

        [TestMethod()]
        [Timeout(2000)]
        public void SpaceReplaceTest()
        {
            Console.WriteLine(ArrayStrings.SpaceReplace("a b c"));
            Assert.AreEqual(ArrayStrings.SpaceReplace("a b c"), "a%20b%20c");
            Assert.AreEqual(ArrayStrings.SpaceReplace("abc"), "abc");
        }
    }
}