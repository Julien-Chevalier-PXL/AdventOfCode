namespace AdventOfCode
{
    using AdventOfCode.Helpers;

    public static class Day2
    {
        const string FORWARD_COMMAND = "forward";
        const string DOWN_COMMAND = "down";
        const string UP_COMMAND = "up";

        public static int GetPositionResult()
        {
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            var inputs = InputHelper.GetInput(nameof(Day2));
            //var inputs = InputHelper.GetSmallInput(nameof(Day2));

            foreach (var input in inputs)
            {
                var splittedInput = input.Split();
                var command = splittedInput[0];
                var power = int.Parse(splittedInput[1]);

                switch (command)
                {
                    case FORWARD_COMMAND:
                        horizontalPosition += power;
                        depth += aim * power;
                        break;
                    case DOWN_COMMAND:
                        aim += power;
                        break;
                    case UP_COMMAND:
                        aim -= power;
                        break;
                };
            }

            return horizontalPosition * depth;
        }
    }
}
