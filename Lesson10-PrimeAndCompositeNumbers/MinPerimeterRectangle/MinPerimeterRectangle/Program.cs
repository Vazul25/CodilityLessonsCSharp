using System;

namespace MinPerimeterRectangle
{
    class Program
    {
        public class Solution
        {
            public static int solution(int N)
            {
                if (N == 0 || N==0 )
                    return 0;
                if (N == 1)
                    return 4;
                var factors = 2;
                var sqrt = Math.Sqrt(N);
                var limit = (int)sqrt;
                var divisorClosestToSqrt = 1;
                var perfectSqrt = sqrt % 1 == 0; // Math.Abs(Math.Ceiling(sqrt) - Math.Floor(sqrt)) < Double.Epsilon;
                if (perfectSqrt)
                    return 4 * (int)sqrt;
                for (int i = 2; i <= limit; i++)
                {
                    if (N % i == 0)
                    {
                        divisorClosestToSqrt = i;
                    }
                }

                return 2 * N / divisorClosestToSqrt + 2 * divisorClosestToSqrt;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Solution.solution( 30));
            for (int a = 1; a < 10; a++)
            {
                for (int b = 1; b < 10; b++)
                {
                    var N = a * b;
                    var expected = 2 * a + 2 * b;
                    var computedFromN = Solution.solution(N);
                   
                        Console.WriteLine($"a:{a} b:{b}  a*b:{N} 2(a+b):{expected} {computedFromN}");
                    
                }
            }
        }
    }
}
