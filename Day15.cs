using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC22
{
    public class Sensor
    {
        public int[] location;
        public int[] beacon;
        public int min_range;
        public Sensor(string s)
        {
            var loc_re = new Regex(@"x=(-?\d+), y=(-?\d+)");

            var loc_s = s.Split(':')[0];
            var bcn_s = s.Split(':')[1];
            this.location = [int.Parse(loc_re.Match(loc_s).Groups[1].Value), int.Parse(loc_re.Match(loc_s).Groups[2].Value)];
            this.beacon = [int.Parse(loc_re.Match(bcn_s).Groups[1].Value), int.Parse(loc_re.Match(bcn_s).Groups[2].Value)];
            this.min_range = this.Manhattan(this.location, this.beacon);
        }

        public int Manhattan(int[] a, int[] b)
        {
            return Math.Abs(a[0] - b[0]) + Math.Abs(a[1] - b[1]);
        }
        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append($"S: [{this.location[0]}, {this.location[1]}], ");
            sb.Append($"B: [{this.beacon[0]}, {this.beacon[1]}], ");
            sb.Append($"Min Range: {this.min_range}, ");
            var bb = this.Bounds();
            sb.Append($"Bounds: [{bb[0]}, {bb[1]}, {bb[2]}, {bb[3]}]");

            return sb.ToString();
        }

        public bool IsValidBeaconPos(int[] location)
        {
            if (this.Manhattan(location, this.location) <= this.min_range)
            {
                return false;
            }
            return true;
        }
        public bool IsBeaconPos(int[] location)
        {
            if (this.Manhattan(location, this.beacon) == 0)
            {
                return true;
            }
            return false;
        }
        public bool IsSensorPos(int[] location)
        {
            if (this.Manhattan(location, this.location) == 0)
            {
                return true;
            }
            return false;
        }

        public bool RowInRange(int row)
        {
            return Math.Abs(this.location[1] - row) <= this.min_range;
        }

        public int[] Bounds()
        {
            return [this.location[0] - this.min_range,
                    this.location[1] - this.min_range,
                    this.location[0] + this.min_range,
                    this.location[1] + this.min_range];
        }
    }
    public class Day15(bool test) : Day(15, test)
    {
        public int[] BoundingBox(int[] a, int[] b)
        {
            return [Math.Min(a[0], b[0]),
                    Math.Min(a[1], b[1]),
                    Math.Max(a[2], b[2]),
                    Math.Max(a[3], b[3])];
        }

        public bool CheckOverlap(int[] a, int[] b)
        {
            if (a[0] >= b[0] & a[0] <= b[1])
                return true;
            else if (a[1] >= b[0] & a[1] <= b[1])
                return true;
            else if (b[0] >= a[0] & b[0] <= a[1])
                return true;
            else if (b[1] >= a[0] & b[1] <= a[1])
                return true;

            else return false;
        }

        public List<int[]> FindInvalidRanges(int y, List<Sensor> sensors, int[] bounds)
        {
            List<int[]> invalid_ranges = [];

            foreach (var s in sensors)
            {
                if (s.RowInRange(y))
                {
                    int delta = s.min_range - Math.Abs((y - s.location[1]));

                    invalid_ranges.Add([Math.Max(bounds[0], s.location[0] - delta), Math.Min(bounds[1], s.location[0] + delta)]);

                }
            }

            invalid_ranges = invalid_ranges.OrderBy(r => r[0]).ToList();


            var modified = true;

            while (modified)
            {
                modified = false;
                for (int i = 0; i < invalid_ranges.Count - 1; i++)
                {
                    if (CheckOverlap(invalid_ranges[i], invalid_ranges[i + 1]))
                    {
                        invalid_ranges[i] = FindOverlap(invalid_ranges[i], invalid_ranges[i + 1]);
                        invalid_ranges.RemoveAt(i + 1);
                        modified = true;
                        break;
                    }
                }
            }

            return invalid_ranges;
        }

        public int[] FindOverlap(int[] a, int[] b)
        {
            return [Math.Min(a[0], b[0]), Math.Max(a[1], b[1])];
        }

        public void DrawHeader(int[] bounds)
        {
            Console.WriteLine();

            Console.Write("         ");

            for (int i = bounds[0]; i < bounds[1] + 1; i++)
            {
                if (i < 0)
                {
                    if (i % 5 == 0)
                    {
                        Console.Write('-');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();

            Console.Write("         ");

            for (int i = bounds[0]; i < bounds[1] + 1; i++) 
            { 
                if(i >= 10 | i <= -10 )
                {
                    if (i % 5 == 0)
                    {
                        Console.Write(Math.Abs(i / 10));
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                else
                {
                    Console.Write(' ');
                }
            }
            Console.WriteLine();

            Console.Write("         ");

            for (int i = bounds[0]; i < bounds[1] + 1; i++)
            {
                if (i % 5 == 0)
                {
                    Console.Write(Math.Abs(i) % 10);
                }

                else
                {
                    Console.Write(' ');
                }
        
            }
            Console.WriteLine();
        }
        public void DrawRow(int y, List<int[]> invalid, List<Sensor> sensors, int[] bounds)
        {
            Console.Write("{0, 8} ", y);
            for (int x = bounds[0]; x < bounds[1] + 1; x++)
            {
                var empty = true;
                foreach(var sensor in sensors)
                {
                    if (sensor.location[1] == y & sensor.location[0] == x)
                    {
                        Console.Write('S');
                        empty = false;
                        break;
                    }
                    if (sensor.beacon[1] == y & sensor.beacon[0] == x)
                    {
                        Console.Write('B');
                        empty = false;
                        break;
                    }
                }

                if (empty)
                {
                    var valid = true;

                    foreach (var range in invalid)
                    {
                        if (x >= range[0] & x <= range[1])
                        {
                            valid = false;
                        }
                    }

                    if (valid)
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write('#');
                    }

                }
            }
            Console.Write('\n');
        }

        public override void Part1()
        {
            Console.Write("\tPart 1: ");
            
            var lines = this.Load();

            var sensors = new List<Sensor>();

            foreach (var line in lines)
            {
                var s = new Sensor(line);
                
                sensors.Add(s);
            }

            int y = 2000000;
            if (this.test)
            {
                y = 11;
            }

            int invalid_pos = 0;


            var invalid_ranges = FindInvalidRanges(y, sensors, [int.MinValue, int.MaxValue]);

            foreach(var r in invalid_ranges)
            {
                invalid_pos += (r[1] - r[0]);
            }




            Console.WriteLine(invalid_pos);

        }
        public override void Part2()
        {
            Console.Write("\tPart 2: ");
            var lines = this.Load();
            var debug = true;

            var sensors = new List<Sensor>();

            foreach (var line in lines)
            {
                var s = new Sensor(line);
                
                sensors.Add(s);
            }

            int ybound = 4000000;
            if (this.test)
            {
                ybound = 20;
            }

            for(int y = 0; y <= ybound + 1; y++)
            {
                var invalid_ranges = FindInvalidRanges(y, sensors, [0, ybound]);
                if (invalid_ranges.Count > 1)
                {
                     long x = invalid_ranges[0][1] + 1;

                    Console.WriteLine(4000000 * x + (long)y);
                    break;
                }
                else 
                {
                    if (invalid_ranges[0][0] > 0)
                    {
                        Console.WriteLine(y);
                        break;
                    }
                    if (invalid_ranges[0][1] < 20)
                    {
                        Console.WriteLine(4000000 * 20  + y);
                        break;
                    } 
                }
            }

        }
    }
}
