using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.TestFunctions
{
    public interface ITestFunction
    {
        (double, double) InputDomain { get; }
        double MinValue { get; }
        (double, double) MinValueArguments { get; }
        double Calculate(double x1, double x2);
        double Calculate(double[] x);
    }
}
