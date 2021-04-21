using GeneticAlgorithmSimulator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithmSimulator.SelectionMethods
{
    public class TournamentSelectionMethod : ISelectionMethod
    {
        private static readonly Random rand = new();
        private readonly int numOfIndividualsInGroup;

        public TournamentSelectionMethod(int numOfIndividualsInGroup)
        {
            this.numOfIndividualsInGroup = numOfIndividualsInGroup;
        }

        public IEnumerable<Individual> GetNewPopulation(IEnumerable<Individual> population)
        {
            int count = population.Count();
            int groupsAmount = count / numOfIndividualsInGroup;
            
            if (groupsAmount < 2)
                return population;

            var availableIndivsIndices = Enumerable.Range(0, count).ToList();
            var groupBuffer = new List<Individual>(numOfIndividualsInGroup);
            var bestFromGroups = new List<Individual>(groupsAmount);

            for (int i = 0; i < groupsAmount; i++)
            {
                for (int j = 0; j < numOfIndividualsInGroup; j++)
                {
                    int randIndex = availableIndivsIndices[rand.Next(0, availableIndivsIndices.Count)];
                    groupBuffer.Add(population.ElementAt(randIndex));
                    availableIndivsIndices.Remove(randIndex);
                }
                var best = groupBuffer.OrderByDescending(x => x.FitnessValue).ElementAt(0);
                bestFromGroups.Add(best);
                groupBuffer.Clear();
            }

            return bestFromGroups;
        }
    }
}
