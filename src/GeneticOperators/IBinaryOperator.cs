using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.GeneticOperators
{
    public interface IBinaryOperator
    {
        void ApplyOn(Individual individual1, Individual individual2);
    }
}
