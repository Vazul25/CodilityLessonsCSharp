using System;

namespace MinAvgTwoSLice
{
    struct Sequence
    {
        public int startIndex;
        public int length;
        public int sum;
        public double Value { get => sum / (double)length; }
        public double ValueWith(int number) => ((sum + number) / (double)(length + 1));

        public Sequence(int startIndex, int sum, int length)
        {
            this.startIndex = startIndex;
            this.sum = sum;
            this.length = length;
        }
        public Sequence(Sequence s, int number)
        {
            this = s;
            length++;
            sum += number;

        }
        public override string ToString()
        {
            return $"From: {startIndex} To: {startIndex + length} {sum}/{length} = {Value}";
        }
    }
    public class Solution
    {
        public static int solution(int[] A)
        {
            int minIndex = 0;
            Sequence[] sequences = new Sequence[A.Length - 1];
            sequences[0] = new Sequence { length = 2, sum = A[0] + A[1], startIndex = 0 };
            for (int i = 2; i < A.Length; i++){
                sequences[i - 1] = sequences[i - 2].ValueWith(A[i]) < ((A[i] + A[i - 1]) / 2.0) ?
                    new Sequence(sequences[i - 2], A[i]) :
                    new Sequence(i - 1, A[i - 1] + A[i], 2);
                minIndex = sequences[minIndex].Value > sequences[i - 1].Value ? i - 1 : minIndex;
                Console.WriteLine($"{sequences[minIndex].Value } > {sequences[i - 1].Value } : {minIndex}");
                Console.WriteLine(minIndex);
            }
            foreach (var item in sequences)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine(minIndex);
            Console.WriteLine(sequences[minIndex].startIndex);
            return sequences[minIndex].startIndex;
        }
        static void Main(string[] args)
        {
            var a = new Sequence { length = 5, sum = 10, startIndex = 1 };
            var b = new Sequence(a, 10);
            int[] A = new int[] { 4, 2, 2, 5, 1, 5, 8 };
            int[] B = new int[] { -3, -5, -8, -4, -10 };
            solution(B);
          
            Console.WriteLine("Hello World!");

        }
    }
}
