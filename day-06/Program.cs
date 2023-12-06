using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_06 {
    internal abstract class Program {
        public static void Main(string[] args) {
            var input = File.ReadAllLines(@"../../input.txt");

            var regExNumbers = new Regex(@" +(\d+)");

            var times = regExNumbers.Matches(input[0]).Cast<Match>().Select(m => int.Parse(m.Groups[1].Value)).ToArray();
            var distances = regExNumbers.Matches(input[1]).Cast<Match>().Select(m => int.Parse(m.Groups[1].Value)).ToArray();

            long numPossibilities(long maxTime, long maxDist) {
                // var acc = 0;
                // for (var t = 0; t < maxTime; ++t) {
                //     if (-t * t + maxTime * t - maxDist > 0)
                //         ++acc;
                // }
                
                double a = -1;
                double b = maxTime;
                double c = -maxDist;

                var left = (-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                var right = (-b - Math.Sqrt(b * b - 4 * a * c)) / (2 * a);
                
                return (long)(Math.Ceiling(right) - Math.Floor(left) - 1);
            }

            var accPartOne = times.Zip(distances, Tuple.Create).Aggregate(1, (current, td) => (int)(current * numPossibilities(td.Item1, td.Item2)));

            var accPartTwo = numPossibilities(
                    long.Parse(regExNumbers.Matches(input[0]).Cast<Match>().Select(m => m.Groups[1].Value).Aggregate((a, b) => a + b)),
                    long.Parse(regExNumbers.Matches(input[1]).Cast<Match>().Select(m => m.Groups[1].Value).Aggregate((a, b) => a + b)));
            
            Console.WriteLine($"Part one: {accPartOne}");
            Console.WriteLine($"Part two: {accPartTwo}");
        }
    }
}
