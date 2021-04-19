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

        public void RemoveUnselectedIndividuals(ICollection<Individual> population)
        {
            int removeCount = population.Count - (int)(percentage / 100.0 * population.Count);
            foreach (var item in population.Where(x => x.IsEvolving).OrderByDescending(x => x.FitnessValue).Take(removeCount))
            {
                population.Remove(item);
            }
        }
    }
}
