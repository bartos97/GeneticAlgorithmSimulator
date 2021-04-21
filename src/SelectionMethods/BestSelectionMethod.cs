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
            int count = (int)(percentage / 100.0 * orderedPopulation.Count());
            return orderedPopulation.Take(count);
        }

        //public void RemoveUnselectedIndividuals(ICollection<Individual> mutablePopulation, IEnumerable<Individual> orderedPopulationEnumerator)
        //{
        //    int count = orderedPopulationEnumerator.Count();
        //    int removeCount = count - (int)(percentage / 100.0 * count);
        //    foreach (var item in orderedPopulationEnumerator.Reverse().Take(removeCount))
        //    {
        //        mutablePopulation.Remove(item);
        //    }
        //}
    }
}
