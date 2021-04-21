using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.TestFunctions
{
    public class AckleyTestFunction : ITestFunction
    {
        public (double, double) InputDomain => (-32.768, 32.768);

        public double MinValue => 0.0;

        public (double, double) MinValueArguments => (0.0, 0.0);

        public double Calculate(double x1, double x2) => Calculate(new[] { x1, x2 });

        public double Calculate(double[] x)
        {
            const double a = 20.0;
            const double b = 0.2;
            const double c = 2 * Math.PI;
            double d = x.Length;

            double exp1 = -b * Math.Sqrt(1 / d * x.Sum(xi => xi * xi));
            double exp2 = 1 / d * x.Sum(xi => Math.Cos(c * xi));
            return -a * Math.Exp(exp1) - Math.Exp(exp2) + a + Math.E;
        }
    }
}
