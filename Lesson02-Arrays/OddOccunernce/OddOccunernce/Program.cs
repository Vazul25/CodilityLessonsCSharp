using System;
using System.Collections.Generic;

namespace OddOccunernce
{
    class Program
    {
        static int solution(int[] A)
        {
            Dictionary<int, bool> unpaired = new Dictionary<int, bool>();
            for (int i = 0; i < A.Length; i++)
            {
                int currentValue = A[i];
                if (unpaired.ContainsKey(currentValue))
                {
                    unpaired.Remove(currentValue);
                } 
                else
                {
                    unpaired.Add(A[i], true);
                }

            }


            foreach (KeyValuePair<int, bool> entry in unpaired)
            {
                return entry.Key;
            }
            return 0;

        }
        static void Main(string[] args)
        {
            int[] a = { 9, 3, 9, 3, 9, 7, 9 };
            Console.WriteLine( Program.solution(a));
        }
    }
}
