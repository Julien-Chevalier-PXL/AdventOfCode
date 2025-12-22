namespace AdventOfCode._2025.Days;

internal static class Day6
{
    private const string Divider = " ";
    private const StringSplitOptions SplitOptions = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public static long Part1(string[] lines)
    {
        var result = 0L;

        var numbers = lines.SkipLast(1)
                           .Select(l => l.Split(Divider, SplitOptions).Select(int.Parse).ToArray())
                           .ToArray();

        var operations = lines.Last()
                              .Split(Divider, SplitOptions)
                              .Select(op => op switch
                              {
                                  "+" => Operation.Add,
                                  "*" => Operation.Multiply,
                                  _ => throw new InvalidDataException($"Unknown operation: {op}"),
                              })
                              .ToArray();

        for (var colIndex = 0; colIndex < operations.Length; colIndex++)
        {
            long opResult = numbers[0][colIndex];
            for (var rowIndex = 1; rowIndex < numbers.Length; rowIndex++)
            {
                if (operations[colIndex] is Operation.Add)
                    opResult += numbers[rowIndex][colIndex];
                else if (operations[colIndex] is Operation.Multiply)
                    opResult *= numbers[rowIndex][colIndex];
            }

            result += opResult;
        }

        return result;
    }

    public static int Part2(string[] lines)
    {
        return default;
    }

    record Problem(Operation Operation, long[] Numbers);

    enum Operation
    {
        Add,
        Multiply,
    }
}
