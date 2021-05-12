using GeneticAlgorithmSimulator.TestFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.Models
{
    public class Individual
    {
        public double[] Chromosomes { get; init; }
        public bool IsInNewPopulation { get; set; } = false;

        /// <summary>
        /// Greater than 0. The higher the better; regardles of optimization type.
        /// Not necessarily actual test function value.
        /// </summary>
        public double FitnessValue { get; private set; }

        private static readonly Random rand = new();
        private readonly ITestFunction testFunction;
        private readonly OptimizationTypeEnum optimizationType;

        public Individual(int numOfVariables, ITestFunction testFunction, OptimizationTypeEnum optimizationType)
        {
            Debug.Assert(numOfVariables > 0);

            this.testFunction = testFunction;
            this.optimizationType = optimizationType;

            Chromosomes = new double[numOfVariables];
            for (int i = 0; i < numOfVariables; i++)
            {
                Chromosomes[i] = rand.NextDouble();
            }

            RecalculateFitnessValue();
        }

        public double[] Decode() => Chromosomes;

        public void RecalculateFitnessValue()
        {
            double val = testFunction.Calculate(Decode());
            if (testFunction.MinValue < 0.0)
                val += -testFunction.MinValue;
            FitnessValue = optimizationType switch
            {
                OptimizationTypeEnum.MIN => 1.0 / (val == 0.0 ? double.Epsilon : val),
                OptimizationTypeEnum.MAX => val,
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
