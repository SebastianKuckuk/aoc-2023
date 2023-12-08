using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_08 {
    internal abstract class Program {
        private static long GreatersCommonFactor(long a, long b) {
            while (b != 0) {
                var temp = b;
                b = a % b;
                a = temp;
            }

            return a;
        }

        private static long LeastCommonMultiple(long a, long b) { return a / GreatersCommonFactor(a, b) * b; }

        public static void Main(string[] args) {
            var input = File.ReadAllLines(@"../../input.txt");

            var turns = input[0].ToCharArray().Select(c => 'L' == c ? 0 : 1).ToArray();
            var nodes = new SortedDictionary<string, string[]>();
            foreach (var line in input.Skip(2)) {
                var groups = new Regex(@"(\w+) = \((\w+), (\w+)\)").Match(line).Groups.Cast<Group>().Skip(1).Select(m => m.Value).ToArray();
                nodes[groups[0]] = groups.Skip(1).ToArray();
            }

            var curNode = "AAA";
            var step = 0L;
            for (; "ZZZ" != curNode; ++step) 
                curNode = nodes[curNode][turns[step % turns.Length]];

            Console.WriteLine($"Part one: {step}");

            var ghostNodes = nodes.Keys.Where(n => n.EndsWith("A")).ToArray();
            var ghostLoops = new long[ghostNodes.Length];

            // determine loop length
            for (var ghost = 0; ghost < ghostNodes.Length; ++ghost) {
                for (step = 0; !ghostNodes[ghost].EndsWith("Z"); ++step)
                    ghostNodes[ghost] = nodes[ghostNodes[ghost]][turns[step % turns.Length]];
                ghostLoops[ghost] = step;
            }

            // aggregate
            var accPartTwo = ghostLoops.Aggregate(LeastCommonMultiple);

            Console.WriteLine($"Part two: {accPartTwo}");
        }
    }
}
