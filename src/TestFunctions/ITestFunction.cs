using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.TestFunctions
{
    public interface ITestFunction
    {
        double GetMinValue();
        (double, double) GetMinValueArguments();
        double Calculate(double x1, double x2);
    }
}
