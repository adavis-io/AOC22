using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{   
    internal class Day4 : Day
    {
        public class Elf
        {
            public int upper = 0;
            public int lower = 0;
            public Elf(string bounds)
            {

                var split_bounds = bounds.Split('-');

                this.lower = int.Parse(split_bounds[0]);
                this.upper = int.Parse(split_bounds[1]);
            }

            public bool contains(Elf e)
            {
                // returns true if e's bounds are entirely inside this elf
                return (e.lower >= this.lower && e.upper <= this.upper);
            }
            public bool overlaps(Elf e)
            {
                return ((e.lower >= this.lower && e.lower <= this.upper) || (e.upper <= this.upper && e.upper >= this.lower));
            }
        }

        public void part1(string filename)
        {
            Console.Write("Part 1:");
            var pairs= load(filename);

            int fully_contained = 0;

            foreach (var pair in pairs)
            {
                var elf1 = new Elf(pair.Split(',')[0]);
                var elf2 = new Elf(pair.Split(',')[1]);

                if (elf1.contains(elf2) || elf2.contains(elf1))
                {
                    fully_contained++;
                }

            }
            Console.WriteLine(fully_contained);
        }

        public void part2(string filename)
        {
            Console.Write("Part 2:");
            var pairs= load(filename);

            int overlap = 0;

            foreach (var pair in pairs)
            {
                var elf1 = new Elf(pair.Split(',')[0]);
                var elf2 = new Elf(pair.Split(',')[1]);

                if (elf1.overlaps(elf2) || elf2.overlaps(elf1))
                {
                    overlap++;
                }

            }
            Console.WriteLine(overlap);
        }
    } 
}
