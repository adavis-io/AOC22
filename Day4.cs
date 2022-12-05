using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{   
    internal class Day4 : Day
    {
        public Day4(bool test) : base(4, test)
        {
        }
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

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var pairs= this.Load();

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

        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var pairs= this.Load();

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
