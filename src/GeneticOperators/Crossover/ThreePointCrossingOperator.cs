using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class ThreePointCrossingOperator : IBinaryOperator
    {
        public void ApplyOn(Individual individual1, Individual individual2)
        {
#if DEBUG
            OnePointCrossingOperator.AssertChromosomes(individual1, individual2);
#endif
            int cutoffStart1 = individual1.Chromosomes[0].Builder.Length / 4;
            int cutoffStart3 = cutoffStart1 * 3;
            for (int i = 0; i < individual1.Chromosomes.Length; i++)
            {
                OnePointCrossingOperator.Swap(individual1.Chromosomes[i], individual2.Chromosomes[i], cutoffStart1, cutoffStart1);
                OnePointCrossingOperator.Swap(individual1.Chromosomes[i], individual2.Chromosomes[i], cutoffStart3, cutoffStart1);
            }
        }
    }
}
