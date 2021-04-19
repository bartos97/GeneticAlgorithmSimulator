using GeneticAlgorithmSimulator.TestFunctions;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using GeneticAlgorithmSimulator.Models;
using System.Linq;
using System;

namespace GeneticAlgorithmSimulator
{
    public class GeneticAlgorithmManager
    {
        public ITestFunction TestFunction { get => testFunctionsImpl[settings.TestFunction]; }
        public long LastComputationTimeInMs { get; private set; }

        private static readonly Dictionary<TestFunctionEnum, ITestFunction> testFunctionsImpl = new()
        {
            { TestFunctionEnum.SCHWEFEL, new SchwefelTestFunction() }
        };

        private readonly GeneticAlgorithmSettings settings;
        private readonly List<EpochResult> currentResults;
        private readonly EvolutionManager evolutionManager;

        public GeneticAlgorithmManager(GeneticAlgorithmSettings settings)
        {
            this.settings = settings;
            currentResults = new List<EpochResult>(settings.EpochsAmount);
            evolutionManager = new EvolutionManager(settings, TestFunction);
        }

        public IEnumerable<EpochResult> GetResults()
        {
            currentResults.Clear();
            var computationTimer = Stopwatch.StartNew();

            for (int i = 0; i < settings.EpochsAmount; i++)
            {
                evolutionManager.RunNextCycle();
                currentResults.Add(GetEpochResult(i));
            }

            computationTimer.Stop();
            LastComputationTimeInMs = computationTimer.ElapsedMilliseconds;
            return currentResults;
        }

        private EpochResult GetEpochResult(int currentEpochNum)
        {
            var decodedBest = evolutionManager.GetBestIndividual().Decode();
            var mean = GetResultsMean();
            return new()
            {
                epochNumber = currentEpochNum,
                functionValue = TestFunction.Calculate(decodedBest[0], decodedBest[1]),
                x1 = decodedBest[0],
                x2 = decodedBest[1],
                mean = mean,
                stdDev = GetResultsStdDev(mean)
            };
        }

        private double GetResultsMean()
        {
            if (!currentResults.Any())
                return default;
            return currentResults.Average(x => x.functionValue);
        }

        private double GetResultsStdDev(double? mean = null)
        {
            if (!currentResults.Any())
                return default;

            double _mean = mean == null ? GetResultsMean() : mean.Value;
            double sum = currentResults.Sum(x => Math.Pow(x.functionValue - _mean, 2));
            return Math.Sqrt(sum / currentResults.Count);
        }
    }
}