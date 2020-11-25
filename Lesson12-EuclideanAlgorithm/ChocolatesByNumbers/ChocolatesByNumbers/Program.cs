using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace ChocolatesByNumbers
{
    class Program
    {
        public static void Draw(int N, int M)
        {
            bool[] visited = new bool[N];
            int i = 0;

            while (true)
            {
                Console.WriteLine();
                var visitPos = i * M >= N ? (i * M) % N : i * M;

                for (int j = 0; j < visited.Length; j++)
                {
                    if (j == visitPos)
                        Console.Write("C ");
                    else
                    if (visited[j])
                        Console.Write("V ");
                    else
                        Console.Write("_ ");
                }

                if (visited[visitPos])
                {
                    break;
                }
                visited[visitPos] = true;

                i++;

            }

        }
        public static int Gcd(int a,int b)
        {
            if (a % b == 0)
                return b;
            return Gcd(b, a % b);
        }
        public static int solution(int N, int M)
        {
            //Draw(N, M);
            //Console.WriteLine();
            var lcm = (long)N * M / Gcd(N, M);
          
             return (int) (lcm/M);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(solution(947853, 4453)); //-16657 expected 947853
            Console.WriteLine(solution(12, 12));
            Console.WriteLine(solution(15, 6));

            Console.WriteLine(solution(10, 4));
            Console.WriteLine(solution(6, 3));
            Console.WriteLine(solution(12, 5));
            Console.WriteLine(solution(10, 4));
        }
    }
}
