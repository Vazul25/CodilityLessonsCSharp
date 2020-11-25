using System;
using System.Collections.Generic;
using System.IO;

namespace CommonPrimeDivisors
{
    class Program
    {
 
        public static Dictionary<int, int> GetSmallestPrimeFactorTable(int N)
        {
            var factors = new HashSet<int>();
            Dictionary<int, int> smallestPrimeFactors = new Dictionary<int, int>((int)(N / Math.Log(N)));
            if (N == 1)
                return smallestPrimeFactors;


            int i = 2;
            var sqrtN = (int)Math.Sqrt(N);
            int limit = (N + 1) < N ? int.MaxValue : N + 1;
            while (i <= sqrtN)
            {
                int j = 2;
                int valueAtI;
                smallestPrimeFactors.TryGetValue(i, out valueAtI);
                if (valueAtI == 0)
                {
                    j = i;
                }
                while (i * j < limit)
                {
                    smallestPrimeFactors.TryGetValue(i * j, out valueAtI);
                    if (valueAtI == 0)
                        smallestPrimeFactors[i * j] = i;
                    j++;
                }
                i++;
            }
            return smallestPrimeFactors;
        }
        public static List<int> getFactors(int N, Dictionary<int, int> smallestPrimeFactors)
        {
            var factors = new List<int>();

            int x = N;
            int valueAtX;
            smallestPrimeFactors.TryGetValue(x, out valueAtX);
            while (valueAtX > 0)
            {
                factors.Add(valueAtX);
                x /= valueAtX;
                smallestPrimeFactors.TryGetValue(x, out valueAtX);

            }

            factors.Add(x);
            return factors;
        }
        
        //https://stackoverflow.com/questions/34251682/finding-common-prime-divisors-in-two-sets-of-numbers-quickly
        //Algo by vacawama   
        public static int solution(int[] A, int[] B)
        {
            int count = 0;
            for (int i = 0; i < A.Length; i++)
            {

                if (HasCommonPrimeDivisors(A[i], B[i]))
                    count++;
            }
            return count;
        }
        public static bool HasCommonPrimeDivisors(int A, int B)
        {

            var greater = Math.Max(A, B);
            var lesser = Math.Min(A, B);

            if (greater == lesser)
                return true;
            if (A == 1 || B == 1 || A == 0 || B == 0)
                return false;
            var greatestCommonDenom = GCD(lesser, greater);
            if (greatestCommonDenom == 1)
                return false;

            var a1 = reduceToOne(greater, greatestCommonDenom);
            var b1 = reduceToOne(lesser, greatestCommonDenom);

            if (a1 && b1)
                return true;
            return false;
        }

        public static bool reduceToOne(int a, int gcd)
        {

            while (true)
            {
                var denominator = GCD(a, gcd);
                var newa = a / denominator;
                if (denominator == 1 && a!=1)
                    return false;
                if (newa == 1)
                    return true;
                a = newa;
            }
        }
        public static int GCD(int a, int b)
        {
            if (a % b == 0)
                return b;
            return GCD(b, a % b);
        }
        static void Main(string[] args)
        {
            int[] A = new int[] { 2 * 3 * 5, 10, 3, 7, 9, 9, 10, 36 * 36 };
            int[] B = new int[] { 2 * 5 * 7, 30, 5, 35, 21, 12, 5, 6 };
            int[] A2 = new int[] { 15, 10, 9 };
            int[] B2 = new int[] { 75, 30, 5 };
            var primeTable = GetSmallestPrimeFactorTable(6000000);
            for (int i = 0; i < A2.Length; i++)
            {
                Console.WriteLine(String.Join(" ", getFactors(A2[i], primeTable).ToArray()));
                Console.WriteLine(String.Join(" ", getFactors(B2[i], primeTable).ToArray()));
                Console.WriteLine("===");
                Console.WriteLine(HasCommonPrimeDivisors(A2[i], B2[i]));
                Console.WriteLine("===");
                Console.WriteLine();
            }

          
        }
    }
}
