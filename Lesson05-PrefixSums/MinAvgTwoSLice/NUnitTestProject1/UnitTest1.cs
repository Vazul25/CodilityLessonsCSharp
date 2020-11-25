using Microsoft.VisualStudio.TestPlatform.TestHost;
using MinAvgTwoSLice;
using NUnit.Framework;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void Test2()
        {
            int[] A = new int[] { 4, 2, 2, 5, 1, 5, 8 };

            var result = Solution.solution(A);
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void Test1()
        {
            int[] A = new int[] { -3, -5, -8, -4, -10 };

            var result = Solution.solution(A);
            Assert.That(result, Is.EqualTo(2));
        }
    }
}