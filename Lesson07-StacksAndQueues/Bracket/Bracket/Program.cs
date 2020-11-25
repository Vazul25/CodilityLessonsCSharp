using System;
using System.Collections.Generic;

namespace Bracket
{
    class Program
    {

        public class Solver
        {
            private static HashSet<char> openingBrackets = new HashSet<char>() { '{', '(', '[' };
            private static HashSet<char> closingBrackets = new HashSet<char>() { '}', ')', ']' };

            private static bool isCloser(char a, char b)
            {
                if (a == '(' && b == ')' || a == ')' && b == '(')
                    return true;
                if (a == '[' && b == ']' || a == ']' && b == '[')
                    return true;
                if (a == '{' && b == '}' || a == '}' && b == '{')
                    return true;
                return false;
            }
            public int Solution(String S)
            {
                Stack<char> brackets = new Stack<char>();
                foreach (var bracket in S)
                {
                    if (openingBrackets.Contains(bracket))
                    {
                        brackets.Push(bracket);
                        continue;
                    }
                    if (closingBrackets.Contains(bracket))
                    {
                        if (brackets.Count == 0)
                            return 0;
                        var lastOpeningBracket = brackets.Pop();
                        if (!isCloser(lastOpeningBracket, bracket))
                        {
                            return 0;
                        }
                    }

                }
                if (brackets.Count != 0)
                    return 0;
                return 1;

            }
        }
        static void Main(string[] args)
        {
            var A = "{[()()]}";
            var B = "([)()]";
            var C = ")(";
            var solver = new Solver();
            Console.WriteLine(solver.Solution(A));
            Console.WriteLine(solver.Solution(B));
            Console.WriteLine(solver.Solution(C));
            Console.WriteLine("Hello World!");
        }
    }
}
