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
        public Chromosome[] Chromosomes { get; private set; }
        
        /// <summary>
        /// If false then don't apply genetic operators on this individual
        /// </summary>
        public bool IsEvolving { get; set; } = true;
        
        /// <summary>
        /// Distance from calculated function value with arguments from this individual to actual function minimum value; 
        /// i.e., the lower the better
        /// </summary>
        public double FitnessValue { get; private set; }

        private readonly int numOfBits;
        private readonly int numOfVariables;
        private readonly ITestFunction testFunction;

        public Individual(int numOfBits, int numOfVariables, ITestFunction testFunction)
        {
            Debug.Assert(numOfBits > 0 && numOfVariables > 0);

            this.numOfBits = numOfBits;
            this.numOfVariables = numOfVariables;
            this.testFunction = testFunction;

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
            FitnessValue = Math.Abs(testFunction.MinValue - testFunction.Calculate(Decode()));
        }
    }
}
