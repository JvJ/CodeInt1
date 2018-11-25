using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1
{
    public class Sorting
    {
        public static void QuickSort<T>(Func<T,T,int> comparator, T[] array)
        {
            QuickSort(comparator, array, 0, array.Length);
        }

        public static void QuickSort<T>(Func<T,T,int> comp, T[] array, int l, int r)
        {
            if (l < r)
            {
                int p = Partition(comp, array, l, r);
                QuickSort(comp, array, l, p);
                QuickSort(comp, array, p + 1, r);
            }
        }

        public static int Partition<T>(Func<T,T,int> comp, T[] array, int l, int r)
        {
            T pivot = array[r - 1];
            int i = l - 1;

            for (int j = l; j < r - 1; j++)
            {
                int c = comp(array[j], pivot);
                if (c < 0 && i != j)
                {
                    i++;
                    Swap(ref array[j], ref array[i]);
                }
            }

            i++;
            Swap(ref array[i], ref array[r - 1]);

            return i;
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        public static void MergeSort<T>(Func<T,T,int> comparator, T[] array)
        {
            MergeSort(comparator, array, new T[array.Length], 0, array.Length);
        }

        // The helper method
        public static void MergeSort<T>(Func<T,T,int> comparator, T[] array, T[] tempArray, int left, int right)
        {
            if (right - left <= 1)
            {
                return;
            }

            int mid = (left + right) / 2;
            MergeSort(comparator, array, tempArray, left, mid);
            MergeSort(comparator, array, tempArray, mid, right);
            Merge(comparator, array, tempArray, left, right);
        }

        public static void Merge<T>(Func<T,T,int> comp, T[] array, T[] tempArray, int left, int right)
        {
            int mid = (left + right) / 2;
            int l = left;
            int r = mid;
            int idx = left;

            while (l < mid && r < right)
            {
                int c = comp(array[l], array[r]);
                if (c <= 0)
                {
                    tempArray[idx] = array[l];
                    l++;
                }
                else
                {
                    tempArray[idx] = array[r];
                    r++;
                }
                idx++;
            }

            for (; l < mid; l++)
            {
                tempArray[idx] = array[l];
                idx++;
            }

            for (; r < right; r++)
            {
                tempArray[idx] = array[r];
                idx++;
            }

            // Now, merge into the original array
            for (int i = left; i < right; i++)
            {
                array[i] = tempArray[i];
            }
        }
    }
}
