using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators
{
    public interface IUnaryOperator
    {
        void ApplyOn(Individual individual);
    }
}
