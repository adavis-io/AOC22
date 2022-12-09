using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AOC22
{
    public class Day9 : Day
    {
        public class Rope
        {
            List<(int x, int y)> knots;
            public Dictionary<(int x, int y), int> visits;

            public Rope(int length)
            {
                knots = new();
                for (int i = 0; i < length; i++)
                {
                    knots.Add((0, 0));
                }

                visits = new();
                visits.Add((0, 0), 1);

            }

            private void UpdatePos(int i)
            {
                var dx = (knots[i - 1].x - knots[i].x);
                var dy = (knots[i - 1].y - knots[i].y);

                var tail = knots[i];

                if (Math.Abs(dx) > 1 || Math.Abs(dy) > 1)
                {
                    tail.x += Math.Sign(dx);
                    tail.y += Math.Sign(dy);
                }

                knots[i] = tail;

                if (i == knots.Count - 1)
                {
                    if (!visits.ContainsKey(tail))
                    {
                        visits.Add(tail, 1);
                    }
                    else
                    {
                        visits[tail] += 1;
                    }

                    return;
                }
                else
                {
                    UpdatePos(i + 1);
                }
            }

            private void ApplyMove(int steps, int dir)
            {
                var head = knots[0];
                for (int i = 0; i < steps; i++)
                {
                    switch(dir)
                    {
                        case 0:
                            head.x += 1;
                            break;
                        
                        case 1:
                            head.y += 1;
                            break;

                        case 2:
                            head.x -= 1;
                            break;

                        case 3:
                            head.y -= 1;
                            break;
                    }
                    knots[0] = head;
                    UpdatePos(1);
                }
            }
            public void Move(string command)
            {
                var cmd_split = command.Split(' ');
                int move_steps = int.Parse(cmd_split[1]);
                switch(cmd_split[0])
                {
                    case "R":
                        ApplyMove(move_steps, 0);
                        break;

                    case "U":
                        ApplyMove(move_steps, 1);
                        break;

                    case "L":
                        ApplyMove(move_steps, 2);
                        break;

                    case "D":
                        ApplyMove(move_steps, 3);
                        break;

                }
                
            }

            public void Draw(int minx, int maxx, int miny, int maxy)
            {
                Console.WriteLine("Current State:");

                for (int j = maxy; j >= miny; j--)
                {
                    for (int i = minx; i <= maxx; i++)
                    {
                        bool empty = true;
                        for(int k = 0; k < knots.Count; k++)
                        {
                            if(knots[k].x == i && knots[k].y == j)
                            {
                                if (k != 0)
                                {
                                    Console.Write(k);
                                }
                                else
                                {
                                    Console.Write('H');
                                }
                                empty = false;
                                break;
                            }
                        }
                        if (empty)
                        {
                            if (i == 0 && j == 0)
                            {
                                Console.Write('s');
                            }
                            else
                            {
                                Console.Write('.');
                            }
                        }
                    }
                    Console.WriteLine("");

                }
                
            }
        }



        public Day9(bool test) : base(9, test)
        { 
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            Rope r = new Rope(2);

            foreach (var line in lines)
            {
                r.Move(line);
            }

            Console.WriteLine(r.visits.Count);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            Rope r = new Rope(10);

            foreach (var line in lines)
            {
                r.Move(line);
            }

            Console.WriteLine(r.visits.Count);
        }
    }
}
