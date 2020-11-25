using System;
using System.Linq;

namespace Triangle
{
    class Program
    {
        public class TriangleFinder
        {
            private static bool TestTriangleStrong(long a, long b, long c)
            {
                long ab, ac, bc;
                try
                {
                     ab = checked(a + b);
                     ac = checked(a + c);
                     bc = checked(b+c);

                }
                catch (OverflowException) { 
                    return false;
                }
                
                if (ab <= c || ac <= b || bc <= a)
                    return false;
                return true;
            }
            private static bool TestTriangle(long a, long b, long c)
            {
                if (a + b < c || a + c < b || c + b < a)
                    return false;
                return true;
            }
            public int Solution1(int[] A)
            {
                 
                A = A.OrderByDescending(a => a).ToArray();
                for (int i = 2; i < A.Length; i++)
                {
                    if (TestTriangleStrong(A[i], A[i - 1], A[i - 2]))
                        return 1;
                }
                return 0;
            }
        }
        static void Main(string[] args)
        {
            int[] A = new int[] { 10, 2, 6, 1, 8, 20 };
            int[] B = new int[] { 10,50,5,1};
            int[] C = new int[] { 1, 1, 2, 3, 5 };
            int[] D = new int[] { Int32.MaxValue, Int32.MaxValue, Int32.MaxValue };
            var solver = new TriangleFinder();
            //Console.WriteLine(solver.Solution1(A));
            //Console.WriteLine(solver.Solution1(B));
            Console.WriteLine(solver.Solution1(D));
            Console.WriteLine("Hello World!");
        }
    }
}
