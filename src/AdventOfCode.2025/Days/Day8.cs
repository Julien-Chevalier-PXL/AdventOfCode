namespace AdventOfCode._2025.Days;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

internal static class Day8
{
    public static int Part1(string[] lines)
    {
        var junctionBoxes = GetJunctionBoxes(lines);
        var distances = GetDistances(junctionBoxes);

        List<Circuit> circuits = [.. junctionBoxes.Select(jb => new Circuit([jb]))];
        var topDistances = distances.Distinct(JunctionBoxDistanceComparer.Instance).OrderBy(d => d.Distance).Take(1000).ToArray();
        foreach (var distance in topDistances)
        {
            var circuitOfJunctionBox = circuits.Single(c => c.Contains(distance.JunctionBox));
            var circuitOfOtherJunctionBox = circuits.Single(c => c.Contains(distance.OtherJunctionBox));

            if (circuitOfJunctionBox == circuitOfOtherJunctionBox)
                continue;

            circuits.Add(new([.. distance.JunctionBoxCollection, .. circuitOfJunctionBox, .. circuitOfOtherJunctionBox]));
            circuits.Remove(circuitOfJunctionBox);
            circuits.Remove(circuitOfOtherJunctionBox);
        }

        return GetResult(circuits);
    }

    public static int Part2(string[] lines)
    {
        var junctionBoxes = GetJunctionBoxes(lines);
        var distances = GetDistances(junctionBoxes);

        List<Circuit> circuits = [.. junctionBoxes.Select(jb => new Circuit([jb]))];
        var orderedDistances = distances.Distinct(JunctionBoxDistanceComparer.Instance).OrderBy(d => d.Distance).ToArray();
        foreach (var distance in orderedDistances)
        {
            var circuitOfJunctionBox = circuits.Single(c => c.Contains(distance.JunctionBox));
            var circuitOfOtherJunctionBox = circuits.Single(c => c.Contains(distance.OtherJunctionBox));

            if (circuitOfJunctionBox == circuitOfOtherJunctionBox)
                continue;

            circuits.Add(new([.. distance.JunctionBoxCollection, .. circuitOfJunctionBox, .. circuitOfOtherJunctionBox]));
            circuits.Remove(circuitOfJunctionBox);
            circuits.Remove(circuitOfOtherJunctionBox);

            if (circuits.Count == 1)
                return distance.JunctionBox.X * distance.OtherJunctionBox.X;
        }

        throw new Exception("There is a problem in the algorithm, iterated all distances but did not complete one large circuit");
    }

    private static List<JunctionBoxDistance> GetDistances(HashSet<JunctionBox> junctionBoxes)
    {
        List<JunctionBoxDistance> distances = [];
        foreach (var currentJb in junctionBoxes)
        {
            distances.AddRange(junctionBoxes.Except([currentJb]).Select(jb => new JunctionBoxDistance(currentJb, jb, GetEucledianDistance(currentJb, jb))));
        }

        return distances;
    }

    private static int GetResult(List<Circuit> circuits) 
        => circuits.OrderByDescending(c => c.Count).Take(3).Aggregate(1, (result, c) => { checked { return result *= c.Count; } });

    private static HashSet<JunctionBox> GetJunctionBoxes(string[] lines)
        => new(
            lines.Select(l =>
            {
                var points = l.Split(',').Select(x => int.Parse(x)).ToArray();
                return new JunctionBox(points[0], points[1], points[2]);
            }),
            JunctionBoxComparer.Instance);

    private static double GetEucledianDistance(JunctionBox p1, JunctionBox p2)
        => Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2) + Math.Pow(p1.Z - p2.Z, 2));

    private sealed class Circuit : HashSet<JunctionBox>
    {
        public Circuit() : base(JunctionBoxComparer.Instance)
        {
        }

        public Circuit(IEnumerable<JunctionBox> circuit) : base(circuit, JunctionBoxComparer.Instance)
        {
        }
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    private sealed class JunctionBox : IEquatable<JunctionBox>
    {
        public JunctionBox(int x, int y, int z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public int X { get; init; }
        public int Y { get; init; }
        public int Z { get; init; }

        public override string ToString()
            => $"{{ X = {X}, Y = {Y}, Z = {Z} }}";

        public bool Equals(JunctionBox? other)
        {
            if (other is null)
                return false;

            return this.X == other.X
                && this.Y == other.Y
                && this.Z == other.Z;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.X, this.Y, this.Z);
        }

        private string GetDebuggerDisplay()
            => this.ToString();
    }

    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    private sealed record JunctionBoxDistance(JunctionBox JunctionBox, JunctionBox OtherJunctionBox, double Distance) : IEquatable<JunctionBoxDistance>
    {
        public HashSet<JunctionBox> JunctionBoxCollection => new(comparer: JunctionBoxComparer.Instance) { this.JunctionBox, this.OtherJunctionBox };

        private string GetDebuggerDisplay()
            => this.ToString();

        public override string ToString()
        {
            return $"{{ JunctionBox = {JunctionBox}, OtherJunctionBox = {OtherJunctionBox}, Distance = {Distance} }}";
        }

        public bool Equals(JunctionBoxDistance? other)
        {
            if (other is null)
                return false;

            return (this.JunctionBox.Equals(other.JunctionBox) && this.OtherJunctionBox.Equals(other.OtherJunctionBox))
                || (this.JunctionBox.Equals(other.OtherJunctionBox) && this.OtherJunctionBox.Equals(other.JunctionBox));
        }

        public override int GetHashCode()
        {
            return this.JunctionBox.GetHashCode() + this.OtherJunctionBox.GetHashCode();
        }
    }

    private sealed class JunctionBoxComparer : IEqualityComparer<JunctionBox>
    {
        public static JunctionBoxComparer Instance = new();

        public bool Equals(JunctionBox? x, JunctionBox? y)
        {
            if (x is null || y is null)
                return false;

            return x.Equals(y);
        }

        public int GetHashCode([DisallowNull] JunctionBox obj)
            => obj.GetHashCode();
    }

    private sealed class JunctionBoxDistanceComparer : IEqualityComparer<JunctionBoxDistance>
    {
        public static JunctionBoxDistanceComparer Instance = new();

        public bool Equals(JunctionBoxDistance? x, JunctionBoxDistance? y)
        {
            if (x is null || y is null)
                return false;

            return (x.JunctionBox.Equals(y.JunctionBox) && x.OtherJunctionBox.Equals(y.OtherJunctionBox))
                || (x.JunctionBox.Equals(y.OtherJunctionBox) && x.OtherJunctionBox.Equals(y.JunctionBox));
        }

        public int GetHashCode([DisallowNull] JunctionBoxDistance obj)
            => obj.GetHashCode();
    }
}
