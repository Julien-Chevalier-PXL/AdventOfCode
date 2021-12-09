namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AdventOfCode.Helpers;

    public static class Day8
    {
        private static readonly Dictionary<int, int> UNIQUE_NUMBERS_LENGTH = new Dictionary<int, int> {
            { 1, 2 },
            { 4, 4 },
            { 7, 3 },
            { 8, 7 }
        };

        #region Part1
        public static int GetCountEasyDigits()
        {
            var input = InputHelper.GetInput(nameof(Day8));
            //var input = InputHelper.GetSmallInput(nameof(Day8));

            List<(string[] SignalPatterns, string[] OutputValue)> signals = GetSignals(input);

            return signals.Sum(s => s.OutputValue.Count(o => UNIQUE_NUMBERS_LENGTH.Values.Contains(o.Length)));
        }
        #endregion

        #region Part2
        public static int GetSumOfOutputValues()
        {
            var input = InputHelper.GetInput(nameof(Day8));
            //var input = InputHelper.GetSmallInput(nameof(Day8));
            //var input = InputHelper.GetTestInput(nameof(Day8));

            List<(string[] SignalPatterns, string[] OutputValue)> signals = GetSignals(input);

            var result = 0;

            foreach (var signal in signals)
            {
                var wireMapping = GetSignalWiresMapping(signal.SignalPatterns);

                var numberStr = new StringBuilder();
                foreach (var codeNumber in signal.OutputValue)
                {
                    numberStr.Append(wireMapping.GetValueOrDefault(codeNumber));
                }

                result += int.Parse(numberStr.ToString());
            }

            return result;
        }
        #endregion

        private static List<(string[] SignalPatterns, string[] OutputValue)> GetSignals(IEnumerable<string> input) => input.Select(i => (SignalPatterns: i.Split(" | ")[0].Split(" ").Select(s => new string(s.ToCharArray().OrderBy(c => c).ToArray())).ToArray(), OutputValue: i.Split(" | ")[1].Split(" ").Select(s => new string(s.ToCharArray().OrderBy(c => c).ToArray())).ToArray())).ToList();

        private static Dictionary<string, int> GetSignalWiresMapping(string[] signalPatterns)
        {
            var wireDecoder = new Dictionary<char, char>();
            var decoder = new Dictionary<string, int>();

            var one = signalPatterns.Single(s => s.Length == 2);
            var four = signalPatterns.Single(s => s.Length == 4);
            var seven = signalPatterns.Single(s => s.Length == 3);
            var eight = signalPatterns.Single(s => s.Length == 7);

            var fiveChars = signalPatterns.Where(s => s.Length == 5);
            var sixChars = signalPatterns.Where(s => s.Length == 6);

            wireDecoder.Add('a', seven.Except(one).Single());

            var nine = signalPatterns.Single(s => s.Length == 6 && !four.Except(s).Any());
            wireDecoder.Add('g', nine.Except(wireDecoder['a'].ToCharArray()).Except(four).Single());

            wireDecoder.Add('f', signalPatterns.SelectMany(s => s.ToCharArray()).GroupBy(x => x).Where(g => g.Count() == 9).Single().Key);
            var two = signalPatterns.Single(s => !s.Contains(wireDecoder['f']));

            var three = fiveChars.Single(s => s.Except(one).Count() == 3);
            var five = fiveChars.Single(s => s != two && s != three);
            var six = sixChars.Where(s => s != nine && s.Except(five).Count() == 1).Single();
            var zero = sixChars.Single(s => s != nine && s != six);


            wireDecoder.Add('b', four.Except(three).Single());
            wireDecoder.Add('c', eight.Except(six).Single());
            wireDecoder.Add('d', eight.Except(zero).Single());
            wireDecoder.Add('e', eight.Except(nine).Single());

            decoder.Add(zero, 0);
            decoder.Add(one, 1);
            decoder.Add(two, 2);
            decoder.Add(three, 3);
            decoder.Add(four, 4);
            decoder.Add(five, 5);
            decoder.Add(six, 6);
            decoder.Add(seven, 7);
            decoder.Add(eight, 8);
            decoder.Add(nine, 9);

            return decoder;
        }

        private static char[] ToCharArray(this char c) => c.ToString().ToCharArray();
    }
}
