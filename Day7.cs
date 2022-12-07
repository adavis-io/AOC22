using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC22
{
   
    public class Day7 : Day
    {
        private class D7Path
        {
            public List<D7Path> children = new();
            public D7Path parent = null;
            public string name;
            public bool is_file = false;
            public int size = -1;

            public D7Path(D7Path parent, string desc)
            {
                var split_desc = desc.Split(' ');

                this.name = split_desc[1];

                if (split_desc[0] != "dir")
                {
                    this.size = int.Parse(split_desc[0]);
                    this.is_file = true;
                }

                this.parent = parent;

            }

            public D7Path(string name)
            {
                this.name = name;
            }

            public void AddChild(string desc)
            {
                D7Path child = new(this, desc);

                children.Add(child);
            }

            public D7Path ChDir(string path)
            {
                if (path == "..")
                {
                    return parent;
                }
                else if (path == "/")
                {
                    return this.GetRoot();
                }
                else
                {
                    foreach(D7Path child in children)
                    {
                        if(child.name == path)
                        {
                            return child;
                        }
                    }
                }

                return null;
            }

            public D7Path GetRoot()
            {
                if (parent == null)
                {
                    return this;
                }

                return this.parent;
            }

            public int GetSizeRecursive()
            {
                if (this.is_file) { return this.size; }

                int total_size = 0;
                foreach(D7Path child in this.children)
                {
                    total_size += child.GetSizeRecursive();
                }

                return total_size;
            }

            public List<D7Path> GetDirsSmallerThan(int size)
            {
                List<D7Path> smallerdirs = new();

                foreach(D7Path child in this.children)
                {
                    smallerdirs.AddRange(child.GetDirsSmallerThan(size));

                    if (!child.is_file && child.GetSizeRecursive() < size)
                    {
                        smallerdirs.Add(child);
                    }
                }

                if (this.GetSizeRecursive() < size)
                {
                    smallerdirs.Add(this);
                }

                return smallerdirs;
            }
            public List<D7Path> GetDirsLargerThan(int size)
            {
                List<D7Path> largerdirs = new();

                foreach (D7Path child in this.children)
                {
                    largerdirs.AddRange(child.GetDirsLargerThan(size));

                    if (!child.is_file && child.GetSizeRecursive() >= size)
                    {
                        largerdirs.Add(child);
                    }
                }

                if (this.GetSizeRecursive() >= size)
                {
                    largerdirs.Add(this);
                }

                return largerdirs;
            }
        }

            public Day7(bool test) : base(7, test)
        { 
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            var lines = this.Load();

            D7Path cwd = new("/");

            bool in_ls = false;

            foreach(var line in lines)
            {
                if (line[0] == '$')
                {
                    in_ls = false;

                    var cmd = line.Split(' ');
                    if (cmd[1] == "cd")
                    {
                        cwd = cwd.ChDir(cmd[2]);
                    }
                    else { in_ls = true; }

                }
                else if (in_ls)
                {
                    cwd.AddChild(line);
                }
            }
            cwd = cwd.GetRoot();

            var small = cwd.GetDirsSmallerThan(100000);

            int small_size = 0;
            foreach(D7Path dir in small)
            {
                small_size += dir.GetSizeRecursive();
            }

            Console.WriteLine(small_size);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();

            D7Path cwd = new("/");

            bool in_ls = false;

            foreach (var line in lines)
            {
                if (line[0] == '$')
                {
                    in_ls = false;

                    var cmd = line.Split(' ');
                    if (cmd[1] == "cd")
                    {
                        cwd = cwd.ChDir(cmd[2]);
                    }
                    else { in_ls = true; }

                }
                else if (in_ls)
                {
                    cwd.AddChild(line);
                }
            }

            cwd = cwd.GetRoot();

            int total_size = cwd.GetSizeRecursive();
            int max_size = 70000000;

            int available = max_size - total_size;
            
            int required = 30000000;

            int threshold = required - available;

            var small = cwd.GetDirsLargerThan(threshold);

            small = small.OrderBy(o => o.GetSizeRecursive()).ToList();
            
            Console.WriteLine(small[0].GetSizeRecursive());
        }
    }
}
