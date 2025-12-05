namespace AdventOfCode._2025.Days;

using System;
using System.Linq;

internal static class Day4
{
    const int RollsThreshold = 4;

    public static int Part1(string[] lines)
    {
        var grid = BuildGrid(lines);
        RemoveRolls(grid);

        return grid.SelectMany(p => p).Count(p => p.IsRemoved);
    }

    public static int Part2(string[] lines)
    {
        var rollsCounter = 0;

        var grid = BuildGrid(lines);

        int currentIterationRemovedRolls = 0;
        do
        {
            RemoveRolls(grid);
            var removedRolls = grid.SelectMany(p => p).Where(p => p.IsRemoved);
            currentIterationRemovedRolls = removedRolls.Count();
            removedRolls.ToList().ForEach(r => { r.IsRoll = 0; r.IsRemoved = false; });

            rollsCounter += currentIterationRemovedRolls;

        } while (currentIterationRemovedRolls != 0);

        return rollsCounter;
    }

    private static void RemoveRolls(GridLocation[][] grid)
    {
        var numberOfRows = grid.Length;
        var numberOfColumns = grid[0].Length;

        foreach (var roll in grid.SelectMany(r => r).Where(p => p.IsRoll is 1))
        {
            var adjacentPositions = GetAdjacentPositions(roll.RowIndex, roll.ColumnIndex, numberOfRows - 1, numberOfColumns - 1);
            if (adjacentPositions.Sum(p => grid[p.RowIndex][p.ColIndex].IsRoll) < RollsThreshold)
                roll.IsRemoved = true;
        }
    }

    private static (int RowIndex, int ColIndex)[] GetAdjacentPositions(int rowIndex, int colIndex, int maxRowIndex, int maxColIndex)
    {
        return [.. GetHeightAdjacentPositions(rowIndex, colIndex).Where(p => p.RowIndex >= 0 && p.RowIndex <= maxRowIndex && p.ColIndex >= 0 && p.ColIndex <= maxColIndex)];

        static (int RowIndex, int ColIndex)[] GetHeightAdjacentPositions(int rowIndex, int colIndex)
        {
            return [
                (rowIndex - 1, colIndex - 1), // Top Left
                (rowIndex - 1, colIndex), // Top Center
                (rowIndex - 1, colIndex + 1), // Top Right
                (rowIndex, colIndex - 1), // Left
                (rowIndex, colIndex + 1), // Right
                (rowIndex + 1, colIndex - 1), // Bottom Left
                (rowIndex + 1, colIndex), // Bottom Center
                (rowIndex + 1, colIndex + 1), // Bottom Right
            ];
        }
    }

    private static GridLocation[][] BuildGrid(string[] lines) 
        => [.. lines.Select((l, row) => l.Select((c, column) => new GridLocation { RowIndex = row, ColumnIndex = column, IsRoll = c == '@' ? 1 : 0 }).ToArray())];

    class GridLocation
    {
        public int RowIndex { get; init; }
        public int ColumnIndex { get; init; }
        public int IsRoll { get; set; }
        public bool IsRemoved { get; set; }
    }
}
