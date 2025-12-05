namespace AdventOfCode._2025.Days;

using System.Text;

internal static class Day3
{
    public static long Part1(string[] lines)
    {
        const int MaximumNbOfBatteries = 2;
        return GetBestJoltage(lines, MaximumNbOfBatteries);
    }

    public static long Part2(string[] lines)
    {
        const int MaximumNbOfBatteries = 12;
        return GetBestJoltage(lines, MaximumNbOfBatteries);
    }

    private static long GetBestJoltage(string[] lines, int MaximumNbOfBatteries)
    {
        var result = 0L;

        var banks = lines.Select(l => l.Select((c, index) => (Number: index + 1, Value: int.Parse(c.ToString()))));

        foreach (var bank in banks)
        {
            var lastChosenBatteryNumber = 0;
            var bankJoltageSb = new StringBuilder(MaximumNbOfBatteries);
            var orderedBankByNumber = bank.OrderBy(b => b.Number);

            for (var i = 1; i <= MaximumNbOfBatteries; i++)
            {
                var (bestBatteryNumber, bestBatteryValue) = orderedBankByNumber.Skip(lastChosenBatteryNumber).SkipLast(MaximumNbOfBatteries - i).MaxBy(b => b.Value);
                lastChosenBatteryNumber = bestBatteryNumber;
                bankJoltageSb.Append(bestBatteryValue);
            }

            result += long.Parse(bankJoltageSb.ToString());
        }

        return result;
    }
}
