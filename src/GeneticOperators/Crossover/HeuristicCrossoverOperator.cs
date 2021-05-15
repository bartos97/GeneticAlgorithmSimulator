using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class HeuristicCrossoverOperator : IBinaryOperator
    {
        private static readonly Random rand = new();

        public void ApplyOn(Individual individual1, Individual individual2)
        {
            double x1 = individual1.Chromosomes[0];
            double y1 = individual1.Chromosomes[1];
            double x2 = individual2.Chromosomes[0];
            double y2 = individual2.Chromosomes[1];

            if (x2 > x1 && y2 > y1)
            {
                DoCrossover(individual1, individual2);
            }
            else if (x2 < x1 && y2 < y1)
            {
                DoCrossover(individual2, individual1);
            }
            else if (y2 < y1)
            {
                individual2.Chromosomes[1] = y1;
                individual1.Chromosomes[1] = y2;
                DoCrossover(individual1, individual2);
            }
            else if (x2 < x1)
            {
                individual2.Chromosomes[0] = x1;
                individual1.Chromosomes[0] = x2;
                DoCrossover(individual1, individual2);
            }
            //else Debug.Assert(false, "This should never happened");


        }

        private static void DoCrossover(Individual individual1, Individual individual2)
        {
            double k = rand.NextDouble();
            double x1 = individual1.Chromosomes[0];
            double y1 = individual1.Chromosomes[1];
            double x2 = individual2.Chromosomes[0];
            double y2 = individual2.Chromosomes[1];

            individual1.Chromosomes[0] = k * (x2 - x1) + x1;
            individual2.Chromosomes[1] = k * (y2 - y1) + y1;
        }
    }
}
