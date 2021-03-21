using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator
{
    public enum SelectionMethod
    {
        BEST, ROULETTE, TOURNAMENT
    }

    public enum CrossingMethod
    {
        ONE_POINT, TWO_POINT, THREE_POINT, HOMOGENEOUS
    }

    public enum MutationMethod
    {
        EDGE, ONE_POINT, TWO_POINT
    }

    public enum OptimizationMethod
    {
        MIN, MAX
    }

    public class Settings
    {
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
        public SelectionMethod SelectionMethod { get; set; }
        public CrossingMethod CrossingMethod { get; set; }
        public MutationMethod MutationMethod { get; set; }
        public OptimizationMethod OptimizationMethod { get; set; }
    }
}
