namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day11
    {
        private const int OCTOPUS_MAP_SIZE = 10;

        #region Part1
        public static int GetTotalFlashes()
        {
            var input = InputHelper.GetInput(nameof(Day11));
            //var input = InputHelper.GetSmallInput(nameof(Day11));

            var octopusMap = GetOctopusMap(input);

            PlaySteps(100, octopusMap);

            //PrintMap(octopusMap);

            return octopusMap.SelectMany(o => o).Sum(o => o.Flashes);
        }
        #endregion

        #region Part2
        public static int GetFirstFlashSync()
        {
            var input = InputHelper.GetInput(nameof(Day11));
            //var input = InputHelper.GetSmallInput(nameof(Day11));

            var octopusMap = GetOctopusMap(input);

            var step = 0;
            while(!octopusMap.SelectMany(o => o).All(o => o.Energy == 0))
            {
                PlayStep(octopusMap);
                step++;
            }

            //PrintMap(octopusMap);

            return step;
        }
        #endregion

        private static void PrintMap(Octopus[][] octopusMap)
        {
            foreach(var oRow in octopusMap)
            {
                Console.WriteLine(string.Join("", oRow.Select(o => o.Energy)));
            }
        }

        private static void PlaySteps(int steps, Octopus[][] octopusMap)
        {
            for (var i = 0; i < steps; i++)
            {
                PlayStep(octopusMap);
            }
        }

        private static void PlayStep(Octopus[][] octopusMap)
        {
            octopusMap.SelectMany(o => o).ToList().ForEach(o => o.Energy++);

            octopusMap.SelectMany(o => o).Where(o => o.Energy > 9).ToList().ForEach(o => o.Flash());

            octopusMap.SelectMany(o => o).Where(o => o.HasFlashed).ToList().ForEach(o =>
            {
                o.HasFlashed = false;
                o.Energy = 0;
            });
        }

        private static Octopus[][] GetOctopusMap(IEnumerable<string> lines)
        {
            var octopusMap = new Octopus[10][];
            var row = 0;

            foreach (var line in lines)
            {
                octopusMap[row] = new Octopus[10];
                var column = 0;

                foreach (var c in line)
                {
                    octopusMap[row][column] = new Octopus { Location = (Row: row, Column: column), Energy = int.Parse(c.ToString()) };
                    column++;
                }

                row++;
            }

            octopusMap.SelectMany(o => o).ToList().ForEach(o => o.SetOctopusNeighbours(octopusMap));

            return octopusMap;
        }

        #region Models
        class Octopus
        {
            public (int Row, int Column) Location { get; set; }
            public int Flashes { get; private set; }
            public bool HasFlashed { get; set; }

            private int _energy;
            public int Energy
            {
                get { return _energy; }
                set { 
                    _energy = value;

                    if (_energy > 9)
                        this.Flash();
                }
            }

            public List<Octopus> OctopusNeighbours { get; } = new List<Octopus>();

            public void SetOctopusNeighbours(Octopus[][] octopusMap)
            {
                this.OctopusNeighbours.Clear();

                var isTopBorder = this.Location.Row == 0;
                var isLeftBorder = this.Location.Column == 0;
                var isBottomBorder = this.Location.Row == OCTOPUS_MAP_SIZE - 1;
                var isRightBorder = this.Location.Column == OCTOPUS_MAP_SIZE - 1;

                // Top Left
                if (!isTopBorder && !isLeftBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row - 1][this.Location.Column - 1]);

                // Top
                if (!isTopBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row - 1][this.Location.Column]);

                // Top Right
                if (!isTopBorder && !isRightBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row - 1][this.Location.Column + 1]);

                // Bottom Left
                if (!isBottomBorder && !isLeftBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row + 1][this.Location.Column - 1]);

                // Bottom
                if (!isBottomBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row + 1][this.Location.Column]);

                // Bottom Right
                if (!isBottomBorder && !isRightBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row + 1][this.Location.Column + 1]);

                // Left
                if (!isLeftBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row][this.Location.Column - 1]);

                // Right
                if (!isRightBorder)
                    this.OctopusNeighbours.Add(octopusMap[this.Location.Row][this.Location.Column + 1]);
            }

            public void Flash()
            {
                if (!this.HasFlashed)
                {
                    this.Flashes++;
                    this.HasFlashed = true;

                    foreach (var octopus in this.OctopusNeighbours)
                    {
                        octopus.Energy++;
                    }
                }
            }
        }
        #endregion
    }
}
