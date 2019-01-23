using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day3
    {
        public static int PartOne(string input)
        {
            var cords = input.Split('\n').Where(c => !string.IsNullOrWhiteSpace(c));
            var grid = new Dictionary<int, Dictionary<int, int>>();
            var overlaps = 0;
            foreach (var cord in cords)
            {
                var parts = GetParts(cord);
                var id = parts[0]; var xCord = parts[1]; var yCord = parts[2]; var w = parts[3]; var h = parts[4];
                grid = MapDict(grid, xCord, w, yCord, h);
            }
            for (int x = 0; x < 1000; ++x)
            {
                for (int y = 0; y < 1000; ++y)
                {
                    if (grid.TryGetValue(x, out var gridDictY))
                    {
                        if (gridDictY.TryGetValue(y, out var gridAtLocation))
                        {
                            if (gridAtLocation > 1)
                            {
                                overlaps++;
                            }
                        }
                    }
                }
            }
            return overlaps;
        }
        public static int PartTwo(string input)
        {
            var cords = input.Split('\n').Where(c => !string.IsNullOrWhiteSpace(c));
            var grid = new Dictionary<int, Dictionary<int, int>>();
            foreach (var cord in cords)
            {
                var parts = GetParts(cord);
                var id = parts[0]; var xCord = parts[1]; var yCord = parts[2]; var w = parts[3]; var h = parts[4];
                grid = MapDict(grid, xCord, w, yCord, h);
            }
            foreach (var cord in cords)
            {
                var parts = GetParts(cord);
                var id = parts[0]; var xCord = parts[1]; var yCord = parts[2]; var w = parts[3]; var h = parts[4];
                bool isCandidate = true;

                for (int x = xCord; x < xCord + w; ++x)
                {
                    for (int y = yCord; y < yCord + h; ++y)
                    {
                        if (grid.TryGetValue(x, out var gridDictY))
                        {
                            if (gridDictY.TryGetValue(y, out var gridAtLocation))
                            {
                                if (gridAtLocation > 1)
                                {
                                    isCandidate = false;
                                    break;
                                }
                            }
                        }
                    }
                }

                if (isCandidate)
                {
                    return id;
                }
            }
            
            return -1;
        }
        private static int[] GetParts(string cord)
        {
            return cord.Split(new[] { '#', '@', ' ', ',', ':', 'x' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        }
        private static Dictionary<int, Dictionary<int, int>> MapDict(Dictionary<int, Dictionary<int, int>> grid, int xCord, int w, int yCord, int h)
        {
            for (int x = xCord; x < xCord + w; ++x)
            {
                for (int y = yCord; y < yCord + h; ++y)
                {
                    if (!grid.TryGetValue(x, out var gridDictY))
                    {
                        gridDictY = new Dictionary<int, int>();
                        grid[x] = gridDictY;
                    }

                    if (!gridDictY.TryGetValue(y, out var gridAtLocation))
                    {
                        gridAtLocation = 0;
                    }

                    ++gridAtLocation;
                    gridDictY[y] = gridAtLocation;
                }
            }
            
            return grid;
        }
    }
}
