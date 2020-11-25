using System;
using System.Collections.Generic;
using System.Linq;

namespace PermCheck
{
    class Program
    {
       
        static int solution(int[] A)
        {
            HashSet<int> dict = new HashSet<int>(from x in A select x);
            var range = Enumerable.Range(1, A.Length).ToArray();
            for (int i = 0; i < A.Length; i++)
            {
                if (!dict.Contains(range[i])) return 0;
            }
            return 1;
        }
        static int solution2(int[] A)
        {
            HashSet<int> dict = new HashSet<int>(from x in A select x);
            var range = Enumerable.Range(1, A.Length).ToArray();
            for (int i = 0; i < A.Length; i++)
            {
                if (!dict.Contains(range[i])) return 0;
            }
            return 1;
        }
        static void Main(string[] args)
        {
            int[] A = new int[4];
            A[0] = 4;
            A[1] = 1;
            A[2] = 3;
            A[3] = 2;
            Console.WriteLine(Program.solution(A));
        }
    }
}
