using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    internal class Day3
    {
        internal List<string> load(string filename)
        {
            var inputfile = String.Format("../../../inputs/{0}", filename);
            var input_path = Path.GetFullPath(inputfile);

            List<string> packs = new List<string>();
            if (!File.Exists(input_path))
            {
                Console.WriteLine(String.Format("File not found: {0}", input_path));
                return packs;
            }

            using (StreamReader sr = File.OpenText(input_path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    packs.Add(s);
                }
            }
            return packs;
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
        public void part1(string filename)
        {
            Console.Write("Part 1:");
            var packs = this.load(filename);

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
        public void part2(string filename)
        {
            Console.Write("Part 2:");
            var packs = this.load(filename);

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
