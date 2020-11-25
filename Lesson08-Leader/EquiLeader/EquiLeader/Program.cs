using System;
using System.Collections.Generic;
using System.Linq;

namespace EquiLeader
{
    class Program
    {
        class Solution
        {
            public int solution(int[] A)
            {
                Dictionary<int, int> CountOfValue = new Dictionary<int, int>();
                int maxCount = 0;
                int euiCount = 0;
                int currentLeader = 0;
                int postfixLeader;
                Dictionary<int, int> postfixLeaders = PostfixLeaders(A);
                for (int i = 0; i < A.Length; i++)
                {
                    int currentCount;
                    CountOfValue.TryGetValue(A[i], out currentCount);
                    currentCount++;
                    CountOfValue[A[i]] = currentCount;
                    if (maxCount < currentCount)
                    {
                        maxCount = currentCount;
                        currentLeader = A[i];
                    }
                    if ((i + 1) / 2 < maxCount)
                    {
                        if (currentLeader == 0)
                        {
                            try
                            {
                                postfixLeader = postfixLeaders[i + 1];
                            }
                            catch
                            {   // this can be assigned anything at this point but 0
                                postfixLeader = -1;
                            }
                        }
                        else
                        {   //this is faster than try catch, should benchmark if the added if mitigates this. 
                            //But we cant use this when the zero is the leader
                            postfixLeaders.TryGetValue(i + 1, out postfixLeader);
                        }
                        if (postfixLeader == currentLeader)
                            euiCount++;
                    }
                }
                return euiCount;
            }

            public static Dictionary<int, int> PostfixLeaders(int[] A)
            {
                Dictionary<int, int> leadersInSubArrays = new Dictionary<int, int>();
                Dictionary<int, int> CountOfValue = new Dictionary<int, int>();
                int maxCount = 0;
                int maxValue = 0;
                int currentCount = 0;
                for (int i = A.Length - 1; i >= 0; i--)
                {
                    CountOfValue.TryGetValue(A[i], out currentCount);
                    currentCount++;
                    CountOfValue[A[i]] = currentCount;
                    if (maxCount < currentCount)
                    {
                        maxCount = currentCount;
                        maxValue = A[i];
                        
                    }
                    if (maxCount > (A.Length - i) / 2)
                        leadersInSubArrays[i] = maxValue;
                }
                return leadersInSubArrays;
            }
        }
        static void Main(string[] args)
        {
            var solver = new Solution();
            var testArray = Enumerable.Range(1, 300).Select((n, i) => i >= 50 && i < 250 ? Int32.MinValue : n).ToArray();
            ;
            //var TestA = new int[] { 4, 3, 4, 4, 4, 2 };
            
            var TestA = new int[] { 4, 4, 2, 5, 3, 4, 4, 4 };
            var TestA2 = new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 1 };
            var TestA3 = new int[] { 3,3,3, 3, 3, 3, 3, 2, 2, 2, 2, 2 };
            var TestA4 = new int[] { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 3 };
            var TestA5 = new int[] { 3, 2, 2, 3, 3, 3 };
            Console.WriteLine(solver.solution(TestA));
            Console.WriteLine(solver.solution(TestA2));
            Console.WriteLine(solver.solution(TestA3));
            Console.WriteLine(solver.solution(TestA4));
            Console.WriteLine(solver.solution(TestA5));
            Console.WriteLine(solver.solution(testArray));

        }
    }
}
