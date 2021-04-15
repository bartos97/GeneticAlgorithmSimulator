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
        private readonly List<Individual> population;
        private readonly List<EpochResult> currentResults;

        public GeneticAlgorithmManager(GeneticAlgorithmSettings settings)
        {
            this.settings = settings;
            population = new List<Individual>(settings.PopulationSize);
            currentResults = new List<EpochResult>(settings.EpochsAmount);
        }

        public IEnumerable<EpochResult> GetResults()
        {
            currentResults.Clear();
            var computationTimer = Stopwatch.StartNew();

            Init();
            currentResults.Add(GetEpochResult(0));
            for (int i = 1; i < settings.EpochsAmount; i++)
            {
                // Do the evolution
                currentResults.Add(GetEpochResult(i));
            }

            computationTimer.Stop();
            LastComputationTimeInMs = computationTimer.ElapsedMilliseconds;
            return currentResults;
        }

        private void Init()
        {
            for (int i = 0; i < settings.PopulationSize; i++)
            {
                population.Add(new Individual(settings.NumOfBits, 2));
            }
        }

        private EpochResult GetEpochResult(int currentEpochNum)
        {
            var decodedBest = GetBestIndividual().Decode(TestFunction.InputDomain.Item1, TestFunction.InputDomain.Item2);
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

        private Individual GetBestIndividual()
        {
            var (start, end) = TestFunction.InputDomain;
            double min = TestFunction.Calculate(population[0].Decode(start, end));
            var minInd = population[0];

            for (int i = 1; i < population.Count; i++)
            {
                var val = TestFunction.Calculate(population[i].Decode(start, end));
                if (val < min)
                {
                    min = val;
                    minInd = population[i];
                }
            }

            return minInd;
        }
    }
}