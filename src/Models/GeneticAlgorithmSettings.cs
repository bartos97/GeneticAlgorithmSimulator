using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.Models
{
    public enum TestFunctionEnum
    {
        SCHWEFEL
    }

    public enum SelectionMethodEnum
    {
        BEST, ROULETTE, TOURNAMENT
    }

    public enum CrossingOperatorEnum
    {
        ONE_POINT, TWO_POINT, THREE_POINT, HOMOGENEOUS
    }

    public enum MutationOperatorEnum
    {
        EDGE, ONE_POINT, TWO_POINT
    }

    public class GeneticAlgorithmSettings
    {
        public TestFunctionEnum TestFunction { get; set; }
        public int NumOfBits { get; set; }
        public int PopulationSize { get; set; }
        public int EpochsAmount { get; set; }
        public int? PercentageToCross { get; set; }
        public int? TournamentsAmount { get; set; }
        public int PercentageInElite { get; set; }
        public int CrossingProbabPerc { get; set; }
        public int MutationProbabPerc { get; set; }
        public int InversionProbabPerc { get; set; }
        public SelectionMethodEnum SelectionMethod { get; set; }
        public CrossingOperatorEnum CrossingMethod { get; set; }
        public MutationOperatorEnum MutationMethod { get; set; }

        public static GeneticAlgorithmSettings GetDefault()
        {
            return new()
            {
                TestFunction = TestFunctionEnum.SCHWEFEL,
                NumOfBits = 40,
                PopulationSize = 100,
                EpochsAmount = 1000,
                PercentageToCross = 10,
                TournamentsAmount = 20,
                PercentageInElite = 10,
                CrossingProbabPerc = 60,
                MutationProbabPerc = 40,
                InversionProbabPerc = 10,
                SelectionMethod = SelectionMethodEnum.BEST,
                CrossingMethod = CrossingOperatorEnum.ONE_POINT,
                MutationMethod = MutationOperatorEnum.ONE_POINT
            };
        }
    }
}
