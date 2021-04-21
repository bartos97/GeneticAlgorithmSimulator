using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.SelectionMethods
{
    public interface ISelectionMethod
    {
        /// <summary>
        /// </summary>
        /// <returns>Enumerator to given population with individuals selected to new epoch.</returns>
        IEnumerable<Individual> GetNewPopulation(IEnumerable<Individual> population);
    }
}
