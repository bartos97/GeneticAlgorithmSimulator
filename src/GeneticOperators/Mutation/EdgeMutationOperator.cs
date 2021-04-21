using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Mutation
{
    public class EdgeMutationOperator : IUnaryOperator
    {
        public void ApplyOn(Individual individual)
        {
            foreach (var chromosome in individual.Chromosomes)
            {
                MutateAt(chromosome, chromosome.Builder.Length - 1);
            }
        }

        public static void MutateAt(Chromosome chromosome, int index)
        {
            char gene = chromosome.Builder[index];
            chromosome.Builder[index] = gene == '0' ? '1' : '0';
        }
    }
}
