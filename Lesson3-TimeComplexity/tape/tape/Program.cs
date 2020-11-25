using System;
using System.Collections.Generic;
using System.Linq;

namespace tape
{
    class Program
    {
        static int solution(int[] A)
        {
            var sum = A.Sum();
            int diff = 0, maxDiff = Int32.MaxValue, sumBehind = 0;
            for (var i =0; i<A.Length-1;i++)
            {
                int tapeValue = (A[i] + sumBehind);
                diff = Math.Abs(tapeValue-(sum-tapeValue));
                maxDiff = diff > maxDiff ? diff : maxDiff;
                sumBehind += A[i];
                Console.WriteLine($"{A[i]}, {diff}, {sumBehind}, {sum}, {maxDiff}");

            }
            return maxDiff;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Program.solution(new int[] {3,1,2,4,3}));
        }
    }
}
