using System;
using System.Collections.Generic;
using System.Linq;

namespace FrogRiverOne
{
    class Program
    {

        public static int solution(int X, int[] A)
        /*{
            int sum = 0;
            Dictionary<int, bool> dictionary = Enumerable.Range(1, X).ToDictionary(v => v, v => true);
            int checkSum = (int)Math.Round((((X + 1) / 2.0) * X));
            int i = 0;
            while (sum != checkSum)
            {
                if (i == A.Length) {
            Console.WriteLine($"sum: {sum} csum:{checkSum} length:{A.Length} i:{i} X:{X}");
                    ; return -1; }
                if (dictionary.ContainsKey(A[i]))
                {
                    dictionary.Remove(A[i]);
                    sum += A[i];
                }
                i++;
            }
            Console.WriteLine($"sum: {sum} csum:{checkSum} length:{A.Length} i:{i} X:{X}");
            return i-1;
        }*/
        
          {
            Int64 sum = 0;
            double avg;
            Dictionary<int, bool> dictionary = Enumerable.Range(1, X).ToDictionary(v => v, v => true);
            avg = ((X + 1) / 2.0);
            Int64 checkSum = (Int64)(avg * (Int64)X);
            int i = 0;
            while (sum != checkSum)
            {
                if (i == A.Length)
                {
                    //Console.WriteLine($"sum: {sum} csum:{checkSum} length:{A.Length} i:{i} X:{X} avg:{avg}");                    
                    return -1;
                }
                if (dictionary.ContainsKey(A[i]))
                {
                    dictionary.Remove(A[i]);
                    sum += (Int64)A[i];
                }
                i++;
            }
            //Console.WriteLine($"sum: {sum} csum:{checkSum} length:{A.Length} i:{i} X:{X} avg:{avg}");

            return i -1;
        }
        
        static void Main(string[] args)
        {
            Random rnd = new Random();
            int[] A = Enumerable.Range(1,100000).OrderBy(x=>rnd.Next()).ToArray();
            /*A[0] = 1;
            A[1] = 3;
            A[2] = 1;
            A[3] = 4;
            A[4] = 2;
            A[5] = 3;
            A[6] = 5;
            A[7] = 4;
            */Console.WriteLine("Hello World!");
            Console.WriteLine(Program.solution(90000, A));
        }
    }
}
