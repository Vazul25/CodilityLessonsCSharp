using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NailingPlanks
{
    public class NailingPlanks
    {
        public class Interval
        {
            public int start;
            public int end;
        }
        public class NailWithIndex
        {
            public int positionItNails;
            public int orginialIndex;
            public NailWithIndex(int index,int position)
            {
                positionItNails = position;
                orginialIndex = index;
            }
        }
        //100% but its kind of an ugly solution which could be optimised a lot more
        public static int Solution2(int[] A, int[] B, int[] C)
        {
            int left = 1;
            int right = C.Length;
            int result = -1;
            Interval[] intervals = new Interval[A.Length];

            for (int i = 0; i < A.Length; i++)
            {

                intervals[i] = new Interval { end = B[i], start = A[i] };
            }
            NailWithIndex[] nailsWithIndexes = C.Select((v, i) => new NailWithIndex(  i,v)).OrderBy(n => n.positionItNails).ToArray();
            intervals = intervals.OrderBy(i => i.start).ToArray();
            Draw(intervals, C);
            while (left <= right)
            {
                int nailCountToTest = (right + left) / 2;
                var success = CheckNailsEnough2(intervals, nailsWithIndexes, nailCountToTest);
                if (success)
                {
                    result = nailCountToTest;
                    right = nailCountToTest - 1;
                }
                else
                {
                    left = nailCountToTest + 1;
                }
            }
            return result;
        }
         
        static bool CheckNailsEnough2(Interval[] intervals, NailWithIndex[] nails, int nailCount)
        {                      
            int nailIndex = 0;
            foreach (var interval in intervals)
            {
                while (interval.start > nails[nailIndex].positionItNails|| nails[nailIndex].orginialIndex >= nailCount)
                {
                    nailIndex++;
                    if (nailIndex >= nails.Length)
                        return false;
                    if (nails[nailIndex].orginialIndex >= nailCount)
                        continue;
                    if (nailIndex >= nails.Length)
                        return false;
                }
                if (interval.end < nails[nailIndex].positionItNails)
                    return false;
            }
            return true;
        }
        public static int Solution(int[] A, int[] B, int[] C)
        {
            int left = 1;
            int right = C.Length;
            int result = -1;
            Interval[] intervals = new Interval[A.Length];

            for (int i = 0; i < A.Length; i++)
            {
                intervals[i] = new Interval { end = B[i], start = A[i] };
            }
            
            intervals = intervals.OrderBy(i => i.start).ToArray();
            Draw(intervals,C);
            while (left <= right)
            {
                int nailCountToTest = (right + left) / 2;
                var success = CheckNailsEnough(intervals, C, nailCountToTest);
                if (success)
                {
                    result = nailCountToTest;
                    right = nailCountToTest - 1;
                }
                else
                {
                    left = nailCountToTest + 1;
                }
            }
            return result;
        }

        static bool CheckNailsEnough(Interval[] intervals, int[] nails, int nailCount)
        {
            int[] useableNails = nails.Take(nailCount).OrderBy(i => i).ToArray();
            int nailIndex = 0;
            foreach (var interval in intervals)
            {
                while (interval.start > useableNails[nailIndex])
                {
                    nailIndex++;
                    if (nailIndex >= useableNails.Length)
                        return false;
                }
                if (interval.end < useableNails[nailIndex])
                    return false;
            }
            return true;
        }
        public static void Draw(Interval[] intervals, int[] nails)
        {
            var nailsOrdered = nails.OrderBy(i => i).ToArray();
            
            for (int i = 0; i < nailsOrdered.Length; i++)
            {
                for (int j = 0; j < nailsOrdered[i]-(i==0?0 : nailsOrdered[i-1]) - 1; j++)
                {
                    Console.Write("   ");
                }
                Console.Write("|N|");
            }
            foreach (var interval in intervals)
            {
                Console.WriteLine();
                for (int i = 0; i < interval.start-1; i++)
                {
                    Console.Write("   ");
                }
                for (int i = 0; i <= interval.end - interval.start; i++)
                {
                    Console.Write("|X|");
                }
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            int[] A = new int[] { 1, 4, 5, 8 };
            int[] B = new int[] { 4, 5, 9, 10 };
            int[] C = new int[] { 4, 6, 7, 10, 2 };
            Console.WriteLine(Solution(A, B, C));
            Console.WriteLine("Hello World!");
        }
    }
}
