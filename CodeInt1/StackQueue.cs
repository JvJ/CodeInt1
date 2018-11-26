using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1
{
    public class MyStack<T>
    {
        protected const int DEFAULT_CAPACITY = 8;

        protected T[] m_array;
        protected int m_count;

        public MyStack()
        {
            m_array = new T[DEFAULT_CAPACITY];
            m_count = 0;
        }

        public virtual T Top => m_array[m_count - 1];

        public virtual T Pop()
        {
            if (m_count == 0)
            {
                throw new InvalidOperationException("Can't pop from empty stack.");
            }

            T ret = m_array[m_count - 1];
            if (m_count <= m_array.Length / 2)
            {
                shrink();
            }
            m_count--;

            return ret;
        }

        public virtual void Push(T val)
        {
            if (m_count >= m_array.Length )
            {
                grow();
            }

            m_array[m_count] = val;
            m_count++;
        }

        public bool IsEmpty()
        {
            return m_count <= 0;
        }

        private void grow()
        {
            int initial = m_array.Length;
            int final = m_array.Length * 2;

            T[] newArray = new T[m_array.Length * 2];
            m_array.CopyTo(newArray, 0);
            m_array = newArray;

            Console.WriteLine("Grew from {0} to {1}", initial, final);
        }

        private void shrink()
        {
            // We won't shrink any further than the initial default
            if (m_array.Length <= DEFAULT_CAPACITY)
            {
                return;
            }

            int initial = m_array.Length;

            int newLength = m_array.Length / 2;
            if (newLength < DEFAULT_CAPACITY) { newLength = DEFAULT_CAPACITY;  }

            T[] newArray = new T[newLength];

            Array.Copy(m_array, newArray, newLength);

            m_array = newArray;

            Console.WriteLine("Shrank from {0} to {1}", initial, newLength);
        }
    }

    // Problem 3.2 - This class has O(1) push, pop, and min operation
    public class MinStack<T>
    {
        private List<MinStackNode> m_buffer;
        private Comparator<T> m_comparator;

        public MinStack(Comparator<T> comparator)
        {
            m_buffer = new List<MinStackNode>();
            m_comparator = comparator;
        }

        public void Push(T item)
        {
            if (this.IsEmpty())
            {
                // The first element pushed in is automatically
                // the minimum
                m_buffer.Add(new MinStackNode(item, 0));
                return;
            }

            int minIndex = m_buffer.Last().minIndex;
            int compResult = m_comparator(item, m_buffer[minIndex].item);
            if (compResult < 0)
            {
                minIndex = m_buffer.Count;
            }

            m_buffer.Add(new MinStackNode(item, minIndex));
        }

        public T Top()
        {
            return m_buffer.Last().item;
        }
        
        public T Pop()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("Empty stack.");
            }

            T ret = m_buffer.Last().item;
            m_buffer.RemoveAt(m_buffer.Count - 1);
            return ret;
        }

        public T Min()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException("Empty stack.");
            }

            return m_buffer[m_buffer.Last().minIndex].item;
        }

        public bool IsEmpty()
        {
            return m_buffer.Count == 0;
        }

        private struct MinStackNode
        {
            public T item;
            public int minIndex;
            public MinStackNode(T item, int minIndex)
            {
                this.item = item;
                this.minIndex = minIndex;
            }
        }

    }

    // Problem 3.3
    // A set of stacks
    public class SetOfStacks<T>
    {
        private List<SubStack> m_stacks;
        private int m_stackSize;

        public SetOfStacks(int stackSize)
        {
            if (stackSize <= 0)
            {
                throw new ArgumentException("Stack size must be greater than 0.");
            }

            m_stacks = new List<SubStack>();
            m_stacks.Add(new SubStack(stackSize));
            m_stackSize = stackSize;
        }

        public void Push(T item)
        {
            var lastStack = m_stacks.Last();
            if (lastStack.TryPush(item))
            {
                return;
            }
            else
            {
                var newStack = new SubStack(m_stackSize);
                m_stacks.Add(newStack);
                if (!newStack.TryPush(item))
                {
                    throw new Exception("Something went wrong and I don't know why.");
                }
            }
        }

        public T Pop()
        {
            if (m_stacks.Count == 1 && m_stacks.Last().top == -1)
            {
                throw new InvalidOperationException("All stacks are empty.");
            }

            var topStack = m_stacks.Last();
            T ret = default(T);
            while (!topStack.TryPop(ref ret))
            {
                if (m_stacks.Count == 1)
                {
                    throw new InvalidOperationException("All stacks are empty.");
                }
                m_stacks.RemoveAt(m_stacks.Count - 1);
                topStack = m_stacks.Last();
            }

            return ret;
        }

        public T PopFromSubStack(int index)
        {
            T ret = default(T);

            if (m_stacks[index].TryPop(ref ret))
            {
                return ret;
            }
            else
            {
                throw new InvalidOperationException("Substack is empty.");
            }
        }

        public bool IsEmpty()
        {
            return m_stacks.Count == 1 && m_stacks[0].top < 0;
        }

        public string StackString()
        {
            string ret = "[\n";

            foreach (var sub in m_stacks)
            {
                ret += string.Format("[{0}]\n", string.Join(",", sub.buffer.Take(sub.top + 1)));
            }

            ret += "]\n";
            return ret;
        }


        private class SubStack
        {
            public T[] buffer;
            public int top;
            
            public SubStack(int size)
            {
                top = -1;
                buffer = new T[size];
            }

            public bool TryPush(T item)
            {
                if (top >= buffer.Length - 1)
                {
                    return false;
                }

                top++;
                buffer[top] = item;
                return true;
            }

            public bool TryPop (ref T item)
            {
                if (top < 0)
                {
                    return false;
                }

                item = buffer[top];
                top--;
                return true;
            }
        }
    }

    // Wrapping array implementation of queue
    public class MyQueue<T> : IEnumerable<T>
    {
        private const int DEFAULT_CAPACITY = 8;

        private T[] m_array;

        // Both start and end can go past the end of the array,
        // as long as the difference between them is less than
        // the size of the array
        private long m_start = 0;
        private long m_end = 0;

        public long Count => m_end - m_start;
        public long Capacity => m_array.Length;
        private long StartIdx => m_start % m_array.Length;
        private long EndIdx => m_end % m_array.Length;

        public MyQueue()
        {
            m_array = new T[DEFAULT_CAPACITY];
        }

        public void Push(T val)
        {
            if (Count >= m_array.Length)
            {
                resize(m_array.Length * 2);
            }

            m_array[EndIdx] = val;
            m_end++;
        }

        public T Pop()
        {
            if (Count == 0)
            {
                throw new InvalidOperationException("Empty queue.");
            }

            var ret = m_array[StartIdx];

            m_start++;
            if (m_array.Length > DEFAULT_CAPACITY &&
                Count <= m_array.Length / 4)
            {
                resize(m_array.Length / 2);
            }

            return ret;
        }

        private void resize (long size)
        {
            long newCap = Math.Max(size, DEFAULT_CAPACITY);
            long oldCount = Count;
            T[] newArray = new T[newCap];

            for (int i = 0; i < oldCount; i++)
            {
                newArray[i] = m_array[(m_start + i) % m_array.Length];
            }

            m_start = 0;
            m_end = oldCount;

            m_array = newArray;
        }

        public bool IsEmpty()
        {
            return Count == 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (long i = m_start; i < m_end; i++)
            {
                yield return m_array[i % m_array.Length];
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    // Problem 3.1: Implement a triple-stack
    // One possibility: use each n/3 block to create a stack
    // Other possibility: Maintain a list of structs with
    // back-pointers.
    // My solution: just use allocate in blocks of N elements.
    public class MultiStack<T>
    {
        public MultiStack(int numStacks)
        {
            // LEFTOFF: Do this later.  I already get the gist of it.
        }

    }




    // This is for the static methods
    // Problem 3.5 - Not important at the moent
    public class StackQueue<T>
    {
        private Stack<T> m_s1, m_s2;

        public StackQueue()
        {
            m_s1 = new Stack<T>();
            m_s2 = new Stack<T>();
        }


    }
    
    // Problem 3.6 : Sort a stack in ascending order, using only push, pop, peek, && isEmpty
    // Easiest I can think of: pop them onto an array, sort that, push them back on
    // However, this is probably not right.
    // It does look like I need another buffer of SOME kind, though
    // The solution presented uses another stack.  Try that one.
}
