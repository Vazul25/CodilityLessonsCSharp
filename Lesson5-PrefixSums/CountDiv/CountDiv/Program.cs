using System;
//given three integers A, B and K, returns the number of integers within the range[A..B] that are divisible by K
namespace CountDiv
{
    class Program
    {
        static int Solution(int A, int B, int K)
        {
            if (A == B && A == 0) return 1;
            if (A > B || K==0 || K>B) return 0;
            int smallest;
            if (A <= K)
            {
                smallest = K;
            }
            else
            {
                smallest = A + A % K;
            }
            if (smallest > B) return 0;

            int greatest = (B / K) * K;
            int result = (greatest - smallest) / K ;
            if (A == 0) result++;
            return result + 1;
        }
        static void Main(string[] args)
        {
            Console.WriteLine($"{Program.Solution(0,5,2    )}");
            Console.WriteLine($"{Program.Solution(6, 8, 2   )}");
            Console.WriteLine($"{Program.Solution(6, 10, 2  )}");
            Console.WriteLine($"{Program.Solution(6, 6, 2   )}");
            Console.WriteLine($"{Program.Solution(6, 12, 2  )}");
            Console.WriteLine($"{Program.Solution(12, 12, 2 )}");
            Console.WriteLine($"{Program.Solution(20, 12, 2 )}");
            Console.WriteLine($"P2");

            Console.WriteLine($"{Program.Solution(7, 11, 2  )}");
            Console.WriteLine($"{Program.Solution(7, 8, 2   )}");
            Console.WriteLine($"{Program.Solution(7, 10,2   )}");
            Console.WriteLine($"{Program.Solution(7, 6, 2   )}");
            Console.WriteLine($"{Program.Solution(7, 12, 2  )}");
            Console.WriteLine($"{Program.Solution(13, 12, 2 )}");
            Console.WriteLine($"{Program.Solution(21, 12, 2 )}");
            Console.WriteLine($"P3");

            Console.WriteLine($"{Program.Solution(6, 11,3 )}");
            Console.WriteLine($"{Program.Solution(6, 8, 3 )}");
            Console.WriteLine($"{Program.Solution(6, 10,3 )}");
            Console.WriteLine($"{Program.Solution(6, 6, 3 )}");
            Console.WriteLine($"{Program.Solution(6, 12,3 )}");
            Console.WriteLine($"{Program.Solution(12, 12,3 )}");
            Console.WriteLine($"{Program.Solution(20, 12,3 )}");
            Console.WriteLine($"P3");
            Console.WriteLine($"{Program.Solution(6, 11, 30)}");
            Console.WriteLine($"{Program.Solution(6, 8, 30)}");
            Console.WriteLine($"{Program.Solution(6, 10, 30)}");
            Console.WriteLine($"{Program.Solution(6, 6, 30)}");
            Console.WriteLine($"{Program.Solution(6, 12, 30)}");
            Console.WriteLine($"{Program.Solution(12, 12, 30)}");
            Console.WriteLine($"{Program.Solution(20, 60, 1)}");
        }
    }
}

