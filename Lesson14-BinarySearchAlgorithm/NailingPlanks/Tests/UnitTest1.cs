using NUnit.Framework;

namespace Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }
        [TestCase(new int[] { 1, 4, 5, 8 }, new int[] { 4, 5, 9, 10 }, new int[] { 4, 6, 7, 10, 2 }, 4)]
        [TestCase(new int[] { 1 }, new int[] { 1 }, new int[] { 1 }, 1)]
        [TestCase(new int[] { 1, 4 }, new int[] { 2, 7 }, new int[] { 4 }, -1)]
        [TestCase(new int[] { 1, 3, 5, 7, 11 }, new int[] { 5, 7, 5, 11, 14 }, new int[] { 3, 4, 5, 2, 1, 10, 13 }, 7)]
        public void Test2(int[] A, int[] B, int[] C, int expected)
        {
            var result = NailingPlanks.NailingPlanks.Solution2(A, B, C);

            Assert.That(result, Is.EqualTo(expected));
        }
        [TestCase(new int[] { 1, 4, 5, 8 }, new int[] { 4, 5, 9, 10 }, new int[] { 4, 6, 7, 10, 2 }, 4)]
        [TestCase(new int[] {1 }, new int[] {1 }, new int[] { 1}, 1)]
        [TestCase(new int[] { 1,4}, new int[] {2,7 }, new int[] { 4}, -1)]
        [TestCase(new int[] { 1,3,5,7,11}, new int[] {5,7,5,11,14 }, new int[] { 3,4,5,2,1,10,13}, 7)]
        public void Test1(int[] A, int[] B, int[] C, int expected)
        {
            var result = NailingPlanks.NailingPlanks.Solution(A, B, C);

            Assert.That(result, Is.EqualTo(expected));
        }
    }
}