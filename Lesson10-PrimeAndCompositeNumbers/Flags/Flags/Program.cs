using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Microsoft.Diagnostics.Runtime.ICorDebug;
using Microsoft.Diagnostics.Tracing.Parsers.IIS_Trace;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Security;

namespace Flags
{
    [MemoryDiagnoser]
    public class Benchmark
    {
        [Params("Random", "1.HalfNoPeaks", "2.HalfNoPeaks", "NoPeak", "OnePeak")]
        public string Mode;
        int[] A;
        [IterationSetup]
        public void Setup()
        {
            Random r = new Random();
            A = new int[10000000];

            if (Mode == "NoPeak")
                A[0] = 0;
            else if (Mode == "OnePeak")
            {

                A[10000000 / 2] = 100;
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
        public void VerifiedCodilitySolution() => Program.Solution.VerifiedCodilitySolution(A);
        [Benchmark]
        public void solution() => Program.Solution.solution(A);
        [Benchmark]
        public void SolutionWithInnerBinarySearch() => Program.Solution.SolutionWithBinaryInnerLoop(A);
        [Benchmark]
        public void SolutionWithOuterBinarySearch() => Program.Solution.SolutionWithBinaryOuterLoop(A);
        [Benchmark]
        public void SolutionWithInnerAndOuterBinarySearch() => Program.Solution.SolutionWithBinaryInnerOuterLoop(A);


    }

    class Program
    {
        public class Solution
        {

            //https://codility.com/media/train/solution-flags.pdf
            public static void CreatePeaks(int[] A, out bool[] _peaks)
            {
                var N = A.Length;
                var peaks = new bool[N];
                for (int i = 1; i < N - 1; i++)
                {
                    if (A[i] > Math.Max(A[i - 1], A[i + 1]))
                        peaks[i] = true;
                }
                _peaks = peaks;
            }
            public static void NextPeak(int[] A, out int[] _next)
            {


                var N = A.Length;
                CreatePeaks(A, out var peaks);
                var next = new int[N];
                next[N - 1] = -1;
                for (int i = N - 2; i > -1; i--)
                {
                    if (peaks[i])
                        next[i] = i;
                    else
                        next[i] = next[i + 1];
                }


                _next = next;
            }
            // this should be the verified solution but oddly the benchmark dosen't think so
            public static int VerifiedCodilitySolution(int[] A)
            {
                var N = A.Length;
                if (N < 3)
                    return 0;
                NextPeak(A, out var next);
                int i = 1;
                var result = 0;
                while ((i - 1) * i <= N)
                {
                    var pos = 0;
                    var num = 0;
                    while (pos < N && num < i)
                    {
                        pos = next[pos];
                        if (pos == -1)
                            break;

                        num++;
                        pos += i;
                    }
                    result = Math.Max(result, num);
                    i++;
                }
                return result;
            }
            //Sadly not 100% solution only 86% O(n*sqrt(n)) its performance degrades on sequences where there are fewer peeks than would be if randomed
            public static int solution(int[] A)
            {

                int greatestPossible = 1 + (int)Math.Sqrt(A.Length);
                for (int flags = greatestPossible; flags > 0; flags--)
                {

                    //iterationCount++;
                    int flagsRemaining = flags;
                    for (int j = 1; j < A.Length - 1 && flagsRemaining > 0 && (A.Length - j) / (Math.Max(flagsRemaining - 1, 1)) >= flagsRemaining; j++)
                    {
                        if (A[j - 1] < A[j] && A[j + 1] < A[j])
                        {
                            flagsRemaining--;
                            j += flags - 1;//so j++ dont count
                                           //  if (countPeaks = true)  peakCount++;
                        }
                    }
                    //countPeaks=false;
                    if (flagsRemaining == 0)
                    {
                        //Console.WriteLine("iteartionCount:" + iterationCount);
                        return flags;
                    }
                }
                //Console.WriteLine("iteartionCount:" + iterationCount);

                return 0;
            }
            public static int SolutionWithBinaryOuterLoop(int[] A)
            {
                if (A.Length < 3)
                    return 0;
                int greatestPossible = 1 + (int)Math.Sqrt(A.Length);

                int leastFlag = 0;
                int mostFlag = greatestPossible;
                int flags = (leastFlag + mostFlag) / 2;
                while (leastFlag <= mostFlag)
                {
                    bool doable = false;
                    flags = (leastFlag + mostFlag) / 2;
                    int flagsRemaining = flags;
                    for (int j = 1; j < A.Length - 1 && flagsRemaining > 0 && (A.Length - j) / (Math.Max(flagsRemaining - 1, 1)) >= flagsRemaining; j++)
                    {
                        if (A[j - 1] < A[j] && A[j + 1] < A[j])
                        {
                            flagsRemaining--;
                            j += flags - 1;//so j++ dont count
                                           //  if (countPeaks = true)  peakCount++;
                        }
                    }
                    //countPeaks=false;
                    if (flagsRemaining == 0)
                    {
                        //Console.WriteLine("iteartionCount:" + iterationCount);
                        doable = true;


                    }
                    if (doable)
                        leastFlag = flags + 1;
                    else
                        mostFlag = flags - 1;
                }
                return mostFlag;
                //Console.WriteLine("iteartionCount:" + iterationCount);


            }
            public static int SolutionWithBinaryInnerLoop(int[] A)
            {
                if (A.Length < 3)
                    return 0;
                int[] peaksOnly = new int[(int)Math.Ceiling(A.Length / 2.0) - 1];
                int k = 0;
                int greatestPossible = 1 + (int)Math.Sqrt(A.Length);
                for (int j = 1; j < A.Length - 1; j++)
                {
                    if (A[j - 1] < A[j] && A[j + 1] < A[j])
                    {
                        peaksOnly[k] = j;
                        k++;
                    }
                }
                int NPeak = k;
                for (int flags = greatestPossible; flags > 0; flags--)
                {
                    if (k < flags)
                        continue;
                    int flagsRemaining = flags - 1;
                    int left = 1;
                    int right = NPeak;
                    int valueToFind = peaksOnly[0] + flags;

                    while (flagsRemaining > 0 && NPeak - left >= flagsRemaining)
                    {
                        var indexOfNext = Array.BinarySearch<int>(peaksOnly, left, right - left, valueToFind);
                        if (indexOfNext < 0)
                        {
                            if (~indexOfNext >= NPeak)
                                break;
                            else
                            {
                                valueToFind = peaksOnly[~indexOfNext] + flags;
                                left = ~indexOfNext + 1;
                            }
                        }
                        else
                        {
                            valueToFind = peaksOnly[indexOfNext] + flags;
                            left = indexOfNext + 1;
                        }
                        flagsRemaining--;
                    }
                    if (flagsRemaining == 0)
                    {
                        return flags;
                    }
                }
                return 0;

            }
            public static int SolutionWithBinaryInnerOuterLoop(int[] A)
            {
                if (A.Length < 3)
                    return 0;
                int greatestPossible = 1 + (int)Math.Sqrt(A.Length);
                int leastFlag = 0;
                int mostFlag = greatestPossible;

                int[] peaksOnly = new int[(int)Math.Ceiling(A.Length / 2.0) - 1];
                int k = 0;
                for (int j = 1; j < A.Length - 1; j++)
                {
                    if (A[j - 1] < A[j] && A[j + 1] < A[j])
                    {
                        peaksOnly[k] = j;
                        k++;
                    }
                }
                int NPeak = k;

                while (leastFlag <= mostFlag && mostFlag != 0)
                {
                    int flags = (leastFlag + mostFlag) / 2;

                    if (k < flags)
                    {
                        mostFlag = flags - 1;
                        continue;
                    }
                    int flagsRemaining = flags - 1;
                    int left = 1;
                    int right = NPeak;
                    int valueToFind = peaksOnly[0] + flags;

                    while (flagsRemaining != 0 && NPeak - left >= flagsRemaining)
                    {
                        var indexOfNext = Array.BinarySearch<int>(peaksOnly, left, right - left, valueToFind);
                        if (indexOfNext < 0)
                        {
                            if (~indexOfNext >= NPeak)
                                break;
                            else
                            {
                                valueToFind = peaksOnly[~indexOfNext] + flags;
                                left = ~indexOfNext + 1;
                            }
                        }
                        else
                        {
                            valueToFind = peaksOnly[indexOfNext] + flags;
                            left = indexOfNext + 1;
                        }
                        flagsRemaining--;
                    }
                    if (flagsRemaining == 0)
                        leastFlag = flags + 1;
                    else
                        mostFlag = flags - 1;
                }
                return mostFlag;
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
                //var data7 = new int[] { 5, 1, 2, 1, 5 };
                //var data7b = new int[] { 1, 2, 1, 2, 1 };
                //var data8 = new int[] { 5, 5 };
                //var data9 = new int[] { };
                //var data10 = new int[] { 5 };
                //var data30 = new int[] { 2, 1, 2, 1, 2, 1,
                //                         2, 1, 2, 1, 2, 1,
                //                         2, 1, 2, 2, 1, 2,
                //                         1, 2, 1, 2, 1, 2,
                //                         1, 2, 1, 2, 1, 2,
                //                         2, 1, 2, 1, 2, 1,
                //                         2, 1, 2, 1, 2, 1,
                //                         2, 1, 2 };

                //var allData = new int[][] { data1, data2, data3, data4, data5, data6, data7, data7b, data8, data9, data10 };
                //for (int i = 0; i < allData.Length; i++)
                //{
                //    var dataUnderTest = allData[i];
                //    Console.WriteLine("=====Test" + (i + 1) + "=====");
                //    drawMountain(dataUnderTest);
                //    Console.WriteLine("FlagCount: BinnaryInnerOuter" + Solution.SolutionWithBinaryInnerOuterLoop(dataUnderTest));
                //    Console.WriteLine("\n\n\n\n\n\n");
                //    Console.WriteLine("FlagCount: BinaryOuter" + Solution.SolutionWithBinaryOuterLoop(dataUnderTest));
                //    Console.WriteLine("\n\n\n\n\n\n");
                //    Console.WriteLine("FlagCount BinaryInner: " + Solution.SolutionWithBinaryInnerLoop(dataUnderTest));
                //    Console.WriteLine("\n\n\n\n\n\n");
                //    Console.WriteLine("FlagCount Codility: " + Solution.VerifiedCodilitySolution(dataUnderTest));
                //    Console.WriteLine("\n\n\n\n\n\n");

                //}
                //Random r = new Random();
                //var A = new int[2000];
                //for (int i = 0; i < 1000; i++)
                //{
                //    A = A.Select(_ => r.Next(-10000000, 100000000)).ToArray();
                //    var a = Solution.SolutionWithBinaryInnerLoop(A);
                //    Console.WriteLine();
                //    var c = Solution.SolutionWithBinaryOuterLoop(A);
                //    Console.WriteLine();
                //    var b = Solution.SolutionWithBinaryInnerOuterLoop(A);
                //    Console.WriteLine();
                //    var d = Solution.VerifiedCodilitySolution(A);
                //    if (!(a == b && c == b && c == d))
                //        Console.WriteLine($"Ops {a} {b} {c} {d}");
                //}

                var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
            }
        }
    }
}
