using System;

namespace CyclycRotation
{
    class Solution
    {
        public int[] solution(int[] A, int K)
        {
            int[] result = new int[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                result[(i + K % A.Length) % A.Length] = A[i];
            }
            return result;

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            int[] A = new int[] { 3, 8, 9, 7, 6 };
            int[] B = new int[] { 1,2,3,4};
            int[] C = new int[] { 1,2,3,4};
            Solution s = new Solution();
           
            Console.WriteLine($"[{String.Join(", ", s.solution(A,3))}]");
            Console.WriteLine($"[{String.Join(", ", s.solution(B,201))}]");
            Console.WriteLine($"[{String.Join(", ", s.solution(C,4))}]");
        }
    }
}
