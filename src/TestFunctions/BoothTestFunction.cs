using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.TestFunctions
{
    public class BoothTestFunction : ITestFunction
    {
        public (double, double) InputDomain => (-10.0, 10.0);

        public double MinValue => 0.0;

        public (double, double) MinValueArguments => (1.0, 3.0);

        public double Calculate(double x1, double x2) =>
            Math.Pow(x1 + 2 * x2 - 7, 2) + Math.Pow(2 * x1 + x2 - 5, 2);

        public double Calculate(double[] x)
        {
            Debug.Assert(x.Length == 2);
            return Calculate(x[0], x[1]);
        }
    }
}
