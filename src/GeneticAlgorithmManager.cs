using GeneticAlgorithmSimulator.TestFunctions;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithmSimulator
{
    internal class GeneticAlgorithmManager
    {
        private static readonly Dictionary<TestFunctionEnum, ITestFunction> testFunctions = new()
        {
            { TestFunctionEnum.SCHWEFEL, new SchwefelTestFunction() }
        };

        private readonly GeneticAlgorithmSettings settings;

        public GeneticAlgorithmManager(GeneticAlgorithmSettings settings)
        {
            this.settings = settings;
        }

        internal IEnumerable<EpochResult> GetResults()
        {
            var f = testFunctions[settings.TestFunction];

            for (int i = 0; i < settings.EpochsAmount; i++)
            {
                yield return new()
                {
                    epochNumber = i,
                    functionValue = f.Calculate(i, i),
                    x1 = i,
                    x2 = i,
                    mean = 1.0,
                    stdDev = 1.0
                };
            }
        }

        internal double GetLastComputationTime()
        {
            return 1230.0;
        }
    }
}