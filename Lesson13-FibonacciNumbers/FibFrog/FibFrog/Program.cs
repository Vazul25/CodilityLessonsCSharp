using System;
using System.Collections.Generic;
using System.Linq;

namespace FibFrog
{
    public class FibFrog
    {
        private class Node
        {
            public int pos = 0;
            public int routeLength = 0;
            public Node(int pos, int routeLength)
            {
                this.pos = pos;
                this.routeLength = routeLength;
            }
        }
        private static List<int> PrecomputeFibonacciNumbers(int limit) {
            // 50.th fibonacci: 12586269025 which is greater than int.MaxValue
            // 100th fib: 354224848179261915075   if we use int64 100 would be more than enough 
            List<int> fibonacciNumbersList = new List<int>(50);
            fibonacciNumbersList.Add(1);
            fibonacciNumbersList.Add(1);
            for (int i = 2; limit >= fibonacciNumbersList[i - 1]; i++)
            {
                fibonacciNumbersList.Add(fibonacciNumbersList[i - 1] + fibonacciNumbersList[i - 2]);
            }
            return fibonacciNumbersList;
        }
        //100% with BFS
        public static int solution(int[] A)
        {
            var fibonacciNumbersList = PrecomputeFibonacciNumbers(A.Length);
            return TracePathStart(A, fibonacciNumbersList);
        }
        private static int TracePathStart(int[] A, List<int> fibonacciNumbers)
        {
            Queue<Node> bfsNodes = new Queue<Node>();
            HashSet<int> set = new HashSet<int>(A.Length / 2);
            bfsNodes.Enqueue(new Node(-1, 0));
            int shorthestPath = 0;
            while (bfsNodes.Count > 0)
            {
                Node node = bfsNodes.Dequeue();
                if (TraceNext(A, fibonacciNumbers, node.pos, node.routeLength, bfsNodes, out shorthestPath, set))
                {
                    return shorthestPath;
                };
            }
            return -1;

        }
        private static bool TraceNext(int[] A, List<int> fibonacciNumbers, int i, int routeLength, Queue<Node> bfsNodes, out int shortestPath, HashSet<int> beenThere)
        {
            shortestPath = 0;
            routeLength++;
            if (i > A.Length + 1)
                return false;
            int j = 1;
            int pos = 0;
            while (j < fibonacciNumbers.Count && (pos = i + fibonacciNumbers[j]) <= A.Length)
            {
                //Console.WriteLine(routeLength + ": " + pos + "  " + string.Join(" ", Enumerable.Range(0, pos).Select(_ => '_')) + " X " + string.Join(" ", Enumerable.Range(0, A.Length - pos).Select(_ => '_')));
                if (pos == A.Length)
                {
                    shortestPath = routeLength;
                    //Console.WriteLine();
                    //Console.WriteLine(fibonacciNumbers[j] + " " + i);
                    //Console.WriteLine();
                    return true;
                }
                if (A[pos] == 1 && !beenThere.Contains(pos))
                {
                    beenThere.Add(pos);
                    bfsNodes.Enqueue(new Node(pos, routeLength));
                }
                j++;
            }
            return false;
        }
        static void Main(string[] args)
        {
            int n = 0;
            int[] A = new int[] { 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0 };
            int[] A2 = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

            Console.WriteLine(solution(A2));
        }
    }
}
