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
            { TestFunctionEnum.SCHWEFEL, new SchwefelTestFunction() },
            { TestFunctionEnum.BOOTH, new BoothTestFunction() },
            { TestFunctionEnum.ACKLEY, new AckleyTestFunction() }
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
                if (!evolutionManager.RunNextCycle())
                    break;
                currentResults.Add(GetEpochResult(i));
            }

            computationTimer.Stop();
            LastComputationTimeInMs = computationTimer.ElapsedMilliseconds;
            return currentResults;
        }

        private EpochResult GetEpochResult(int currentEpochNum)
        {
            var decodedBest = evolutionManager.GetBestIndividual().Decode();
            var mean = evolutionManager.GetPopulationFunctionValues().Average();
            return new()
            {
                epochNumber = currentEpochNum,
                functionValue = TestFunction.Calculate(decodedBest),
                x1 = decodedBest[0],
                x2 = decodedBest[1],
                mean = mean,
                stdDev = GetStdDev(mean)
            };
        }

        private double GetStdDev(double mean)
        {
            double sum = evolutionManager.GetPopulationFunctionValues().Sum(x => Math.Pow(x - mean, 2));
            return Math.Sqrt(sum / currentResults.Count);
        }
    }
}