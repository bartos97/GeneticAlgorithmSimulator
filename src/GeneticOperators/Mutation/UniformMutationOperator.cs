using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Mutation
{
    public class UniformMutationOperator : IUnaryOperator
    {
        private static readonly Random rand = new();
        private readonly double min;
        private readonly double max;

        public UniformMutationOperator(double min, double max)
        {
            this.min = min;
            this.max = max;
        }

        public void ApplyOn(Individual individual)
        {
            var which = rand.Next(0, individual.Chromosomes.Length);
            individual.Chromosomes[which] = rand.NextDouble() * (max - min) + min;
        }
    }
}
