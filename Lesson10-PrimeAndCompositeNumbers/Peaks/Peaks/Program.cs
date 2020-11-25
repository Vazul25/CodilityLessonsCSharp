using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace Peaks
{
    [MemoryDiagnoser]
    public class Bench
    {
        [Params("Random", "1.HalfNoPeaks", "2.HalfNoPeaks", "NoPeak", "OnePeak")]
        public string Mode;
        int[] A;
        [GlobalSetup]
        public void Setup()
        {
            Random r = new Random();
            A = new int[1000000000];

            if (Mode == "NoPeak")
                A[0] = 0;
            else if (Mode == "OnePeak")
            {

                A[100000 / 2] = 100;
            }
            else
            {

                for (int i = 0; i < A.Length; i++)
                {
                    switch (Mode)
                    {
                        case "Random":
                            A[i] = r.Next(-100000, 100000);
                            break;
                        case "2.HalfNoPeaks":
                            A[i] = i > A.Length / 2 ? r.Next(-100000, 100000) : 1;
                            break;
                        case "1.HalfNoPeaks":
                            A[i] = i < A.Length / 2 ? r.Next(-100000, 100000) : 1;
                            break;

                    }

                }
            }
        }
        [Benchmark]
        public void Mysolution() => Peaks.MySolution(A);
        [Benchmark]
        public void OnlineSolution() => Peaks.onlineSolution(A);
        [Benchmark]
        public void MyErroneousSolution() => Peaks.MyErroneousSolution(A);
        [Benchmark]
        public void MySolutionWIthPrefixSums() => Peaks.MySolutionWIthPrefixSums(A);
    }
    public class Peaks
    {
        static List<int> GetDivisors(int N)
        {
            var root = Math.Sqrt(N);
            var beforeSqrt = new List<int>((int)Math.Ceiling(root));
            beforeSqrt.Add(1);
            //var afterSqrt = new List<int>((int)Math.Ceiling(root));
            Stack<int> afterSqrt = new Stack<int>((int)Math.Ceiling(root));

            bool perfectSqrt = true;
            int j = 0;
            if (root % 1 != 0)
                perfectSqrt = false;
            for (int i = 2; i <= (int)root; i++)
            {
                if (N % i == 0)
                {
                    beforeSqrt.Add(i);
                    afterSqrt.Push(N / i);
                    j++;
                }
            }
            if (perfectSqrt && N != 1)
                beforeSqrt.RemoveAt(beforeSqrt.Count - 1);
            while (afterSqrt.Count > 0)
            {
                beforeSqrt.Add(afterSqrt.Pop());
            }
            //beforeSqrt.AddRange(afterSqrt);
            if (N != 1)
                beforeSqrt.Add(N);
            return beforeSqrt;

        }

        static int[] GetPeaks(int[] A, out int length)
        {
            int[] peaks = new int[A.Length / 2];
            int k = 0;
            for (int i = 1; i < A.Length - 1; i++)
            {
                if (A[i] > Math.Max(A[i + 1], A[i - 1]))
                {
                    peaks[k] = i;
                    k++;
                }
            }
            length = k;
            return peaks;
        }

        static bool CanBeDividedIntoGroups(int groupCount, int[] peaks, int[] A, int peaksLength)
        {
            if ((A.Length / (double)groupCount) % 1 != 0)
                return false;
            int remainingGroup = groupCount;
            int groupSize = A.Length / groupCount;
            int left = 0;

            for (int i = 0; i < groupCount; i++)
            {
                var index = Array.BinarySearch<int>(peaks, left, peaksLength - left, (i * groupSize));
                if (index < 0)
                {
                    if (~index == peaksLength)
                        return false;

                    if (peaks[~index] >= (i + 1) * groupSize)
                    {
                        return false;

                    }
                    left = ~index + 1;
                }
                else
                {
                    left = index;
                }
            }
            return true;
        }
        public static int onlineSolution(int[] A)
        {
            int N = A.Length;

            List<int> peaks = new List<int>();
            for (int i = 1; i < N - 1; i++)
            {
                if (A[i] > A[i - 1] && A[i] > A[i + 1])
                    peaks.Add(i);
            }

            for (int size = 1; size <= N; size++)
            {
                if (N % size != 0)
                    continue;
                int find = 0;
                int groups = N / size;
                bool ok = true;
                foreach (var peakIdx in peaks)
                {
                    if (peakIdx / size > find)
                    {
                        ok = false;
                        break;
                    }
                    if (peakIdx / size == find)
                        find++;
                }
                if (find != groups)
                    ok = false;
                if (ok)
                    return groups;
            }

            return 0;
        }

        public static int MySolution(int[] A)
        {
            int peaksLength = 0;
            int[] peaks = GetPeaks(A, out peaksLength);
            if (peaksLength == 0)
                return 0;
            List<int> divisors = GetDivisors(A.Length);
            for (int i = divisors.Count - 1; i >= 0; i--)
            {
                if (CanBeDividedIntoGroups(divisors[i], peaks, A, peaksLength))
                    return divisors[i];
            }
            return 0;
        }


        public static int MySolutionWIthPrefixSums(int[] A)
        {
            int greatestGap = 0;
            int currentGap = 0;
            if (A.Length < 3)
                return 0;
            int[] prefixCountOfPeaks = new int[A.Length];
            for (int i = 1; i < A.Length-1; i++)
            {
                if (A[i] > Math.Max(A[i - 1], A[i + 1]))
                {
                    greatestGap = currentGap > greatestGap ? currentGap : greatestGap;
                    currentGap = 0;
                    prefixCountOfPeaks[i] = prefixCountOfPeaks[i-1] + 1;
                }
                else
                {
                    prefixCountOfPeaks[i] = prefixCountOfPeaks[i - 1];
                    currentGap += 1;

                }
            }
            prefixCountOfPeaks[A.Length - 1] = prefixCountOfPeaks[A.Length - 2];
            List<int> divisors = GetDivisors(A.Length);
            for (int i = divisors.Count - 1; i >= 0; i--)
            {
                int groupsize = A.Length / divisors[i];
                bool suc = true;

                if (2*groupsize < greatestGap)
                    continue;
                if (prefixCountOfPeaks[groupsize - 1] == 0)
                    continue;
                for (int j = 1; j < divisors[i]; j++)
                {
                    if (!( prefixCountOfPeaks[(j + 1) * groupsize - 1] > prefixCountOfPeaks[j * groupsize - 1]))
                    {
                        suc = false;
                        break;
                    }
                }
                if (suc)
                    return divisors[i];            
            }
            return 0;
        }

        //being undividable to 3 pices dosen't imply that it cannot be done with 4 pieces so this outer 
        // binary search is logically flawed for example:
        // 1 2 1 2 1 1 1 1 2 1 2 1 --> 1 2 1 | 2 1 1 | 1 2 1 | 1 2 1| but 1 2 1 2| 1 1 1 1| 2 1 2 1 
        public static int MyErroneousSolution(int[] A)
        {
            int peaksLength = 0;
            int[] peaks = GetPeaks(A, out peaksLength);
            if (peaksLength == 0)
                return 0;
            List<int> divisors = GetDivisors(A.Length);
            int left = 0;
            int right = divisors.Count - 1;
            while (left <= right)
            {
                int indexUnderTest = (left + right) / 2;

                bool canBeDivided = CanBeDividedIntoGroups(divisors[indexUnderTest], peaks, A, peaksLength);
                if (canBeDivided)
                {
                    left = indexUnderTest + 1;
                }
                else
                {
                    right = indexUnderTest - 1;
                }
            }

            if (right < left)
                return right < 0 ? 0 : divisors[right];
            else
                return divisors[left];
        }


        static void drawMountain(int[] A)
        {
            string neighbourSplitter = "\t";
            var lookUp = new Dictionary<int, List<int>>();
            var sortedSet = new SortedSet<int>();
            for (int i = 0; i < A.Length; i++)
            {
                sortedSet.Add(A[i]);
                if (!lookUp.ContainsKey(A[i]))
                {
                    lookUp[A[i]] = new List<int>();
                    lookUp[A[i]].Add(i);
                }
                else
                {
                    lookUp[A[i]].Add(i);
                }
            }
            while (sortedSet.Count > 0)
            {
                var max = sortedSet.Max;
                sortedSet.Remove(sortedSet.Max);
                var currentHighest = lookUp[max];
                var previousIndex = 0;
                for (int i = 0; i < currentHighest.Count; i++)
                {
                    var currentIndex = currentHighest[i];
                    foreach (var item in Enumerable.Range(0, currentIndex - previousIndex))
                    {
                        Console.Write(neighbourSplitter);
                    }
                    Console.Write('X');
                    previousIndex = currentHighest[i];

                }
                Console.WriteLine();
                var linebreakCount = max - sortedSet.Max;
                for (int lb = 0; lb < linebreakCount; lb++)
                    Console.WriteLine();

            }
        }
        static void Main(string[] args)
        {



            //var data1 = new int[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 };
            //var data2 = new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1 };
            //var data3 = new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            //var data4 = new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            //var data5 = new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1 };
            //var data6 = new int[] { 5, 1, 1, 5 };
            //var data7 = new int[] { 5, 1, 2, 1, 5, 5 };
            //var data7b = new int[] { 1, 2, 1, 2, 1 };
            //var data8 = new int[] { 5, 5 };
            //var data9 = new int[] { };
            //var data10 = new int[] { 5 };
            //var data11 = new int[] { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1 };
            //var data12 = new int[] { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1 };
            //var data13 = new int[] { 1, 4, 1, 1, 1, 1, 5, 1, 1, 1, 5, 1, 1, 1, 4, 1 };
            //var data14 = new int[] { 1, 3, 2, 1 };
            //var data15 = new int[] { 1, 4, 1, 1, 1, 1, 1, 1, 4, 1, 1, 4, 1, 1, 4, 1 };

            //var data16 = new int[] { 21, 26, 3, 22, 19, 12, 8, 4, 29, 27, 28, 4 };
            //var allData = new int[][] { data1, data16, data14,  data2, data3, data4, data5, data6, data7, data7b, data8, data9, data10, data11, data12, data13 };
            //for (int i = 0; i < allData.Length; i++)
            //{
            //    var dataUnderTest = allData[i];
            //    Console.WriteLine("expected:"+onlineSolution(dataUnderTest));

            //    Console.WriteLine("=====Test" + (i + 1) + "=====");
            //    drawMountain(dataUnderTest);
            //    Console.WriteLine(dataUnderTest.Length + " length,  GroupCount:  " + MySolutionWIthPrefixSums(dataUnderTest));
            //    Console.WriteLine("\n\n");


            //}
            var summary = BenchmarkRunner.Run(typeof(Bench).Assembly);

        }
    }
}
