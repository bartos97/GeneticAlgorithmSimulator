using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class UniformCrossingOperator : IBinaryOperator
    {
        private static readonly Random rand = new();

        public void ApplyOn(Individual individual1, Individual individual2)
        {
#if DEBUG
            OnePointCrossingOperator.AssertChromosomes(individual1, individual2);
#endif
            for (int i = 0; i < individual1.Chromosomes.Length; i++)
            {
                for (int j = 0; j < individual1.Chromosomes[i].Builder.Length; j++)
                {
                    if (TossACoinToYourWitcher())
                    {
                        OnePointCrossingOperator.Swap(individual1.Chromosomes[i], individual2.Chromosomes[i], j, 1);
                    }
                }
            }
        }

        private static bool TossACoinToYourWitcher()
        {
            return rand.Next(0, 100) < 50;
        }
    }
}
