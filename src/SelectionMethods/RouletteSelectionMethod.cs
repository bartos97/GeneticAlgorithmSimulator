using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.SelectionMethods
{
    public class RouletteSelectionMethod : ISelectionMethod
    {
        private static readonly Random rand = new();
        private readonly int percentage;

        public RouletteSelectionMethod(int percentageToSelect)
        {
            percentage = percentageToSelect;
        }

        public IEnumerable<Individual> GetNewPopulation(IEnumerable<Individual> population)
        {
            int populationCount = population.Count();
            int newPopulationCount = populationCount <= 20 ? populationCount : (int)(percentage / 100.0 * populationCount);

            double fitnessValuesSum = population.Sum(x => x.FitnessValue);
            var probabilities = population.Select(x => x.FitnessValue / fitnessValuesSum);
            var distribution = GetDistribution(probabilities, populationCount);

            double rouletteValue = rand.NextDouble();
            var newPopulation = new List<Individual>(newPopulationCount);

            for (int i = 0; i < newPopulationCount; i++)
            {
                newPopulation.Add(GetIndividualFromRoulette(population, distribution, rouletteValue));
            }

            return newPopulation;
        }

        private static Individual GetIndividualFromRoulette(IEnumerable<Individual> population, List<double> distribution, double rouletteValue)
        {
            for (int i = 0; i < distribution.Count; i++)
            {
                if (rouletteValue > distribution[i])
                    continue;
                return i > 0 ? population.ElementAt(i - 1) : population.ElementAt(0);
            }
            return population.ElementAt(0);
        }

        private static List<double> GetDistribution(IEnumerable<double> probabilities, int count)
        {
            var distribution = new List<double>(count);
            double tmpSum = 0.0;
            foreach (var item in probabilities)
            {
                distribution.Add(tmpSum += item);
            }
            return distribution;
        }
    }
}
