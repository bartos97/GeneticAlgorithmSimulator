using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class OnePointCrossingOperator : IBinaryOperator
    {
        public void ApplyOn(Individual individual1, Individual individual2)
        {
            var copy = individual1.Chromosomes[1].Genes;
            individual1.Chromosomes[1].Builder.Clear();
            individual1.Chromosomes[1].Builder.Append(individual2.Chromosomes[1].Builder);
            individual2.Chromosomes[1].Builder.Clear();
            individual2.Chromosomes[1].Builder.Append(copy);
        }
    }
}
