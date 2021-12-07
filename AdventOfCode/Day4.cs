namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day4
    {
        public const int BINGO_BOARD_SIZE = 5;

        public static int GetFinalScore()
        {
            var input = InputHelper.GetInput(nameof(Day4));
            //var input = InputHelper.GetSmallInput(nameof(Day4));

            var draws = GetDraw(input);
            var boards = GetBoards(input);

            var remainingBoards = new List<List<List<int>>>(boards);

            var currentDraws = new List<int>();
            foreach (var draw in draws)
            {
                currentDraws.Add(draw);

                if (remainingBoards.Count > 1)
                {
                    remainingBoards = remainingBoards.Where(board => !IsBoardWinning(currentDraws, board)).ToList();
                }
                else
                {
                    if (IsBoardWinning(currentDraws, remainingBoards.First()))
                        return GetSumUnmarkedNumbers(currentDraws, remainingBoards.First()) * draw;
                }
            }

            return 0;
        }

        private static int GetSumUnmarkedNumbers(List<int> draws, List<List<int>> board)
        {
            return board.SelectMany(x => x).Except(draws).Sum();
        }

        private static bool IsBoardWinning(List<int> draws, List<List<int>> board)
        {
            for (var i = 0; i < BINGO_BOARD_SIZE; i++)
            {
                if (!board.ElementAt(i).Except(draws).Any() || !board.Select(x => x.ElementAt(i)).Except(draws).Any())
                {
                    return true;
                }
            }

            return false;
        }

        #region Game setup
        private static IEnumerable<int> GetDraw(IEnumerable<string> input)
        {
            return input.First().Split(",").Select(x => int.Parse(x));
        }

        private static List<List<List<int>>> GetBoards(IEnumerable<string> input)
        {
            var boards = new List<List<List<int>>>();
            var currentBoard = new List<List<int>>();

            foreach (var line in input.Skip(2))
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    currentBoard.Add(line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList());
                }
                else
                {
                    boards.Add(new List<List<int>>(currentBoard));
                    currentBoard.Clear();
                }
            }

            boards.Add(new List<List<int>>(currentBoard));

            return boards;
        }

        private static void PrintBoards(List<List<List<int>>> boards)
        {
            foreach (var board in boards)
            {
                Console.WriteLine();

                foreach (var row in board)
                {
                    Console.WriteLine(string.Join(" ", row.Select(x => x.ToString().PadLeft(2))));
                }

                Console.WriteLine();
            }
        }
        #endregion
    }
}
