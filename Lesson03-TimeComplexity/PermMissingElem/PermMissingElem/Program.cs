using System;
using System.Collections.Generic;
using System.Linq;
namespace PermMissingElem
{
    class Program
    {
        static int solution(int[] A)
        {
            Dictionary<int, bool> dict = A.ToDictionary(v => v, v => true);

            foreach (var item in Enumerable.Range(1, A.Length + 1))
            {
                if (!dict.ContainsKey(item))
                {
                    return item;
                }
            }
            return -1;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Program.solution(new int[]{ 2, 3, 1, 5 }));
        }
    }
}
