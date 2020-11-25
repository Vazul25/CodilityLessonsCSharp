using System;
using System.Linq;

namespace CountFactors
{
    class Program
    {
        static class Solution
        {
            public static int NaiveSolution(int n)
            {
                int factors = 0;
                for (int i = 1; i <= n; i++)
                {
                    if (n % i == 0)
                    {
                        factors++;
                    }
                }
                return factors;
            }
            public static int solution(int N)
            {

                if (N == 0)
                    return 0;
                if (N == 1)
                    return 1;
                var factors = 2;
                var sqrt = Math.Sqrt(N);
                var limit = (int)sqrt;
                var perfectSqrt = sqrt % 1 == 0; // Math.Abs(Math.Ceiling(sqrt) - Math.Floor(sqrt)) < Double.Epsilon;

                for (int i = 2; i <= limit; i++)
                {
                    if (N % i == 0)
                    {
                        factors += 2;
                    }
                }
                if (perfectSqrt)
                    factors--;
                return factors;
            }
        }
        static void Main(string[] args)
        {
            var random = new Random();
            for (int i = 1; i < 200000; i++)
            {
                var algo1 = Solution.NaiveSolution(i);
                var algo2 = Solution.solution(i);
                if (algo1 != algo2)
                    Console.WriteLine($"i: {i}  NaiveSolution: {algo1} algo2:{algo2}");
            }
       
      
        }
    }
}
