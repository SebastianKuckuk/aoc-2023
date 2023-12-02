using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_02 {
    internal abstract class Program {
        public static void Main(string[] args) {
            var input = File.ReadAllLines(@"../../input.txt");

            var regExGame = new Regex(@"Game (\d+):");
            var regExSet = new Regex(@"(\d+) (blue|green|red)");

            var accPartOne = 0;
            var accPartTwo = 0;

            foreach (var line in input) {
                var gameId = int.Parse(regExGame.Match(line).Groups[1].Value);

                var cubeMaxCnt = new Dictionary<string,int> {
                    { "red", 0 },
                    { "green", 0 },
                    { "blue", 0 }
                };

                foreach (var draw in line.Split(':')[1].Split(';').Select(d => d.Trim())) {
                    foreach (var set in draw.Split(',').Select(d => d.Trim())) {
                        var cnt = int.Parse(regExSet.Match(set).Groups[1].Value);
                        var color = regExSet.Match(set).Groups[2].Value;

                        cubeMaxCnt[color] = Math.Max(cubeMaxCnt[color], cnt);
                    }
                }

                if (cubeMaxCnt["red"] <= 12 && cubeMaxCnt["green"] <= 13 && cubeMaxCnt["blue"] <= 14) 
                    accPartOne += gameId;

                accPartTwo += cubeMaxCnt.Values.Aggregate((left, right) => left * right);
            }

            Console.WriteLine(accPartOne);
            Console.WriteLine(accPartTwo);
        }
    }
}
