using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.Validation
{
    public static class Regexes
    {
        public static readonly Regex number = new("[^0-9.-]+");
        public static readonly Regex integer = new("[^0-9-]+");
        public static readonly Regex positiveInteger = new("[^0-9]+");
    }
}
