using System;
using System.Collections.Generic;

namespace Ladder
{
    class Program
    {
        
        class Solution {
            Dictionary<int, int[]> fibtableCache = new Dictionary<int, int[]>();
            int[] CalculateNthFibMod2PowM(int n, int m)
            {
                var fibtable = new int[n];
                var modulo = (int) Math.Round( Math.Pow(2, m));
                fibtable[0] = 1 % modulo;
                fibtable[1] = fibtable[0];
                for (int i = 2; i < n; i++)
                {
                    fibtable[i] = (fibtable[i - 1] + fibtable[i - 2]) % modulo;
                }
                return fibtable;
            }
            //given two non-empty arrays A and B of L integers,
            //returns an array consisting of L integers specifying the consecutive answers;
            //position I should contain the number of different ways of climbing the ladder
            //with A[I] rungs modulo 2B[I].

            //100% could be optimised some more
            public int[] solution(int[] A, int[] B)
            {
                Dictionary<int, int> maxNForModulus = new Dictionary<int, int>(A.Length);
                for (int i = 0; i < A.Length; i++)
                {
                    var modulus = B[i];
                    if (!maxNForModulus.ContainsKey(modulus))
                    {
                        maxNForModulus.Add(B[i], A[i]);
                    }
                    else
                    {
                        maxNForModulus[modulus] = Math.Max(A[i], maxNForModulus[modulus]);
                    }
                }
                foreach (var entry in maxNForModulus)
                {
                    fibtableCache.Add(entry.Key, CalculateNthFibMod2PowM(entry.Value+1, entry.Key));
                }
                var results = new int[A.Length];
                for (int i = 0; i < A.Length; i++)
                {
                    var cachedTable = fibtableCache[B[i]];
                        results[i] = cachedTable[A[i ] ];
                     
                }
                return results;
            }
        
        }
        static void Main(string[] args)
        {
            int[] A = new int[] { 4, 4, 5, 5, 1,3 };
            int[] B = new int[] { 3, 2, 4, 3, 1,1 };
            var solver = new Solution();
            Console.WriteLine(String.Join(" ",solver.solution(A,B)));
        }
    }
}
