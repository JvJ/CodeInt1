using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeInt1.Util;

namespace CodeInt1
{
    // Note: by default, this is considered a min heap,
    // in the sense that comparing a node to its child should
    // yield <= 0
    public class Heap<T> : IEnumerable<T>
    {

        public static int DEFAULT_SIZE = 16;

        private readonly List<T> m_list;
        private readonly Comparator<T> m_comp;

        public Heap(Comparator<T> comp)
        {
            m_list = new List<T>(DEFAULT_SIZE);
            m_comp = comp;
        }

        public void Add(T val)
        {
            m_list.Add(val);

            int idx = m_list.Count - 1;

            // Now, bubble up
            while (parent(idx) >= 0
                && m_comp(m_list[idx], m_list[parent(idx)]) < 0)
            {
                // Swap
                T temp = m_list[idx];
                m_list[idx] = m_list[parent(idx)];
                m_list[parent(idx)] = temp;
                idx = parent(idx);
            }
        }

        public void Insert(IEnumerable<T> elems)
        {
            foreach (var e in elems)
            {
                this.Add(e);
            }
        }

        public T Poll()
        {
            T ret = m_list[0];

            // Strategy here is to copy the last element into the root position,
            // Then balance-down
            m_list[0] = m_list[m_list.Count - 1];
            m_list.RemoveAt(m_list.Count - 1);

            int idx = 0;

            while (leftChild(idx) < m_list.Count)
            {
                // First, get the min child, then compare
                int lc = leftChild(idx);
                int rc = rightChild(idx);
                int childIdx = leftChild(idx);
                if (rightChild(idx) < m_list.Count)
                {
                    int compChildren = m_comp(m_list[leftChild(idx)], m_list[rightChild(idx)]);
                    childIdx = compChildren <= 0 ? leftChild(idx) : rightChild(idx);
                }

                int compToChild = m_comp(m_list[idx], m_list[childIdx]);

                // Swap down if we need to
                if (compToChild > 0)
                {
                    T temp = m_list[idx];
                    m_list[idx] = m_list[childIdx];
                    m_list[childIdx] = temp;
                    idx = childIdx;
                }
                else
                {
                    break;
                }
            }
            
            return ret;
        }

        public T Top
        {
            get { return m_list[0]; }
        }

        public IEnumerable<T> Poller()
        {
            while (m_list.Count > 0)
            {
                yield return this.Poll();
            }
            yield break;
        }

        public bool IsEmpty()
        {
            return m_list.Count == 0;
        }

        // Helper functions
        private static int leftChild(int n)
        {
            return 2 * n + 1;
        }

        private static int rightChild(int n)
        {
            return 2 * n + 2;
        }

        private static int parent(int n)
        {
            return (n - 1) / 2;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)m_list).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<T>)m_list).GetEnumerator();
        }
    }
}
