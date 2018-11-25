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
    public class HeapTests
    {

        [TestMethod()]
        public void AddTest()
        {
            var h = new Heap<string>(string.Compare);

            // Test that elements are added int he expected order.
            h.Add("Bob");
            Assert.AreEqual(h.Top, "Bob");
            h.Add("Alice");
            Assert.AreEqual(h.Top, "Alice");
            h.Add("Carl");
            Assert.AreEqual(h.Top, "Alice");

            Assert.AreEqual(h.Poll(), "Alice");
            Assert.AreEqual(h.Poll(), "Bob");
            Assert.AreEqual(h.Poll(), "Carl");
        }

        [TestMethod()]
        public void InsertTest()
        {
            var h1 = new Heap<int>((x, y) => x - y);
            var h2 = new Heap<int>((x, y) => x - y);

            // Start with a forward and reverse list and add them
            // each to their own heaps.
            // Telements should be removed in the correct order
            h1.Insert(Enumerable.Range(0, 10));
            h2.Insert(Enumerable.Range(0, 10).Reverse());

            while (!h1.IsEmpty() && !h2.IsEmpty())
            {
                Assert.AreEqual(h1.Poll(), h2.Poll());
            }
        }
    }
}