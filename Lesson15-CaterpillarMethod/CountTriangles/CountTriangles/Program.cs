using System;
using System.Linq;

namespace CountTriangles
{
    class Program
    {
        public static int Solution(int[] A, int N)
        {

            var sortedA = A.OrderBy(a => a).ToArray();
            int tail = 0, mid = 1, head = 2;
            int sum = 0;
            for (tail = 0; tail < sortedA.Length - 2; tail++)
            {
                head = tail + 2;
                for (mid=tail+1; mid < A.Length-1; mid++)
                {
                    while (head<A.Length&&Check(sortedA[head], sortedA[mid], sortedA[tail])){
                        head++;
                    }
                    sum += head - mid - 1;
                }
            }
            return sum;
        }
        public static bool Check(int greatest,int mid,int smallest)
        {
            if (greatest - mid < smallest)
                return true;
            return false;
        }
        public static void Draw(int[] A, int head, int tail, int slice)
        {

            Console.WriteLine($"({tail} {head})");

            Console.WriteLine("Slice: " + slice);
            foreach (var item in A)
            {
                Console.Write("{0,3}", item);
            }
            Console.WriteLine();
            if (tail == head)
            {
                Console.Write(String.Join("", Enumerable.Repeat(String.Format("{0,3}", " "), tail)));
                Console.Write("{0,3}", 'X');
            }
            else
            {
                Console.Write(String.Join("", Enumerable.Repeat(String.Format("{0,3}", " "), tail)));
                Console.Write("{0,3}", 'T');
                Console.Write(String.Join("", Enumerable.Repeat(String.Format("{0,3}", " "), head - tail - 1)));
                Console.Write("{0,3}", 'H');
            }

            Console.WriteLine();

        }
        static void Main(string[] args)
        {
            Console.WriteLine(Solution(new int[] { 10,2,5,1,8,12},10));
        }
    }
}
