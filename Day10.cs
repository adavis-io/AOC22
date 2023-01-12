using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    public class Day10 : Day
    {
        public enum OpType
        {
            Noop = 0,
            Addx = 2
        }

        public class OpCode
        {
            int cycles_remain;
            public readonly int operand = 0;

            public OpCode(string desc)
            {
                string operation = desc.Split(' ')[0];

                switch (operation)
                {
                    case "noop":
                        this.cycles_remain = 1;
                        break;

                    case "addx":
                        this.cycles_remain = 2;
                        this.operand = int.Parse(desc.Split(' ')[1]);
                        break;
                }
            }

            public bool DoTick()
            {
                this.cycles_remain -= 1;
                return this.cycles_remain <= 0;
            }
            public bool Executing()
            {
                return this.cycles_remain > 0;
            }

        }
        public Day10(bool test) : base(10, test)
        { 
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            int x = 1;
            int ticks = 0;

            List<int> interesting = new() { 20, 60, 100, 140, 180, 220 };

            List<int> sigstren = new();

            foreach (string line in lines)
            {
                var current_op = new OpCode(line);
                while (current_op.Executing())
                {
                    current_op.DoTick();
                    ticks++;
                    if (interesting.Contains(ticks))
                    {
                        sigstren.Add(ticks * x);
                    }
            
                }
                x += current_op.operand;
                
            }

            int interesting_sum = sigstren.Sum();

            Console.WriteLine(interesting_sum);
        }
        public override void Part2()
        {
            Console.WriteLine("\tPart 2: ");
            var lines = this.Load();
            int x = 1;
            int ticks = 0;
            int raster_pos = 0;

            foreach (string line in lines)
            {
                var current_op = new OpCode(line);
                while (current_op.Executing())
                {
                    if (ticks > 0 && ticks % 40 == 0)
                    {
                        Console.WriteLine("");
                        ticks -= 40;
                    }

                    if (ticks == x || ticks == x - 1 || ticks == x + 1)
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                    current_op.DoTick();
                    ticks++;
                }
                x += current_op.operand;

            }

        }
    }
}
