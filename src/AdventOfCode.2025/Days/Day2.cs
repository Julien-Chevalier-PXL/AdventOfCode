namespace AdventOfCode._2025.Days;

internal static class Day2
{
    public static long GetSumOfInvalidIdsDoublePattern(string[] lines)
    {
        long result = 0;

        var line = lines.First();

        foreach (var range in line.Split(","))
        {
            var bounds = range.Split("-");
            var lowerBound = long.Parse(bounds[0]);
            var upperBound = long.Parse(bounds[1]);

            for (var id = lowerBound; id <= upperBound; id++)
            {
                var idStr = id.ToString();
                var middle = idStr.Length / 2;
                if (idStr[..middle] == idStr[middle..])
                    result += id;
            }
        }

        return result;
    }

    public static long GetSumOfInvalidIdsMultiplePatterns(string[] lines)
    {
        long result = 0;

        var line = lines.First();
        var ranges = line.Split(",").Select(range =>
        {
            var bounds = range.Split("-");
            return (LowerBound: long.Parse(bounds[0]), UpperBound: long.Parse(bounds[1]));
        });

        foreach (var (lowerBound, upperBound) in ranges)
        {
            for (var id = lowerBound; id <= upperBound; id++)
            {
                var idStr = id.ToString();

                var possiblePatternLengths = Enumerable.Range(1, idStr.Length - 1).Where(x => idStr.Length % x == 0);

                foreach (var patternLength in possiblePatternLengths)
                {
                    var pattern = idStr[..patternLength];
                    var idPatternLengthChucks = idStr.Chunk(patternLength);
                    if(idPatternLengthChucks.All(x => new string(x) == pattern))
                    {
                        result += id;
                        break;
                    }
                }
            }
        }

        return result;
    }
}
