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
        public IEnumerable<Individual> GetNewPopulation(IEnumerable<Individual> orderedPopulation)
        {
            throw new NotImplementedException();
        }
    }
}
