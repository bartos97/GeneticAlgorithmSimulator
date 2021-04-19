using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.Models
{
    public struct Chromosome
    {
        public string Genes { get; private set; }
        private static readonly Random rand = new();

        public Chromosome(int numOfBits)
        {
            var sb = new StringBuilder(numOfBits);
            for (int i = 0; i < numOfBits; i++)
            {
                sb.Append(rand.Next(0, 100) >= 50 ? '1' : '0');
            }
            Genes = sb.ToString();
        }

        public ulong GetDecimalValue()
        {
            return Convert.ToUInt64(Genes, 2);
        }
    }
}
