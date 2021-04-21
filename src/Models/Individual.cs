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
        public Chromosome[] Chromosomes { get; init; }
        public bool IsInNewPopulation { get; set; } = false;

        /// <summary>
        /// The higher the better; regardles of optimization type.
        /// Not necessarily actual test function value.
        /// </summary>
        public double FitnessValue { get; private set; }

        private readonly int numOfBits;
        private readonly int numOfVariables;
        private readonly ITestFunction testFunction;
        private readonly OptimizationTypeEnum optimizationType;

        public Individual(int numOfBits, int numOfVariables, ITestFunction testFunction, OptimizationTypeEnum optimizationType)
        {
            Debug.Assert(numOfBits > 0 && numOfVariables > 0);

            this.numOfBits = numOfBits;
            this.numOfVariables = numOfVariables;
            this.testFunction = testFunction;
            this.optimizationType = optimizationType;

            Chromosomes = new Chromosome[numOfVariables];
            for (int i = 0; i < numOfVariables; i++)
            {
                Chromosomes[i] = new Chromosome(numOfBits);
            }

            RecalculateFitnessValue();
        }

        public double[] Decode()
        {
            var (start, end) = testFunction.InputDomain;
            var retval = new double[numOfVariables];
            double dx = (end - start) / (Math.Pow(2, numOfBits) - 1);
            for (int i = 0; i < numOfVariables; i++)
            {
                retval[i] = start + Chromosomes[i].GetDecimalValue() * dx;
            }
            return retval;
        }

        public void RecalculateFitnessValue()
        {
            FitnessValue = optimizationType switch
            {
                OptimizationTypeEnum.MIN => 1.0 / testFunction.Calculate(Decode()),
                OptimizationTypeEnum.MAX => testFunction.Calculate(Decode()),
                _ => throw new InvalidOperationException(),
            };
        }
    }
}
