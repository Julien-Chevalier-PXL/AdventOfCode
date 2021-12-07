namespace AdventOfCode
{
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day1
    {
        #region Part1
        public static int GetNumberOfLargerMeasurements()
        {
            var result = 0;

            var inputs = GetInputMeasurements();

            for (var i = 1; i < inputs.Count(); i++)
            {
                if (inputs.ElementAt(i) > inputs.ElementAt(i - 1))
                {
                    result++;
                }
            }

            return result;
        }
        #endregion

        #region Part2
        public static int GetNumberOfLargerThreeSlidingMeasurements()
        {
            var result = 0;

            var inputs = GetInputMeasurements().ToArray();

            for (var i = 3; i < inputs.Length; i++)
            {

                var currentSum = inputs[(i - 2)..(i + 1)].Sum();
                var previousSum = inputs[(i - 3)..i].Sum();

                if (currentSum > previousSum)
                {
                    result++;
                }
            }

            return result;
        }
        #endregion

        private static int GetSumOfThreeSlidingMeasurements(int startIndex, int[] inputs)
        {
            return inputs[startIndex..(startIndex + 3)].Sum();
        }

        private static IEnumerable<int> GetInputMeasurements()
        {
            return InputHelper.GetInput(nameof(Day1)).Select(x => int.Parse(x));
        }

        private static IEnumerable<int> GetSmallInputMeasurements()
        {
            return InputHelper.GetSmallInput(nameof(Day1)).Select(x => int.Parse(x));
        }
    }
}
