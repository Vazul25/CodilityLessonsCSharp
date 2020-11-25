using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System;
using System.Linq;

namespace peaksTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [TestCase(new int[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 }, 3)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1 }, 3)]
        [TestCase(new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 }, 5)]
        [TestCase(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 }, 5)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1 }, 3)]
        [TestCase(new int[] { 5, 1, 1, 5 }, 0)]
        [TestCase(new int[] { 5, 1, 2, 1, 5, 5 }, 1)]
        [TestCase(new int[] { 1, 2, 1, 2, 1 }, 1)]
        [TestCase(new int[] { 5, 5 }, 0)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 5 }, 0)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1 }, 1)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1 }, 2)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 5, 1, 1, 1, 5, 1, 1, 1, 4, 1 }, 4)]
        [TestCase(new int[] { 1, 3, 2, 1 }, 1)]
        [TestCase(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 }, 4)]
        [TestCase(new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 }, 4)]
        [TestCase(new int[] { 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, }, 2)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, }, 4)]
        [TestCase(new int[] { 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, }, 4)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 4, 1, }, 4)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, }, 2)]


        public void MySolution_OnHandmadeData_ReturnsExpectedValue(int[] a, int expected)
        {
            var result = Peaks.Peaks.MySolution(a);
            if (result != expected)
                System.Console.WriteLine("new int[]{" + string.Join(", ", a) + "}");
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new int[] { 1, 5, 3, 4, 3, 4, 1, 2, 3, 4, 6, 2 }, 3)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1 }, 3)]
        [TestCase(new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 }, 5)]
        [TestCase(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 }, 5)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1 }, 3)]
        [TestCase(new int[] { 5, 1, 1, 5 }, 0)]
        [TestCase(new int[] { 5, 1, 2, 1, 5, 5 }, 1)]
        [TestCase(new int[] { 1, 2, 1, 2, 1 }, 1)]
        [TestCase(new int[] { 5, 5 }, 0)]
        [TestCase(new int[] { }, 0)]
        [TestCase(new int[] { 5 }, 0)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1 }, 1)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1 }, 2)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 5, 1, 1, 1, 5, 1, 1, 1, 4, 1 }, 4)]
        [TestCase(new int[] { 1, 3, 2, 1 }, 1)]
        [TestCase(new int[] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 }, 4)]
        [TestCase(new int[] { 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 }, 4)]
        [TestCase(new int[] { 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, }, 2)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, }, 4)]
        [TestCase(new int[] { 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, 1, }, 4)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 4, 1, }, 4)]
        [TestCase(new int[] { 1, 4, 1, 1, 1, 1, 1, 4, 1, 1, 1, 4, 1, 1, 1, 4, }, 2)]


        public void MySolutionWithPrefixSums_OnHandmadeData_ReturnsExpectedValue(int[] a, int expected)
        {
            var result = Peaks.Peaks.MySolutionWIthPrefixSums(a);
            if (result != expected)
                System.Console.WriteLine("new int[]{" + string.Join(", ", a) + "}");
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        public void OnlineSolution_WithRandomSequence_SameResultAsMySolution()
        {
            int[] a = new int[50000];
            var r = new Random();
            for (int i = 0; i < 5000; i++)
            {
                a = a.Select(_ => r.Next(0, 30)).ToArray();
                var result = Peaks.Peaks.MySolutionWIthPrefixSums(a);
                var result3 = Peaks.Peaks.MySolution(a);
                var result2 = Peaks.Peaks.onlineSolution(a);
                if (result != result2 || result != result3)
                {
                    System.Console.WriteLine("new int[]{" + string.Join(", ", a) + "}");
                    System.Console.WriteLine(result2 + " , " + result);
                }
                Assert.That(result, Is.EqualTo(result2));
            }

        }
        [Test]
        public void OnlineSolution_WithFirstHalfNoPeak_SameResultAsMySolution()
        {
            int[] a = new int[50000];
            var r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                a = a.Select((_, index) => index < a.Length / 2 ? 0 : r.Next(0, 30)).ToArray();
                var result = Peaks.Peaks.MySolutionWIthPrefixSums(a);
                var result3 = Peaks.Peaks.MySolution(a);
                var result2 = Peaks.Peaks.onlineSolution(a);
                if (result != result2 || result != result3)
                {
                    System.Console.WriteLine("new int[]{" + string.Join(", ", a) + "}");
                    System.Console.WriteLine(result2 + " , " + result);
                }
                Assert.That(result, Is.EqualTo(result2));
            }

        }
        [Test]
        public void OnlineSolution_WithSecondHalfNoPeak_SameResultAsMySolution()
        {
            int[] a = new int[50000];
            var r = new Random();
            for (int i = 0; i < 1000; i++)
            {

                a = a.Select((_, index) => index > a.Length / 2 ? 0 : r.Next(0, 30)).ToArray();
                var result = Peaks.Peaks.MySolutionWIthPrefixSums(a);
                var result3 = Peaks.Peaks.MySolution(a);
                var result2 = Peaks.Peaks.onlineSolution(a);
                if (result != result2 || result != result3)
                {
                    System.Console.WriteLine("new int[]{" + string.Join(", ", a) + "}");
                    System.Console.WriteLine(result2 + " , " + result);
                }
                Assert.That(result, Is.EqualTo(result2));
            }

        }
        [Test]
        public void OnlineSolution_NoPeak_SameResultAsMySolution()
        {
            int[] a = new int[50000];
            var r = new Random();
            a = a.Select((_, index) => 0).ToArray();
            var result = Peaks.Peaks.MySolutionWIthPrefixSums(a);
            var result3 = Peaks.Peaks.MySolution(a);
            var result2 = Peaks.Peaks.onlineSolution(a);
            if (result != result2 || result != result3)
            {
                System.Console.WriteLine("new int[]{" + string.Join(", ", a) + "}");
                System.Console.WriteLine(result2 + " , " + result);

            }
            Assert.That(result, Is.EqualTo(result2));
        }
        public void OnlineSolution_WithOnePeak_SameResultAsMySolution()
        {
            int[] a = new int[50000];
            var r = new Random();
            for (int i = 0; i < 1000; i++)
            {

                int peakLocation = r.Next(1, a.Length - 1);
                a = a.Select((_, index) => index == peakLocation ? 5 : 0).ToArray();
                var result = Peaks.Peaks.MySolutionWIthPrefixSums(a);
                var result3 = Peaks.Peaks.MySolution(a);
                var result2 = Peaks.Peaks.onlineSolution(a);
                if (result != result2 || result != result3)
                {
                    System.Console.WriteLine("new int[]{" + string.Join(", ", a) + "}");
                    System.Console.WriteLine(result2 + " , " + result);

                }
                Assert.That(result, Is.EqualTo(result2));
            }

        }
    }

}