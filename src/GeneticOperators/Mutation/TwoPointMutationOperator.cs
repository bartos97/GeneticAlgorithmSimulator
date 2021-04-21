using GeneticAlgorithmSimulator.Models;
using System;

namespace GeneticAlgorithmSimulator.GeneticOperators.Mutation
{
    public class TwoPointMutationOperator : IUnaryOperator
    {
        public void ApplyOn(Individual individual)
        {
            foreach (var chromosome in individual.Chromosomes)
            {
                int randomLocus1, randomLocus2;
                do
                {
                    randomLocus1 = OnePointMutationOperator.GetRandomLocus(chromosome);
                    randomLocus2 = OnePointMutationOperator.GetRandomLocus(chromosome);
                } while (randomLocus1 == randomLocus2);

                EdgeMutationOperator.MutateAt(chromosome, randomLocus1);
                EdgeMutationOperator.MutateAt(chromosome, randomLocus2);
            }
        }

        
    }
}
