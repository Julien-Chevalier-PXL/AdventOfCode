namespace AdventOfCode.Helpers
{
    using System.Collections.Generic;
    using System.IO;

    public static class InputHelper
    {
        public const string INPUT_FOLDER_LOCATION = @"Inputs";
        public const string INPUT_FILE = "{0}.txt";
        public const string SMALL_INPUT_FILE = "{0}Small.txt";
        public const string TEST_INPUT_FILE = "{0}Test.txt";

        public static IEnumerable<string> GetInput(string day) => File.ReadAllLines(Path.Combine(INPUT_FOLDER_LOCATION, string.Format(INPUT_FILE, day)));
        public static IEnumerable<string> GetSmallInput(string day) => File.ReadAllLines(Path.Combine(INPUT_FOLDER_LOCATION, string.Format(SMALL_INPUT_FILE, day)));
        public static IEnumerable<string> GetTestInput(string day) => File.ReadAllLines(Path.Combine(INPUT_FOLDER_LOCATION, string.Format(TEST_INPUT_FILE, day)));
    }
}
