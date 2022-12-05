using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    internal class Day3 : Day
    {
        public Day3(bool test) : base(3, test)
        {
        }
        int priority(char c)
        {
            //assume no non-alpha characters are passed in...
            int cc = (int)c;
            
            int a = (int)'a';
            int z = (int)'z';
            int A = (int)'A';
            int Z = (int)'Z';

            if (cc >= A && cc <= Z) // A-Z
            {
                return (cc - A) + 27;
            }
            else if (cc >= a && cc <= z) // a-z
            {
                return (cc - a) + 1;
            }
            else return 0;
        }
        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var packs = this.Load();

            int priority_total = 0;

            foreach(string pack in packs)
            {
                var packlen = pack.Length;
                HashSet<char> items = new();

                items.UnionWith(pack[0..(packlen / 2)]);
                items.IntersectWith(pack[(packlen / 2)..packlen]);

                priority_total += priority(items.ToArray()[0]);
            }

            Console.WriteLine(priority_total.ToString());
        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var packs = this.Load();

            int priority_total = 0;

            for (int i = 0; i < packs.Count; i += 3)
            {
                HashSet<char> items = new();

                items.UnionWith(packs[i]);
                items.IntersectWith(packs[i + 1]);
                items.IntersectWith(packs[i + 2]);

                priority_total += priority(items.ToArray()[0]);
            }

            Console.WriteLine(priority_total.ToString());
        }
    } 
}
