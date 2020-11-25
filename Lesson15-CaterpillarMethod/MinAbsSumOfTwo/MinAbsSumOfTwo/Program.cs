using System;
using System.Linq;

namespace MinAbsSumOfTwo
{
    class Program
    {
        public static int Solution(int[] A) {
            if (A.Length == 1)
                return Math.Abs(A[0] + A[0]);
            var sortedA = A.OrderBy(u => u).ToArray();
            if (sortedA[A.Length - 1] < 0)
                return Math.Abs((sortedA[A.Length - 1] + sortedA[A.Length - 1]));
            var startindex = Array.BinarySearch(sortedA,0);
            if (startindex < 0) startindex = ~startindex;
            var tail = startindex - 1;
            var absSumMin = int.MaxValue;
            for(int head = startindex; head < A.Length-1; head++)
            {
                absSumMin = Math.Min(Math.Abs(sortedA[head] + sortedA[Math.Max(tail,0)]), absSumMin);
                while (tail>=0&&Math.Abs(sortedA[head] + sortedA[tail]) < Math.Abs(sortedA[head + 1] + sortedA[tail]))
                {
                    absSumMin = Math.Min(Math.Abs(sortedA[head] + sortedA[Math.Max(tail, 0)]), absSumMin);
                    tail--;

                }
                if (absSumMin == 0)
                    return 0;

            }
            absSumMin = Math.Min(Math.Abs(sortedA[A.Length - 1] + sortedA[Math.Max(tail, 0)]), absSumMin);

            return absSumMin;


        }
        static void Main(string[] args)
        {
            int[] A1 = new int[] { -8, 4, 5, -10, 3 };
            int[] A6 = new int[] { -8, -4, -5, -10, -3 };
            int[] A2 = new int[] { 1,4,-3 };
            int[] A7 = new int[] { -6 };
            int[] A3 = new int[] { 1,4,-3,-1,5 };
            int[] A4 = new int[] { 1,4,-3,6,7,8,4,5,};
            int[] A10 = new int[] { -1000000000, -999999999 };
            int[] A5 = new int[] { 1,4,-7,11,25,-65,-18,-15 };
            
            Console.WriteLine(Solution(A1));
            Console.WriteLine(Solution(A2));
            Console.WriteLine(Solution(A3));
            Console.WriteLine(Solution(A4));
            Console.WriteLine(Solution(A5));
            Console.WriteLine(Solution(A6));
            Console.WriteLine(Solution(A7));
            Console.WriteLine(Solution(A10));
            Console.WriteLine( -1000000000);
            Console.WriteLine( -999999999);
            Console.WriteLine( -1000000000+ -999999999);
            Console.WriteLine(-1*( -1000000000+ -999999999));
            Console.WriteLine( int.MinValue);
            Console.WriteLine(int.MaxValue);


        }
    }
}
