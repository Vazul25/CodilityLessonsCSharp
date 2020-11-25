using NUnit.Framework;

namespace FrogTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new int[] { 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0 },3)]
        [TestCase(new int[] { 0, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0 },-1)]
        [TestCase(new int[] { 1, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1 },3)]
        [TestCase(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },3)]
        [TestCase(new int[] {  },1)]
        [TestCase(new int[] { 0 },1)]
        [TestCase(new int[] { 0,0,0 },-1)]
        [TestCase(new int[] { 0,0,0,0 },1)]
        [TestCase(new int[] { 0,0,0,0,1 },2)]
        public void solution_onHandmadeData_returnsExpected(int[] A, int expected)
        {

            var result = FibFrog.FibFrog.solution(A);
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}