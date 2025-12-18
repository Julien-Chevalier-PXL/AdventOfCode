namespace AdventOfCode._2025.Days;

internal static class Day7
{
    const string BeamStartingPointIdentifier = "S";

    public static int Part1(string[] lines)
    {
        var splitCounter = 0;

        int[] lastRowBeamsIndexes = [lines[0].IndexOf(BeamStartingPointIdentifier)];

        for (var i = 1; i < lines.Length - 1; i++)
        {
            var currentLine = lines[i];
            var splitterIndexes = currentLine.Select((c, index) => (Char: c, Index: index)).Where(c => c.Char == '^').Select(x => x.Index);
            var splittedBeams = splitterIndexes.Intersect(lastRowBeamsIndexes);
            splitCounter += splittedBeams.Count();

            lastRowBeamsIndexes = [.. lastRowBeamsIndexes.Except(splittedBeams), .. splittedBeams.SelectMany(x => new int[2] { x - 1, x + 1, })];
        }

        return splitCounter;
    }

    public static int Part2(string[] lines)
    {
        var timelines = 0;

        return timelines;
    }
}
