using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    public class Day6 : Day
    {
        public Day6(bool test) : base(6, test)
        { 
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            foreach (var message in lines)
            {

                int msg_start_index = 0;
                for (int i = 3; i < message.Length; i++)
                {
                    var msg_segment = message.Substring(i - 3, 4);

                    HashSet<char> msg_set = new HashSet<char>(msg_segment);

                    if (msg_set.Count == 4)
                    {
                        msg_start_index = i + 1;
                        break;
                    }
                }
                Console.WriteLine(msg_start_index);
            }
        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            foreach (var message in lines)
            {

                int pkt_start_index = 0;
                for (int i = 13; i < message.Length; i++)
                {
                    var msg_segment = message.Substring(i - 13, 14);

                    HashSet<char> msg_set = new HashSet<char>(msg_segment);

                    if (msg_set.Count == 14)
                    {
                        pkt_start_index = i + 1;
                        break;
                    }
                }
                Console.WriteLine(pkt_start_index);
            }
        }
    }
}
