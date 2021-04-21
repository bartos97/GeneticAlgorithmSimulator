﻿using GeneticAlgorithmSimulator.Models;
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
                var lastGene = chromosome.Builder[^1];
                chromosome.Builder[^1] = lastGene == '0' ? '1' : '0';
            }
        }
    }
}
