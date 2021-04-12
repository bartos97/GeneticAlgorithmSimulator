using GeneticAlgorithmSimulator.TestFunctions;
using System.Threading;
using System.Collections.Generic;
using System;

namespace GeneticAlgorithmSimulator
{
    public class GeneticAlgorithmManager
    {
        private static readonly Dictionary<TestFunctionEnum, ITestFunction> testFunctions = new()
        {
            { TestFunctionEnum.SCHWEFEL, new SchwefelTestFunction() }
        };

        private readonly GeneticAlgorithmSettings settings;

        public (double, double) TestFuncInputDomain { get => testFunctions[settings.TestFunction].InputDomain; }
        public double TestFuncMinValue { get => testFunctions[settings.TestFunction].MinValue; }
        public (double, double) TestFuncMinValueArg { get => testFunctions[settings.TestFunction].MinValueArguments; }

        public GeneticAlgorithmManager(GeneticAlgorithmSettings settings)
        {
            this.settings = settings;
        }

        public IEnumerable<EpochResult> GetResults()
        {
            var f = testFunctions[settings.TestFunction];

            for (int i = 0; i < settings.EpochsAmount; i++)
            {
                yield return new()
                {
                    epochNumber = i,
                    functionValue = f.Calculate(i, i),
                    //x1 = rnd.Next(-50, 50),
                    //x2 = rnd.Next(-50, 50),
                    mean = 1.0,
                    stdDev = 1.0
                };
            }
        }

        public double GetLastComputationTime()
        {
            return 1230.0;
        }
    }
}