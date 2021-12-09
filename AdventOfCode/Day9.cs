namespace AdventOfCode
{
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day9
    {

        #region Part1
        public static int GetSumOfRiskOfLowPoints()
        {
            var input = InputHelper.GetInput(nameof(Day9));
            //var input = InputHelper.GetSmallInput(nameof(Day9));

            var heightMap = GetHeightMapPoints(input);

            return GetLowPointsValues(heightMap).Sum(x => x + 1);
        }
        #endregion

        #region Part2
        public static int GetProductOfThreeLargestBasins()
        {
            var input = InputHelper.GetInput(nameof(Day9));
            //var input = InputHelper.GetSmallInput(nameof(Day9));

            var heightMap = GetHeightMapPoints(input);

            var lowPoints = GetLowPoints(heightMap);

            var basins = GetBasins(heightMap, lowPoints);

            return basins.OrderByDescending(b => b.Count).Take(3).Select(x => x.Count).Aggregate((x, y) => x * y);
        }
        #endregion

        private static HeightPoint[][] GetHeightMapPoints(IEnumerable<string> input)
        {
            var heightMap = new HeightPoint[input.Count()][];

            for (var rowIndex = 0; rowIndex < input.Count(); rowIndex++)
            {
                heightMap[rowIndex] = new HeightPoint[input.ElementAt(rowIndex).Length];

                for (var columnIndex = 0; columnIndex < input.ElementAt(rowIndex).Length; columnIndex++)
                {
                    heightMap[rowIndex][columnIndex] = new HeightPoint { Location = (Row: rowIndex, Column: columnIndex), Value = int.Parse(input.ElementAt(rowIndex)[columnIndex].ToString()) };
                }
            }

            return heightMap;
        }

        private static List<HeightPoint> GetLowPoints(HeightPoint[][] heightMap)
        {
            var lowPoints = new List<HeightPoint>();

            for (var rowIndex = 0; rowIndex < heightMap.Length; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < heightMap[rowIndex].Length; columnIndex++)
                {
                    var point = heightMap[rowIndex][columnIndex];
                    if (GetAdjacentPointsValues(heightMap, rowIndex, columnIndex).All(x => point.Value < x))
                    {
                        lowPoints.Add(heightMap[rowIndex][columnIndex]);
                    }
                }
            }

            return lowPoints;
        }

        private static List<int> GetLowPointsValues(HeightPoint[][] heightMap) => GetLowPoints(heightMap).Select(p => p.Value).ToList();

        private static List<int> GetAdjacentPointsValues(HeightPoint[][] heightMap, int pointRow, int pointColumn) => GetAdjacentPoints(heightMap, pointRow, pointColumn).Select(p => p.Value).ToList();

        private static List<HeightPoint> GetAdjacentPoints(HeightPoint[][] heightMap, int pointRow, int pointColumn)
        {
            var adjacentPoints = new List<HeightPoint>();

            // Top
            if (pointRow > 0)
                adjacentPoints.Add(heightMap[pointRow - 1][pointColumn]);

            // Bottom
            if (pointRow < heightMap.Length - 1)
                adjacentPoints.Add(heightMap[pointRow + 1][pointColumn]);

            // Left
            if (pointColumn > 0)
                adjacentPoints.Add(heightMap[pointRow][pointColumn - 1]);

            // Right
            if (pointColumn < heightMap[pointRow].Length - 1)
                adjacentPoints.Add(heightMap[pointRow][pointColumn + 1]);

            return adjacentPoints;
        }

        private static List<List<HeightPoint>> GetBasins(HeightPoint[][] heightMap, List<HeightPoint> lowPoints)
        {
            var basins = new List<List<HeightPoint>>();

            foreach (var lowPoint in lowPoints)
            {
                var currentBasin = new List<HeightPoint>() { lowPoint };

                for (var i = 0; i < currentBasin.Count; i++)
                {
                    currentBasin.AddRange(GetAdjacentPoints(heightMap, currentBasin[i].Location.Row, currentBasin[i].Location.Column).Where(p => !currentBasin.Contains(p) && p.Value != 9));
                }

                basins.Add(currentBasin);
            }

            return basins;
        }

        class HeightPoint
        {
            public (int Row, int Column) Location { get; set; }
            public int Value { get; set; }
        }
    }
}
