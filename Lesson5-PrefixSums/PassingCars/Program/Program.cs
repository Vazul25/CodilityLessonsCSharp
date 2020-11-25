using System;

namespace Program
{
    class Program
    {
        public static int Solution(int[] A) {
            int passedCars = 0;
            int eastwardMovingCars = 0;
            foreach (var car in A)
            {
                if (car == 0)
                    eastwardMovingCars++;
                else
                    passedCars += eastwardMovingCars;

            }
            return passedCars;
        }
        static void Main(string[] args)
        {
            int[] A = new int[] { 0, 1, 0, 1, 1 };
            Console.WriteLine(Solution(A));
            Console.WriteLine("Hello World!");
        }
    }
}
