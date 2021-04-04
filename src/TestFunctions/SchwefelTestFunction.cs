using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.TestFunctions
{
    public class SchwefelTestFunction : ITestFunction
    {
        private const double minVal = 0.0;
        private static readonly (double, double) minArg = ( 1.0, 1.0 );

        public double Calculate(double x1, double x2)
        {
            return 837.9658 - (x1 * Math.Sin(Math.Sqrt(Math.Abs(x1))) + x2 * Math.Sin(Math.Sqrt(Math.Abs(x2))));
        }

        public double GetMinValue() => minVal;

        public (double, double) GetMinValueArguments() => minArg;
    }
}
