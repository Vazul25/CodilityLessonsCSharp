using System;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;

namespace MaxDoubleSlice
{
    public class Program
    {
        static readonly bool _allowLog = true;

        //ironically i managed to create a bug in this too
        public static int NaiveSolutionForTesting(int[] A)
        {
            int[] maxSumsByStart = new int[A.Length];
            for (int start = 0; start < A.Length - 2; start++)
            {
                int currentSum = 0;
                int maxSum = 0;
                int minimalForPivot = A[start + 1];
                for (int end = start + 2; end < A.Length; end++)
                {
                    minimalForPivot = Math.Min(minimalForPivot, A[end - 1]);
                    currentSum += A[end - 1];
                    maxSum = Math.Max(currentSum - minimalForPivot, maxSum);

                }
                maxSumsByStart[start] = maxSum;
            }
            return maxSumsByStart.Max();
        }
        public static void LogValue(int startIndex, int pivotIndex, int endIndex, int j, int valueToLog)
        {
            if (j == startIndex)
                Console.Write("S");
            if (j == pivotIndex)
                Console.Write("P");
            if (j == endIndex)
                Console.Write("E");
            Console.Write(valueToLog + " ");

        }
        public static void Log(int[] A, int pivotIndex, int startIndex, int endIndex, int afterPivot, int beforePivot, int currentSum, int maxSum)
        {

            if (endIndex - startIndex < 20)
            {
                for (int j = startIndex; j <= endIndex; j++)
                {
                    LogValue(startIndex, pivotIndex, endIndex, j, A[j]);
                }
            }
            else
            {
                for (int j = startIndex; j < startIndex + 5 && j < pivotIndex; j++)
                {
                    LogValue(startIndex, pivotIndex, endIndex, j, A[j]);
                }
                Console.Write("... ");

                for (int j = Math.Min(Math.Max(startIndex + 5, pivotIndex - 5), pivotIndex); j < pivotIndex + 5 && j < endIndex; j++)
                {
                    LogValue(startIndex, pivotIndex, endIndex, j, A[j]);
                }
                Console.Write("... ");

                for (int j = Math.Min(Math.Max(pivotIndex + 5, endIndex - 5), endIndex); j < endIndex + 1; j++)
                {
                    LogValue(startIndex, pivotIndex, endIndex, j, A[j]);
                }
            }
            Console.WriteLine();
            Console.WriteLine("BeforePivot: " + beforePivot + " AfterPivot: " + afterPivot + " Sum: " + currentSum + " MaxSum: " + maxSum + " startindex: " + startIndex + " pivotindex: " + pivotIndex + " endindex: " + endIndex);
            Console.WriteLine();
        }
        // I really tried to make it work this way but after realizing that there are some very nasty cases i didn't thought about at first i gave up on it,
        // it is too much of a hassle considering how easy it is to use prefix and postfix sums
        // alas i really liked this approach, maybe by combining this with the prefix and postfix sums we could achive better performance
        // next step would be to record the next best place for the pivot or use some prefix or postfix sum to search for it  and most cases could be combined 
        
        public static int SolutionWithCatterpillarLikeAlgorithm(int[] A)
        {
            int maxSum = 0;
            int currentSum = 0;
            int beforePivot = 0;
            int afterPivot = 0;
            int pivotIndex = 1;
            int startIndex = 0;
            int endIndex = 2;
            if (_allowLog && A.Length < 40)
                Console.WriteLine(string.Join("", A.Select(i => " " + i + " ").ToArray<string>()) + "\n\n");
            for (int i = 3; i < A.Length; i++)
            {
                if (_allowLog)
                    Console.WriteLine("\n\nStep\n\n");

                endIndex = i;

                int lastBeforePivot = beforePivot;
                int lastAfterPivot = afterPivot;
                int lastPivotIndex = pivotIndex;
                afterPivot += A[i - 1];

                // i do the algorithm in 2 steps since it can be visualised better for understanding and testing, obviously it would be more performant if done in 1 step
                if (_allowLog)
                    Log(A, pivotIndex, startIndex, endIndex, afterPivot, beforePivot, afterPivot + beforePivot, maxSum);
                bool newPivotWouldBeLesser = A[pivotIndex] >= A[i - 1];
                bool weHaveNegativeSubArrays = beforePivot + afterPivot < 0;
                bool startCouldEatTheOldPivotNewPivotNegativeOrZero = (pivotIndex == startIndex + 1 && A[i - 1] <= 0 && A[pivotIndex] < 0);
                // bool beforePivotIsLesserThanNewNegativePivotAndShouldBeDiscarded = beforePivot + A[pivotIndex] <= 0 && beforePivot + A[pivotIndex] >= A[i - 1] && A[i - 1] < 0;
                bool beforePivotIsLesserThanNewNegativePivotAndShouldBeDiscarded = A[pivotIndex] < 0 && A[i - 1] < 0 && beforePivot + A[i - 1] < 0;

                //int maxDiscardableWithStart = lastBeforePivot;
                //int k = lastPivotIndex + 1;
                //while (A[k] < 0 && k < endIndex)
                //{ maxDiscardableWithStart += A[k]; k++; }
                //bool maxDiscardableGreaterThanNewPivotCandidate = maxDiscardableWithStart + A[i - 1] <= 0;

                bool pivotNeedsToBeMoved =
                    newPivotWouldBeLesser ||
                    weHaveNegativeSubArrays ||
                    startCouldEatTheOldPivotNewPivotNegativeOrZero ||
                    beforePivotIsLesserThanNewNegativePivotAndShouldBeDiscarded;
                    //||                    maxDiscardableGreaterThanNewPivotCandidate;
                if (pivotNeedsToBeMoved)
                {
                    //if (maxDiscardableGreaterThanNewPivotCandidate)
                    //{
                    //    pivotIndex = i - 1;
                    //    startIndex = k - 1;
                    //    afterPivot = afterPivot - maxDiscardableWithStart - lastBeforePivot;
                    //    beforePivot = 0;
                    //}
                    //else
                    //{
                        if (weHaveNegativeSubArrays || startCouldEatTheOldPivotNewPivotNegativeOrZero || beforePivotIsLesserThanNewNegativePivotAndShouldBeDiscarded)
                        {
                            pivotIndex = endIndex - 1;
                            beforePivot = lastBeforePivot + A[lastPivotIndex] + lastAfterPivot;
                        }
                        else
                        //the new pivot is smaller than the old 
                        if (newPivotWouldBeLesser)
                        {
                            beforePivot += lastAfterPivot + A[lastPivotIndex];
                        }

                        pivotIndex = i - 1;
                        afterPivot = 0;
                    //}


                }
                bool startNeedsToBeMoved = beforePivot < 0 ||
                    pivotNeedsToBeMoved && lastBeforePivot + A[lastPivotIndex] < 0
                    || startCouldEatTheOldPivotNewPivotNegativeOrZero;

                if (_allowLog && pivotNeedsToBeMoved)
                {
                    Console.WriteLine("Pivot Moved\n");
                    Log(A, pivotIndex, startIndex, endIndex, afterPivot, beforePivot, afterPivot + beforePivot, maxSum);
                }
                if (startNeedsToBeMoved)
                {

                    if (pivotNeedsToBeMoved && lastPivotIndex == startIndex + 1)

                    {
                        beforePivot = beforePivot - lastBeforePivot - A[lastPivotIndex];
                        startIndex++;

                    }
                    else if (pivotNeedsToBeMoved && lastBeforePivot + A[lastPivotIndex] <= 0)
                    {
                        beforePivot = lastAfterPivot;
                        startIndex = lastPivotIndex;
                        while ((A[startIndex + 1] < 0 || (startIndex + 2 != pivotIndex && A[startIndex + 1] + A[startIndex + 2] < 0)) && startIndex + 1 < pivotIndex)
                        {
                            startIndex++;
                            beforePivot -= A[startIndex];
                        }
                    }
                    if (beforePivot < 0)
                    {
                        beforePivot = 0;
                        startIndex = pivotIndex - 1;
                    }
                }
                currentSum = afterPivot + beforePivot;


                if (currentSum > maxSum)
                    maxSum = currentSum;
                if (_allowLog && startNeedsToBeMoved)
                {
                    Console.WriteLine("Start Moved\n");
                    Log(A, pivotIndex, startIndex, endIndex, afterPivot, beforePivot, beforePivot + afterPivot, maxSum);
                }
                #region a
                /*
                if (pivotNeedsToBeMoved)
                {
                    //if (beforePivot + A[pivotIndex] >= A[i - 1] && A[i - 1] < 0)
                    //{
                    //    beforePivot = afterPivot - A[i - 1] + A[i] + beforePivot;
                    //}
                    //else
                    if ((beforePivot + A[pivotIndex]) < 0)
                    {
                        startIndex = pivotIndex;
                        beforePivot = afterPivot;
                    }
                    else
                    {
                        beforePivot += A[pivotIndex] + afterPivot;
                    }
                    afterPivot = 0;
                    pivotIndex = i - 1;
                }
                else
                {
                    afterPivot += A[i - 1];
                }

                if (beforePivot < 0)
                {
                    startIndex = pivotIndex - 1;
                    beforePivot = 0;
                }
                currentSum = afterPivot + beforePivot;
                if (currentSum > maxSum)
                    maxSum = currentSum;
                */
                #endregion
            }
            if (_allowLog)
                Log(A, pivotIndex, startIndex, endIndex, afterPivot, beforePivot, currentSum, maxSum);

            return maxSum;
        }

        public static int SolutionWithCatterpillarLikeAlgorithmFirstTry(int[] A)
        {
            int maxSum = 0;
            int currentSum = 0;
            int beforePivot = 0;
            int afterPivot = 0;
            int pivotIndex = 1;
            int startIndex = 0;
            int endIndex = 2;
            if (_allowLog && A.Length < 40)
                Console.WriteLine(string.Join("", A.Select(i => " " + i + " ").ToArray<string>()) + "\n\n");
            for (int i = 3; i < A.Length; i++)
            {
                if (_allowLog)
                    Log(A, pivotIndex, startIndex, endIndex, afterPivot, beforePivot, currentSum, maxSum);
                endIndex = i;
                if (A[pivotIndex] >= A[i - 1] ||
                    afterPivot < 0 ||
                    (pivotIndex == startIndex + 1 && A[i - 1] <= 0)// ||
                                                                   //beforePivot + A[pivotIndex] >= A[i - 1] && A[i - 1] < 0
                    )
                {
                    if ((beforePivot + A[pivotIndex]) < 0)
                    {
                        startIndex = pivotIndex;
                        beforePivot = afterPivot;
                    }
                    else
                    {
                        beforePivot += A[pivotIndex] + afterPivot;
                    }
                    afterPivot = 0;
                    pivotIndex = i - 1;
                }
                else
                {
                    afterPivot += A[i - 1];
                }

                if (beforePivot < 0)
                {
                    startIndex = pivotIndex - 1;
                    beforePivot = 0;
                }
                currentSum = afterPivot + beforePivot;
                if (currentSum > maxSum)
                    maxSum = currentSum;
            }
            if (_allowLog)
                Log(A, pivotIndex, startIndex, endIndex, afterPivot, beforePivot, currentSum, maxSum);

            return maxSum;
        }

        public static int SolutionWithPrefixPostfixSums(int[] A)
        {
            int[] prefixSums = new int[A.Length];
            int[] postfixSums = new int[A.Length];
            int negativeNumberCount = 0;
            for (int i = 1; i < A.Length; i++)
            {
                prefixSums[i] = Math.Max(0, prefixSums[i - 1] + A[i]);
                if (A[i] < 0)
                    negativeNumberCount++;
            }
            if (negativeNumberCount == A.Length - 1)
                return 0;
            for (int i = A.Length - 2; i > 0; i--)
            {
                postfixSums[i] = Math.Max(postfixSums[i + 1] + A[i], 0);
            }
            int sum;
            int maxSum = 0;
            for (int i = 1; i < A.Length - 1; i++)
            {
                sum = prefixSums[i - 1] + postfixSums[i + 1];
                maxSum = Math.Max(sum, maxSum);
            }
            return maxSum;
        }
        static void Main(string[] args)
        {
            #region testData
            var r = new Random();

            var testA1 = new int[] { 3, 2, 6, -1, 4, 5, -1, 2 };
            var testA2 = new int[] { 1, 1, 1, 1, 50, 50, 1, 1, 1, 1 };
            var testA3 = new int[] { 5, 9, 10, 15, -1, 2, -2, -3, -20, -50, -60, 30, 20, -5, 20 - 60 };
            var testA4 = new int[] { -1, -10, -100, -1000, -10000 };
            var testA5 = new int[] { 1, 10, 100, 1000, 2, 20, 200, 2000, 20000 };
            var testA6 = new int[] { 1, 10, 100, -1000, 2, 20, 200, 2000, -20000 };
            var testA8 = new int[] { 8312, -8482, 6254, 7391, -7046, -5101, -6076, 599 };
            var testA9 = new int[] { 0, 50, -100, 5, -51, 100, 200 };
            var testA7 = Enumerable.Range(0, 70).Select(i => r.Next(-10000, 10000)).ToArray();
            var testA10 = new int[] { 41, 41, -46, -27, 35, -40, -4, 16, 10, -2, -5, -1, 37, -49, 7, 0, 29, -40, -21, 28 };
            var testA11 = new int[] { 12, 41, 8, 5, -36, -44, -3, -14, -19, -26, -36, 2, 9, -41, -41, -23, -45, 25, -24, 29 };
            var testA12 = new int[] { 23, 48, 36, -7, 27, -6, 10, -19, 2, 12, -50, 37, -25, 3, -33, -50, -17, -1, -42, 29 };
            var testA13 = new int[] { 12, -40, -15, -15, -16, 43, 40, 27, 41, -20, -6, -47, 34, -1, -45, 13, -45, -36, 27, 20 };
            var testA14 = new int[] { -5, -34, 33, 19, -26, -24, -23, -44, 42, -41, 44, 5, 16, -4, -33, 12, 12, 0, -41, 34 };
            var testA15 = new int[] { 3, 32, -11, -28, -33, 46, -47, 25, -3, 3, 45, -32, 21, -12, -17, -5, -23, 16, -39, -43 };
            var testA16 = new int[] { 16, 34, 35, 32, -15, 38, -14, -13, -18, -7, -49, 4, -45, 43, -34, 13, 26, 10, 1, -23 };
            var testA17 = new int[] { -28, 1, 8, -12, -36, 25, 33, -22, -38, 21, -15, 46, -12, -48, -32, 19, 46, 7, 36, -22 };
            var testA18 = new int[] { 9, 15, -8, -46, 2, -10, -38, 28, -29, 20, 18, 16, -35, 33, 34, -36, -25, -3, 17, 9 };
            var testA19 = new int[] { -46, -43, -25, 10, -43, -46, -34, -47, 25, -3, 17, -31, 24, -28, 17, 6, -42, 3, 36, 24 };
            var testA20 = new int[] { -10, -40, -48, -36, 34, -5, 33, 15, -34, -38 };
            var testA21 = new int[] { -18, -38, 5, -37, -35, -23, 44, -24, 48, -25 };
            var testA22 = new int[] { -40, -32, 1, 40, 23, 21, -48, -8, 31, -39 };
            var testA23 = new int[] { -2, -25, 12, 36, -10, -19, 8, 45, 16, 14 };
            var testA24 = new int[] { 9, 43, 49, 48, -35, 26, 21, -41, 5, 23 };
            var testA25 = new int[] { 16, -2, -12, 32, 17, -28, 8, -39, -9, -38 };
            var testA26 = new int[] { -7, -9, 33, -45, 13, -43, -1, 18, 22, 33 };
            var testA27 = new int[] { 18, 48, 9, -17, 16, -34, -31, 3, 25, 46 };
            var testA28 = new int[] { -50, 42, 37, -42, -5, -50, 36, 30, -32, -32 };
            var testA29 = new int[] { 23, -3, 3, 3, -29, -32, -41, 43, -16, -43 };
            #endregion 
            Console.WriteLine(SolutionWithCatterpillarLikeAlgorithm(new int[] { 8079, 7025, -2794, 323, 2442, 6202, -2193, -4143, -6083, 8255, -8537, -4563, 8723, 3677, -6416, 8519, -2610, 8660, 9392, -3713 }));
            Console.WriteLine(SolutionWithPrefixPostfixSums(new int[] { 8079, 7025, -2794, 323, 2442, 6202, -2193, -4143, -6083, 8255, -8537, -4563, 8723, 3677, -6416, 8519, -2610, 8660, 9392, -3713 }));



        }
    }
}
