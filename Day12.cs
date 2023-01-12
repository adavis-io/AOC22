using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AOC22.Day5;

namespace AOC22
{
    public class Day12 : Day
    {
        public class Node
        {
            public List<Node> neighbors = new();

            public int height = 30;
            
            
            public int[] position = new int[2];
            public bool isStart = false;
            public bool isEnd = false;
            public bool visited = false;
            public int distance = int.MaxValue - 1;
            public Node previous = null;



            public Node(char mapheight, int x, int y)
            {
                if(mapheight == 'S')
                {
                    isStart = true;
                    height = 0;
                }
                else if(mapheight == 'E')
                {
                    isEnd = true;
                    height = 25;
                }
                else
                {
                    height = (int)(mapheight - 'a');
                }
                position[0] = x;
                position[1] = y;
            }
        }
        
        public List<Node> BuildGraph(List<string> lines)
        {
            var graph = new Dictionary<(int x, int y), Node>();

            int i = 0, j = 0;
            
            foreach(string line in lines)
            {
                i = 0;
                foreach(char c in line)
                {
                    graph.Add((i, j), new Node(c, i, j));
                    i++;
                }
                j++;
            }

            for (i = 0; i < lines[0].Length; i++)
            {
                for(j = 0; j < lines.Count; j++)
                {
                    if (i > 0)
                    {
                        graph[(i, j)].neighbors.Add(graph[(i - 1, j)]);

                    }
                    if (i < lines[0].Length - 1)
                    {
                        graph[(i, j)].neighbors.Add(graph[(i + 1, j)]);
                    }

                    if (j > 0)
                    { 
                        graph[(i, j)].neighbors.Add(graph[(i, j - 1)]); 
                    }

                    if (j < lines.Count - 1)
                    {
                        graph[(i, j)].neighbors.Add(graph[(i, j + 1)]);
                    }
                }
            }

            return graph.Values.ToList();
        }

        public void FindPaths(List<Node> graph)
        {
            List<Node> unvisited = new(graph);

            while(unvisited.Count > 0)
            {
                unvisited = unvisited.OrderBy(n => n.distance).ToList();
                Node candidate = unvisited[0];
                candidate.visited = true;
                unvisited.RemoveAt(0);

                foreach (Node neighbor in candidate.neighbors)
                {
                    int alt = candidate.distance + 1;

                    if (candidate.height - neighbor.height <= 1)
                    {
                        if (alt < neighbor.distance)
                        {
                            neighbor.distance = alt;
                            neighbor.previous = candidate;
                        }
                    }
                }

            }

        }

        public Day12(bool test) : base(12, test)
        { 
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            var graph = BuildGraph(lines);



            Node end = graph[0];
            Node start = graph[0];
            foreach(Node n in graph)
            {   
                if(n.isStart)
                {
                    start = n;
                }
                if(n.isEnd)
                {
                    n.distance = 0;
                    end = n;
                }
            }
            
            FindPaths(graph);

            Console.WriteLine(start.distance);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            var graph = BuildGraph(lines);

            Node end = graph[0];

            foreach (Node n in graph)
            {
                if (n.isEnd)
                {
                    end = n;
                }
            }

            end.distance = 0;

            FindPaths(graph);

            int min_distance = int.MaxValue;
            foreach (Node n in graph)
            {
                if (n.height == 0 && n.visited)
                {
                    min_distance = Math.Min(min_distance, n.distance);
                }
            }

            Console.WriteLine(min_distance);
        }
    }
}
