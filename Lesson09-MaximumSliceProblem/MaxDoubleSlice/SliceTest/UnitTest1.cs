using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using MaxDoubleSlice;
using System.Linq;
using System;

namespace SliceTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }


        [TestCase(new int[] { 3, 2, 6, -1, 4, 5, -1, 2 }, 17)]
        [TestCase(new int[] { 1, 1, 1, 1, 50, 50, 1, 1, 1, 1 }, 105)]
        [TestCase(new int[] { 5, 9, 10, 15, -1, 2, -2, -3, -20, -50, -60, 30, 20, -5, 20 - 60 }, 50)]
        [TestCase(new int[] { -1, -10, -100, -1000, -10000 }, 0)]
        [TestCase(new int[] { 1, 10, 100, 1000, 2, 20, 200, 2000, 20000 }, 3330)]
        [TestCase(new int[] { 1, 10, 100, -1000, 2, 20, 200, 2000, -20000 }, 2332)]
        [TestCase(new int[] { 8000, -8001, 6001, 7001, -7000, -5101, -6076, 599, 9137 }, 13002)]
        [TestCase(new int[] { 0, 50, -100, 5, -51, 100, 200 }, 105)]

        [TestCase(new int[] { 41, 41, -46, -27, 35, -40, -4, 16, 10, -2, -5, -1, 37, -49, 7, 0, 29, -40, -21, 28 }, 91)]
        [TestCase(new int[] { 12, 41, 8, 5, -36, -44, -3, -14, -19, -26, -36, 2, 9, -41, -41, -23, -45, 25, -24, 29 }, 54)]
        [TestCase(new int[] { 23, 48, 36, -7, 27, -6, 10, -19, 2, 12, -50, 37, -25, 3, -33, -50, -17, -1, -42, 29 }, 140)]
        [TestCase(new int[] { 12, -40, -15, -15, -16, 43, 40, 27, 41, -20, -6, -47, 34, -1, -45, 13, -45, -36, 27, 20 }, 159)]
        [TestCase(new int[] { -5, -34, 33, 19, -26, -24, -23, -44, 42, -41, 44, 5, 16, -4, -33, 12, 12, 0, -41, 34 }, 107)]
        [TestCase(new int[] { 3, 32, -11, -28, -33, 46, -47, 25, -3, 3, 45, -32, 21, -12, -17, -5, -23, 16, -39, -43 }, 116)]
        [TestCase(new int[] { 16, 34, 35, 32, -15, 38, -14, -13, -18, -7, -49, 4, -45, 43, -34, 13, 26, 10, 1, -23 }, 139)]
        [TestCase(new int[] { -28, 1, 8, -12, -36, 25, 33, -22, -38, 21, -15, 46, -12, -48, -32, 19, 46, 7, 36, -22 }, 116)]
        [TestCase(new int[] { 9, 15, -8, -46, 2, -10, -38, 28, -29, 20, 18, 16, -35, 33, 34, -36, -25, -3, 17, 9 }, 121)]
        [TestCase(new int[] { -46, -43, -25, 10, -43, -46, -34, -47, 25, -3, 17, -31, 24, -28, 17, 6, -42, 3, 36, 24 }, 66)]

        [TestCase(new int[] { -10, -40, -48, -36, 34, -5, 33, 15, -34, -38 }, 82)]
        [TestCase(new int[] { -18, -38, 5, -37, -35, -23, 44, -24, 48, -25 }, 92)]
        [TestCase(new int[] { -40, -32, 1, 40, 23, 21, -48, -8, 31, -39 }, 108)]
        [TestCase(new int[] { -2, -25, 12, 36, -10, -19, 8, 45, 16, 14 }, 107)]
        [TestCase(new int[] { 9, 43, 49, 48, -35, 26, 21, -41, 5, 23 }, 187)]
        [TestCase(new int[] { 16, -2, -12, 32, 17, -28, 8, -39, -9, -38 }, 57)]
        [TestCase(new int[] { -7, -9, 33, -45, 13, -43, -1, 18, 22, 33 }, 52)]
        [TestCase(new int[] { 18, 48, 9, -17, 16, -34, -31, 3, 25, 46 }, 73)]
        [TestCase(new int[] { -50, 42, 37, -42, -5, -50, 36, 30, -32, -32 }, 98)]
        [TestCase(new int[] { 23, -3, 3, 3, -29, -32, -41, 43, -16, -43 }, 43)]
        [TestCase(new int[] { 8079, 7025, -2794, 323, 2442, 6202, -2193, -4143, -6083, 8255, -8537, -4563, 8723, 3677, -6416, 8519, -2610, 8660, 9392, -3713 },36361]
    public void SolutionWithCatterpillarLikeAlgorithm_ReturnsExpectedValues(int[] A, int expected)
        {
            var result = MaxDoubleSlice.Program.SolutionWithCatterpillarLikeAlgorithm(A);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new int[] { 3, 2, 6, -1, 4, 5, -1, 2 }, 17)]
        [TestCase(new int[] { 1, 1, 1, 1, 50, 50, 1, 1, 1, 1 }, 105)]
        [TestCase(new int[] { 5, 9, 10, 15, -1, 2, -2, -3, -20, -50, -60, 30, 20, -5, 20 - 60 }, 50)]
        [TestCase(new int[] { -1, -10, -100, -1000, -10000 }, 0)]
        [TestCase(new int[] { 1, 10, 100, 1000, 2, 20, 200, 2000, 20000 }, 3330)]
        [TestCase(new int[] { 1, 10, 100, -1000, 2, 20, 200, 2000, -20000 }, 2332)]
        [TestCase(new int[] { 8000, -8001, 6001, 7001, -7000, -5101, -6076, 599, 9137 }, 13002)]
        [TestCase(new int[] { 0, 50, -100, 5, -51, 100, 200 }, 105)]

        [TestCase(new int[] { 41, 41, -46, -27, 35, -40, -4, 16, 10, -2, -5, -1, 37, -49, 7, 0, 29, -40, -21, 28 }, 91)]
        [TestCase(new int[] { 12, 41, 8, 5, -36, -44, -3, -14, -19, -26, -36, 2, 9, -41, -41, -23, -45, 25, -24, 29 }, 54)]
        [TestCase(new int[] { 23, 48, 36, -7, 27, -6, 10, -19, 2, 12, -50, 37, -25, 3, -33, -50, -17, -1, -42, 29 }, 140)]
        [TestCase(new int[] { 12, -40, -15, -15, -16, 43, 40, 27, 41, -20, -6, -47, 34, -1, -45, 13, -45, -36, 27, 20 }, 159)]
        [TestCase(new int[] { -5, -34, 33, 19, -26, -24, -23, -44, 42, -41, 44, 5, 16, -4, -33, 12, 12, 0, -41, 34 }, 107)]
        [TestCase(new int[] { 3, 32, -11, -28, -33, 46, -47, 25, -3, 3, 45, -32, 21, -12, -17, -5, -23, 16, -39, -43 }, 116)]
        [TestCase(new int[] { 16, 34, 35, 32, -15, 38, -14, -13, -18, -7, -49, 4, -45, 43, -34, 13, 26, 10, 1, -23 }, 139)]
        [TestCase(new int[] { -28, 1, 8, -12, -36, 25, 33, -22, -38, 21, -15, 46, -12, -48, -32, 19, 46, 7, 36, -22 }, 116)]
        [TestCase(new int[] { 9, 15, -8, -46, 2, -10, -38, 28, -29, 20, 18, 16, -35, 33, 34, -36, -25, -3, 17, 9 }, 121)]
        [TestCase(new int[] { -46, -43, -25, 10, -43, -46, -34, -47, 25, -3, 17, -31, 24, -28, 17, 6, -42, 3, 36, 24 }, 66)]

        [TestCase(new int[]{-10, -40, -48, -36, 34, -5, 33, 15, -34, -38},82)  ]
        [TestCase(new int[]{-18, -38, 5, -37, -35, -23, 44, -24, 48, -25},92)  ]
        [TestCase(new int[] { -40, -32, 1, 40, 23, 21, -48, -8, 31, -39 },108)  ]
        [TestCase(new int[] { -2, -25, 12, 36, -10, -19, 8, 45, 16, 14 },107)   ]
        [TestCase(new int[] { 9, 43, 49, 48, -35, 26, 21, -41, 5, 23 },187)     ]
        [TestCase(new int[] { 16, -2, -12, 32, 17, -28, 8, -39, -9, -38 },57)  ]
        [TestCase(new int[] { -7, -9, 33, -45, 13, -43, -1, 18, 22, 33 },52)   ]
        [TestCase(new int[] { 18, 48, 9, -17, 16, -34, -31, 3, 25, 46 },73)    ]
        [TestCase(new int[] { -50, 42, 37, -42, -5, -50, 36, 30, -32, -32 },98)]
        [TestCase(new int[] { 23, -3, 3, 3, -29, -32, -41, 43, -16, -43 },43)   ]
        public void SolutionWithPrefixPostfixSums_ReturnsExpectedValues(int[] A, int expected)
        {
            var result = MaxDoubleSlice.Program.SolutionWithPrefixPostfixSums(A);
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCase(new int[] { 3, 2, 6, -1, 4, 5, -1, 2 }, 17)]
        [TestCase(new int[] { 1, 1, 1, 1, 50, 50, 1, 1, 1, 1 }, 105)]
        [TestCase(new int[] { 5, 9, 10, 15, -1, 2, -2, -3, -20, -50, -60, 30, 20, -5, 20 - 60 }, 50)]
        [TestCase(new int[] { -1, -10, -100, -1000, -10000 }, 0)]
        [TestCase(new int[] { 1, 10, 100, 1000, 2, 20, 200, 2000, 20000 }, 3330)]
        [TestCase(new int[] { 1, 10, 100, -1000, 2, 20, 200, 2000, -20000 }, 2332)]
        [TestCase(new int[] { 8000, -8001, 6001, 7001, -7000, -5101, -6076, 599, 9137 }, 13002)]
        [TestCase(new int[] { 0, 50, -100, 5, -51, 100, 200 }, 105)]

        [TestCase(new int[] { 41, 41, -46, -27, 35, -40, -4, 16, 10, -2, -5, -1, 37, -49, 7, 0, 29, -40, -21, 28 }, 91)]
        [TestCase(new int[] { 12, 41, 8, 5, -36, -44, -3, -14, -19, -26, -36, 2, 9, -41, -41, -23, -45, 25, -24, 29 }, 54)]
        [TestCase(new int[] { 23, 48, 36, -7, 27, -6, 10, -19, 2, 12, -50, 37, -25, 3, -33, -50, -17, -1, -42, 29 }, 140)]
        [TestCase(new int[] { 12, -40, -15, -15, -16, 43, 40, 27, 41, -20, -6, -47, 34, -1, -45, 13, -45, -36, 27, 20 }, 159)]
        [TestCase(new int[] { -5, -34, 33, 19, -26, -24, -23, -44, 42, -41, 44, 5, 16, -4, -33, 12, 12, 0, -41, 34 }, 107)]
        [TestCase(new int[] { 3, 32, -11, -28, -33, 46, -47, 25, -3, 3, 45, -32, 21, -12, -17, -5, -23, 16, -39, -43 }, 116)]
        [TestCase(new int[] { 16, 34, 35, 32, -15, 38, -14, -13, -18, -7, -49, 4, -45, 43, -34, 13, 26, 10, 1, -23 }, 139)]
        [TestCase(new int[] { -28, 1, 8, -12, -36, 25, 33, -22, -38, 21, -15, 46, -12, -48, -32, 19, 46, 7, 36, -22 }, 116)]
        [TestCase(new int[] { 9, 15, -8, -46, 2, -10, -38, 28, -29, 20, 18, 16, -35, 33, 34, -36, -25, -3, 17, 9 }, 121)]
        [TestCase(new int[] { -46, -43, -25, 10, -43, -46, -34, -47, 25, -3, 17, -31, 24, -28, 17, 6, -42, 3, 36, 24 }, 66)]

        [TestCase(new int[] { -10, -40, -48, -36, 34, -5, 33, 15, -34, -38 }, 82)]
        [TestCase(new int[] { -18, -38, 5, -37, -35, -23, 44, -24, 48, -25 }, 92)]
        [TestCase(new int[] { -40, -32, 1, 40, 23, 21, -48, -8, 31, -39 }, 108)]
        [TestCase(new int[] { -2, -25, 12, 36, -10, -19, 8, 45, 16, 14 }, 107)]
        [TestCase(new int[] { 9, 43, 49, 48, -35, 26, 21, -41, 5, 23 }, 187)]
        [TestCase(new int[] { 16, -2, -12, 32, 17, -28, 8, -39, -9, -38 }, 57)]
        [TestCase(new int[] { -7, -9, 33, -45, 13, -43, -1, 18, 22, 33 }, 52)]
        [TestCase(new int[] { 18, 48, 9, -17, 16, -34, -31, 3, 25, 46 }, 73)]
        [TestCase(new int[] { -50, 42, 37, -42, -5, -50, 36, 30, -32, -32 }, 98)]
        [TestCase(new int[] { 23, -3, 3, 3, -29, -32, -41, 43, -16, -43 }, 43)]
        public void NaiveSolutionForTesting_ReturnsExpectedValues(int[] A, int expected)
        {
            var result = MaxDoubleSlice.Program.NaiveSolutionForTesting(A);
            Assert.That(result, Is.EqualTo(expected));
        }
        //Please outsider dont hate me for this and dont use this as an example for unit test, this violates a lot of best practices
        [Test]
        public void SolutionWIthPrefixPostfixSums_CorrectnessTestWithNaiveSolution()
        {
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var data = Enumerable.Range(0, 2000).Select(i => r.Next(-10000, 10000)).ToArray();
              
                var result = MaxDoubleSlice.Program.SolutionWithPrefixPostfixSums(data);
                var baselineResult = MaxDoubleSlice.Program.NaiveSolutionForTesting(data);
                Assert.That(result, Is.EqualTo(baselineResult));
                if (result != baselineResult)
                {
                    for (int j = 0; j < data.Length; j++)
                    {
                        Console.Write(data[j] + ", ");
                    }
                }
                
                Assert.That(result, Is.EqualTo(baselineResult));

            }
        }
        [Test]
        public void SolutionWithCatterpillarLikeAlgorithm_ReturnsExpectedValues_CorrectnessTestWithNaive()
        {
            Random r = new Random();
            for (int i = 0; i < 1000; i++)
            {
                var data = Enumerable.Range(0, 20).Select(i => r.Next(-10000, 10000)).ToArray();

                var result = MaxDoubleSlice.Program.SolutionWithCatterpillarLikeAlgorithm(data);
                var baselineResult = MaxDoubleSlice.Program.NaiveSolutionForTesting(data);
                if (result != baselineResult)
                {
                    Console.Write("new int[] {");
                    for (int j = 0; j < data.Length; j++)
                    {
                        Console.Write(data[j] + (j==data.Length-1?"":", "));
                    }
                    Console.Write("}");
                }

                Assert.That(result, Is.EqualTo(baselineResult));

            }
        }
    }
}