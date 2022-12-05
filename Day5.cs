using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    public class Day5 : Day
    {
        public Day5(bool test) : base(5, test)
        { 
        }

		public class Stacks
		{
			public List<List<char>> stacks;
			public List<int[]> moves;

			public Stacks(List<string> lines)
			{
				int numstacks = 0;
				int numlines = 0;
				foreach (var line in lines)
				{
					numlines++;
					if (line[1] == '1')
					{
						numstacks = int.Parse(line[(line.Length - 2)..line.Length]);
						break;
					}
				}

				List<List<char>> stacks = new();

				for (int i = 0; i < numstacks; i++) stacks.Add(new List<char>());

				for (int i = 0; i < numlines - 1; i++)
				{
					for (int j = 0; j < numstacks; j++)
					{
						var stack_chr = lines[i][4 * j + 1];
						if (lines[i][4 * j + 1] != ' ')
						{
							stacks[j].Insert(0, stack_chr);
						}
					}

				}
				this.stacks = stacks;

				List<int[]> moves = new();
				for (int i = numlines; i < lines.Count; i++)
				{
					var splits = lines[i].Split(' ');
					int[] move = new int[] { 0, 0, 0 };
					if (splits[0] == "move")
					{
						move[0] = int.Parse(splits[1]);
						move[1] = int.Parse(splits[3]) - 1;
						move[2] = int.Parse(splits[5]) - 1;
						moves.Add(move);
					}
				}
				this.moves = moves;
			}
			public void Move(int[] move)
            {
				this.Move(move[0], move[1], move[2]);
            }
			public void Move(int count, int from, int to)
			{
				for(int i = 0; i < count; i++)
                {
					var exchange = this.stacks[from].Last();
					this.stacks[from].RemoveAt(this.stacks[from].Count - 1);

					this.stacks[to].Add(exchange);
                }
			}
			public void MoveMulti(int[] move)
			{
				this.MoveMulti(move[0], move[1], move[2]);
			}
			public void MoveMulti(int count, int from, int to)
			{
				List<char> move_elems = new();
				for (int i = 0; i < count; i++)
				{
					var exchange = this.stacks[from].Last();
					this.stacks[from].RemoveAt(this.stacks[from].Count - 1);

					move_elems.Insert(0, exchange);
				}
				this.stacks[to].AddRange(move_elems);
			}
			public string GetTop()
            {
				List<char> top_elems = new();
				foreach (var stack in this.stacks)
                {
					top_elems.Add(stack.Last());
                }

				return String.Concat(top_elems);
            }
		}
        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();
			var stacks = new Stacks(lines);

			foreach(var move in stacks.moves)
            {
				stacks.Move(move);
            }
			Console.WriteLine(stacks.GetTop());
        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
			var lines = this.Load();
			var stacks = new Stacks(lines);

			foreach (var move in stacks.moves)
			{
				stacks.MoveMulti(move);
			}
			Console.WriteLine(stacks.GetTop());

		}
    }
}
