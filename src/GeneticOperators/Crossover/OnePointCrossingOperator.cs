using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators.Crossover
{
    public class OnePointCrossingOperator : IBinaryOperator
    {
        public void ApplyOn(Individual individual1, Individual individual2)
        {
            for (int i = 0; i < individual1.Chromosomes.Length; i++)
            {
                Debug.Assert(individual1.Chromosomes[i].Builder.Length == individual2.Chromosomes[i].Builder.Length);
                //Debug.Assert(individual1.GetHashCode() == individual2.GetHashCode());

                int genesLength = individual1.Chromosomes[i].Builder.Length;
                int middleIndex = genesLength / 2;
                int cutoffLength = genesLength - middleIndex;
                var sb1 = individual1.Chromosomes[i].Builder;
                var sb2 = individual2.Chromosomes[i].Builder;
                var copyInd1 = sb1.ToString(middleIndex, cutoffLength);

                sb1.Remove(middleIndex, cutoffLength);
                sb1.Insert(middleIndex, sb2.ToString(middleIndex, cutoffLength));
                sb2.Remove(middleIndex, cutoffLength);
                sb2.Insert(middleIndex, copyInd1);
            }
        }
    }
}
