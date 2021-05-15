using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class ArithmeticCrossoverOperator : IBinaryOperator
    {
        private static readonly Random rand = new();

        public void ApplyOn(Individual individual1, Individual individual2)
        {
            double k = rand.NextDouble();
            double x1 = individual1.Chromosomes[0];
            double y1 = individual1.Chromosomes[1];
            double x2 = individual2.Chromosomes[0];
            double y2 = individual2.Chromosomes[1];

            individual1.Chromosomes[0] = k * x1 + (1.0 - k) * x2;
            individual1.Chromosomes[1] = k * y1 + (1.0 - k) * y2;
            individual2.Chromosomes[0] = (1.0 - k) * x1 + k * x2;
            individual2.Chromosomes[1] = (1.0 - k) * y1 + k * y2;
        }
    }
}
