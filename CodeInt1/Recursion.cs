using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1
{
    public static class Recursion
    {
        // Problem 8.1
        public static IEnumerable<int> Fibs()
        {
            int last1 = 0;
            int last2 = 1;

            yield return 0;
            yield return 1;
            while (true)
            {
                int current = last1 + last2;
                last1 = last2;
                last2 = current;
                yield return current;
            }
        }

        // Problem 8.4 - Permutations

        // The generalized problem is implemented later.  The string
        // Case is just a wrapper around it
        public static IEnumerable<string> StringPermutations(string s)
        {
            List<char> list = s.ToList();
            foreach (List<char> c in Permutations(list))
            {
                yield return new string(c.ToArray());
            }
        }

        public static IEnumerable<List<T>> Permutations<T>(List<T> initial)
        {
            return Generate(initial.Count, new List<T>(initial));
        }

        // This is an implementation of Heap's algorithm
        // (The creator is named Heap, it has nothing to do with heaps)
        private static IEnumerable<List<T>> Generate<T>(int n, List<T> a)
        {
            if (n == 1)
            {
                yield return new List<T>(a);
            }
            else
            {
                for (int i = 0; i < n-1; i++)
                {
                    foreach (var g in Generate(n-1, a)) { yield return g; }

                    int swapIdx = n % 2 == 0 ? i : 0;
                    T temp = a[swapIdx];
                    a[swapIdx] = a[n - 1];
                    a[n - 1] = temp; 
                }
                foreach (var g in Generate(n-1, a)) { yield return g; }
            }
        }

        // Problem 8.8 in the book.  The N queens problem.
        public static IList<IList<string>> SolveNQueens(int n)
        {
            return null;
        }

        // We're treating the numbers as representing each queen's "height"
        // in column n.
        public static bool CheckQueens(List<int> queens)
        {
            int end = queens.Count();

            // Go through each column
            for (int c1 = 0; c1 < end; c1++)
            {
                // For each column, go until the last column
                // looking for collisions on diagonals
                int q = queens[c1];
                for (int c2 = c1 + 1; c2 < end; c2++)
                {
                    // A diagonal collision occurs whenever the horizontal
                    // and vertical differences are identical
                    
                }
            }

            return false;
        }
    }
}
