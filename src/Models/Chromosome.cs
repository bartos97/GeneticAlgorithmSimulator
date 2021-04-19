using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.Models
{
    public class Chromosome
    {
        private static readonly Random rand = new();
        public StringBuilder Builder { get; init; }
        public string Genes { get => Builder.ToString(); }

        public Chromosome(int numOfBits)
        {
            Builder = new StringBuilder(numOfBits);
            for (int i = 0; i < numOfBits; i++)
            {
                Builder.Append(rand.Next(0, 100) >= 50 ? '1' : '0');
            }
        }

        public ulong GetDecimalValue()
        {
            return Convert.ToUInt64(Genes, 2);
        }
    }
}
