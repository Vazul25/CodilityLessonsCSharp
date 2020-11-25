using System;
using System.Collections;
using System.Collections.Generic;

namespace Sorting_Distinct
{
    class Program
    {
        public static List<int> MergeSort(List<int> A)
        {
            if (A.Count <= 1)
                return A;
            var middle = A.Count / 2;
            List<int> left = new List<int>(A.GetRange(0, middle));
            List<int> right = new List<int>(A.GetRange(middle, A.Count));
            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }
        public static List<int> Merge(List<int> A, List<int> B)
        {
            int i = 0, j = 0;
            List<int> merged = new List<int>(A.Count + B.Count);
            while (i!=A.Count && j != B.Count)
            {
                if (A[i] >= B[j])
                {
                    merged.Add(A[i]);
                    i++;
                }
                else
                {
                    merged.Add(B[j]);
                    j++;
                }
            }
            return merged;
        }
        static void Main(string[] args)
        {
            var Test = new int[] { 1, 7, 3, 9, 4, 7, 7, 10, 22, 4, 6, 7 }; 
            Console.WriteLine("Hello World!");
        }
    }
}
