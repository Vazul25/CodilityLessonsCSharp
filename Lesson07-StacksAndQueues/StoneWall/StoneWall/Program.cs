using System;
using System.Collections;
using System.Collections.Generic;

namespace StoneWall
{
    class Program
    {
        #region BrokenDreams
        //public static int GetNeededBlocks(Queue<int> group)
        //{
        //    if (group.Count == 0)
        //        return 0;
        //    if (group.Count == 1)
        //        return 1;
        //    var newGroup = new Queue<int>();
        //    var blockHeight = group.Dequeue();

        //    while (group.Count > 0 && group.Peek() >= blockHeight )
        //    {
        //        if (group.Peek() == blockHeight)
        //        {
        //            group.Dequeue();
        //        }
        //        else
        //        {
        //            newGroup.Enqueue(group.Dequeue());
        //        }
        //    }
        //    return 1 + GetNeededBlocks(newGroup) + GetNeededBlocks(group);
        //}
        //public static int Solution1BadButInterestingWayOfThinking(int[] H)
        //{
        //    Queue<int> wall = new Queue<int>(H);
        //    return GetNeededBlocks(wall); 

        //}
        #endregion
        public static int Solution(int[] heights)
        {
            int stoneCounter = 0;
            Stack<int> wall = new Stack<int>();
            foreach (var height in heights)
            {
                while (wall.Count > 0 && wall.Peek() > height)
                {
                    wall.Pop();
                }
                if (wall.Count == 0 || wall.Peek() < height)
                {
                    wall.Push(height);
                    stoneCounter++;
                }
            }
            return stoneCounter;

        }
        static void Main(string[] args)
        {
            Console.WriteLine(Solution(new int[] { 8, 8, 5, 7, 9, 8, 7, 4, 8 }));
            Console.WriteLine("Hello World!");
        }
    }
}
