using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.TestFunctions
{
    public class SchwefelTestFunction : ITestFunction
    {
        public (double, double) InputDomain => (-500.0, 500.0);

        public double MinValue => 0.0;

        public (double, double) MinValueArguments => (1.0, 1.0);

        public double Calculate(double x1, double x2)
            => 837.9658 - (x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2))));

    }
}
