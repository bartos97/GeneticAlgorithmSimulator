using GeneticAlgorithmSimulator.GeneticOperators;
using GeneticAlgorithmSimulator.GeneticOperators.Crossover;
using GeneticAlgorithmSimulator.GeneticOperators.Mutation;
using GeneticAlgorithmSimulator.Models;
using GeneticAlgorithmSimulator.SelectionMethods;
using GeneticAlgorithmSimulator.TestFunctions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator
{
    public class EvolutionManager
    {
        private static readonly Random rand = new();
        private readonly GeneticAlgorithmSettings settings;
        private readonly ITestFunction testFunction;
        private readonly ISelectionMethod selectionMethod;
        private readonly IBinaryOperator crossoverOperator;
        private readonly IUnaryOperator mutationOperator;
        private List<Individual> population;
        private bool isPopulationInitialized = false;
        private IEnumerable<Individual> OrderedPopulation
        {
            get => population.OrderByDescending(x => x.FitnessValue);
        }

        public EvolutionManager(GeneticAlgorithmSettings settings, ITestFunction testFunction)
        {
            this.settings = settings;
            this.testFunction = testFunction;
            population = new List<Individual>(settings.PopulationSize);

            selectionMethod = settings.SelectionMethod switch
            {
                SelectionMethodEnum.BEST => new BestSelectionMethod(settings.PercentageToSelect),
                SelectionMethodEnum.ROULETTE => new RouletteSelectionMethod(settings.PercentageToSelect),
                SelectionMethodEnum.TOURNAMENT => new TournamentSelectionMethod(settings.NumOfIndividualsInGroup),
                _ => throw new InvalidOperationException(),
            };

            crossoverOperator = settings.CrossingMethod switch
            {
                CrossingOperatorEnum.ARITHMETIC => new ArithmeticCrossoverOperator(),
                CrossingOperatorEnum.HEURISTIC => new HeuristicCrossoverOperator(),
                _ => throw new InvalidOperationException(),
            };

            mutationOperator = settings.MutationMethod switch
            {
                MutationOperatorEnum.UNIFORM => new UniformMutationOperator(testFunction.InputDomain.Item1, testFunction.InputDomain.Item2),
                _ => throw new InvalidOperationException(),
            };
        }

        public Individual GetBestIndividual() => OrderedPopulation.ElementAt(0);

        public IEnumerable<double> GetPopulationFunctionValues() => population.Select(x => testFunction.Calculate(x.Decode()));

        public bool RunNextCycle()
        {
            if (!isPopulationInitialized)
                InitPopulation();
            if (population.Count < 2)
                return false;

            FlagIndividualsFromEliteStrategy();

            var newPopulation = selectionMethod.GetNewPopulation(population.Where(x => !x.IsInNewPopulation));
            ApplyCrossovers(newPopulation);
            ApplyMutations(newPopulation);

            population = population.Where(x => x.IsInNewPopulation).ToList();
            population.AddRange(newPopulation);

            ClearEliteStrategyFlags();
            return true;
        }

        private void InitPopulation()
        {
            for (int i = 0; i < settings.PopulationSize; i++)
            {
                population.Add(new Individual(2, testFunction, settings.OptimizationType));
            }
            isPopulationInitialized = true;
        }

        private void FlagIndividualsFromEliteStrategy()
        {
            var eliteCount = (int)(settings.PercentageInElite / 100.0 * population.Count);
            if (eliteCount == 0)
                eliteCount = 1;
            foreach (var item in OrderedPopulation.Take(eliteCount))
                item.IsInNewPopulation = true;
        }

        private void ClearEliteStrategyFlags()
        {
            foreach (var item in population)
                item.IsInNewPopulation = false;
        }

        private void ApplyCrossovers(IEnumerable<Individual> newPopulation)
        {
            using var iter = newPopulation.GetEnumerator();

            while (iter.MoveNext())
            {
                if (CheckProbability(settings.CrossingProbabPerc))
                {
                    var prev = iter.Current;
                    if (iter.MoveNext())
                    {
                        crossoverOperator.ApplyOn(prev, iter.Current);
                        prev.RecalculateFitnessValue();
                        iter.Current.RecalculateFitnessValue();
                    }
                }
            }
        }

        private void ApplyMutations(IEnumerable<Individual> newPopulation)
        {
            foreach (var item in newPopulation)
            {
                if (CheckProbability(settings.MutationProbabPerc))
                {
                    mutationOperator.ApplyOn(item);
                    item.RecalculateFitnessValue();
                }
            }
        }

        private static bool CheckProbability(int percentage)
        {
            return rand.Next(1, 101) <= percentage;
        }
    }
}
