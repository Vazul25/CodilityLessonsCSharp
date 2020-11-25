using System;

namespace FrogJump
{
    class Program
    {
        static int solution(int X, int Y, int D)
        {
            return (int) Math.Ceiling( (double)((Y - X)) / D);
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Program.solution(10,85,30));
        }
    }
}
