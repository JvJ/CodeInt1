using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Miscellaneous definitions
namespace CodeInt1
{
    public delegate int Comparator<T>(T a, T b);

    public class Util
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static bool IsSorted<T>(Comparator<T> comp, IEnumerable<T> coll)
        {
            T last = default(T);
            bool firstElPassed = false;

            foreach (var x in coll)
            {
                if (!firstElPassed)
                {
                    firstElPassed = true;
                    last = x;
                    continue;
                }

                int c = comp(last, x);
                if (c > 0)
                {
                    return false;
                }
                last = x;
            }

            return true;
        }

        public static int Mod(int n, int x)
        {
            return ((n % x) + x) % x;
        }
    }
}
