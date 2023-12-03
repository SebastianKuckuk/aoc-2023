using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_03 {
    internal abstract class Program {
        public static void Main(string[] args) {
            var input = File.ReadAllLines(@"../../input.txt").ToArray().Select(l => l.ToCharArray()).ToArray();

            var nRow = input.Length;
            var nCol = input[0].Length;

            var accPartOne = 0;

            var gearCons = new Dictionary<Tuple<int, int>, List<int>>();

            for (var rowIdx = 0; rowIdx < nRow; ++rowIdx) {
                var number = "";
                var connected = false;
                var gearCon = new SortedSet<Tuple<int, int>>();

                for (var colIdx = 0; colIdx < nCol; ++colIdx) {
                    var c = input[rowIdx][colIdx];

                    // in number
                    if (char.IsDigit(c)) {
                        number += c;

                        for (var neighRow = Math.Max(0, rowIdx - 1); neighRow <= Math.Min(nRow - 1, rowIdx + 1); ++neighRow) {
                            for (var neighCol = Math.Max(0, colIdx - 1); neighCol <= Math.Min(nCol - 1, colIdx + 1); ++neighCol) {
                                var neigh = input[neighRow][neighCol];
                                if (!char.IsDigit(neigh) && '.' != neigh)
                                    connected = true;

                                // part two
                                if ('*' == neigh)
                                    gearCon.Add(new Tuple<int, int>(neighRow, neighCol));
                            }
                        }
                    }

                    // check if number specification has just ended
                    if ((!char.IsDigit(c) || nCol - 1 == colIdx) && "" != number) {
                        if (connected) {
                            accPartOne += int.Parse(number);

                            // part two
                            foreach (var gear in gearCon) {
                                if (!gearCons.ContainsKey(gear))
                                    gearCons[gear] = new List<int>();
                                gearCons[gear].Add(int.Parse(number));
                            }
                        }

                        // reset counters
                        number = "";
                        connected = false;
                        gearCon = new SortedSet<Tuple<int, int>>();
                    }
                }
            }

            // part two
            var accPartTwo = gearCons.Where(e => 2 == e.Value.Count).Select(e => e.Value[0] * e.Value[1]).Sum();

            Console.WriteLine($"Part one: {accPartOne}");
            Console.WriteLine($"Part two: {accPartTwo}");
        }
    }
}
