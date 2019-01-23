using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day22
    {
        public enum RegionType
        {
            unknown = -1,
            rocky = 0,
            wet = 1,
            narrow = 2
        }
        public enum Tools
        {
            neither = 0,
            torch = 1,
            climbingGear = 2
        }
        public static (int x, int y)[] Directions = new(int x, int y)[] { (0, -1), (0, 1), (1, 0), (-1, 0)};
        public static long PartOne(string input)
        {
            var depth = 4002;
            var (targetX, targetY) = (5, 746);
            long riskLevel = 0;
            var map = new int[targetX + 1][];
            for(var i =0; i <= targetX; i++)
            {
                map[i] = new int[targetY + 1];
            }
            for(var y = 0; y <= targetY; y++)
            {
                for(var x =0; x <=targetX; x++)
                {
                    map[x][y] = GetGeologicalIndex(map, depth, x, y, targetX, targetY);
                    riskLevel += (long)GetType(depth, map[x][y]);
                }
            }
            return riskLevel;
        }
        public static int PartTwo(string input)
        {
            var depth = 4002;
            var (targetX, targetY) = (5, 746);
            var map = new int[targetX + 50][];
            var distance = new Dictionary<(int x, int y, int tools), int>();
            var queue = new Dictionary<(int x, int y, int tool), int>();
            for (var i = 0; i < targetX + 50; i++)
            {
                map[i] = new int[targetY + 50];
            }
            for (var y = 0; y < targetY + 50; y++)
            {
                for (var x = 0; x < targetX + 50; x++)
                {
                    map[x][y] = GetGeologicalIndex(map, depth, x, y, targetX, targetY);
                    for (var t = 0; t <= 2; t++)
                    {
                        distance[(x, y, t)] = int.MaxValue;
                    }
                }
            }
            distance[(0, 0, (int)Tools.torch)] = 0;
            queue.Add((0, 0, (int)Tools.torch), 0);
            while (queue.Count > 0)
            {
                var set = queue.SortedDequeue();
                foreach (var (x, y) in Directions)
                {
                    CheckNeighbors(map, distance, queue, set.Key.x, set.Key.y, set.Key.tool, x, y, targetX, targetY, depth);
                }
                CheckCorrectTool(map, distance, queue, set.Key.x, set.Key.y, set.Key.tool, depth, Tools.neither);
                CheckCorrectTool(map, distance, queue, set.Key.x, set.Key.y, set.Key.tool, depth, Tools.torch);
                CheckCorrectTool(map, distance, queue, set.Key.x, set.Key.y, set.Key.tool, depth, Tools.climbingGear);
            }
            return distance[(targetX, targetY, (int)Tools.torch)];
        }
        private static bool CheckTool(int depth, Tools tool, int geoIndex)
        {
            switch (GetType(depth, geoIndex))
            {
                case RegionType.rocky:
                    return tool == Tools.climbingGear || tool == Tools.torch;

                case RegionType.wet:
                    return tool == Tools.climbingGear || tool == Tools.neither;

                case RegionType.narrow:
                    return tool == Tools.neither || tool == Tools.torch;
            }
            return false;
        }
        private static void CheckNeighbors(int[][] map, Dictionary<(int x, int y, int tools), int> distance, Dictionary<(int x, int y, int tool), int> queue, int x, int y, int tool, int xOffSet, int yOffSet, int targetX, int targetY, int depth)
        {
            if ((x + xOffSet < 0) || (y + yOffSet < 0) || (x + xOffSet > targetX + 49) || (y + yOffSet > targetY + 49))
            {
                return;
            }
            if (CheckTool(depth, (Tools)tool, map[x + xOffSet][y + yOffSet]))
            {
                var dist = distance[(x, y, tool)];
                if (distance[(x + xOffSet, y + yOffSet, tool)] > 1 + dist)
                {
                    distance[(x + xOffSet, y + yOffSet, tool)] = 1 + dist;

                    if (!queue.ContainsKey((x + xOffSet, y + yOffSet, tool)))
                    {
                        queue.Add((x + xOffSet, y + yOffSet, tool), 1 + dist);
                    }
                    else
                    {
                        queue[(x + xOffSet, y + yOffSet, tool)] = 1 + dist;
                    }
                }
            }
        }
        private static void CheckCorrectTool(int[][] map, Dictionary<(int x, int y, int tools), int> distance, Dictionary<(int x, int y, int tool), int> queue, int x, int y, int tool, int depth, Tools correctTool)
        {
            if ((Tools)tool == correctTool)
            {
                return;
            }
            if (CheckTool(depth, correctTool, map[x][y]))
            {
                var dist = distance[(x, y, tool)];
                if (distance[(x, y, (int)correctTool)] > 7 + dist)
                {
                    distance[(x, y, (int)correctTool)] = 7 + dist;
                    if (!queue.ContainsKey((x, y, (int)correctTool)))
                    {
                        queue.Add((x, y, (int)correctTool), 7 + dist);
                    }
                    else
                    {
                        queue[(x, y, (int)correctTool)] = 7 + dist;
                    }
                }
            }
        }
        private static int GetGeologicalIndex(int[][] map, int depth, int x, int y, int targetX, int targetY)
        {
            if (x == 0 && y == 0)
            {
                return 0;
            }

            if (x == targetX && y == targetY)
            {
                return 0;
            }

            if (y == 0)
            {
                return x * 16807;
            }

            if (x == 0)
            {
                return y * 48271;
            }

            return ErosionLevel(depth, map[x - 1][y]) * ErosionLevel(depth, map[x][y - 1]);
        }
        private static int ErosionLevel(int depth, int geoIndex) => (geoIndex + depth) % 20183;

        private static RegionType GetType(int depth, int geoIndex)
        {
            switch(ErosionLevel(depth, geoIndex) % 3)
            {
                case 0:
                    return RegionType.rocky;
                case 1:
                    return RegionType.wet;
                case 2:
                    return RegionType.narrow;
            }
            return RegionType.unknown;
        }
        
    }
    public static class Extensions
    {
        public static KeyValuePair<(int x, int y, int tool), int> SortedDequeue(this Dictionary<(int x, int y, int tool), int> dictionary)
        {
            var sorted = dictionary.OrderBy(kvp => kvp.Value).ToList();
            var first = sorted.Select(kvp => kvp).First();
            dictionary.Remove(first.Key);
            return first;
            
        }
    }
}
