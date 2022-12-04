using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    internal class Day
    {
        public List<string> load(string filename)
        {
            var inputfile = String.Format("../../../inputs/{0}", filename);
            var input_path = Path.GetFullPath(inputfile);

            List<string> lines = new List<string>();
            if (!File.Exists(input_path))
            {
                Console.WriteLine(String.Format("File not found: {0}", input_path));
                return lines;
            }

            using (StreamReader sr = File.OpenText(input_path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    lines.Add(s);
                }
            }
            return lines;
        }
    }
}
