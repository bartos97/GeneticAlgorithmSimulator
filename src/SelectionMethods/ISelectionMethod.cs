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
        void RemoveUnselectedIndividuals(ICollection<Individual> population);
    }
}
