using System;
using System.Collections.Generic;
using System.Linq;

namespace CountDistinctSlices
{
    public class Program
    {
        public static int Solution(int M, int[] A)
        {
            if (A.Length == 0)
            { return 0; }
            if (A.Length == 1)
                return 1;
            Dictionary<int, int> valuesWithPositions = new Dictionary<int, int>(M / 2);
            int tail = 0;
            int head = 0;
            int slice = 0;

            while (head != A.Length)
            {
                //Draw(A, head, tail, slice); 

                while (head < A.Length && !valuesWithPositions.ContainsKey(A[head]))
                {
                    slice += head - tail + 1;

                    valuesWithPositions.Add(A[head], head);
                    head++;
                   // Draw(A, head, tail, slice);
                    if (slice > 1000000000)
                        return 1000000000;

                }
                if (head >= A.Length)
                {
                    return slice;
                }
                 
                var newTail = valuesWithPositions[A[head]];
                valuesWithPositions[A[head]] = head;
                
                for (int i = tail; i < newTail; i++)
                {
                    valuesWithPositions.Remove(A[i]);
                }
                tail = newTail + 1;
                slice += head - tail + 1;
                if (slice > 1000000000)
                    return 1000000000;
                head++;

            }
            if (slice > 1000000000)
                return 1000000000;
            //slice += head - tail+1;
            return slice;
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
            int[] A1 = new int[] { 3, 4, 5, 5, 2 };
            int[] A2 = new int[] { 1, 2, 3, 4, 5, 6, 7, 3, 2, 4 };
            Solution(6, A2);
        }
    }
}
