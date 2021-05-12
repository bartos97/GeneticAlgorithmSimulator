using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.Models
{
    public enum TestFunctionEnum
    {
        SCHWEFEL, BOOTH, ACKLEY
    }

    public enum OptimizationTypeEnum
    {
        MIN, MAX
    }

    public enum SelectionMethodEnum
    {
        BEST, ROULETTE, TOURNAMENT
    }

    public enum CrossingOperatorEnum
    {
        ARITHMETIC, HEURISTIC
    }

    public enum MutationOperatorEnum
    {
        UNIFORM
    }

    public class GeneticAlgorithmSettings
    {
        public TestFunctionEnum TestFunction { get; set; }
        public OptimizationTypeEnum OptimizationType { get; set; }
        public int PopulationSize { get; set; }
        public int EpochsAmount { get; set; }
        public int PercentageToSelect { get; set; }
        public int NumOfIndividualsInGroup { get; set; }
        public int PercentageInElite { get; set; }
        public int CrossingProbabPerc { get; set; }
        public int MutationProbabPerc { get; set; }
        public SelectionMethodEnum SelectionMethod { get; set; }
        public CrossingOperatorEnum CrossingMethod { get; set; }
        public MutationOperatorEnum MutationMethod { get; set; }

        public static GeneticAlgorithmSettings GetDefault()
        {
            return new()
            {
                TestFunction = TestFunctionEnum.SCHWEFEL,
                OptimizationType = OptimizationTypeEnum.MIN,
                PopulationSize = 100,
                EpochsAmount = 1000,
                PercentageToSelect = 80,
                NumOfIndividualsInGroup = 5,
                PercentageInElite = 5,
                CrossingProbabPerc = 60,
                MutationProbabPerc = 40,
                SelectionMethod = SelectionMethodEnum.TOURNAMENT,
                CrossingMethod = CrossingOperatorEnum.ARITHMETIC,
                MutationMethod = MutationOperatorEnum.UNIFORM
            };
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            Type itemType = typeof(GeneticAlgorithmSettings);
            var props = itemType.GetProperties();

            sb.Append(string.Join(";", props.Select(p => p.Name)));
            sb.Append('\n');
            sb.Append(string.Join(";", props.Select(p => p.GetValue(this))));
            sb.Append('\n');

            return sb.ToString();
        }
    }
}
