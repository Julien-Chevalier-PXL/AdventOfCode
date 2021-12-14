namespace AdventOfCode
{
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day12
    {
        private static readonly List<string> REALLY_SMALL_INPUT_TEST = new()
        {
            "start-A",
            "start-b",
            "A-c",
            "A-b",
            "b-d",
            "A-end",
            "b-end"
        };

        private const string START = "start";
        private const string END = "end";

        #region Part1
        public static int GetNumberOfPaths()
        {
            var input = InputHelper.GetInput(nameof(Day12));
            //var input = InputHelper.GetSmallInput(nameof(Day12));
            //var input = InputHelper.GetTestInput(nameof(Day12));
            //var input = REALLY_SMALL_INPUT_TEST;

            var caves = GetCaves(input);

            var paths = FindPaths(caves);

            return paths.Count();
        }
        #endregion

        #region Part2
        public static int GetNumberOfPathsPart2()
        {
            var input = InputHelper.GetInput(nameof(Day12));
            //var input = InputHelper.GetSmallInput(nameof(Day12));
            //var input = InputHelper.GetTestInput(nameof(Day12));
            //var input = REALLY_SMALL_INPUT_TEST;

            var caves = GetCaves(input);

            var paths = FindPathsPart2(caves);

            return paths.Count();
        }
        #endregion

        #region Path finding Part 1
        private static IEnumerable<List<Cave>> FindPaths(List<Cave> caves)
        {
            var paths = new List<List<Cave>>();

            var start = caves.Single(c => c.IsStart);
            var startPath = new List<Cave> { start };
            paths.Add(startPath);

            FindNewPathsRecursive(start, startPath, paths);

            return paths.Where(p => p.Last().IsEnd);
        }

        private static void FindNewPathsRecursive(Cave currentCave, List<Cave> currentPath, List<List<Cave>> allPaths)
        {
            var currentCaveConnections = currentCave.ConnectedCaves.Where(c => c.IsBigCave || !currentPath.Contains(c)).ToList();
            foreach (var cave in currentCaveConnections)
            {
                var newPath = new List<Cave>(currentPath)
                {
                    cave
                };
                allPaths.Add(newPath);
                FindNewPathsRecursive(cave, newPath, allPaths);
            }
        }
        #endregion

        #region Path finding Part 2
        private static IEnumerable<List<Cave>> FindPathsPart2(List<Cave> caves)
        {
            var paths = new List<List<Cave>>();

            var start = caves.Single(c => c.IsStart);
            var startPath = new List<Cave> { start };
            paths.Add(startPath);

            FindNewPathsRecursivePart2(start, startPath, paths);

            return paths.Where(p => p.Last().IsEnd);
        }

        private static void FindNewPathsRecursivePart2(Cave currentCave, List<Cave> currentPath, List<List<Cave>> allPaths)
        {
            var smallCaveTwice = currentPath.Where(c => !c.IsBigCave).GroupBy(c => c).SingleOrDefault(grp => grp.Count() == 2);
            var currentCaveConnections = currentCave.ConnectedCaves.Where(c => c.IsBigCave ||
                            (!c.IsBigCave &&
                                ((smallCaveTwice != default && !currentPath.Contains(c)) ||
                                (smallCaveTwice == default && (!c.IsEnd || !currentPath.Any(p => p.IsEnd)))))).ToList();

            foreach (var cave in currentCaveConnections)
            {
                var newPath = new List<Cave>(currentPath)
                {
                    cave
                };
                allPaths.Add(newPath);
                FindNewPathsRecursivePart2(cave, newPath, allPaths);
            }
        }
        #endregion

        private static List<Cave> GetCaves(IEnumerable<string> input)
        {
            var caves = new Dictionary<string, Cave>();

            foreach (var line in input)
            {
                var connection = line.Split("-");
                var cave1 = caves.GetValueOrDefault(connection[0], new Cave { Name = connection[0] });
                var cave2 = caves.GetValueOrDefault(connection[1], new Cave { Name = connection[1] });

                if (!cave1.IsEnd && !cave2.IsStart)
                    cave1.ConnectedCaves.Add(cave2);

                if (!cave2.IsEnd && !cave1.IsStart)
                    cave2.ConnectedCaves.Add(cave1);

                if (!caves.ContainsKey(cave1.Name))
                    caves.Add(cave1.Name, cave1);

                if (!caves.ContainsKey(cave2.Name))
                    caves.Add(cave2.Name, cave2);
            }

            return caves.Values.ToList();
        }

        class Cave
        {
            public string Name { get; set; }
            public bool IsStart => this.Name == START;
            public bool IsEnd => this.Name == END;
            public bool IsBigCave => this.Name == this.Name.ToUpper();
            public List<Cave> ConnectedCaves { get; } = new();
        }
    }
}
