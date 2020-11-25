using System;
using System.Collections.Generic;
using System.Linq;

namespace MissingInt
{
    class Program
    {
        static int solution(int[] A)
        {
          
            Array.Sort(A);
            int max = 0;
            for (int i = 0; i < A.Length-1; i++)
            {
               
                if (A[i]>=0 && A[i] + 1 != A[i + 1] && A[i]!=A[i+1] ) return A[i] + 1;
                max = max > A[i] ? max: A[i];
                
            }
                max = max > A[A.Length-1] ? max: A[A.Length-1];

            return max ==0 ? 1 : max+1;
        }
        static int solution2(int[] A)
        {
            HashSet<int> numbers = new HashSet<int>(A);
            int[] candidates = Enumerable.Range(1, A.Length+1).ToArray();
            for (int i = 0; i < candidates.Count(); i++)
            {
                if(!numbers.Contains( candidates[i])) return candidates[i];
            }
            return -1;

        }
        static void Main(string[] args)
        {
            int[] test = new int[] { 1, 3, 6, 4, 1, 2 };
            int[] test2 = new int[] { -1,-2};
            int[] test3 = new int[] { -1, -3, 6, 4, 1, 2 };
            int[] test4 = new int[] { 1, 3, 6, -4, 1, 2 };
            Console.WriteLine(String.Join(',',Program.solution2(test)));
            Console.WriteLine(String.Join(',', Program.solution2(test2)));
                        Console.WriteLine(String.Join(',', Program.solution2(test3)));
                        Console.WriteLine(String.Join(',', Program.solution2(test4)));

        }
    }
}
