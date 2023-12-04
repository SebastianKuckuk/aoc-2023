using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day_04 {
    internal abstract class Program {
        public static void Main(string[] args) {
            var input = File.ReadAllLines(@"../../input.txt");

            var regExGame = new Regex(@"Card +(\d+):((?: +\d+)+) +\|((?: +\d+)+)");
            var regExNumbers = new Regex(@" +(\d+)");
            var games = input.Select(l => regExGame.Match(l).Groups);

            var numWinning = games.Select(game => {
                var winningNumbers = regExNumbers.Matches(game[2].Value).Cast<Match>().Select(m => int.Parse(m.Groups[1].Value)).ToArray();
                var myNumbers = regExNumbers.Matches(game[3].Value).Cast<Match>().Select(m => int.Parse(m.Groups[1].Value)).ToArray();
                
                return myNumbers.Count(n => winningNumbers.Contains(n));
            }).ToArray();
            
            var accPartOne = numWinning.Select(nw =>  0 == nw ? 0 : Math.Pow(2, nw - 1)).Sum();

            var numCards = new int[input.Length];

            for (var i = 0; i < input.Length; ++i) {
                numCards[i] += 1; // honor original card
                for (var j = i + 1; j <= i + numWinning[i] && j < input.Length; ++j)
                    numCards[j] += numCards[i];
            }

            var accPartTwo = numCards.Sum();
            
            Console.WriteLine($"Part one: {accPartOne}");
            Console.WriteLine($"Part two: {accPartTwo}");
        }
    }
}
