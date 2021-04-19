using GeneticAlgorithmSimulator.GeneticOperators;
using GeneticAlgorithmSimulator.GeneticOperators.Crossover;
using GeneticAlgorithmSimulator.GeneticOperators.Mutation;
using GeneticAlgorithmSimulator.Models;
using GeneticAlgorithmSimulator.SelectionMethods;
using GeneticAlgorithmSimulator.TestFunctions;
using System;
using System.Collections.Generic;
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
        private readonly List<Individual> population;
        private readonly ISelectionMethod selectionMethod;
        private readonly IBinaryOperator crossoverOperator;
        private readonly IUnaryOperator mutationOperator;
        private readonly IUnaryOperator inversionOperator;
        private bool isPopulationInitialized = false;

        public EvolutionManager(GeneticAlgorithmSettings settings, ITestFunction testFunction)
        {
            this.settings = settings;
            this.testFunction = testFunction;
            population = new List<Individual>(settings.PopulationSize);

            selectionMethod = settings.SelectionMethod switch
            {
                SelectionMethodEnum.BEST => new BestSelectionMethod(settings.PercentageToSelect),
                SelectionMethodEnum.ROULETTE => new RouletteSelectionMethod(),
                SelectionMethodEnum.TOURNAMENT => new TournamentSelectionMethod(),
                _ => throw new InvalidOperationException(),
            };

            crossoverOperator = settings.CrossingMethod switch
            {
                CrossingOperatorEnum.ONE_POINT => new OnePointCrossingOperator(),
                CrossingOperatorEnum.TWO_POINT => new TwoPointCrossingOperator(),
                CrossingOperatorEnum.THREE_POINT => new ThreePointCrossingOperator(),
                CrossingOperatorEnum.HOMOGENEOUS => new HomogeneousCrossingOperator(),
                _ => throw new InvalidOperationException(),
            };

            mutationOperator = settings.MutationMethod switch
            {
                MutationOperatorEnum.EDGE => new EdgeMutationOperator(),
                MutationOperatorEnum.ONE_POINT => new OnePointMutationOperator(),
                MutationOperatorEnum.TWO_POINT => new TwoPointMutationOperator(),
                _ => throw new InvalidOperationException(),
            };

            inversionOperator = new StandardInversionOperator();
        }

        public Individual GetBestIndividual()
        {
            return population.OrderBy(x => x.FitnessValue).Take(1).ElementAt(0);
        }

        public bool RunNextCycle()
        {
            if (!isPopulationInitialized)
                InitPopulation();
            if (population.Count < 2)
                return false;

            // select individuals to new population
            // do crossover on them...
            // ...then mutation
            // ...then inversion
            // add to new population individuals selected in elity strategy from previous population

            FlagIndividualsFromEliteStrategy();
            selectionMethod.RemoveUnselectedIndividuals(population);
            ApplyCrossovers();
            ApplyMutations();
            ApplyInversions();
            ClearEliteStrategyFlags();
            return true;
        }

        private void InitPopulation()
        {
            for (int i = 0; i < settings.PopulationSize; i++)
            {
                population.Add(new Individual(settings.NumOfBits, 2, testFunction));
            }
            isPopulationInitialized = true;
        }

        private void FlagIndividualsFromEliteStrategy()
        {
            var eliteCount = (int)(settings.PercentageInElite / 100.0 * settings.PopulationSize);
            foreach (var item in population.OrderBy(x => x.FitnessValue).Take(eliteCount))
            {
                item.IsEvolving = false;
            }
        }

        private void ApplyCrossovers()
        {
            using var iter = population.Where(x => x.IsEvolving).GetEnumerator();

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

        private void ApplyMutations()
        {
            foreach (var item in population.Where(x => x.IsEvolving))
            {
                if (CheckProbability(settings.MutationProbabPerc))
                {
                    mutationOperator.ApplyOn(item);
                    item.RecalculateFitnessValue();
                }
            }
        }

        private void ApplyInversions()
        {
            foreach (var item in population.Where(x => x.IsEvolving))
            {
                if (CheckProbability(settings.InversionProbabPerc))
                {
                    inversionOperator.ApplyOn(item);
                    item.RecalculateFitnessValue();
                }
            }
        }

        private void ClearEliteStrategyFlags()
        {
            foreach (var item in population)
            {
                item.IsEvolving = true;
            }
        }

        private static bool CheckProbability(int percentage)
        {
            return rand.Next(1, 101) <= percentage;
        }
    }
}
