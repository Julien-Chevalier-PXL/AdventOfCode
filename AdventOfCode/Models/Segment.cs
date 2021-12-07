namespace AdventOfCode.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class Segment
    {
        public Point FirstPoint { get; set; }
        public Point SecondPoint { get; set; }

        public SegmentType Type => this.FirstPoint.X == this.SecondPoint.X ? SegmentType.Vertical : this.FirstPoint.Y == this.SecondPoint.Y ? SegmentType.Horizontal : SegmentType.Diagonal;

        public Point StartPoint => this.SecondPoint.X < this.FirstPoint.X || this.FirstPoint.X == this.SecondPoint.X && this.SecondPoint.Y < this.FirstPoint.Y ? this.SecondPoint : this.FirstPoint;
        public Point EndPoint => this.FirstPoint.X > this.SecondPoint.X || this.FirstPoint.X == this.SecondPoint.X && this.FirstPoint.Y > this.SecondPoint.Y ? this.FirstPoint : this.SecondPoint;

        public IEnumerable<Point> CoveredPoints()
        {
            var coveredPoints = new List<Point>();
            int realStart;
            int realEnd;
            switch (this.Type)
            {
                case SegmentType.Horizontal:
                    realStart = Math.Min(this.FirstPoint.X, this.SecondPoint.X);
                    realEnd = Math.Max(this.FirstPoint.X, this.SecondPoint.X);
                    for (var i = realStart; i <= realEnd; i++)
                    {
                        coveredPoints.Add(new Point(i, this.FirstPoint.Y));
                    }
                    break;
                case SegmentType.Vertical:
                    realStart = Math.Min(this.FirstPoint.Y, this.SecondPoint.Y);
                    realEnd = Math.Max(this.FirstPoint.Y, this.SecondPoint.Y);
                    for (var i = realStart; i <= realEnd; i++)
                    {
                        coveredPoints.Add(new Point(this.FirstPoint.X, i));
                    }
                    break;
                case SegmentType.Diagonal:
                    var xDiff = this.EndPoint.X - this.StartPoint.X;
                    var angle = (this.EndPoint.Y - this.StartPoint.Y) / Math.Abs(this.EndPoint.Y - this.StartPoint.Y);

                    for (var i = 0; i <= xDiff; i++)
                    {
                        coveredPoints.Add(new Point(this.StartPoint.X + i, this.StartPoint.Y + i * angle));
                    }

                    break;
            }

            return coveredPoints;
        }
    }

    public enum SegmentType
    {
        Horizontal,
        Vertical,
        Diagonal
    }
}
