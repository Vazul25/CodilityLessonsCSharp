using System;
using System.Collections.Generic;

namespace Dominator
{
    class Program
    {
        class Solution
        {
            public int solution(int[] A)
            {
                Dictionary<int, int> CountOfValue = new Dictionary<int, int>();
                int maxCount = 0;
                int currentCount = 0;
                int maxIndex=0;

               for (int i = 0; i<A.Length;i++)
                {
                    CountOfValue.TryGetValue(A[i], out currentCount);
                    currentCount++;
                    CountOfValue[A[i]] = currentCount;
                    if (maxCount < currentCount) {
                        maxCount = currentCount;
                        maxIndex = i;
                        if (A.Length / 2 < maxCount)
                            return maxIndex;
                    }                   
                }
                return -1;
            }
        }
        static void Main(string[] args)
        {
            var solver = new Solution();
            var TestA = new int[] { 3, 2, 4, 3, 2, 3, -1, 3, 3, 3, 10 };
            var TestA2 = new int[] { 3,3,3,3,3,3,3,3,1 };
            var TestA3 = new int[] { 3,3,3,3,3,2,2,2,2,2 };
            var TestA4 = new int[] { 3,3,3,3,3,2,2,2,2,2,3 };
            var TestA5= new int[] {3,2,2 };

            Console.WriteLine(solver.solution(TestA));
            Console.WriteLine(solver.solution(TestA2));
            Console.WriteLine(solver.solution(TestA3));
            Console.WriteLine(solver.solution(TestA4));
            Console.WriteLine(solver.solution(TestA5));
        }
    }
}
