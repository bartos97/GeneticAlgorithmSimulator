using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator
{
    public enum TestFunctionEnum
    {
        SCHWEFEL
    }

    public enum SelectionMethodEnum
    {
        BEST, ROULETTE, TOURNAMENT
    }

    public enum CrossingMethodEnum
    {
        ONE_POINT, TWO_POINT, THREE_POINT, HOMOGENEOUS
    }

    public enum MutationMethodEnum
    {
        EDGE, ONE_POINT, TWO_POINT
    }

    public class GeneticAlgorithmSettings
    {
        public TestFunctionEnum TestFunction { get; set; }
        public double RangeStart { get; set; }
        public double RangeEnd { get; set; }
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
        public CrossingMethodEnum CrossingMethod { get; set; }
        public MutationMethodEnum MutationMethod { get; set; }

        public static GeneticAlgorithmSettings GetDefault()
        {
            return new()
            {
                TestFunction = TestFunctionEnum.SCHWEFEL,
                RangeStart = 0,
                RangeEnd = 10,
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
                CrossingMethod = CrossingMethodEnum.ONE_POINT,
                MutationMethod = MutationMethodEnum.ONE_POINT
            };
        }
    }
}
