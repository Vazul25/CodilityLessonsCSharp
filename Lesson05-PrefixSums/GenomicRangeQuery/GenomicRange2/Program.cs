using Microsoft.VisualBasic.CompilerServices;
using System;

namespace GenomicRange2
{
    class Program
    {

        public struct GenomeCounter
        {
            public int A { get; set; }
            public int C { get; set; }
            public int G { get; set; }
            public int T { get; set; }
            public int MinimalImpactFactor()
            {
                if (A != 0)
                    return 1;
                if (C != 0)
                    return 2;               
                if (G != 0)
                    return 3;
                if (T != 0)
                    return 4;
                return -1;
            }
            public static GenomeCounter operator -(GenomeCounter A, GenomeCounter B) => new GenomeCounter
            {
                A = A.A - B.A,
                C = A.C - B.C,
                G = A.G - B.G,
                T = A.T - B.T
            };
            public override string ToString()
            {
                return $"A:{A} C:{C} G:{G} T:{T}";
            }
            public void AddGenome(char genome)
            {
                switch (genome)
                {
                    case 'A':
                        A++;
                        break;
                    case 'C':
                        C++;
                        break;
                    case 'G':
                        G++;
                        break;
                    case 'T':
                        T++;
                        break;
                    default:
                        throw new ArgumentException("genome must be A,C,G or T");
                }
            }
        }
        public int[] solution(string S, int[] P, int[] Q)
        {

            GenomeCounter[] genomeSequencePrefixCounter = new GenomeCounter[S.Length];
            genomeSequencePrefixCounter[0] = new GenomeCounter();
            genomeSequencePrefixCounter[0].AddGenome(S[0]);

            for (int i = 1; i < S.Length; i++)
            {
                genomeSequencePrefixCounter[i] = genomeSequencePrefixCounter[i - 1];
                genomeSequencePrefixCounter[i].AddGenome(S[i]);
            }
            var results = new int[P.Length];
            for (int i = 0; i < P.Length; i++)
            {
                if (P[i] == 0)
                {
                    results[i] = genomeSequencePrefixCounter[Q[i]].MinimalImpactFactor();
                    continue;
                }
                var sequence = genomeSequencePrefixCounter[Q[i]] - genomeSequencePrefixCounter[P[i] - 1];
                results[i] = sequence.MinimalImpactFactor();
            }
            return results;
        }
        static void Main(string[] args)
        {
            int[] P = new int[] { 2, 5, 0 };
            int[] Q = new int[] {4,5,6 };
            string genome = "CAGCCTA";
            var p = new Program();
            var result = p.solution(genome, P, Q);
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }
        }
    }
}
