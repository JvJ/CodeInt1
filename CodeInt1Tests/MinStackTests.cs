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
    public class MinStackTests
    {
        [TestMethod()]
        public void MinStackTest()
        {
            var minStack = new MinStack<int>((x, y) => x - y);

            foreach (var i in new[] { 1, 2, 3, 4, 5 })
            {
                minStack.Push(i);
            }

            Assert.AreEqual(5, minStack.Top());
            Assert.AreEqual(1, minStack.Min());

            minStack.Push(-1);

            Assert.AreEqual(-1, minStack.Top());
            Assert.AreEqual(-1, minStack.Min());

            Assert.AreEqual(-1, minStack.Pop());

            Assert.AreEqual(1, minStack.Min());
        }
    }
}