namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day14
    {
        public static long GetQuantitySubstraction(int amoutOfSteps)
        {
            var input = InputHelper.GetInput(nameof(Day14));
            //var input = InputHelper.GetSmallInput(nameof(Day14));

            var polymerTemplate = input.First();
            var rules = input.Skip(2).ToDictionary(line => line.Split(" -> ")[0], line => line.Split(" -> ")[1]);

            var sequences = GetStartSequencesAmouts(rules, polymerTemplate);

            ApplyStepsPart2(sequences, rules, amoutOfSteps);

            var quantities = GetQuantitiesOfElements(sequences, rules.Values.Distinct().ToList());
            return quantities.Values.Max() - quantities.Values.Min();
        }

        private static Dictionary<string, long> GetStartSequencesAmouts(Dictionary<string, string> rules, string polymer)
        {
            var sequencesAmounts = new Dictionary<string, long>();

            // Init dictionary
            foreach (var rule in rules)
            {
                sequencesAmounts.Add(rule.Key, 0);
            }

            // Populate dictionary with initial polymer
            for (var i = 1; i < polymer.Length; i++)
            {
                var currentTwoCharSequence = new string(polymer.ToCharArray()[(i - 1)..(i + 1)]);
                sequencesAmounts[currentTwoCharSequence] += 1;
            }

            return sequencesAmounts;
        }

        private static void ApplyStepsPart2(Dictionary<string, long> sequencesAmounts, Dictionary<string, string> rules, int amountOfSteps)
        {
            for (var i = 0; i < amountOfSteps; i++)
            {
                ApplyStepPart2(sequencesAmounts, rules);
            }
        }

        private static void ApplyStepPart2(Dictionary<string, long> sequencesAmounts, Dictionary<string, string> rules)
        {
            var initialSequencesAmouts = new Dictionary<string, long>(sequencesAmounts);

            foreach (var rule in rules)
            {
                var leftCreatedSequence = string.Join("", rule.Key[0], rule.Value);
                var rightCreatedSequence = string.Join("", rule.Value, rule.Key[1]);

                sequencesAmounts[leftCreatedSequence] += initialSequencesAmouts[rule.Key];
                sequencesAmounts[rightCreatedSequence] += initialSequencesAmouts[rule.Key];

                sequencesAmounts[rule.Key] -= initialSequencesAmouts[rule.Key];
            }
        }

        private static Dictionary<string, long> GetQuantitiesOfElements(Dictionary<string, long> sequencesAmounts, List<string> elements)
        {
            var quantitiesOfElements = new Dictionary<string, long>();

            foreach (var element in elements)
            {
                var amout = (long)Math.Round((sequencesAmounts.Where(s => s.Key.Contains(element)).ToList().Sum(x => x.Value) + sequencesAmounts[element + element]) / 2.0, MidpointRounding.AwayFromZero);
                quantitiesOfElements.Add(element, amout);
            }

            return quantitiesOfElements;
        }
    }
}
