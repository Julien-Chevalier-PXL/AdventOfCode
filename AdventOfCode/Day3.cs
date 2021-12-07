namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using AdventOfCode.Helpers;

    public static class Day3
    {
        #region Part1
        public static int GetPowerConsumption()
        {
            var inputs = InputHelper.GetInput(nameof(Day3));
            //var inputs = InputHelper.GetSmallInput(nameof(Day3));

            var inputLength = inputs.FirstOrDefault().Length;

            var gammaRateBinary = new StringBuilder();

            for (var i = 0; i < inputLength; i++)
            {
                var mostCommon = GetMostCommonBitOnIndex(i, inputs);
                gammaRateBinary.Append(mostCommon);
            }

            var gammaRate = Convert.ToInt32(gammaRateBinary.ToString(), 2);
            var epsilonRate = Convert.ToInt32(BinaryNot(gammaRateBinary.ToString()), 2);

            return gammaRate * epsilonRate;
        }
        #endregion

        #region Part2
        public static int GetLifeSupportRating()
        {
            var inputs = InputHelper.GetInput(nameof(Day3));
            //var inputs = InputHelper.GetSmallInput(nameof(Day3));

            var oxygenGeneratorRating = GetOxygenGeneratorRating(inputs);
            var co2ScrubberRating = GetCO2ScrubberRating(inputs);

            return Convert.ToInt32(oxygenGeneratorRating, 2) * Convert.ToInt32(co2ScrubberRating, 2);
        }
        #endregion

        private static string GetOxygenGeneratorRating(IEnumerable<string> inputs)
        {
            IEnumerable<string> oxygenGeneratorPossibilities = inputs.ToList();

            for (var bitIndex = 0; bitIndex < inputs.FirstOrDefault().Length && oxygenGeneratorPossibilities.Count() > 1; bitIndex++)
            {
                oxygenGeneratorPossibilities = FilterByMostCommonOnIndexOrOne(bitIndex, oxygenGeneratorPossibilities).ToList();
            }

            return oxygenGeneratorPossibilities.First();
        }

        private static string GetCO2ScrubberRating(IEnumerable<string> inputs)
        {
            IEnumerable<string> co2ScrubberPossibilities = inputs.ToList();
            for (var bitIndex = 0; bitIndex < inputs.FirstOrDefault().Length && co2ScrubberPossibilities.Count() > 1; bitIndex++)
            {
                co2ScrubberPossibilities = FilterByLessCommonOnIndexOrZero(bitIndex, co2ScrubberPossibilities);
            }

            return co2ScrubberPossibilities.First();
        }

        private static string GetBitAtIndex(this string value, int bitIndex) => value.Substring(bitIndex, 1);

        private static IEnumerable<string> FilterByMostCommonOnIndexOrOne(int bitIndex, IEnumerable<string> inputs)
        {
            var mostCommonBit = GetMostCommonBitOnIndexOrOne(bitIndex, inputs);
            return inputs.Where(x => x.GetBitAtIndex(bitIndex) == mostCommonBit);
        }

        private static IEnumerable<string> FilterByLessCommonOnIndexOrZero(int bitIndex, IEnumerable<string> inputs)
        {
            var mostCommonBit = GetLessCommonBitOnIndexOrZero(bitIndex, inputs);
            return inputs.Where(x => x.GetBitAtIndex(bitIndex) == mostCommonBit);
        }

        private static string GetMostCommonBitOnIndexOrOne(int bitIndex, IEnumerable<string> inputs)
        {
            var grouped = inputs.Select(x => x.GetBitAtIndex(bitIndex)).GroupBy(x => x).OrderByDescending(group => group.Count());

            return grouped.ElementAt(0).Count() != grouped.ElementAt(1).Count() ? grouped.FirstOrDefault().Key : "1";
        }

        private static string GetLessCommonBitOnIndexOrZero(int bitIndex, IEnumerable<string> inputs)
        {
            var grouped = inputs.Select(x => x.GetBitAtIndex(bitIndex)).GroupBy(x => x).OrderBy(group => group.Count());

            return grouped.ElementAt(0).Count() != grouped.ElementAt(1).Count() ? grouped.FirstOrDefault().Key : "0";
        }

        private static string GetMostCommonBitOnIndex(int bitIndex, IEnumerable<string> inputs)
        {
            return inputs.Select(x => x.GetBitAtIndex(bitIndex)).GroupBy(x => x).OrderByDescending(group => group.Count()).FirstOrDefault().Key;
        }

        private static string GetLessCommonBitOnIndex(int bitIndex, IEnumerable<string> inputs)
        {
            return inputs.Select(x => x.GetBitAtIndex(bitIndex)).GroupBy(x => x).OrderBy(group => group.Count()).FirstOrDefault().Key;
        }

        private static string BinaryNot(string input) => string.Concat(input.Select(x => x == '0' ? '1' : '0'));
    }
}
