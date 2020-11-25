using System;
using System.Linq;

namespace MaxSliceSum
{
    class Program
    {
        static class Solution {
            public static int SolutionWithPostFix(int[] A) {
                var postfixSum = new int[A.Length];
                postfixSum[A.Length - 1] = Math.Max(A[A.Length - 1],0);
                for (int i = A.Length-2; i >=0 ; i--)
                {
                    postfixSum[i] = Math.Max(postfixSum[i + 1] + A[i], 0);
                }
                int max = Int32.MinValue;
                for (int i = 0; i < A.Length-1; i++)
                {
                    max = Math.Max(A[i] + postfixSum[i + 1],max);
                }
                max = Math.Max(max, A[A.Length-1]);
                return max;
            }
            public static int SolutionWithCatterPillar(int[] A) {
                int currentSum=0;
                int maxSum=Int32.MinValue;
                for (int i = 0; i < A.Length; i++)
                {
                    currentSum = Math.Max(currentSum + A[i], A[i]);
                    maxSum = Math.Max(currentSum, maxSum);
                }
                return maxSum;
            }
        }
    static void Main(string[] args)
        {
            var random = new Random();
            for(int i = 0; i < 2000; i++)
            {
                var data = new int[100].Select(_ => random.Next(-100000, 100)).ToArray();
                var Algorithm2 = Solution.SolutionWithPostFix(data);
                var Algorithm1 = Solution.SolutionWithCatterPillar(data);
                if (Algorithm1 != Algorithm2)
                {
                    Console.WriteLine();
                    Console.WriteLine($"catterpillar: {Algorithm1}  postfix: {Algorithm2}");
                    Console.WriteLine(String.Join(" ",data.Select(n=>n.ToString())));
                    Console.WriteLine();
                }
            }
            Console.WriteLine("Hello World!");
        }
    }
}
