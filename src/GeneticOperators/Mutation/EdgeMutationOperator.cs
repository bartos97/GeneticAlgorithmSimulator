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
            var sb = individual.Chromosomes[^1].Builder;
            sb[^1] = sb[^1] == '0' ? '1' : '0';
        }
    }
}
