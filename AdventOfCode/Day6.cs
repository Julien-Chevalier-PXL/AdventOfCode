namespace AdventOfCode
{
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day6
    {
        #region Part2
        public static long GetNumberOfLanternfish()
        {
            var input = InputHelper.GetInput(nameof(Day6));
            //var input = InputHelper.GetSmallInput(nameof(Day6));

            var lanternfish = GetLanternFishes(input);
            //Console.WriteLine($"Initial state: {string.Join(",", lanternfish)}");

            Dictionary<int, long> fishByDay = GetNbFishByDay(lanternfish);

            for (var day = 1; day <= 256; day++)
            {
                var fishByDayAtStart = new Dictionary<int, long>(fishByDay);
                for (var i = 8; i >= 0; i--)
                {
                    if (i == 0)
                    {
                        fishByDay[8] += fishByDayAtStart[i];
                        fishByDay[6] += fishByDayAtStart[i];
                    }
                    else
                    {
                        fishByDay[i - 1] += fishByDayAtStart[i];
                    }

                    fishByDay[i] -= fishByDayAtStart[i];
                }
            }

            return fishByDay.Sum(x => x.Value);
        }
        #endregion

        #region Part1
        public static int GetNumberOfLanternfishPart1()
        {
            //var input = InputHelper.GetInput(nameof(Day6));
            var input = InputHelper.GetSmallInput(nameof(Day6));

            var lanternfish = GetLanternFishes(input);

            for (var day = 1; day <= 80; day++)
            {
                var nbFishAtStartOfDay = lanternfish.Count;
                for (int i = 0; i < nbFishAtStartOfDay; i++)
                {
                    if (lanternfish[i] == 0)
                    {
                        lanternfish.Add(8);
                        lanternfish[i] = 6;
                    }
                    else
                    {
                        lanternfish[i] = lanternfish[i] - 1;
                    }
                }
            }

            return lanternfish.Count();
        }
        #endregion

        private static Dictionary<int, long> GetNbFishByDay(List<int> lanternfish)
        {
            var fishByDay = lanternfish.GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());
            for (var i = 0; i <= 8; i++)
            {
                if (!fishByDay.ContainsKey(i))
                {
                    fishByDay.Add(i, 0);
                }
            }

            return fishByDay;
        }

        private static List<int> GetLanternFishes(IEnumerable<string> input)
        {
            return input.First().Split(",").Select(x => int.Parse(x)).ToList();
        }
    }
}
