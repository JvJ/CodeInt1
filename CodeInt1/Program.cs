using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeInt1
{

    class Program
    {

        public class Foo
        {
            public void DoSomething()
            {
                Console.WriteLine("HELLO");
            }

            public int GiveMeNumber()
            {
                return 0;
            }

            public IEnumerable<int> Numbers()
            {
                yield return 1;
                yield return 2;
                yield return 3;
                yield break;
            }
        }

        static void Main(string[] args)
        {
            //ListTests();
            // HeapTests();
            // StackTests();
            // SetofStacksTests();
            TreeTests();
            // GraphTests();
            // QueueTests();

            Console.WriteLine("Some mods: {0}", -1 % -10);

            
            Console.ReadKey();
        }

        static void ListTests()
        {
            Console.WriteLine("Hey there.");

            Node<int> myList = Node<int>.FromColl(new[] { 1, 2, 1, 3, 4, 3, 2, 5 });
            
            Console.WriteLine("My List: [{0}]", string.Join(",", myList));

            LinkedList.RemoveDuplicates(myList);

            Console.WriteLine("My After dedupes: [{0}]", string.Join(",", myList));

            Console.WriteLine("Indices 0: {0}, 1: {1}, 2: {2}",
                LinkedList.Nth(myList, 0).data,
                LinkedList.Nth(myList, 1).data,
                LinkedList.Nth(myList, 2).data);

            // Testing removal
            Node<string> strList = Node<string>.FromColl("The quick brown fox jumps over the lazy dog.".Split(' '));

            Console.WriteLine("The strList: \"{0}\"", string.Join("->", strList));

            // Removing the 7th ("lazy")
            var seventh = LinkedList.Nth(strList, 7);
            LinkedList.DeleteNode(seventh);
            Console.WriteLine("The strList after removal: \"{0}\"", string.Join("->", strList));

            // Testing the numbers being added up
            var n_123 = LinkedList.NumToList(123);
            Console.WriteLine("123 list: {0}", string.Join("", n_123));
            var n_246 = LinkedList.AddLists(n_123, n_123);
            Console.WriteLine("246 list: {0}", string.Join("", n_246));

            // Testing cycle detection
            var words = Node<string>.FromColl("This list stared going and it keeps".Split(' '));
            var last = words.Last();
            var cycleStart = words.Nth(3);
            last.Attach(cycleStart);

            Console.WriteLine("My list is this: {0}", string.Join(" ", words.Take(20)));

            var cycleFound = LinkedList.FindCycleStart(words);

            Console.WriteLine("Here is my cycle start: {0}", cycleFound.data);
        }

        static void HeapTests()
        {
            Heap<int> myHeap = new Heap<int>((x, y) => x - y);

            myHeap.Insert(new[] { 1, 3, 2, 10, 9, 3, 4, 7, 12, 20, 13, 14, 7 } );

            Console.WriteLine("My stuff: [{0}]", string.Join(",", myHeap.Poller()));
        }
         
        static void StackTests()
        {
            MyStack<int> myStack = new MyStack<int>();

            for (int i = 0; i < 21; i++)
            {
                myStack.Push(i);
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Popped: {0}", myStack.Pop());
            }

            for (int i = 0; i < 21; i++)
            {
                myStack.Push(i);
            }

            while (!myStack.IsEmpty())
            {
                Console.WriteLine("Final Pop: {0}", myStack.Pop());
            }
        }

        static void QueueTests()
        {
            var myQ = new MyQueue<int>();

            Console.WriteLine("I have a queue here.");

            foreach (int i in new[] { 1, 2, 3, 4, 5 })
            {
                myQ.Push(i);
            }

            // Let's cycle this for a while
            for (int i = 6; i < 10000; i++)
            {
                myQ.Pop();
                myQ.Push(i);
            }

            Console.WriteLine("Current queue cap: {0}", myQ.Capacity);
            Console.WriteLine("Current queue: [{0}]", string.Join(",", myQ));

            // Upsizing and downsizing
            myQ = new MyQueue<int>();
            for (int i = 0; i < 9; i++)
            {
                myQ.Push(i);
            }
            Console.WriteLine("Current queue cap: {0}", myQ.Capacity);
            Console.WriteLine("Current queue: [{0}]", string.Join(",", myQ));
            for (int i = 0; i < 5; i++)
            {
                myQ.Pop();
            }
            Console.WriteLine("Current queue cap: {0}", myQ.Capacity);

        }

        static void SetofStacksTests()
        {
            var myStack = new SetOfStacks<int>(3);


            foreach (int i in new[] { 1, 2, 3, 4, 5, 6, 7})
            {
                myStack.Push(i);
            }

            Console.WriteLine("Here is my stack: {0}", myStack.StackString());

            // Try popping 1 element
            Console.WriteLine("Element Popped: {0}", myStack.Pop());
            Console.WriteLine("After popping 1 element: {0}", myStack.StackString());
            Console.WriteLine("Element Popped: {0}", myStack.Pop());
            Console.WriteLine("After popping 2 element: {0}", myStack.StackString());

            Console.WriteLine("Popping from first substack: {0}", myStack.PopFromSubStack(0));
            Console.WriteLine("After that: {0}", myStack.StackString());

            // Pushing 3 new oes on
            myStack.Push(666);
            myStack.Push(777);
            myStack.Push(888);
            Console.WriteLine("After pushing new elems: {0}", myStack.StackString());

            Console.WriteLine("Popping all remaining elements:...");
            while (!myStack.IsEmpty())
            {
                Console.WriteLine("Popped: {0}", myStack.Pop());
            }
        }

        static void TreeTests()
        {
            // Just a utility function
            var t = BTNode<int>.UtilityFunc();

            var balancedTree1 =
                t(1,
                    t(2,
                        t(4, null, null),
                        t(5, null, null)),
                    t(3,
                        t(6, null, null),
                        t(7, null, null)));

            var balancedTree2 = 
                t(1,
                    t(2,
                        t(4, null, null),
                        t(5, null, null)),
                    t(3, null, null));

            var unbalancedTree1 =
                t(1,
                    t(2,
                        t(4,
                            t(8,
                                t(9, null, null),
                                null),
                            null),
                        t(5, null, null)),
                    t(3,
                        t(6, null, null),
                        t(7, null, null)));



            Console.WriteLine("Is bt1 balanced?: {0}", balancedTree1.IsBalanced());
            Console.WriteLine("Is bt2 balanced?: {0}", balancedTree2.IsBalanced());
            Console.WriteLine("Is ut1 balanced?: {0}", unbalancedTree1.IsBalanced());


            Console.WriteLine("Depth first of bt1: [{0}]", string.Join(",", balancedTree1.DepthFirst()));
            Console.WriteLine("Breadth first of bt1: [{0}]", string.Join(",", balancedTree1.BreadthFirst()));
        }

        public static void GraphTests()
        {
            var A = new DGraph<string>("A");
            var B = new DGraph<string>("B");
            var C = new DGraph<string>("C");
            var D = new DGraph<string>("D");
            var E = new DGraph<string>("E");
            var F = new DGraph<string>("F");

            A.Attach(B);
            A.Attach(D);
            B.Attach(C);
            B.Attach(F);
            C.Attach(E);
            E.Attach(B);

            Console.WriteLine("Route from A to F?: {0}", A.RouteTo(F));
            Console.WriteLine("Route from D to A?: {0}", D.RouteTo(A));
            Console.WriteLine("Route from E to E?: {0}", E.RouteTo(E));
            Console.WriteLine("Route from A to A?: {0}", A.RouteTo(A));
        }
    }
}
