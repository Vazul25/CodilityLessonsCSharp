using NUnit.Framework;

namespace AbsDistinctTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new int[] { -7,-5, -2, 2, 3, 5, 6 },5)]
        [TestCase(new int[] { 2, 5, 6 },3)]
        [TestCase(new int[] { 2, 5,5,5,5, 6,6,6 }, 3)]
        [TestCase(new int[] { -2, -5, -6 },3)]
        [TestCase(new int[] { -5, -2, -1, 2, 2, 2,5,7,8 },5)]
        [TestCase(new int[] { },0)]
        [TestCase(new int[] { 1},1)]
        [TestCase(new int[] { 1,2},2)]
        [TestCase(new int[] { 1,1},1)]
        [TestCase(new int[] { -1,1},1)]
        [TestCase(new int[] { -2, 1},2)]
        [TestCase(new int[] { -1, 2},2)]
        [TestCase(new int[] { -1}, 1)]
        [TestCase(new int[] { -1,-1}, 1)]
        [TestCase(new int[] { -1,-1,2}, 1)]
        [TestCase(new int[] { 1,2,2,2,2}, 2)]
        [TestCase(new int[] { 0,1,2,2,2,3,3,4}, 5)]
        [TestCase(new int[] { -1,0,1,2,2,2,3,3,4}, 5)]
        [TestCase(new int[] { -4,-3,-2,-1,0,1,2,2,2,3,3,4}, 5)]
        [TestCase(new int[] { -2147483648, 0 }, 2)]
        [TestCase(new int[] { 0, 1, 2, 2, 2, 3, 3, 4,4,4,4,4 }, 5)]
        [TestCase(new int[] { 0, 1, 2, 3,4,5,6,7,8,9,10}, 11)]
        [TestCase(new int[] { 0, -1,-1, -1, -2, -3,-4,-5,-6,-7,-8,-9,-10,-10,-10 }, 11)]
        [TestCase(new int[] { -1,-1,-1,-2,-2,-2,-3,-3,-3 }, 3)]
        [TestCase(new int[] { -1,-1,-1,-2,-2,-2,-3,-3,-3,-4 }, 4)]



        public void Test1(int[] A, int expected)
        {
            var result = AbsDistinct.AbsDistinct.Solution(A);
            Assert.That(result,Is.EqualTo(expected));
        }
    }
}