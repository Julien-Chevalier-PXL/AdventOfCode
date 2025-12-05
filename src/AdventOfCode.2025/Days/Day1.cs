namespace AdventOfCode._2025.Days;

internal static class Day1
{
    private const char Left = 'L';
    private const char Right = 'R';

    private const int DefaultDialToCount = 0;

    public static int GetPassword(string[] lines, int dialToCount = DefaultDialToCount)
    {
        var dialToCountCounter = 0;
        var safe = new Safe();

        Console.WriteLine($"Starting position: {safe.CurrentPosition}");

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);
            _ = direction switch
            {
                Left => safe.MoveLeft(distance),
                Right => safe.MoveRight(distance),
                _ => throw new InvalidDataException("Unexpected direction"),
            };

            if (safe.CurrentPosition == dialToCount)
                dialToCountCounter++;
        }

        return dialToCountCounter;
    }

    public static int GetPasswordWithMethod0x434C49434B(string[] lines, int dialToCount = DefaultDialToCount)
    {
        var dialToCountCounter = 0;
        var safe = new Safe(dialToCount: dialToCount);

        Console.WriteLine($"Starting position: {safe.CurrentPosition}");

        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);

            dialToCountCounter += direction switch
            {
                Left => safe.MoveLeft(distance),
                Right => safe.MoveRight(distance),
                _ => throw new InvalidDataException("Unexpected direction"),
            };

            if (safe.CurrentPosition < safe.MinPosition || safe.CurrentPosition > safe.MaxPosition)
                throw new Exception("Problème d'algo");

            //Console.WriteLine($"{line} -> {safe.CurrentPosition} - {dialToCountCounter}");
        }

        return dialToCountCounter;
    }

    internal class Safe
    {
        private const int DefaultStartPosition = 50;
        private const int DefaultMinPosition = 0;
        private const int DefaultMaxPosition = 99;

        public int CurrentPosition { get; private set; } = DefaultStartPosition;
        public int DialToCount { get; private init; } = DefaultDialToCount;
        public int MinPosition { get; private init; } = DefaultMinPosition;
        public int MaxPosition { get; private init; } = DefaultMaxPosition;
        private int PositionNumbers => this.MaxPosition - this.MinPosition + 1;

        public Safe() { }

        public Safe(int startPosition = DefaultStartPosition, int minPosition = DefaultMinPosition, int maxPosition = DefaultMaxPosition, int dialToCount = DefaultDialToCount)
        {
            if (startPosition < minPosition || startPosition > maxPosition)
                throw new InvalidDataException($"Start position {startPosition} is not in available positions range {minPosition} - {maxPosition}");

            this.MinPosition = minPosition;
            this.MaxPosition = maxPosition;
            this.CurrentPosition = startPosition;
            this.DialToCount = dialToCount;
        }

        public int MoveLeft(int distance)
        {
            var counter = distance / this.PositionNumbers;
            var moduloDistance = distance % this.PositionNumbers;

            var tempNewPosition = CurrentPosition - moduloDistance;
            if (tempNewPosition < this.MinPosition)
            {
                if (this.CurrentPosition != this.DialToCount)
                    counter++;
                this.CurrentPosition = tempNewPosition + this.PositionNumbers;
            }
            else
            {
                this.CurrentPosition = tempNewPosition;
                if (this.CurrentPosition == this.DialToCount)
                    counter++;
            }

            return counter;
        }

        public int MoveRight(int distance)
        {
            var counter = distance / this.PositionNumbers;
            var moduloDistance = distance % this.PositionNumbers;

            var tempNewPosition = CurrentPosition + moduloDistance;
            if (tempNewPosition > this.MaxPosition)
            {
                this.CurrentPosition = tempNewPosition - this.PositionNumbers;
                counter++;
            }
            else
            {
                this.CurrentPosition = tempNewPosition;
            }

            return counter;
        }
    }
}
