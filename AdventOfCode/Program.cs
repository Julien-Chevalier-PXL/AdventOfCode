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

            Console.WriteLine("Goodbye World!");
        }
    }
}
