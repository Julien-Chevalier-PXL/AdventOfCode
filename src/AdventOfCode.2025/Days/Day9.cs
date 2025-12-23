using System.Drawing;

namespace AdventOfCode._2025.Days;

internal static class Day9
{
    public static long Part1(string[] lines)
    {
        Point[] points = [.. lines.Select(line => line.Split(',')).Select(parts => new Point(int.Parse(parts[0]), int.Parse(parts[1])))];
        List<(Point P1, Point P2, double Distance)> rectangles = [];
        //var rectangles = points.SelectMany(p => points.Except([p]).Select(p2 => (P1: p, P2: p2, Area: GetArea(p, p2)))).ToArray();
        for(var i = 0; i < points.Length; i++)
        {
            for(var j = i + 1; j < points.Length; j++)
            {
                rectangles.Add((P1: points[i], P2: points[j], Distance: GetEucledianDistance(points[i], points[j])));
            }
        }

        //Console.WriteLine(points.MinBy(p => p.X));
        //Console.WriteLine(points.MaxBy(p => p.X));
        //Console.WriteLine(points.MinBy(p => p.Y));
        //Console.WriteLine(points.MaxBy(p => p.Y));

        var biggestDistance = rectangles.MaxBy(x => x.Distance);
        return GetArea(biggestDistance.P1, biggestDistance.P2);
    }

    public static int Part2(string[] lines)
    {
        return default;
    }

    private static double GetEucledianDistance(Point p1, Point p2)
        => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));

    private static long GetArea(Point p1, Point p2)
        => (Math.Abs(p1.X - p2.X) + 1) * (Math.Abs(p1.Y - p2.Y) + 1);
}
