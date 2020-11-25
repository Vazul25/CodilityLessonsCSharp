using NUnit.Framework;
using System.Linq;

namespace CountDistSliceTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new int[] { 3, 4, 5, 5, 2 }, 9)]
        [TestCase(new int[] {  }, 0)]
        [TestCase(new int[] { 1 }, 1)]
        [TestCase(new int[] { 1,1 }, 2)]
        [TestCase(new int[] { 1,2 }, 3)]
        [TestCase(new int[] { 1,1,3 }, 4)]
        [TestCase(new int[] { 1,2,3 }, 6)]
        [TestCase(new int[] { 1,2,1,2 }, 7)]
        [TestCase(new int[] { 1,2,3,4,5,6,7,3,2,4 }, 45)]
        public void Solution_OnCustomData_ReturnsExpected(int[] A, int expected)
        {
            var result = CountDistinctSlices.Program.Solution(A.Length, A);
            Assert.That(result,Is.EqualTo(expected));
        }
        [Test]
        public void Solution_OnLargeRange_Returns1000000000()
        {
            var result = CountDistinctSlices.Program.Solution(100000, Enumerable.Range(0, 100000).ToArray());
            Assert.That(result, Is.EqualTo(1000000000));
        }
    }
}