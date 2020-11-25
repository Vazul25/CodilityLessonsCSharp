using System;
using System.Linq;

namespace AbsDistinct
{
    public class AbsDistinct
    {

        public static int Solution(int[] A)
        {
            if (A.Length == 0)
                return 0;
            if (A.Length == 1)
                return 1;
            //find 0 to start caterpillar from
            int startIndex = Array.BinarySearch(A, 0);
            if (startIndex < 0)
                startIndex = ~startIndex;
            int head = startIndex == A.Length ? startIndex - 1 : startIndex;
            var tail = head;
            var abscount = 0;
            while (tail >= 0 || head < A.Length)
            {

                abscount++;
                //drop samevalues in negative
                while (tail != -1 && A[tail] != Int32.MinValue && A[head] == Math.Abs(A[tail]))
                {
                    tail--;
                }
                //drop same positive values,
                var currentHead = A[head];
                while (head + 1 < A.Length && (A[head + 1] == currentHead))
                {
                    head++;
                }

                var currentTail = tail == -1 ? 0 : A[tail];
                //drop negative numbers til abs less than the head 
                while (tail >= 0 && A[tail] != Int32.MinValue && Math.Abs(A[tail]) < currentHead)
                {
                    tail--;
                    if (tail == -1)
                    {
                        if (currentTail != A[1])
                            abscount++;
                    }
                    else if (currentTail != A[tail])
                    {
                        abscount++;
                        currentTail = A[tail];
                    }
                }
                //drop same negatives
                while (tail != -1 && A[tail] != Int32.MinValue && A[head] == Math.Abs(A[tail]))
                {
                    tail--;
                }

                head++;
                //if we traversed all positive then traverse the negative rest, all of which has  greater abs than head
                if (head >= A.Length && tail != -1)
                {
                    if (startIndex == A.Length)
                        abscount = 0;
                    currentTail = A[tail];
                    //traverse all other negative
                    while (tail >= 0)
                    {
                        tail--;
                        if (tail == -1)
                        {
                            return ++abscount;
                        }

                        if (currentTail != A[tail])
                        {
                            abscount++;
                            currentTail = A[tail];
                        }
                    }
                }
            }
            return abscount;
        }

        public static void Draw(int[] A, int tail, int head, int abscount)
        {
            Console.WriteLine();
            Console.WriteLine("currentAbsCount: " + abscount);
            for (int i = 0; i < A.Length; i++)
            {
                Console.Write("{0,4}", A[i]);
            }
            Console.WriteLine();

            for (int i = 0; i < A.Length; i++)
            {
                if (i == tail && i == head)
                    Console.Write("{0,4}", 'X');
                else if (i == tail)
                    Console.Write("{0,4}", 'T');
                else if (i == head)
                    Console.Write("{0,4}", 'H');
                else
                    Console.Write("{0,4}", ' ');
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            int[] A1 = new int[] { -7, -5, -2, 2, 3, 5, 6 };
            int[] A2 = new int[] { -5, -2, -1, 2, 2, 2, 5, 7, 8 };
            int[] A3 = new int[] { -1, -1, -1 };
            int[] A4 = new int[] { -5, -2, -1, 2, 2, 2 };
            int[] A6 = new int[] { -1, -1, -1, -2, -2, -2, -3, -3, -3, -4 };
            Console.WriteLine(Solution(A6));

            Console.WriteLine("Hello World!");
        }
    }
}
