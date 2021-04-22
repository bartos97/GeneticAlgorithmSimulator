using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class TwoPointCrossingOperator : IBinaryOperator
    {
        private static readonly Random rand = new();

        public void ApplyOn(Individual individual1, Individual individual2)
        {
#if DEBUG
            OnePointCrossingOperator.AssertChromosomes(individual1, individual2);
#endif
            int maxCutoffLength = individual1.Chromosomes[0].Builder.Length - 4;
            for (int i = 0; i < individual1.Chromosomes.Length; i++)
            {
                int cutoffLength = rand.Next(2, maxCutoffLength);
                int cutoffStart = rand.Next(0, individual1.Chromosomes[i].Builder.Length - cutoffLength);
                OnePointCrossingOperator.Swap(individual1.Chromosomes[i], individual2.Chromosomes[i], cutoffStart, cutoffLength);
            }
        }
    }
}
