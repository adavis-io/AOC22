using System;
using System.Collections.Generic;
using System.Text;

namespace AOC22
{
    internal class Day2
    {
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
        public void part1(string filename)
        {
            Console.Write("Part 1: ");
         
            var inputfile = String.Format("../../../inputs/{0}", filename);
            var input_path = Path.GetFullPath(inputfile);

            int score = 0;
            if (!File.Exists(input_path))
            {
                Console.WriteLine(String.Format("File not found: {0}", input_path));
                return;
            }

            using (StreamReader sr = File.OpenText(input_path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    score += this.scores[s];

                }
            }

            Console.WriteLine(String.Format("Score: {0}", score));
        }

        public void part2(string filename)
        {
            Console.Write("Part 2: ");

            var inputfile = String.Format("../../../inputs/{0}", filename);
            var input_path = Path.GetFullPath(inputfile);

            int score = 0;
            if (!File.Exists(input_path))
            {
                Console.WriteLine(String.Format("File not found: {0}", input_path));
                return;
            }

            using (StreamReader sr = File.OpenText(input_path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    score += this.scores_real[s];

                }
            }

            Console.WriteLine(String.Format("Score: {0}", score));
        }

    }
}
