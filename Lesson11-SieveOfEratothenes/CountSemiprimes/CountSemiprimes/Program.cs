using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

namespace CountSemiprimes
{
    public class Program
    {
        [MemoryDiagnoser]
        public class Bench
        {
            int N;
            int[] P;
            int[] Q;
            [GlobalSetup]
            public void Setup()
            {
                P = new int[] { 0, 5000, 15000, 0, 5000, 15000 };
                Q = new int[] { 5000000, 5000000, 5000000, 2000000, 2000000, 2000000 };
                N = 5000000;
            }
            [Benchmark]
            public void MyOptimalisedSolution() => Program.solutionFinalMoreOptimalisation(N,P,Q);

            [Benchmark]
            public void MySolution() => Program.solutionFinal(N, P, Q);
            [Benchmark]
            public void MySolutionFirstTry() => Program.solutionFirstTry(N, P, Q);

        }
        public static List<int> getPrimesBeforeN(int N)
        {
            int primeCountApprox = (int)Math.Ceiling(N / Math.Log(N));
            var primes = new List<int>(primeCountApprox);
            bool[] visited = new bool[N];
            if (N < 2)
                return primes;
            visited[0] = true;
            visited[1] = true;
            int i = 2;
            var sqrtN = (int)Math.Sqrt(N);
            while (i <= sqrtN)
            {
                int j = 2;

                if (!visited[i])
                {
                    j = i;
                }
                while (i * j < N)
                {
                    visited[i * j] = true;
                    j++;
                }
                i++;
            }

            for (int k = 0; k < N; k++)
            {
                if (!visited[k])
                    primes.Add(k);
            }
            return primes;

        }
        public static int[] solutionFirstTry(int N, int[] P, int[] Q)
        {
            int max = N;// Q.Max();
            int primeLimit = (int)Math.Ceiling(max / 2.0);
            var primes = getPrimesBeforeN(primeLimit + 1);
            //primes = primes.OrderBy(i => i).ToList();
            primes.Sort();
            Dictionary<int, bool> isSemiPrime = new Dictionary<int, bool>();
            for (int i = 0; i < primes.Count; i++)
            {
                int j = i;

                for (; j < primes.Count; j++)
                {
                    var semiPrime = primes[i] * primes[j];
                    if (semiPrime > N)
                        break;
                    isSemiPrime[semiPrime] = true;
                }
                if (j == i)
                    break;
            }

            int[] result = new int[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                var semiPrimeCount = 0;
                for (int j = P[i]; j <= Q[i]; j++)
                {
                    bool semiPrime = false;
                    isSemiPrime.TryGetValue(j, out semiPrime);
                    if (semiPrime)
                        semiPrimeCount++;
                }
                result[i] = semiPrimeCount;
            }
            return result;
        }

        //100% on codility
        public static int[] solutionFinal(int N, int[] P, int[] Q)
        {
            int max = N;// Q.Max();
            int primeLimit = (int)Math.Ceiling(max / 2.0);
            var primes = getPrimesBeforeN(primeLimit + 1);
            //creating semiPrimes array, could be optimalised dictionary was dangling logic from old solution
            int semiPrimeCountApprox = (int)Math.Ceiling(N * (Math.Log(Math.Log(N)) / Math.Log(N)));

            Dictionary<int, bool> isSemiPrime = new Dictionary<int, bool>(semiPrimeCountApprox);
            for (int i = 0; i < primes.Count; i++)
            {
                int j = i;

                for (; j < primes.Count; j++)
                {
                    var semiPrime = primes[i] * primes[j];
                    if (semiPrime > N)
                        break;
                    isSemiPrime[semiPrime] = true;
                }
                if (j == i)
                    break;
            }
            var semiPrimeArray = isSemiPrime.Keys.OrderBy(k => k).ToArray();

            int[] result = new int[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                var semiPrimeCount = 0;
                var firstSemiPrimeIndex = Array.BinarySearch(semiPrimeArray, P[i]);
                if (firstSemiPrimeIndex < 0)
                {
                    if (~firstSemiPrimeIndex == semiPrimeArray.Length)
                    { result[i] = 0; continue; }
                }
                var lastSemiPrimeIndex = Array.BinarySearch(semiPrimeArray, Q[i]);

                int j = firstSemiPrimeIndex < 0 ? ~firstSemiPrimeIndex : firstSemiPrimeIndex;
                int k = lastSemiPrimeIndex < 0 ? ~lastSemiPrimeIndex : lastSemiPrimeIndex;
                semiPrimeCount = k - j + 1;
                if (k >= semiPrimeArray.Length || semiPrimeArray[k] > Q[i])
                    semiPrimeCount--;

                result[i] = semiPrimeCount;
            }
            return result;
        }
        
        //100% codility O(N*log(log(N)+M) extreme large test result: 0.052s OK
        public static int[] solutionFinalMoreOptimalisation(int N, int[] P, int[] Q)
        {
            int[] result = new int[P.Length];
            if (N < 4)
                return result;
            int max = N;// Q.Max();
            int primeLimit = (int)Math.Ceiling(max / 2.0);
            var primes = getPrimesBeforeN(primeLimit + 1);

         
            int semiPrimeCountApprox = (int)Math.Ceiling(N * (Math.Log(Math.Log(N)) / Math.Log(N)));
            List<int> semiPrimes = new List<int>(semiPrimeCountApprox);
            for (int i = 0; i < primes.Count; i++)
            {
                int j = i;

                for (; j < primes.Count; j++)
                {
                    var semiPrime = primes[i] * primes[j];
                    if (semiPrime > N)
                        break;
                    semiPrimes.Add(semiPrime);
                }
                if (j == i)
                    break;
            }
            semiPrimes.Sort();

            for (int i = 0; i < P.Length; i++)
            {
                var semiPrimeCount = 0;
                var firstSemiPrimeIndex = semiPrimes.BinarySearch(P[i]);  
                if (firstSemiPrimeIndex < 0)
                {
                    if (~firstSemiPrimeIndex == semiPrimes.Count)
                    { result[i] = 0; continue; }
                }
                var lastSemiPrimeIndex = semiPrimes.BinarySearch(Q[i]);

                int j = firstSemiPrimeIndex < 0 ? ~firstSemiPrimeIndex : firstSemiPrimeIndex;
                int k = lastSemiPrimeIndex < 0 ? ~lastSemiPrimeIndex : lastSemiPrimeIndex;
                semiPrimeCount = k - j + 1;
                if (k >= semiPrimes.Count|| semiPrimes[k] > Q[i])
                    semiPrimeCount--;

                result[i] = semiPrimeCount;
            }
            return result;
        }
        static void Main(string[] args)
        {
            //int[] P = new int[] { 1, 4, 16 };
            //int[] Q = new int[] { 26, 10, 20 };
            //int N = 26;
            //Console.WriteLine(String.Join(" ", solutionFinalMoreOptimalisation(N, P, Q)));
            //Console.WriteLine("Hello World!");
            //P = new int[] { 0, 5000, 15000, 0, 5000, 15000 };
            //Q = new int[] { 50000, 50000, 50000, 20000, 20000, 20000 };
            //N = 50000;
            //var result1 = solutionFinalMoreOptimalisation(N, P, Q);
            //var result2 = solutionFinal(N, P, Q);
            //if (!Enumerable.SequenceEqual(result1, result2))
            //{
            //    Console.WriteLine("Ops problems are coming chief");
            //}

            var summary = BenchmarkRunner.Run(typeof(Bench).Assembly);

        }
    }
}
