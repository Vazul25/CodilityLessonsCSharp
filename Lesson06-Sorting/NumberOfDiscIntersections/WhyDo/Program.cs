using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhyDo
{

    class NumberOfDiscIntersection
    {
        public int Solution1(int[] A)
        {
            var intervals = A.Select((value, index) => (new { startPosition = index - value, endPosition = index + value })).OrderBy(s => s.startPosition);
            var intervalsList = intervals.ToList();
            int sumIntersections = 0;
            var sumOfIntersects = 0;

            Dictionary<int, int> intervalsActive = new Dictionary<int, int>();
            SortedSet<int> intervalActiveKeys = new SortedSet<int>();

            foreach (var item in intervals)
            {
                if (intervalActiveKeys.Min < item.startPosition)
                {
                    foreach (var keyToRemove in intervalActiveKeys.Where(endposition => endposition < item.startPosition).ToList())
                    {
                        intervalActiveKeys.Remove(keyToRemove);
                        intervalsActive.Remove(keyToRemove);
                    }
                }
                //intervalsActive[item.endPosition] = (intervalsActive3.GetValueOrDefault(item.endPosition) + 1);

                sumOfIntersects = intervalsActive.Values.Sum();
                sumIntersections += sumOfIntersects;
                int tmp = 0;
                intervalsActive.TryGetValue(item.endPosition, out tmp);
                intervalsActive[item.endPosition] = (tmp + 1);
                intervalActiveKeys.Add(item.endPosition);
            }
            return sumIntersections;
        }
        public int Solution2(int[] A)
        {

            var intervals = A.Select((value, index) =>
            {
                int startPos = 0;
                int endPos = 0;
                try
                { startPos = checked(index - value); }
                catch (System.OverflowException) { startPos = Int32.MinValue; }

                try
                { endPos = checked(index + value); }
                catch (System.OverflowException) { endPos = Int32.MaxValue; }


                return new { startPosition = startPos, endPosition = endPos };
            }
            ).OrderBy(s => s.startPosition)
            .ToArray();

            int sumIntersections = 0;


            for (int i = 0; i < intervals.Length; i++)
            {
                int j = i + 1;
                while (j < intervals.Length && intervals[j].startPosition <= intervals[i].endPosition)
                {
                    sumIntersections++;
                    if (sumIntersections > 10000000)
                        return -1;
                    j++;
                }

            }

            return sumIntersections;
        }
        class Interval
        {
            public long StartPosition;
            public long EndPosition;
        }
        public int Solution4(int[] A)
        {
            int Length = A.Length;
            var intervals = new Interval[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                intervals[i] = new Interval { EndPosition = i + A[i], StartPosition = i - A[i] };
            };
            intervals = intervals.OrderBy(i => i.StartPosition).ToArray();
            int sumIntersections = 0;
            for (int i = 0; i < Length; i++)
            {
                for (int j = i + 1; j < Length && intervals[j].StartPosition <= intervals[i].EndPosition; j++)
                {
                    sumIntersections++;
                    if (sumIntersections > 10000000)
                        return -1;
                }
            }
            return sumIntersections;
        }
        public int Solution3(int[] A)
        {
            var intervals = A.Select((value, index) =>
            {
                int startPos = 0;
                int endPos = 0;

                try
                { startPos = checked(index - value); }
                catch (System.OverflowException) { startPos = Int32.MinValue; }
                try
                { endPos = checked(index + value); }
                catch (System.OverflowException) { endPos = Int32.MaxValue; }

                return new { startPosition = startPos, endPosition = endPos };
            }).OrderBy(s => s.startPosition).ToList();
            SortedSet<int> endPositions = new SortedSet<int>();
            SortedDictionary<int, int> asd = new SortedDictionary<int, int>();

            int sumIntersections = 0;

            for (int i = 0; i < intervals.Count; i++)
            {
                while (endPositions.Count != 0 && endPositions.Min < intervals[i].startPosition)
                {
                    endPositions.Remove(endPositions.Min);
                }
                sumIntersections += endPositions.Count;
                endPositions.Add(intervals[i].endPosition);
            }
            return sumIntersections;
        }

        public int Solution6(int[] A)
        {
            int N = A.Length;
            int M = N + 2;
            int[] left = new int[M]; // values of nb of "left"  edges of the circles in that point
            int[] sleft = new int[M]; // prefix sum of left[]
            int il, ir;               // index of the "left" and of the "right" edge of the circle

            for (int i = 0; i < N; i++)
            { // counting left edges
                il = tl(i, A);
                left[il]++;
            }

            sleft[0] = left[0];
            for (int i = 1; i < M; i++)
            {// counting prefix sums for future use
                sleft[i] = sleft[i - 1] + left[i];
            }
            int o, pairs, total_p = 0, total_used = 0;
            for (int i = 0; i < N; i++)
            { // counting pairs
                ir = tr(i, A, M);
                o = sleft[ir];                // nb of open till right edge
                pairs = o - 1 - total_used;
                total_used++;
                total_p += pairs;
            }
            if (total_p > 10000000)
            {
                total_p = -1;
            }
            return total_p;
        }

        private int tl(int i, int[] A)
        {
            int tl = i - A[i]; // index of "begin" of the circle
            if (tl < 0)
            {
                tl = 0;
            }
            else
            {
                tl = i - A[i] + 1;
            }
            return tl;
        }
        int tr(int i, int[] A, int M)
        {
            int tr;           // index of "end" of the circle
            if (Int32.MaxValue - i < A[i] || i + A[i] >= M - 1)
            {
                tr = M - 1;
            }
            else
            {
                tr = i + A[i] + 1;
            }
            return tr;
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
             
            //var r = new Random();
            //var A = new int[] { 1, 5, 2, 1, 4, 0 };
            //var B = new int[] { 1, 2147483647, 0 };
            //var C = new int[] { 1, 0, 1, 0, 1 };
            //var D = Enumerable.Range(1, 1000).Select(i => r.Next(0, 1000)).ToArray(); 

            //var solver = new NumberOfDiscIntersection();
            //Console.WriteLine(solver.Solution2(A));
            //Console.WriteLine(solver.Solution2(D));
            //Console.WriteLine(solver.Solution6(D));
            //Console.WriteLine("Hello World!");
            //Console.ReadLine();
        }
    }
    
    public class Tests {
        NumberOfDiscIntersection solver = new NumberOfDiscIntersection();
        private int[] data;
        [GlobalSetup]
        public void Setup() {
            var r = new Random();
            data = Enumerable.Range(1, 1000).Select(i => r.Next(0, 1000)).ToArray();
        }
        [Benchmark]
        public int MyS() => solver.Solution2(data);
        [Benchmark]
        public int MyOnlineSuper() => solver.Solution6(data);
    }
}
