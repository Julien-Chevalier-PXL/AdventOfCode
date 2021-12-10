namespace AdventOfCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AdventOfCode.Helpers;

    public static class Day10
    {
        private static readonly char[] OPEN_CHUNK_CHARS = new char[] { '(', '[', '{', '<' };
        private static readonly char[] CLOSE_CHUNK_CHARS = new char[] { ')', ']', '}', '>' };

        #region Part1
        public static int GetTotalSyntaxErrorScore()
        {
            var input = InputHelper.GetInput(nameof(Day10));
            //var input = InputHelper.GetSmallInput(nameof(Day10));

            return GetAllFirstIllegalCharacters(input).Sum(x => GetSyntaxPoints(x));
        }
        #endregion

        #region Part2
        public static long GetTotalAutocompletionScore()
        {
            var input = InputHelper.GetInput(nameof(Day10));
            //var input = InputHelper.GetSmallInput(nameof(Day10));

            var incompleteLines = FilterCorruptedLines(input);

            var scores = GetAutocompletionScores(incompleteLines);

            return scores.OrderBy(t => t).ElementAt(scores.Count / 2);
        }
        #endregion

        private static List<string> FilterCorruptedLines(IEnumerable<string> lines) => lines.Where(l => GetFirstIllegalCharacter(l) == default).ToList();

        private static List<long> GetAutocompletionScores(IEnumerable<string> lines) => lines.Select(l => GetAutocompletionScore(GetIncompleteSequences(l))).ToList();

        private static List<char> GetAllFirstIllegalCharacters(IEnumerable<string> lines) => lines.Select(l => GetFirstIllegalCharacter(l)).Where(c => c != default).ToList();

        private static char GetFirstIllegalCharacter(string line)
        {
            var openChunks = new Stack<char>();

            foreach (var c in line)
            {
                if (OPEN_CHUNK_CHARS.Contains(c))
                {
                    openChunks.Push(c);
                }

                if (CLOSE_CHUNK_CHARS.Contains(c) && openChunks.Pop() != OPEN_CHUNK_CHARS[GetIndexOfChunkType(c)])
                {
                    return c;
                }
            }

            return default;
        }

        private static Stack<char> GetIncompleteSequences(string line)
        {
            var openChunks = new Stack<char>();

            foreach (var c in line)
            {
                if (OPEN_CHUNK_CHARS.Contains(c))
                {
                    openChunks.Push(c);
                }

                if (CLOSE_CHUNK_CHARS.Contains(c))
                {
                    openChunks.Pop();
                }
            }

            return openChunks;
        }

        public static long GetAutocompletionScore(Stack<char> incompleteSequences) => incompleteSequences.Aggregate<char, long>(0, (x, y) => x * 5 + GetAutocompletePoints(y));

        #region Helpers
        private static int GetIndexOfChunkType(char closingChunkChar) => closingChunkChar switch
        {
            ')' => 0,
            ']' => 1,
            '}' => 2,
            '>' => 3
        };

        private static int GetSyntaxPoints(char illegalChar) => illegalChar switch
        {
            ')' => 3,
            ']' => 57,
            '}' => 1197,
            '>' => 25137
        };

        private static int GetAutocompletePoints(char chunkType) => chunkType switch
        {
            '(' => 1,
            '[' => 2,
            '{' => 3,
            '<' => 4
        };
        #endregion
    }
}
