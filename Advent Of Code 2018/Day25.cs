using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day25
    {
        public static int PartOne(string input)
        {
            var points = input.Split(new string[] { "\n", "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            var constellations = 0;
            var spaceTimePoints = new List<SpaceTimePoints>();
            for(var i =0; i < points.Count(); i +=4)
            {
                spaceTimePoints.Add(new SpaceTimePoints
                {
                    X = points[i],
                    Y = points[i + 1],
                    Z = points[i + 2],
                    Time = points[i + 3]
                });
            }
            var allTried = new List<int>();
            while (true)
            {
                var tried = new List<int>();
                var pointsToCheck = new Queue<int>();
                for (var i = 0; i < spaceTimePoints.Count(); i++)
                {
                    if (!allTried.Contains(i))
                    {
                        pointsToCheck.Enqueue(i);
                        break;
                    }
                }
                while (pointsToCheck.Count > 0)
                {
                    var current = pointsToCheck.Dequeue();
                    if(allTried.Contains(current))
                    {
                        continue;
                    }
                    tried.Add(current);
                    allTried.Add(current);
                    for (var i = 0; i < spaceTimePoints.Count(); i++)
                    {
                        if (!tried.Contains(i) && !allTried.Contains(i))
                        {
                            if ((GetDistance(spaceTimePoints[i], spaceTimePoints[current])) <= 3)
                            {
                                pointsToCheck.Enqueue(i);
                            }
                        }
                    }
                }
                if(tried.Count() == 0)
                {
                    break;
                }
                constellations++;
            }
            return constellations;
        }
        public static string PartTwo(string input)
        {
            return "Advent of Code 2018 is complete! Merry Christmas!";
        }
        public static int GetDistance(SpaceTimePoints a, SpaceTimePoints b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y) + Math.Abs(a.Z - b.Z) + Math.Abs(a.Time - b.Time);
        }
    }
    class SpaceTimePoints
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public int Time { get; set; }
    }
}
