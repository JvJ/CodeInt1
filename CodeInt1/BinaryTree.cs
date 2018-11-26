using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInt1
{
    // Technically, not all binary trees are search trees, necessarily
    // We don't yet have any tree balancing algorithms

    // We'll take a much more procedural approach here, using mostly 
    public class BTNode<T>
    {
        public T Data { get; set; }
        public BTNode<T> Left { get; set; }
        public BTNode<T> Right { get; set; }

        public BTNode(T data)
        {
            Data = data;
            Left = null;
            Right = null;
        }

        public BTNode(T data, BTNode<T> left, BTNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }

        public IEnumerable<BTNode<T>> Children
        {
            get
            {
                if (Left != null) { yield return Left; }
                if (Right != null) { yield return Right; }
                yield break;
            }
        }

        // Just a utility function to create an easier-to-use function
        // for creating trees
        public static Func<T, BTNode<T>, BTNode<T>, BTNode<T>> UtilityFunc()
        {
            return (d, l, r) => new BTNode<T>(d, l, r);
        }

        // Problem 4.1
        // Implement a function to check if a tree is balanced. For the purposes of this question,
        // a balanced tree is defined to be a tree such that no two leaf nodes differ in distance
        // from the root by more than one.
        public bool IsBalanced()
        {
            int minDepth = int.MaxValue;
            int maxDepth = 0;

            MaxMinDepth(0, ref minDepth, ref maxDepth);

            return maxDepth - minDepth <= 1;
        }

        private void MaxMinDepth(int depth, ref int minDepth, ref int maxDepth)
        {
            depth += 1;

            Left?.MaxMinDepth(depth, ref minDepth, ref maxDepth);
            Right?.MaxMinDepth(depth, ref minDepth, ref maxDepth);

            // This only applies to leaf nodes
            if (Left == null && Right == null)
            {
                if (depth < minDepth)
                {
                    minDepth = depth;
                }
                if (depth > maxDepth)
                {
                    maxDepth = depth;
                }
            }
        }

        public IEnumerable<T> BreadthFirst()
        {
            foreach (var n in BreadthFirstNodes())
            {
                yield return n.Data;
            }
            yield break;
        }

        public IEnumerable<BTNode<T>> BreadthFirstNodes()
        {
            var q = new MyQueue<BTNode<T>>();

            q.Push(this);
            while (!q.IsEmpty())
            {
                var node = q.Pop();
                yield return node;
                foreach (var c in node.Children)
                {
                    q.Push(c);
                }
            }
            yield break;
        }

        public IEnumerable<T> DepthFirst()
        {
            foreach (var n in DepthFirstNodes())
            {
                yield return n.Data;
            }
            yield break;
        }

        public IEnumerable<BTNode<T>> DepthFirstNodes()
        {
            yield return this;

            foreach (var child in Children)
            {
                foreach (var n in child.DepthFirstNodes())
                {
                    yield return n;
                }
            }

            yield break;
        }

        // Problem 4.3 - Create a minimal height bin tree from a sorted array
        // NOTE: For now, I'm going to assume that the tree itself doesn't need to 
        // be sorted.  If it does, I'll re-do it
        public static BTNode<T> TreeFromSorted(Comparator<T> comp, T[] array)
        {
            if (!Util.IsSorted(comp, array))
            {
                throw new ArgumentException("Array is not sorted.");
            }

            return null;
        }

        // Problem 4.4 - Create a linked list of all nodes at a given depth
        // My approach: it's just a variation of the breadth-first iteration,
        // But depths are considered as well.
        public LinkedList<BTNode<T>> ListAtDepth(int depth)
        {
            var q = new MyQueue<Tuple<BTNode<T>, int>>();

            var t = (1, 2);
            //var (a,b) = (this, 0);
            //q.Push((this, 0));
        }
    }
}
