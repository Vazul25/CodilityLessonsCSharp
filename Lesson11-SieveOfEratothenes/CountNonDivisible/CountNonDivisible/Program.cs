using System;
using System.Collections.Generic;
using System.Linq;

namespace CountNonDivisible
{
    class Program
    {
       
       
        public static int[] solution(int[] A) {
            var maxElement = A.Max();
            var count = new int[maxElement+1];
            for (int i = 0; i < A.Length; i++)
            {
                count[A[i]] += 1;
            }
            Dictionary<int, int?> cachedDivisors = new Dictionary<int, int?>( A.Length);
            var result = new int[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                cachedDivisors.TryGetValue(A[i], out var resulti);
                if (resulti != null)
                {
                    result[i] = (int)resulti;
                    continue;
                }
                int divisors = 0;
                for (int j = 1; j*j <= A[i]; j++)
                {
                    if (A[i] % j == 0)
                    {
                        divisors += count[j];
                        if (A[i] / j != j)
                        {
                            divisors += count[A[i] / j];
                        }
                    }
                }
                result[i] = A.Length - divisors;
                cachedDivisors[A[i]] = result[i];
            }

            return result;
             



        }
        static void Main(string[] args)
        {
            Console.WriteLine(String.Join(" ",solution(new int[] { 3,1,2,3,6}) ));
        }
    }
}
