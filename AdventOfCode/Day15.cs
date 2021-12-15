namespace AdventOfCode
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using AdventOfCode.Helpers;

    public static class Day15
    {
        public static int GetLowestTotalRisk()
        {
            var input = InputHelper.GetInput(nameof(Day15));
            //var input = InputHelper.GetSmallInput(nameof(Day15));

            var map = GetRiskMap(input);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var bestPath = GetBestPath(map);
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds} ms");

            return bestPath.Skip(1).Sum(p => p.RiskValue);
        }

        public static int GetLowestTotalRiskPart2()
        {
            //var input = InputHelper.GetInput(nameof(Day15) + "-2");
            var input = InputHelper.GetSmallInput(nameof(Day15) + "-2");

            var map = GetRiskMap(input);
            //var map = GetMap(input);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var bestPath = GetBestPath(map);
            watch.Stop();
            Console.WriteLine($"{watch.ElapsedMilliseconds} ms");

            return bestPath.Skip(1).Sum(p => p.RiskValue);
        }

        public static bool ControlInputExpansion()
        {
            var input = InputHelper.GetSmallInput(nameof(Day15) + "-2");
            var smallInput = InputHelper.GetSmallInput(nameof(Day15));

            var map = GetRiskMap(input);
            var fullmap = new Risk[input.Count() * 5][];

            for (var i = 0; i < 5; i++)
            {

            }

            return false;
        }

        private static Risk[][] GetRiskMap(IEnumerable<string> input)
        {
            var map = new Risk[input.Count()][];

            for (var lineIndex = 0; lineIndex < input.Count(); lineIndex++)
            {
                var line = input.ElementAt(lineIndex);
                map[lineIndex] = new Risk[line.Length];

                for (var charIndex = 0; charIndex < line.Length; charIndex++)
                {
                    map[lineIndex][charIndex] = new Risk { Location = (lineIndex, charIndex), RiskValue = int.Parse(line[charIndex].ToString()) };
                }
            }

            return map;
        }

        private static List<Risk> GetBestPath(Risk[][] map)
        {
            var iterationIndex = 0;
            var end = map[^1][^1];
            List<Risk> finalPath = default;
            var initPath = new List<List<Risk>>();

            initPath.Add(new List<Risk> { map[0][0] });

            var remainingPaths = GetAllPossiblePathsFromActualPaths(map, initPath);

            while (remainingPaths.Count > 0)
            {
                var mostAdvanced = remainingPaths.SelectMany(x => x).OrderByDescending(x => x.Location.Column + x.Location.Column).FirstOrDefault();
                Console.WriteLine($"Most advanced path: ({mostAdvanced.Location.Row},{mostAdvanced.Location.Column})");

                Stopwatch watch = new Stopwatch();
                watch.Start();

                var allPossiblePaths = GetAllPossiblePathsFromActualPaths(map, remainingPaths);
                var paths = allPossiblePaths.GroupBy(x => x.Last(), (key, g) => g.OrderBy(x => x.Sum(y => y.RiskValue)).ThenBy(x => x.Count).First()).ToList();

                finalPath = GetBestFinishedPath(finalPath, paths, end);

                remainingPaths = paths.Where(path => !path.Contains(end) && (finalPath == null || path.Sum(p => p.RiskValue) < finalPath.Sum(x => x.RiskValue))).ToList();

                watch.Stop();
                Console.WriteLine($"Iteration {++iterationIndex}: {watch.ElapsedMilliseconds} ms");
                Console.WriteLine($"Number of remaining paths: {remainingPaths.Count}");

                Console.WriteLine($"Best path for now: {(finalPath != null ? finalPath.Sum(x => x.RiskValue) : "None")}");
            }

            return finalPath;
        }

        private static List<Risk> GetBestFinishedPath(List<Risk> finalPath, List<List<Risk>> paths, Risk end)
        {
            if (!paths.Any(path => path.Contains(end)))
            {
                return finalPath;
            }

            var best = paths.Where(path => path.Contains(end)).OrderBy(path => path.Sum(p => p.RiskValue)).ThenBy(p => p.Count).First();
            if (finalPath == null)
            {
                return best;
            }

            return best.Sum(p => p.RiskValue) < finalPath.Sum(p => p.RiskValue) ? best : finalPath;
        }

        private static List<List<Risk>> GetAllPossiblePathsFromActualPaths(Risk[][] map, List<List<Risk>> allPaths)
        {
            var newAllPaths = new ConcurrentBag<List<Risk>>();

            Parallel.ForEach(allPaths, path =>
            {
                var newPossiblePaths = GetNewPossiblePaths(map, path);
                Parallel.ForEach(newPossiblePaths, newPossiblePath =>
                {
                    newAllPaths.Add(newPossiblePath);
                });
            });


            return newAllPaths.ToList();
        }

        private static List<List<Risk>> GetNewPossiblePaths(Risk[][] map, List<Risk> path)
        {
            var newPaths = new ConcurrentBag<List<Risk>>();

            var lastRiskInPath = path.Last();
            var neighbours = GetRiskNeighbours(lastRiskInPath, map).Where(n => !path.Contains(n)).ToList();

            Parallel.ForEach(neighbours, neighbour =>
            {
                var newPath = new List<Risk>();
                newPath.AddRange(path);
                newPath.Add(neighbour);
                newPaths.Add(newPath);
            });

            return newPaths.ToList();
        }

        private static List<Risk> GetRiskNeighbours(Risk risk, Risk[][] map)
        {
            var neighbours = new List<Risk>();

            // Top
            if (risk.Location.Row > 0)
                neighbours.Add(map[risk.Location.Row - 1][risk.Location.Column]);

            // Bottom
            if (risk.Location.Row < map.Length - 1)
                neighbours.Add(map[risk.Location.Row + 1][risk.Location.Column]);

            // Left
            if (risk.Location.Column > 0)
                neighbours.Add(map[risk.Location.Row][risk.Location.Column - 1]);

            // Right
            if (risk.Location.Column < map[risk.Location.Row].Length - 1)
                neighbours.Add(map[risk.Location.Row][risk.Location.Column + 1]);

            return neighbours;
        }

        class Risk
        {
            public (int Row, int Column) Location { get; set; }
            public int RiskValue { get; set; }
        }
    }
}
