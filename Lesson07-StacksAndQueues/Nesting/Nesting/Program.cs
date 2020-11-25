using System;
using System.Collections.Generic;

namespace Nesting
{
    class Program
    {
        public class Solver
        {
           
            public int Solution(String S)
            {
                Stack<char> brackets = new Stack<char>();
                foreach (var bracket in S)
                {
                    if ('('==bracket)
                    {
                        brackets.Push(bracket);
                        continue;
                    }
                    if (')'==bracket)
                    {
                        if (brackets.Count == 0)
                            return 0;
                       brackets.Pop();
                       }
                }
                if (brackets.Count != 0)
                    return 0;
                return 1;

            }
        }
        static void Main(string[] args)
        {
            var A = "((((";
            var B = "))))";
            var C = ")(";
            var D = "(()()()()((())))";
            var E="(()(())())";
            var F= "())";
            var solver = new Solver();
            Console.WriteLine(solver.Solution(A));
            Console.WriteLine(solver.Solution(B));
            Console.WriteLine(solver.Solution(C));
            Console.WriteLine(solver.Solution(D));
            Console.WriteLine(solver.Solution(E));
            Console.WriteLine(solver.Solution(F));
            Console.WriteLine("Hello World!");
        }
    }
}
