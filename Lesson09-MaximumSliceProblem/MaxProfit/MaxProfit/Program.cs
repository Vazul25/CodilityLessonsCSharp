using System;
using System.Linq;

namespace MaxProfit
{
    class Program
    {
        static class Solution
        {
            public static int SolutionWithPostfixMax(int[] A)
            {
                if (A.Length == 0 || A.Length == 1)
                    return 0;
                var postfixMax = new int[A.Length];
                postfixMax[A.Length - 1] = A[A.Length - 1];
                for (int i = A.Length - 2; i >= 0; i--)
                {
                    postfixMax[i] = Math.Max(postfixMax[i + 1], A[i + 1]);
                }
                int max = 0;
                for (int i = 0; i < A.Length; i++)
                {
                    max = Math.Max(max, postfixMax[i] - A[i]);
                }
                return max;
            }

            public static int NaiveSolutionForTesting(int[] A)
            {
                int max = 0;
                for (int i = 0; i < A.Length - 1; i++)
                {
                    max = Math.Max(max, A[Range.StartAt(i + 1)].Max() - A[i]);
                }
                return max;
            }
        }

        static void Main(string[] args)
        {

            var TestA1 = new int[] { 23171, 21011, 21123, 21366, 21013, 21367 };
            Console.WriteLine(Solution.SolutionWithPostfixMax(TestA1));
            Console.WriteLine(Solution.NaiveSolutionForTesting(TestA1));
            Random schrandom = new Random();
            for (int i = 0; i < 100; i++)
            {
                var data = new int[4000].Select(_ => schrandom.Next(0, 200000)).ToArray();
                var naive = Solution.NaiveSolutionForTesting(data);
                var postfix = Solution.SolutionWithPostfixMax(data);
                if (Solution.SolutionWithPostfixMax(data) != Solution.NaiveSolutionForTesting(data))
                {
                    Console.WriteLine(naive+" "+postfix);
                }
            }
            Console.WriteLine( "done");

        }
    }
}
