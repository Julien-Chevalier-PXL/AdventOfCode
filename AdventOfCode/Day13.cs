namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day13
    {
        public static int GetNumberOfDotsAfterFirstFold()
        {
            var input = InputHelper.GetInput(nameof(Day13));
            //var input = InputHelper.GetSmallInput(nameof(Day13));

            (List<Point> Dots, List<(string axe, int coordinate)> FoldInstructions) processedInput = GetProcessedInput(input);

            return Fold(processedInput.FoldInstructions.First().axe, processedInput.FoldInstructions.First().coordinate, processedInput.Dots).Count;
        }

        public static int GetNumberOfDots()
        {
            var input = InputHelper.GetInput(nameof(Day13));
            //var input = InputHelper.GetSmallInput(nameof(Day13));

            (List<Point> Dots, List<(string axe, int coordinate)> FoldInstructions) processedInput = GetProcessedInput(input);

            List<Point> result = processedInput.Dots;

            foreach (var foldInstruction in processedInput.FoldInstructions)
            {
                result = Fold(foldInstruction.axe, foldInstruction.coordinate, result);
            }

            PrintTransparentPaper(result);

            return result.Count;
        }

        private static void PrintTransparentPaper(List<Point> dots)
        {
            for (var i = 0; i <= dots.Max(d => d.Y); i++)
            {
                for (int j = 0; j <= dots.Max(d => d.X); j++)
                {
                    Console.Write($"{(dots.Any(d => d.X == j && d.Y == i) ? "#" : ".")}");
                }
                Console.WriteLine();
            }
        }

        private static (List<Point> Dots, List<(string axe, int coordinate)> FoldInstructions) GetProcessedInput(IEnumerable<string> input)
        {
            var dots = input.Where(line => !line.StartsWith("fold") && !string.IsNullOrWhiteSpace(line)).Select(l => new Point(int.Parse(l.Split(",")[0]), int.Parse(l.Split(",")[1]))).ToList();
            var foldInstructions = input.Where(line => line.StartsWith("fold")).Select(line => new string(line.Skip(11).ToArray())).Select(l => (l.Split("=")[0], int.Parse(l.Split("=")[1]))).ToList();

            return (dots, foldInstructions);
        }

        private static List<Point> Fold(string axe, int coordinate, List<Point> points) => axe switch
        {
            "x" => HorizontalFold(coordinate, points),
            "y" => VerticalFold(coordinate, points)
        };

        private static List<Point> VerticalFold(int coordinate, List<Point> points) => points.Where(p => p.Y < coordinate).Union(points.Where(p => p.Y > coordinate).Select(p => new Point(p.X, p.Y - (p.Y - coordinate) * 2))).ToList();

        private static List<Point> HorizontalFold(int coordinate, List<Point> points) => points.Where(p => p.X < coordinate).Union(points.Where(p => p.X > coordinate).Select(p => new Point(p.X - (p.X - coordinate) * 2, p.Y))).ToList();
    }
}
