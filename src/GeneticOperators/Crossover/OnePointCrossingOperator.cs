using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class OnePointCrossingOperator : IBinaryOperator
    {
        public void ApplyOn(Individual individual1, Individual individual2)
        {
#if DEBUG
            AssertChromosomes(individual1, individual2);
#endif
            if (ReferenceEquals(individual1, individual2))
                return;

            for (int i = 0; i < individual1.Chromosomes.Length; i++)
            {
                int genesLength = individual1.Chromosomes[i].Builder.Length;
                int middleIndex = genesLength / 2;
                int cutoffLength = genesLength - middleIndex;
                Swap(individual1.Chromosomes[i], individual2.Chromosomes[i], middleIndex, cutoffLength);
            }
        }

        public static void Swap(Chromosome chrom1, Chromosome chrom2, int startIndex, int cutoffLength)
        {
            var sb1 = chrom1.Builder;
            var sb2 = chrom2.Builder;
            var copyInd1 = sb1.ToString(startIndex, cutoffLength);

            sb1.Remove(startIndex, cutoffLength);
            sb1.Insert(startIndex, sb2.ToString(startIndex, cutoffLength));
            sb2.Remove(startIndex, cutoffLength);
            sb2.Insert(startIndex, copyInd1);
        }

        public static void AssertChromosomes(Individual individual1, Individual individual2)
        {
            Debug.Assert(individual1.Chromosomes.Length == individual2.Chromosomes.Length);
            for (int i = 0; i < individual1.Chromosomes.Length; i++)
            {
                Debug.Assert(individual1.Chromosomes[i].Builder.Length == individual2.Chromosomes[i].Builder.Length);
            }
        }
    }
}
