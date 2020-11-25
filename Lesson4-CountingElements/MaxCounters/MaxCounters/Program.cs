using System;
using System.Collections.Generic;
using System.Linq;

namespace MaxCounters
{
    class Program
    {
        public static int[] solution(int N, int[] A)
        {
            int[] counters = new int[N];
            int lastMax = 0;
            int max = 0;
            int counterIndex;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == (N + 1))
                {
                    lastMax = max;
                }
                else
                {
                    counterIndex = A[i] - 1;
                    counters[counterIndex] = counters[counterIndex] < lastMax ? lastMax + 1 : counters[counterIndex] + 1;
                    max = max < counters[counterIndex] ? counters[counterIndex] : max;
                }
                //Console.WriteLine("[{0}]", string.Join(", ", counters));

            }
            for (int i = 0; i < counters.Length; i++)
            {
                if (counters[i] < lastMax)
                {
                    counters[i] = lastMax;
                }
            }
            return counters;
        }
        static void Main(string[] args)
        {
            int[] A = new int[7];
            A[0] = 3;
            A[1] = 4;
            A[2] = 4;
            A[3] = 6;
            A[4] = 1;
            A[5] = 4;
            A[6] = 4;
            Console.WriteLine("[{0}]", string.Join(", ", Program.solution(5, A)));
        }
    }
}
