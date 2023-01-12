using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AOC22
{
    public class Day11 : Day
    {
        public class Monkey
        {
            public enum OpType
            {
                ADD = 0,
                MULT = 1
            }

            public readonly int id;
            public List<ulong> items;
            readonly ulong operand;
            readonly OpType operation; 
            public readonly ulong div_check;
            readonly int[] targets;
            public ulong items_inspected = 0;
            public ulong lcm = ulong.MaxValue;
            
            Monkey[] resolvedTargets = new Monkey[2];

            public Monkey(List<string> input)
            {
                string pattern = @"[\d]+";

                id = int.Parse(Regex.Match(input[0], pattern).Value);
                items = new();

                foreach (Match m in Regex.Matches(input[1], pattern, RegexOptions.IgnoreCase))
                {
                    items.Add(ulong.Parse(m.Value));
                }

                string opstring = input[2].Split(' ')[^2];

                if (opstring == "+")
                {
                    operation = OpType.ADD;
                }
                else
                {
                    operation = OpType.MULT;
                }

                opstring = input[2].Split(' ')[^1];
                
                if (opstring == "old")
                {
                    operand = 0;
                }
                else
                {
                    operand = ulong.Parse(opstring);
                }


                div_check = ulong.Parse(input[3].Split(' ')[^1]);

                targets = new int[2];

                targets[0] = int.Parse(input[4].Split(' ')[^1]); // true target
                targets[1] = int.Parse(input[5].Split(' ')[^1]); // false target

            }

            public void ResolveTargets(List<Monkey> monkeys)
            {
                resolvedTargets[0] = monkeys[targets[0]]; // true target
                resolvedTargets[1] = monkeys[targets[1]]; // false target
            }

            public void Redistribute(ulong divisor)
            {
              
                for (int i = 0; i < items.Count; i++)
                {
                    this.items_inspected++;
                    
                    ulong op_int = operand;
                    ulong item_start = items[i];

                    if(op_int == 0)
                    {
                        op_int = items[i];
                    }

                    if (operation == OpType.MULT)
                    {
                        items[i] = items[i] * op_int;
                    }
                    else
                    {
                        items[i] = items[i] + op_int;
                    }


                    items[i] = (items[i] / divisor);
                    items[i] = items[i] % lcm;

                    int target = (this.items[i] % div_check) == 0 ? 0 : 1;

                    
                    resolvedTargets[target].items.Add(items[i]);
                }

                this.items.Clear();
            }
        }
        public Day11(bool test) : base(11, test)
        { 
        }

        public List<Monkey> LoadMonkeys(List<string> lines)
        {
            List<Monkey> monkeys = new();
            List<string> currentMonkey = new();
            ulong lcm = 1;

            foreach(string line in lines)
            {
                currentMonkey.Add(line);
                if (line == "")
                {
                    monkeys.Add(new Monkey(currentMonkey));
                    lcm *= monkeys[^1].div_check;

                    currentMonkey.Clear();
                }
            }
            monkeys.Add(new Monkey(currentMonkey));
            lcm *= monkeys[^1].div_check;
            foreach (Monkey monkey in monkeys)
            {
                monkey.ResolveTargets(monkeys);
                monkey.lcm = lcm;
            }

            return monkeys;
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();
            var monkeys = LoadMonkeys(lines);

            int roundcount = 20;
            for (int i = 0; i < roundcount; i++)
            {
                //Console.WriteLine("Round {0}", i + 1);
                foreach(Monkey monkey in monkeys)
                {
                    monkey.Redistribute(3);
                }
                foreach (Monkey m in monkeys)
                {
                    //Console.WriteLine("Monkey {0}: {1}", m.id, String.Join(", ", m.items));
                }
            }

            monkeys = monkeys.OrderBy(m => m.items_inspected).ToList();

            monkeys.Reverse();

            Console.WriteLine(monkeys[0].items_inspected * monkeys[1].items_inspected);
        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();
            var monkeys = LoadMonkeys(lines);

            int roundcount = 10000;
            List<int> print_at = new List<int>(); //{ 1, 20, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };
            for (int i = 0; i < roundcount; i++)
            {
                foreach (Monkey m in monkeys)
                {
                    //Console.WriteLine("Monkey {0}: {1}. Items: {2}", m.id, m.items_inspected, String.Join(", ", m.items));
                    m.Redistribute(1);
                }
                if (print_at.Contains(i + 1))
                {
                    Console.WriteLine("Round {0}", i + 1);
                    foreach (Monkey m in monkeys)
                    {
                        Console.WriteLine("Monkey {0} inspected items {1} times", m.id, m.items_inspected);
                    }
                }
            }

            monkeys = monkeys.OrderBy(m => m.items_inspected).ToList();

            monkeys.Reverse();

            Console.WriteLine(monkeys[0].items_inspected * monkeys[1].items_inspected);

        }
    }
}
