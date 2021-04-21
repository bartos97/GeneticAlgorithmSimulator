using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.TestFunctions
{
    public class SchwefelTestFunction : ITestFunction
    {
        public (double, double) InputDomain => (-500.0, 500.0);

        public double MinValue => 0.0;

        public (double, double) MinValueArguments => (420.9687, 420.9687);

        public double Calculate(double x1, double x2) => Calculate(new[] { x1, x1 });

        public double Calculate(double[] x)
        {
            double d = x.Length;
            double sum = x.Sum(xi => xi * Math.Sin(Math.Sqrt(Math.Abs(xi))));
            return 418.9829 * d - sum;
        }
    }
}
