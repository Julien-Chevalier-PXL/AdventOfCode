namespace AdventOfCode._2025.Days;

using System;

internal static class Day5
{
    public static int Part1(string[] lines)
    {
        var counter = 0;

        var ingredientDb = ParseLines(lines);
        foreach (var ingredientId in ingredientDb.IngredientsIds)
        {
            if (ingredientDb.FreshRanges.Any(r => r.LowerBound <= ingredientId && ingredientId <= r.UpperBound))
                counter++;
        }

        return counter;
    }

    public static long Part2(string[] lines)
    {
        var ingredientDb = ParseLines(lines);

        var optimizedRanges = GetOptimizedRanges(ingredientDb.FreshRanges);

        return optimizedRanges.Sum(r => r.UpperBound - r.LowerBound + 1);
    }

    private static IngredientDb ParseLines(string[] lines)
    {
        var fileSplitIndex = lines.IndexOf(string.Empty);
        var freshIngredientIdRanges = lines[..fileSplitIndex].Select(l =>
        {
            var bounds = l.Split("-");
            return new IngredientRange { LowerBound = long.Parse(bounds[0]), UpperBound = long.Parse(bounds[1]) };
        });
        var ingredientsIds = lines[(fileSplitIndex + 1)..].Select(long.Parse);

        return new IngredientDb([.. freshIngredientIdRanges], [.. ingredientsIds]);
    }

    private static IEnumerable<IngredientRange> GetOptimizedRanges(IngredientRange[] rangeCollection)
    {
        var optimizedRanges = new List<IngredientRange>();

        var orderedRangeCollection = rangeCollection.GroupBy(r => r.LowerBound).Select(grp => new IngredientRange { LowerBound = grp.Key, UpperBound = grp.Max(r => r.UpperBound)}).OrderBy(r => r.LowerBound).ToArray();

        IngredientRange currentOptimizedRange = new() { LowerBound = orderedRangeCollection[0].LowerBound, UpperBound = orderedRangeCollection[0].UpperBound };
        foreach (var range in orderedRangeCollection.Skip(1))
        {
            if (range.LowerBound <= currentOptimizedRange.UpperBound + 1 && range.UpperBound >= currentOptimizedRange.LowerBound + 1)
            {
                if (currentOptimizedRange.UpperBound < range.UpperBound)
                    currentOptimizedRange.UpperBound = range.UpperBound;

                if (range.LowerBound < currentOptimizedRange.LowerBound)
                    currentOptimizedRange.LowerBound = range.LowerBound;

                continue;
            }

            optimizedRanges.Add(new() { LowerBound = currentOptimizedRange.LowerBound, UpperBound = currentOptimizedRange.UpperBound });
            currentOptimizedRange = new() { LowerBound = range.LowerBound, UpperBound = range.UpperBound };
        }

        optimizedRanges.Add(currentOptimizedRange);

        return optimizedRanges;
    }

    record IngredientDb(IngredientRange[] FreshRanges, long[] IngredientsIds);

    class IngredientRange
    {
        public long LowerBound { get; set; }
        public long UpperBound { get; set; }
    }
}
