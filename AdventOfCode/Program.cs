namespace AdventOfCode
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine($"Day 1 Part 1: {Day1.GetNumberOfLargerMeasurements()}");
            Console.WriteLine($"Day 1 Part 2: {Day1.GetNumberOfLargerThreeSlidingMeasurements()}");
            Console.WriteLine($"Day 2 Part 2: {Day2.GetPositionResult()}");
            Console.WriteLine($"Day 3 Part 1: {Day3.GetPowerConsumption()}");
            Console.WriteLine($"Day 3 Part 2: {Day3.GetLifeSupportRating()}");
            Console.WriteLine($"Day 4 Part 2: {Day4.GetFinalScore()}");
            Console.WriteLine($"Day 5 Part 1: {Day5.GetNumberOfDangerousPointsPart1()}");
            Console.WriteLine($"Day 5 Part 2: {Day5.GetNumberOfDangerousPoints()}");
            Console.WriteLine($"Day 6 Part 1: {Day6.GetNumberOfLanternfishPart1()}");
            Console.WriteLine($"Day 6 Part 2: {Day6.GetNumberOfLanternfish()}");
            Console.WriteLine($"Day 7 Part 1: {Day7.GetFuelAmountPart1()}");
            Console.WriteLine($"Day 7 Part 2: {Day7.GetFuelAmount()}");
            Console.WriteLine($"Day 8 Part 1: {Day8.GetCountEasyDigits()}");
            Console.WriteLine($"Day 8 Part 2: {Day8.GetSumOfOutputValues()}");
            Console.WriteLine($"Day 9 Part 1: {Day9.GetSumOfRiskOfLowPoints()}");
            Console.WriteLine($"Day 9 Part 2: {Day9.GetProductOfThreeLargestBasins()}");
            Console.WriteLine($"Day 10 Part 1: {Day10.GetTotalSyntaxErrorScore()}");
            Console.WriteLine($"Day 10 Part 2: {Day10.GetTotalAutocompletionScore()}");
            Console.WriteLine($"Day 11 Part 1: {Day11.GetTotalFlashes()}");
            Console.WriteLine($"Day 11 Part 2: {Day11.GetFirstFlashSync()}");
            Console.WriteLine($"Day 12 Part 1: {Day12.GetNumberOfPaths()}");
            Console.WriteLine($"Day 12 Part 2: {Day12.GetNumberOfPathsPart2()}");
            Console.WriteLine($"Day 13 Part 1: {Day13.GetNumberOfDotsAfterFirstFold()}");
            Console.WriteLine($"Day 13 Part 2: {Day13.GetNumberOfDots()}");
            Console.WriteLine($"Day 14 Part 1: {Day14.GetQuantitySubstraction(10)}");
            Console.WriteLine($"Day 14 Part 2: {Day14.GetQuantitySubstraction(40)}");
            Console.WriteLine($"Day 15 Part 1: {Day15.GetLowestTotalRisk()}");

            Console.WriteLine("Goodbye World!");
        }
    }
}
