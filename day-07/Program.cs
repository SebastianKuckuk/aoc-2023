using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day_07 {
    internal struct Hand : IComparable<Hand> {
        public string Cards;
        public string CardsForSort;
        public int Bet;
        public int Value;

        public Hand(string cards, int bet) {
            Cards = cards;
            CardsForSort = Cards.Replace('T', (char)('9' + 1))
                .Replace('J', (char)('9' + 2))
                .Replace('Q', (char)('9' + 3))
                .Replace('K', (char)('9' + 4))
                .Replace('A', (char)('9' + 5));
            Bet = bet;
            Value = DetermineValue(CardsForSort.ToCharArray());
        }

        public void UpdateForJokers() {
            CardsForSort = CardsForSort.Replace((char)('9' + 2), '0');
            Value = DetermineValue(CardsForSort.ToCharArray());
        }

        private static int DetermineValue(char[] cards) { // no struct member accesses in lambdas, so pass as argument :/
            var dict = new Dictionary<char, int>();
            var numJoker = 0;
            foreach (var card in cards) {
                if ('0' == card) { // joker
                    numJoker++;
                    continue;
                }

                if (!dict.ContainsKey(card))
                    dict[card] = 0;
                dict[card]++;
            }

            if (dict.Values.Any(v => 5 == v + numJoker) || 5 == numJoker)
                return 6;
            if (dict.Values.Any(v => 4 == v + numJoker))
                return 5;
            if (dict.Values.Any(v => 3 == v) && dict.Values.Any(v => 2 == v))
                return 4;
            if (1 == numJoker && 2 == dict.Values.Count(v => 2 == v))
                return 4;
            if (dict.Values.Any(v => 3 == v + numJoker))
                return 3;
            if (2 - numJoker == dict.Values.Count(v => 2 == v))
                return 2;
            if (dict.Values.Any(v => 2 == v + numJoker))
                return 1;
            return 0;
        }

        public int CompareTo(Hand other) {
            if (other.Value != Value)
                return other.Value - Value;
            return string.Compare(other.CardsForSort, CardsForSort, StringComparison.Ordinal);
        }
    }

    internal abstract class Program {
        public static void Main(string[] args) {
            var input = File.ReadAllLines(@"../../input.txt");

            var hands = input.Select(l => {
                var tuple = l.Split(' ');
                return new Hand(tuple[0], int.Parse(tuple[1]));
            }).OrderByDescending(h => h).ToArray();

            var accPartOne = 0L;
            for (var i = 0; i < hands.Length; ++i)
                accPartOne += (i + 1) * hands[i].Bet;

            for (var i = 0; i < hands.Length; ++i) // foreach is immutable :/ 
                hands[i].UpdateForJokers();
            hands = hands.OrderByDescending(h => h).ToArray();

            var accPartTwo = 0L;
            for (var i = 0; i < hands.Length; ++i)
                accPartTwo += (i + 1) * hands[i].Bet;

            Console.WriteLine($"Part one: {accPartOne}");
            Console.WriteLine($"Part two: {accPartTwo}");
        }
    }
}
