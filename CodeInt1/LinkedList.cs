using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1
{

    public class Node<T> : IEnumerable<T>
    {
        public T data;
        public Node<T> next;

        public Node(T data)
        {
            this.data = data;
            this.next = null;
        }

        public Node(T data, Node<T> next)
        {
            this.data = data;
            this.next = next;
        }

        public Node<T> Append(T data)
        {
            return this.Append(new Node<T>(data));
        }

        // The append method does *not* guard against cycles!!
        public Node<T> Append(Node<T> node)
        {
            Node<T> n = this;
            while (n.next != null)
            {
                n = n.next;
            }
            n.next = node;
            return node;
        }

        public static Node<T> FromColl(ICollection<T> coll)
        {
            Node<T> ret = null;
            Node<T> end = null;

            foreach (T c in coll)
            {
                if (end == null)
                {
                    ret = end = new Node<T>(c);
                }
                else
                {
                    end.next = new Node<T>(c);
                    end = end.next;
                }
            }

            return ret;
        }

        // Attach nodes in an unsafe manner in order to create a cycle
        public void Attach(Node<T> n)
        {
            this.next = n;
        }

        // Gets the node at the end of the list
        // Warning: If there are cycles, this will explode
        public Node<T> Last()
        {
            for (Node<T> n = this; n != null; n = n.next)
            {
                if (n.next == null)
                {
                    return n;
                }
            }
            return null;
        }

        public Node<T> Nth(int n)
        {
            Node<T> ret = this;
            while (n > 0 && ret != null)
            {
                n--;
                ret = ret.next;
            }
            return ret;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (Node<T> n = this; n != null; n = n.next)
            {
                yield return n.data;
            }

            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class LinkedList
    {
        
        
        // Most of tehse algorithms  assume an acyclic linked-list!

        // Problem 2.1
        public static void RemoveDuplicates<T>(Node<T> list)
        {
            // Currently, this is being done with a hash set, but there is certainly a better way
            HashSet<T> seen = new HashSet<T>();

            Node<T> prev = list;
            Node<T> current = list.next;

            seen.Add(prev.data);
            while (current != null)
            {
                if (seen.Contains(current.data))
                {
                    prev.next = current.next;
                    current = prev.next;
                    // GC will delete it for us
                }
                else
                {
                    seen.Add(current.data);
                    current = current.next;
                    prev = prev.next;
                }
            }
        }

        // Problem 2.1 bonus
        public static void RemoveDuplicates_NoBuffer<T>(Node<T> list)
        {

        }

        // Problem 2.2
        public static Node<T> Nth<T>(Node<T> node, int n)
        {
            while (n > 0 && node != null)
            {
                n--;
                node = node.next;
            }
            return node;
        }

        // Problem 2.3
        // It's kind of a trick: Copy data from the next node into this one,
        // Then delete the next node!!
        public static void DeleteNode<T>(Node<T> node)
        {
            if (node.next != null)
            {
                node.data = node.next.data;
                node.next = node.next.next;
            }
        }

        // Problem 2.4
        // Summing a list of digits ( I think I saw this one on leetcode )
        public static Node<int> AddLists(Node<int> a, Node<int> b)
        {
            int carry = 0;
            int sum = 0;
            Node<int> ret = null;
            Node<int> end = null;
            while (a != null || b != null)
            {
                sum = (a != null ? a.data : 0) + (b != null ? b.data : 0) + carry;
                carry = sum / 10;
                sum = sum % 10;
                Node<int> n = new Node<int>(sum);
                if (ret == null)
                {
                    ret = end = n;
                }
                else
                {
                    end.next = n;
                    end = n;
                }
                if (a != null) { a = a.next; }
                if (b != null) { b = b.next; }
            }

            if (carry > 0)
            {
                end.next = new Node<int>(carry);
            }

            return ret;
        }

        // Helper for problem 2.4 - Create a linked list representation of a number
        public static Node<int> NumToList(int n)
        {

            Node<int> ret = null;

            foreach (char c in n.ToString())
            {
                if (c >= '0' && c <= '9')
                {
                    ret = new Node<int>((int)(c - '0'), ret);
                }
                else
                {
                    throw new FormatException("Invalid character: "+c);
                }
            }

            return ret;
        }

        // Problem 2.5 : Find the node at the start of a cycle.
        // A null cycle-start indicates the absence of a cycle.
        public static Node<T> FindCycleStart<T>(Node<T> n)
        {
            Node<T> slow = n, fast = n;
            int slowCount = 0, fastCount = 0;

            while (fast != null
                && fast.next != null
                && slow != null)
            {
                slow = slow.next;
                slowCount += 1;
                fast = fast.next.next;
                fastCount += 2;

                if (slow == fast)
                {
                    break;
                }
            }

            if (fast == null || slow == null)
            {
                return null;
            }

            int diff = fastCount - slowCount;

            slow = fast = n;
            for (int i = 0; i < diff; i++)
            {
                fast = fast.next;
            }

            while (slow != fast)
            {
                slow = slow.next;
                fast = fast.next;
            }

            return slow;
        }
    }
}
