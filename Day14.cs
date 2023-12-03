using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    enum Substance : int
    {
        Empty,
        Sand,
        Rock
    }

    /// <summary>
    /// Represents rock paths within a cave 
    /// </summary>
    public class Rocks
    {
        /// <summary>
        /// List of junction points in the rock path
        /// </summary>
        private List<int[]> nodes = [];
        
        public Rocks(string rockPath)
        {

            foreach(string segment in rockPath.Split(" -> "))
            {
                nodes.Add([int.Parse(segment.Split(",")[0]), int.Parse(segment.Split(",")[1])]);
            }
        }

        /// <summary>
        /// Compute the bounding box for this rock path
        /// </summary>
        /// <returns> An array [minX, minY, maxX, maxY] with the max and min X and Y values for the path </returns>
        public int[] GetBoundingBox()
        {
            int minX = int.MaxValue;
            int minY = int.MaxValue;
            int maxX = int.MinValue;
            int maxY = int.MinValue;

            foreach (int[] node in nodes)
            {
                minX = Math.Min(minX, node[0]);
                maxX = Math.Max(maxX, node[0]);
                minY = Math.Min(minY, node[1]);
                maxY = Math.Max(maxY, node[1]);
            }

            return [minX, minY, maxX, maxY];
        }

        /// <summary>
        /// Realign the rock path to a given origin point. After alignment, 
        /// path points are deltas in X and Y from the original origin point
        /// </summary>
        /// <param name="origin"> - the new origin to use</param>
        public void SetOrigin(int[] origin)
        {
            List<int[]> new_nodes = [];
            
            foreach (int[] node in this.nodes)
            {
                new_nodes.Add([node[0] - origin[0], node[1] - origin[1]]);
            }
            this.nodes = new_nodes;
        }

        /// <summary>
        /// Get the origin of a <c>Rocks</c> object - this is the minX and minY
        /// of the bounding box
        /// </summary>
        /// <returns>int [X, Y] the origin of the path</returns>
        public int[] GetOrigin()
        {
            int[] bb = this.GetBoundingBox();
            return [bb[0], bb[1]];
        }

        /// <summary>
        /// Add entries for this rock path to the given canvas
        /// </summary>
        /// <param name="canvas"> 2D array of int, the canvas data to modify</param>
        /// <returns>modified canvas data with this rock path included</returns>
        public int[,] Draw(int[,] canvas)
        {
            for(int i = 1; i < nodes.Count; i++)
            {
                int[] end = nodes[i];
                int[] start = nodes[i - 1];


                int dx = Math.Sign(end[0] - start[0]);
                int dy = Math.Sign(end[1] - start[1]);

                int[] current = [start[0], start[1]];

                while (current[0] != end[0] || current[1] != end[1])
                {
                    canvas[current[0], current[1]] = (int)Substance.Rock;
                    current[0] += dx;
                    current[1] += dy;
                }
                canvas[current[0], current[1]] = (int)Substance.Rock;

            }

            return canvas;
        }
    }
    public class Cave(int[] sandPos, List<Rocks> terrain, bool floor)
    {
        private int[,] canvas;
        public List<Rocks> caveRocks = terrain;
        public int floor = floor ? -1 : 0;

        private int[] sandPos = sandPos;
        private int[] origin = [0, 0];
        private int[] extents = [1, 1];

        public void Initialize(int[] margins)
        {
            int[] boundingBox = [sandPos[0], sandPos[1], sandPos[0], sandPos[1]];

            //find the overall bounds of the cave by merging all rock path bounding boxes
            foreach(Rocks r in caveRocks)
            {
                boundingBox = MergeBoundingBoxes(boundingBox, r.GetBoundingBox());
            }

            //add padding
            boundingBox[0] -= margins[0];
            boundingBox[1] -= margins[1];
            boundingBox[2] += margins[0];
            boundingBox[3] += margins[1];

            
            this.origin[0] = boundingBox[0];
            this.origin[1] = boundingBox[1];

            if (floor < 0)
            {
                floor = boundingBox[3] + 2;
                boundingBox[3] += 2;

            }


            this.extents[0] = boundingBox[2] - boundingBox[0] + 1;
            this.extents[1] = boundingBox[3] - boundingBox[1] + 1;

            foreach(Rocks r in caveRocks)
            { 
                r.SetOrigin(this.origin);
            }

            int[,] caveData = new int[this.extents[0], this.extents[1]];

            //realign sand position to local frame
            sandPos[0] -= origin[0];
            sandPos[1] -= origin[1];

            origin = [0, 0];

            foreach (Rocks r in caveRocks)
            {
                caveData = r.Draw(caveData);
            }

            if (floor > 0)
            {
                for (int i = 0; i < this.extents[0]; i++)
                {
                    caveData[i, floor] = (int)Substance.Rock;
                }
            }

            this.canvas = caveData;

        }

        /// <summary>
        /// Compute the overall bounding box for the two given bounding box objects
        /// </summary>
        /// <param name="bounds1"></param>
        /// <param name="bounds2"></param>
        /// <returns></returns>
        private int[] MergeBoundingBoxes(int[] bounds1, int[] bounds2)
        {
            return [Math.Min(bounds1[0], bounds2[0]),
                    Math.Min(bounds1[1], bounds2[1]),
                    Math.Max(bounds1[2], bounds2[2]),
                    Math.Max(bounds1[3], bounds2[3])];
        }

        private int[] AddVector(int[] a, int[] b)
        {
            return [a[0] + b[0], a[1] + b[1]];
        }
        /// <summary>
        /// Calculate the movement in X and Y for a sand particle at the given position
        /// </summary>
        /// <param name="sandPosition"></param>
        /// <returns></returns>
        private int[] GetSandMove(int[] sandPosition)
        {
            if (sandPosition[1] + 1 >= this.extents[1])
            {
                return [0, 1];
            }
            else if (this.canvas[sandPosition[0], sandPosition[1] + 1] == (int)Substance.Empty)
            {
                return [0, 1];
            }
            else 
            {
                //position below blocked. Try moving down/left
                if (this.canvas[sandPosition[0] - 1, sandPosition[1] + 1] == (int)Substance.Empty)
                {
                    return [-1, 1];
                }
                
                else if (this.canvas[sandPosition[0] + 1, sandPosition[1] + 1] == (int)Substance.Empty)
                {
                    return [1, 1];
                }
            }

            return [0, 0];
        } 

        private void GrowX(int columns)
        {
            if (columns == 0)
            {
                return;
            }
            
            bool right = true;

            int copy_offset = 0;

            if (columns < 0)
            {
                right = false;
                columns *= -1;
                sandPos[0] += columns;
                copy_offset = columns;
            }

            int[] new_extents = [this.extents[0] + columns, this.extents[1]];
            
            var new_canvas = new int[new_extents[0], new_extents[1]];

            for (int i = 0; i < this.extents[0]; i++)
            {
                for (int j = 0; j < this.extents[1]; j++)
                {
                    new_canvas[i + copy_offset, j] = this.canvas[i, j];
                }
            }

            
            if (this.floor > 0)
            {
                for (int i = 0; i < new_extents[0]; i++)
                {
                    new_canvas[i, floor] = (int)Substance.Rock;
                }
            }

            this.extents = new_extents;
            this.canvas = new_canvas;
        }

        /// <summary>
        /// Test if the given position falls within the bounds of this <c>Cave</c>
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CheckInBounds(int[] position)
        {
            if (position[0] < 0 || position[1] < 0) return false;

            if (position[0] > this.extents[0]) return false;

            if (position[1] > this.extents[1]) return false;

            return true;
        }

        public bool AddSand()
        {
            int[] position = [.. sandPos]; //sand starts at starting sand origin

            int[] newpos = [.. position];

            if (canvas[sandPos[0], sandPos[1]] == (int)Substance.Sand)
            {
                return false;
            }

            while (true) 
            {
                var move = GetSandMove(position);
                newpos = AddVector(position, move);

                if (move[1] == 0) // movement has stopped, add sand at end point
                {
                    if (this.canvas[newpos[0], newpos[1]] != (int)Substance.Sand)
                    {
                        this.canvas[newpos[0], newpos[1]] = (int)Substance.Sand;

                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
                if (!CheckInBounds(newpos)) //sand is outside of bounds, don't modify map
                {
                    return false;
                }

                position = newpos;

                if (newpos[0] <= 1)
                {
                    GrowX(-1);
                    newpos[0] += 1;
                }
                else if (newpos[0] >= this.extents[0] - 2)
                {
                    GrowX(1);
                }

            }
        }

        public int SandCount()
        {
            int count = 0;
            foreach (var loc in this.canvas)
            {
                if (loc == (int)Substance.Sand) { count++; }
            }
            return count;
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            for(int j = 0; j < this.extents[1]; j++)
            {
                for (int i = 0; i < this.extents[0]; i++)
                {
                    switch (this.canvas[i,j])
                    {
                        case (int)Substance.Empty:
                            if (i == this.sandPos[0] && j == this.sandPos[1])
                            {
                                sb.Append('+');
                            }
                            else
                            {
                                sb.Append('.');
                            }
                            break;

                        case (int)Substance.Sand:
                            if (i == this.sandPos[0] && j == this.sandPos[1])
                            {
                                sb.Append('O');
                            }
                            else
                            {
                                sb.Append('o');
                            }
                            break;

                        case (int)Substance.Rock:
                            sb.Append('#');
                            break;

                        default:
                            if (i == this.sandPos[0] && j == this.sandPos[1])
                            {
                                sb.Append('+');
                            }
                            else
                            {
                                sb.Append('?');
                            }
                            break;
                    }
                }
                
                if( j != this.extents[1] - 1)
                {
                    sb.Append('\n');
                }
            }
            return sb.ToString();
        }
    }

    public class Day14(bool test) : Day(14, test)
    {
        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            List<Rocks> terrain = [];

            foreach(string line in lines)
            {
                terrain.Add(new Rocks(line));
            }


            Cave c = new Cave([500, 0], terrain, false);

            c.Initialize([1, 0]);

            int sandcount = 0;
            while (c.AddSand())
            { 
                sandcount += 1;
            }

            Console.WriteLine("AddSand calls: {0}, canvas entities: {1}", sandcount, c.SandCount());

        }

        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            List<Rocks> terrain = [];

            foreach (string line in lines)
            {
                terrain.Add(new Rocks(line));
            }


            Cave c = new Cave([500, 0], terrain, true);

            c.Initialize([1, 0]);



            int sandcount = 0;
            while (c.AddSand())
            {
                sandcount += 1;

            }

            Console.WriteLine("AddSand calls: {0}, canvas entities: {1}", sandcount, c.SandCount());
        }
    }
}
