namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;

    using AdventOfCode.Helpers;
    using AdventOfCode.Models;

    public static class Day5
    {
        #region Part2
        public static int GetNumberOfDangerousPoints()
        {
            var input = InputHelper.GetInput(nameof(Day5));
            //var input = InputHelper.GetSmallInput(nameof(Day5));
            //var input = InputHelper.GetTestInput(nameof(Day5));

            var allSegments = GetLines(input);

            var coveredPoints = GetCoveredPoints(allSegments);
            //PrintCoveredPointDiagram(coveredPoints);

            return coveredPoints.GroupBy(point => point).Count(pointGroup => pointGroup.Count() >= 2);
        }
        #endregion

        #region Part1
        public static int GetNumberOfDangerousPointsPart1()
        {
            var input = InputHelper.GetInput(nameof(Day5));
            //var input = InputHelper.GetSmallInput(nameof(Day5));
            //var input = InputHelper.GetTestInput(nameof(Day5));

            var allSegments = GetLines(input);

            var filteredSegments = FilterHorizontalAndVerticalLines(allSegments);

            var coveredPoints = GetCoveredPoints(filteredSegments);

            PrintCoveredPointDiagram(coveredPoints);

            return coveredPoints.GroupBy(point => point).Count(pointGroup => pointGroup.Count() >= 2);
        }

        public static IEnumerable<Segment> FilterHorizontalAndVerticalLines(IEnumerable<Segment> allSegments) => allSegments.Where(segment => segment.Type != SegmentType.Diagonal);
        #endregion

        private static void PrintCoveredPointDiagram(List<Point> coveredPoints)
        {
            for (var i = 0; i < 10; i++)
            {
                for (var j = 0; j < 10; j++)
                {
                    var danger = coveredPoints.Count(point => point.X == j && point.Y == i);
                    Console.Write($"{(danger == 0 ? "." : danger)}");
                }
                Console.WriteLine();
            }
        }

        private static List<Point> GetCoveredPoints(IEnumerable<Segment> filteredSegments)
        {
            var coveredPoints = new List<Point>();
            foreach (var segment in filteredSegments)
            {
                coveredPoints.AddRange(segment.CoveredPoints());
            }

            return coveredPoints;
        }

        private static IEnumerable<Segment> GetLines(IEnumerable<string> input)
        {
            var segments = new List<Segment>();

            foreach (var i in input)
            {
                var segmentPoints = i.Split("->", StringSplitOptions.TrimEntries);
                var startPoint = segmentPoints[0].Split(",");
                var endPoint = segmentPoints[1].Split(",");
                segments.Add(new Segment
                {
                    FirstPoint = new Point(int.Parse(startPoint[0]), int.Parse(startPoint[1])),
                    SecondPoint = new Point(int.Parse(endPoint[0]), int.Parse(endPoint[1]))
                });
            }

            return segments;
        }
    }
}
