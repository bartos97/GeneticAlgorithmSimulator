using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators
{
    public class StandardInversionOperator : IUnaryOperator
    {
        private static readonly Random rand = new();

        public void ApplyOn(Individual individual)
        {
            foreach (var chromosome in individual.Chromosomes)
            {
                int start = rand.Next(0, chromosome.Builder.Length - 1);
                int end = rand.Next(start + 1, chromosome.Builder.Length);
                var cutoff = chromosome.Builder.ToString(start, end - start + 1).ToCharArray();
                Array.Reverse(cutoff);
                chromosome.Builder.Remove(start, end - start + 1);
                chromosome.Builder.Insert(start, cutoff);
            }
        }
    }
}
