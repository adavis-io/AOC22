using System;
using System.Collections.Generic;
using System.Text;

namespace AOC22
{
    internal class Day1
    {
        public void part1(string filename)
        {
            Console.Write("Part 1: ");
            //Console.WriteLine(input_path);

            List<Elf> elves = load(filename);

            elves.Sort(delegate(Elf e1, Elf e2) { return e1.energy.CompareTo(e2.energy); });

            elves.Reverse();

            Console.WriteLine(elves[0].energy.ToString());

        }

        public void part2(string filename)
        {
            Console.Write("Part 2: ");
            List<Elf> elves = load(filename);

            elves.Sort(delegate (Elf e1, Elf e2) { return e1.energy.CompareTo(e2.energy); });

            elves.Reverse();

            List<Elf> top3 = elves.GetRange(0, 3);

            int total = 0;
              
            foreach (Elf e in top3)
            {   
                //Console.WriteLine(e.energy.ToString());
                total += e.energy;
            }
            Console.WriteLine(total.ToString());
        }


        private List<Elf> load(string filename)
        {
            string inputfile = String.Format("../../../inputs/{0}", filename);
            string input_path = Path.GetFullPath(inputfile);

            if (!File.Exists(input_path))
            {
                Console.WriteLine(String.Format("File not found: {0}", input_path));
                return null;
            }
            List<Elf> elves = new List<Elf>();

            Elf cur_elf = new Elf(0);
            using (StreamReader sr = File.OpenText(input_path))
            {
                string s;
                while((s = sr.ReadLine()) != null)
                {
                    if (s == "")
                    {
                        elves.Add(cur_elf);

                        cur_elf = new Elf(0);
                    }
                    else
                    {
                        cur_elf.energy += int.Parse(s);
                    }
                }

            }
            elves.Add(cur_elf);
            return elves;
        }
    }
    internal class Elf
    {
        public int energy;
        public Elf(int start_energy)
        {
            energy = start_energy;

        }
    }
}
