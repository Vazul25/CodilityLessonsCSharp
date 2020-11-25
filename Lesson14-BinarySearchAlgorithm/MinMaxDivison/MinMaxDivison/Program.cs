using System;
using System.Collections.Generic;
using System.Linq;

namespace MinMaxDivison
{
    class Program
    {
        bool CheckMinSumCandidate(int[] A, int blockNumber, int sumCandidate)
        {
            var blockSum = 0;
            var blockRemaining = blockNumber;

            for (int i = 0; i < A.Length; i++)
            {
                blockSum += A[i];
                if (blockSum > sumCandidate)
                {
                    blockSum = A[i];
                    blockRemaining--;
                }
                if (blockRemaining == 0)
                {
                    return false;
                }
            }
            return true;
        }
        int Solution(int blockNumber, int maxnumber, int[] A)
        {
            var begin = Math.Max(A.Max(), (int)Math.Ceiling((double)(A.Sum() / blockNumber)));
            var end = begin + maxnumber + 1;
            int result = 0;
            while (begin <= end)
            {
                var blockMinSumCandidate = (begin + end) / 2;
                var suc = CheckMinSumCandidate(A, blockNumber, blockMinSumCandidate);
                if (suc)
                {
                    end = blockMinSumCandidate - 1;
                    result = blockMinSumCandidate;
                }
                else
                {
                    begin = blockMinSumCandidate + 1;
                }
            }
            return result;

        }
     
        static void Main(string[] args)
        {
            int[] A = new int[] { 2, 1, 5, 1, 2, 2, 2 };
            int K = 3;
            int M = 5;
            var solver = new Program();
            Console.WriteLine(A[0]);
            Console.WriteLine(solver.Solution(K, M, A));
        }
    }
}
