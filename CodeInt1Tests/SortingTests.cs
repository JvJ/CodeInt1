using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeInt1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeInt1.Tests.Util;

namespace CodeInt1.Tests
{
    [TestClass()]
    public class SortingTests
    {

        

        private void SortHelper(Action<Func<int,int,int>,int[]> sortAlg)
        {
            // We create 1000 int arrays of lengths varyin from 10 to 100 items, containing random integers.
            // Then, we check to ensure they are sorted

            int passCount = 0;
            int failCount = 0;
            var rnd = new Random();

            for (int i = 0; i < 1000; i++)
            {
                int len = rnd.Next(10, 100);
                int[] nums = new int[len];
                for (int j = 0; j < nums.Length; j++)
                {
                    nums[j] = rnd.Next();
                }
                Sorting.QuickSort((x, y) => x - y, nums);
                if (IsSorted(nums))
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

        [TestMethod()]
        public void QuickSortTest()
        {
            SortHelper(Sorting.QuickSort);
        }

        [TestMethod()]
        public void MergeSortTest()
        {
            SortHelper(Sorting.MergeSort);
        }
    }
}