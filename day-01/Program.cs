using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_01 {
    internal abstract class Program {
        public static void Main(string[] args) {
            var input = File.ReadAllLines(@"../../input.txt");

            Func<string, double> digitMapping = l => {
                var digits = l.ToCharArray().Where(char.IsDigit).ToArray();
                return 10 * char.GetNumericValue(digits.First()) + char.GetNumericValue(digits.Last());
            };

            // part one
            var result = input.Select(digitMapping).Sum();

            Console.WriteLine($"Part one: {result}");

            // part two
            var dict = new Dictionary<string, string> {
                { "one", "o1e" },
                { "two", "t2o" },
                { "three", "t3e" },
                { "four", "f4r" },
                { "five", "f5e" },
                { "six", "s6x" },
                { "seven", "s7n" },
                { "eight", "e8t" },
                { "nine", "n9e" }
            };

            input = input.Select(l =>
                dict.Aggregate(l, (current, kvp) => current.Replace(kvp.Key, kvp.Value))
            ).ToArray();

            result = input.Select(digitMapping).Sum();

            Console.WriteLine($"Part two: {result}");
        }
    }
}
