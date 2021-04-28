using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.SelectionMethods
{
    public class BestSelectionMethod : ISelectionMethod
    {
        private readonly int percentage;
        public BestSelectionMethod(int percentageToSelect)
        {
            percentage = percentageToSelect;
        }

        public IEnumerable<Individual> GetNewPopulation(IEnumerable<Individual> population)
        {
            var orderedPopulation = population.OrderByDescending(x => x.FitnessValue);
            int populationCount = orderedPopulation.Count();
            int newPopulationCount = populationCount <= 20 ? populationCount : (int)(percentage / 100.0 * populationCount);
            return orderedPopulation.Take(newPopulationCount);
        }
    }
}
