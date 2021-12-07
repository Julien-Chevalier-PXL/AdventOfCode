namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day7
    {
        #region Part2
        public static int GetFuelAmount()
        {
            var input = InputHelper.GetInput(nameof(Day7));
            //var input = InputHelper.GetSmallInput(nameof(Day7));

            var positions = GetPositions(input);

            var smallestAmoutOfFuel = int.MaxValue;

            for (var i = positions.Min(); i <= positions.Max(); i++)
            {
                var currentAmoutOfFuel = positions.Sum(x => (Math.Abs(x - i) * (Math.Abs(x - i) + 1)) / 2);

                if (currentAmoutOfFuel < smallestAmoutOfFuel)
                {
                    smallestAmoutOfFuel = currentAmoutOfFuel;
                }
            }

            return smallestAmoutOfFuel;
        }
        #endregion

        #region Part1
        public static int GetFuelAmountPart1()
        {
            var input = InputHelper.GetInput(nameof(Day7));
            //var input = InputHelper.GetSmallInput(nameof(Day7));

            var positions = GetPositions(input);

            var smallestAmoutOfFuel = int.MaxValue;

            for (var i = positions.Min(); i <= positions.Max(); i++)
            {
                var currentAmoutOfFuel = positions.Sum(x => Math.Abs(x - i));

                if (currentAmoutOfFuel < smallestAmoutOfFuel)
                {
                    smallestAmoutOfFuel = currentAmoutOfFuel;
                }
            }

            return smallestAmoutOfFuel;
        }
        #endregion

        private static IEnumerable<int> GetPositions(IEnumerable<string> input)
        {
            return input.First().Split(",").Select(x => int.Parse(x));
        }
    }
}
