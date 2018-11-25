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
    public class LinkedListTests
    {
        [TestMethod()]
        public void AddListsTest()
        {

            // This test will randomly generate 1000 numbers, convert them into lists,
            // add them, and then convert them back.

            Random rand = new Random();
            int passCount = 0;
            int failCount = 0;

            for (int i = 0; i < 1000; i++)
            {
                int num1 = rand.Next(99999);
                int num2 = rand.Next(99999);
                int sum = num1 + num2;

                Node<int> node1 = LinkedList.NumToList(num1);
                Node<int> node2 = LinkedList.NumToList(num2);
                Node<int> sumNode = LinkedList.AddLists(node1, node2);

                var ary = sum.ToString().ToCharArray();
                Array.Reverse(ary);
                string expected = new string(ary);
                string actual = string.Join("", sumNode);

                if (expected == actual)
                {
                    passCount++;
                }
                else
                {
                    failCount++;
                }
            }

            Assert.AreEqual(1000, passCount);
            Assert.AreEqual(0, failCount);
        }
    }
}