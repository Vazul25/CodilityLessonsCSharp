using System;
using System.Collections;
using System.Collections.Generic;

namespace Fish
{
    class Program
    {
        public static int Solution(int[] A, int[] B) {
            Stack<int> fishDownstream = new Stack<int>();
            int survivors = 0;

            for (int i = 0; i < A.Length; i++)
            {
                if (B[i] == 1)
                {
                    fishDownstream.Push(A[i]);
                }
                else
                {
                    while (fishDownstream.Count > 0 && fishDownstream.Peek() < A[i])
                        fishDownstream.Pop();
                    if (fishDownstream.Count == 0)
                        survivors++;
                }
            }
            survivors += fishDownstream.Count;
            return survivors;

        }
        static void Main(string[] args)
        {
            int[] ASiz = new int[] { 4, 3, 2, 1, 5 };
            int[] ADir = new int[] { 0,1,0,0,0 };

            int[] ASiz2 = new int[] { 4, 3, 2, 1, 5 };
            int[] ADir2 = new int[] { 1, 0, 0, 0, 0 };

            int[] ASiz3 = new int[] { 6, 7, 2, 1, 5 };
            int[] ADir3 = new int[] { 1, 1, 1, 1, 0 };

            int[] ASiz4 = new int[] { 4, 3, 2, 1, 5 };
            int[] ADir4 = new int[] { 1, 0, 0, 0, 1 };

            int[] ASiz5 = new int[] { 4, 3, 2, 1, 5 };
            int[] ADir5 = new int[] { 1, 1, 1, 1, 1 };

            int[] ASiz6 = new int[] { 4, 3, 2, 1, 5 };
            int[] ADir6 = new int[] {0,0,0,0,0 };
            Console.WriteLine(Solution(ASiz,ADir));
            Console.WriteLine(Solution(ASiz2,ADir2));
            Console.WriteLine(Solution(ASiz3,ADir3));
            Console.WriteLine(Solution(ASiz4,ADir4));
            Console.WriteLine(Solution(ASiz5,ADir5));
            Console.WriteLine(Solution(ASiz6,ADir6));
        }
    }
}
