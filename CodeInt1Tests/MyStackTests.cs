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
    public class MyStackTests
    {
        [TestMethod()]
        public void MyStackTest()
        {
            var stack = new MyStack<int>();
            
            foreach (var i in new[] { 1,2,3,4 })
            {
                stack.Push(i);
            }

            foreach (var i in new[] { 4,3,2,1 })
            {
                var val = stack.Pop();
                Assert.AreEqual(i, val);
            }

            Assert.IsTrue(stack.IsEmpty());
        }
    }
}