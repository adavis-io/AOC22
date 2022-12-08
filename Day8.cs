using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
    public class Day8 : Day
    {
        public Day8(bool test) : base(8, test)
        { 
        }

        public List<List<int>> Transpose(List<List<int>> input)
        {
            List<List<int>> output = new();

            for (int i = 0; i < input[0].Count; i++)
            {
                List<int> row = new();
                for (int j = 0; j < input.Count; j++)
                {
                    row.Add(input[j][i]);
                }
                output.Add(row);
            }
            return output;
        }
        public List<List<int>> LoadTrees(List<string> input)
        {
            List<List<int>> trees = new();

            foreach(string line in input)
            {
                List<int> line_trees = new();
                foreach(char c in line)
                {
                    line_trees.Add(int.Parse(c.ToString()));
                }
                trees.Add(line_trees);
            }

            return trees;
        }

        public int CountVisibleTrees(List<List<int>> treeheights)
        {
            
            int count = 0;
            for (int j = 0; j < treeheights[0].Count; j++)
            {

                for (int i = 0; i < treeheights.Count; i++)
                {
                    if (Visible(treeheights, i, j))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public int Score(List<List<int>> treeheights, int x, int y)
        {
            var north = ScoreNorth(treeheights, x, y);
            var south = ScoreSouth(treeheights, x, y);
            var east = ScoreEast(treeheights, x, y);
            var west = ScoreWest(treeheights, x, y);

            return (north * south * east * west);
        }

        public int MaxScenicScore(List<List<int>> treeheights)
        {
            int max_score = 0;
            for (int j = 0; j < treeheights[0].Count; j++)
            {

                for (int i = 0; i < treeheights.Count; i++)
                {
                    var score = Score(treeheights, i, j);
                    if (score > max_score)
                    {
                        max_score = score;
                    }
                }
            }
            return max_score;
        }

        public bool Visible(List<List<int>> treeheights, int x, int y)
        {
            bool north = VisibleNorth(treeheights, x, y);
            bool south = VisibleSouth(treeheights, x, y);
            bool east = VisibleEast(treeheights, x, y);
            bool west = VisibleWest(treeheights, x, y);
            return ( north || south || east || west);
        }

        public bool VisibleNorth(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_y == 0) { return true; }

            int curheight = treeheights[cur_x][cur_y];
            bool visible = true;

            for (int j = cur_y - 1; j >=0; j--)
            {
                if (treeheights[cur_x][j] >=  curheight){
                    visible = false;
                    break;
                }
            }

            return visible;
        }
        public bool VisibleSouth(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_y == (treeheights[cur_y].Count - 1)) { return true; }

            int curheight = treeheights[cur_x][cur_y];
            bool visible = true;

            for (int j = cur_y + 1; j < treeheights[cur_y].Count; j++)
            {
                if (treeheights[cur_x][j] >= curheight)
                {
                    visible = false;
                    break;
                }
            }

            return visible;
        }
        public bool VisibleWest(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_x == 0) { return true; }

            int curheight = treeheights[cur_x][cur_y];
            bool visible = true;

            for (int i = cur_x - 1; i >= 0; i--)
            {
                if (treeheights[i][cur_y] >= curheight)
                {
                    visible = false;
                    break;
                }
            }

            return visible;
        }
        public bool VisibleEast(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_x == (treeheights.Count - 1)) { return true; }

            int curheight = treeheights[cur_x][cur_y];
            bool visible = true;

            for (int i = cur_x + 1; i < treeheights.Count; i++)
            {
                if (treeheights[i][cur_y] >= curheight)
                {
                    visible = false;
                    break;
                }
            }

            return visible;
        }

        public int ScoreNorth(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_y == 0) { return 0; }

            int curheight = treeheights[cur_x][cur_y];
            int score = 0;

            for (int j = cur_y - 1; j >= 0; j--)
            {
                score++;
                if (treeheights[cur_x][j] >= curheight)
                {
                    break;
                }
            }

            return score;
        }
        public int ScoreSouth(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_y == (treeheights[cur_y].Count - 1)) { return 0; }

            int curheight = treeheights[cur_x][cur_y];
            int score = 0;

            for (int j = cur_y + 1; j < treeheights[cur_y].Count; j++)
            {
                score++;
                if (treeheights[cur_x][j] >= curheight)
                {
                    break;
                }
            }

            return score;
        }
        public int ScoreWest(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_x == 0) { return 0; }

            int curheight = treeheights[cur_x][cur_y];
            int score = 0;

            for (int i = cur_x - 1; i >= 0; i--)
            {
                score++;
                if (treeheights[i][cur_y] >= curheight)
                {
                    break;
                }
            }

            return score;
        }
        public int ScoreEast(List<List<int>> treeheights, int cur_x, int cur_y)
        {
            if (cur_x == (treeheights.Count - 1)) { return 0; }

            int curheight = treeheights[cur_x][cur_y];
            int score = 0;

            for (int i = cur_x + 1; i < treeheights.Count; i++)
            {
                score++;  
                if (treeheights[i][cur_y] >= curheight)
                {
                    break;
                }
            }

            return score;
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            var trees = Transpose(LoadTrees(lines));

            var tree_array = trees.ToArray();

            Console.WriteLine(CountVisibleTrees(trees));

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();
            var trees = Transpose(LoadTrees(lines));

            var tree_array = trees.ToArray();

            Console.WriteLine(MaxScenicScore(trees));

        }
    }
}
