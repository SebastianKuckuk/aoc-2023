using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_05 {
    internal abstract class Program {
        public static void Main(string[] args) {
            var input = new Stack<string>( File.ReadAllLines(@"../../input.txt").Reverse());

            long[] seeds = { };
            var maps = new List<long[]>[7];

            var mapIt = 0;
            while (input.Count > 0) {
                var line = input.Pop();
                
                if (line.StartsWith("seeds:"))
                    seeds = line.Split(':')[1].Split(' ').Skip(1).Select(long.Parse).ToArray();
                else if (line.EndsWith("map:")) {
                    maps[mapIt] = new List<long[]>();
                    while (input.Count > 0) {
                        line = input.Pop();

                        if (0 == line.Length)
                            break;

                        maps[mapIt].Add(line.Split(' ').Select(long.Parse).ToArray());
                    }

                    ++mapIt;
                }
            }

            long MapSeed(long seed) {
                var toMap = seed;
                foreach (var map in maps) {
                    // var next = map.FindAll(m => m[1] <= toMap && toMap < m[1] + m[2]).Select(m => m[0] + toMap - m[1]);
                    // toMap = next.Count() > 0 ? next.First() : toMap;

                    foreach (var m in map) {
                        if (m[1] <= toMap && toMap < m[1] + m[2]) {
                            toMap = m[0] + toMap - m[1];
                            break;
                        }
                    }
                }

                return toMap;
            }

            var solPartOne = seeds.Select(MapSeed).Min();

            var solPartTwo = long.MaxValue;
            for (var seedPair = 0; seedPair < seeds.Length; seedPair += 2) 
                for (var seed = seeds[seedPair]; seed < seeds[seedPair] + seeds[seedPair + 1]; ++seed) 
                    solPartTwo = Math.Min(solPartTwo, MapSeed(seed));

            Console.WriteLine($"Part one: {solPartOne}");
            Console.WriteLine($"Part two: {solPartTwo}");
        }
    }
}
