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

        private int numOfBits;
        private int numOfVariables;

        public Individual(int numOfBits, int numOfVariables)
        {
            Debug.Assert(numOfBits > 0 && numOfVariables > 0);

            this.numOfBits = numOfBits;
            this.numOfVariables = numOfVariables;

            Chromosomes = new Chromosome[numOfVariables];
            for (int i = 0; i < numOfVariables; i++)
            {
                Chromosomes[i] = new Chromosome(numOfBits);
            }
        }

        public double[] Decode(double rangeStart, double rangeEnd)
        {
            Debug.Assert(rangeStart < rangeEnd);

            var retval = new double[numOfVariables];
            double dx = (rangeEnd - rangeStart) / (Math.Pow(2, numOfBits) - 1);
            for (int i = 0; i < numOfVariables; i++)
            {
                retval[i] = rangeStart + Chromosomes[i].GetDecimalValue() * dx;
            }
            return retval;
        }
    }
}
