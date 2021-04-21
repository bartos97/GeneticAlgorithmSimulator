using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Mutation
{
    public class OnePointMutationOperator : IUnaryOperator
    {
        private static readonly Random rand = new();

        public void ApplyOn(Individual individual)
        {
            foreach (var chromosome in individual.Chromosomes)
            {
                EdgeMutationOperator.MutateAt(chromosome, GetRandomLocus(chromosome));
            }
        }

        public static int GetRandomLocus(Chromosome chromosome)
        {
            return rand.Next(0, chromosome.Builder.Length);
        }
    }
}
