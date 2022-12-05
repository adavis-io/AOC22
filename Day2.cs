using System;
using System.Collections.Generic;
using System.Text;

namespace AOC22
{
    internal class Day2 : Day
    {
        public Day2(bool test) : base(2, test)
        {
        }
        private Dictionary<string, int> scores = new Dictionary<string, int>()
        {
            {"A X", 4},
            {"A Y", 8},
            {"A Z", 3},
            {"B X", 1},
            {"B Y", 5},
            {"B Z", 9},
            {"C X", 7},
            {"C Y", 2},
            {"C Z", 6}
        };
        private Dictionary<string, int> scores_real = new Dictionary<string, int>()
        {
            {"A X", 3},
            {"A Y", 4},
            {"A Z", 8},
            {"B X", 1},
            {"B Y", 5},
            {"B Z", 9},
            {"C X", 2},
            {"C Z", 7},
            {"C Y", 6},
        };
        public override void Part1()
        {
            Console.Write("\tPart 1: ");

            var lines = this.Load();

            int score = 0;
            foreach (var line in lines)
            {
                score += this.scores[line];
            }
            

            Console.WriteLine(String.Format("{0}", score));
        }

        public override void Part2()
        {
            Console.Write("\tPart 2: ");

            var lines = this.Load();

            int score = 0;
            foreach (var line in lines)
            {
                score += this.scores_real[line];
            }

            Console.WriteLine(String.Format("{0}", score));
        }

    }
}
