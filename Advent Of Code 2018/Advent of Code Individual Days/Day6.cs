using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day6
    {
        public static int PartOne(string input)
        {
            var splitCoords = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var coords = splitCoords.Select(c => c.Split(",", StringSplitOptions.RemoveEmptyEntries)).Select(c => c.Select(i => Convert.ToInt32(i)).ToArray()).Select(c => (X: c[0], Y: c[1])).ToArray();
            var maxXCoord = coords.Max(x => x.X);
            var maxYCoord = coords.Max(y => y.Y);
            var grid = new int[maxXCoord + 2, maxYCoord + 2];
            for (int x = 0; x <= maxXCoord + 1; x++)
            {
                for (int y = 0; y <= maxYCoord + 1; y++)
                {
                    var distances = coords.Select((c, i) => (i, dist: Math.Abs(c.X - x) + Math.Abs(c.Y - y))).OrderBy(c => c.dist).ToArray();

                    grid[x, y] = distances[1].dist != distances[0].dist ? distances[0].i : -1;
                }
            }  
            var excluded = new List<int>();
            var counts = Enumerable.Range(-1, coords.Length + 1).ToDictionary(i => i, _ => 0);
            for (int x = 0; x <= maxXCoord + 1; x++)
            {
                for (int y = 0; y <= maxYCoord + 1; y++)
                {
                    if (x == 0 || y == 0 || x == maxXCoord + 1 || y == maxYCoord + 1)
                    {
                        excluded.Add(grid[x, y]);
                    }
                    counts[grid[x, y]] += 1;
                }
            }
            excluded = excluded.Distinct().ToList();
            var total = counts.Where(kvp => !excluded.Contains(kvp.Key)).OrderByDescending(kvp => kvp.Value);
            return total.First().Value;
        }
        public static int PartTwo(string input)
        {
            var splitCoords = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            var coords = splitCoords.Select(c => c.Split(",", StringSplitOptions.RemoveEmptyEntries)).Select(c => c.Select(i => Convert.ToInt32(i)).ToArray()).Select(c => (X: c[0], Y: c[1])).ToArray();
            var maxXCoord = coords.Max(x => x.X);
            var maxYCoord = coords.Max(y => y.Y);
            var grid = new int[maxXCoord + 2, maxYCoord + 2];
            var safeCount = 0;
            for (int x = 0; x <= maxXCoord + 1; x++)
            {
                for (int y = 0; y <= maxYCoord + 1; y++)
                {
                    var distances = coords.Select((c, i) => (i, dist: Math.Abs(c.X - x) + Math.Abs(c.Y - y))).OrderBy(c => c.dist).ToArray();

                    grid[x, y] = distances[1].dist != distances[0].dist ? distances[0].i : -1;

                    if (distances.Sum(c => c.dist) < 10000)
                    {
                        safeCount++;
                    }
                }
            }
            return safeCount;
        }
    }
}
