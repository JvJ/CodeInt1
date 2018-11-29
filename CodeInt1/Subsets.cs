using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Code interview problem 8.3.
///
/// It works, even though it's not great.
/// Also on leetcode: https://leetcode.com/problems/subsets-ii/
/// </summary>
namespace CodeInt1
{
    public class Subsetss
    {    

        public IList<IList<int>> SubsetsWithDup (int[] nums)
        {
            return Subsets(nums, 0);
        }

        private List<IList<int>> Subsets(int[] nums, int start)
        {
            var ret = new List<IList<int>>();

            if (start == nums.Length)
            {
                if (!ret.Exists((x) => x.Count == 0))
                {
                    ret.Add(new List<int>());
                }
            }
            else
            {
                var subs = Subsets(nums, start + 1);

                foreach (var list in subs)
                {
                    var list2 = new List<int>(list);
                    list2.Insert(0, nums[start]);
                    if (!ret.Exists(x => x.SequenceEqual(list)))
                    {
                        ret.Add(list);
                    }
                    if (!ret.Exists(x => x.SequenceEqual(list2)))
                    {
                        ret.Add(list2);
                    }
                }
            }

            return ret;
        }
    }
}
