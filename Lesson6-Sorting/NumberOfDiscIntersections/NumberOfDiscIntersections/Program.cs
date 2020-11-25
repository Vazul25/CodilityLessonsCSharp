using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace NumberOfDiscIntersections
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

         
        public int Solution7(int[] a)
        {
            int result = 0;
            int[] dps = new int[a.Length];
            int[] dpe = new int[a.Length];
            int t = a.Length - 1;
            for (int i = 0; i < a.Length; i++)
            {
                int s = i > a[i] ? i - a[i] : 0;
                int e = t - i > a[i] ? i + a[i] : t;
                dps[s]++;
                dpe[e]++;
            }

            t = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (dps[i] > 0)
                {
                    result += t * dps[i];
                    result += dps[i] * (dps[i] - 1) / 2;
                    if (10000000 < result)
                        return -1;
                    t += dps[i];
                }
                t -= dpe[i];
            }

            return result;
        }
        
    }
    [MemoryDiagnoser]
    public class Tests
    {
        NumberOfDiscIntersection solver = new NumberOfDiscIntersection();
        private int[] data;
        [GlobalSetup]
        public void Setup()
        {
            var r = new Random();
            data = Enumerable.Range(1, 1000).Select(i => r.Next(0, 1000)).ToArray();
        }
        [Benchmark]
        public int MyS() => solver.Solution7(data);
        [Benchmark]
        public int MyOnlineSuper() => solver.Solution2(data);
    }
    class Program
    {

        static void Main(string[] args)
        {
            var r = new Random();

            var A = new int[] { 1, 5, 2, 1, 4, 0 };
            var solver = new NumberOfDiscIntersection();
            var data = Enumerable.Range(1, 1000).Select(i => r.Next(0, 1000)).ToArray();

            Console.WriteLine(solver.Solution6(data));
            Console.WriteLine(solver.Solution7(data));
            Console.WriteLine("Hello World!");
            //var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

        }
    }
}
