using System;

namespace Iterations1
{
    class Program
    {
        public static int binaryGap(int N)
        {
            if (N == 1) return 0;
            int lastBinaryDigit = (int) Math.Floor(Math.Log2(N));
            int lastBinaryDigitValue = (int) Math.Pow(2, lastBinaryDigit);
            int maxGap = 0;
            int gap = 0;
            int remaining = N - lastBinaryDigitValue;
            //string binaryString = "1";
 
            /*if (remaining == 0)
            {
                return lastBinaryDigit;
            }*/
            while( remaining > 0)
            {
                lastBinaryDigitValue = lastBinaryDigitValue / 2;
                lastBinaryDigit--;
                if ( remaining / lastBinaryDigitValue > 0 )
                {
                    gap = 0;
                    remaining -= lastBinaryDigitValue;
                    if (maxGap > lastBinaryDigit)
                    {
                        return maxGap;
                    }
                    //binaryString += '1';
                } else
                {
                    //binaryString += '0';
                    gap++;
                    maxGap = gap > maxGap ? gap : maxGap;
                }
                /*if(remaining == 0 && lastBinaryDigit > 0)
                {
                    gap += lastBinaryDigit;
                    maxGap = gap > maxGap ? gap : mayclicxGap;
                }*/
                
            }
            return maxGap;
        }
        static void Main(string[] args)
        {
            while (true)
            {
                int a = Int32.Parse(Console.ReadLine());
                Console.WriteLine($"Gap: {Program.binaryGap(a)}");

            }
        }
    }
}
