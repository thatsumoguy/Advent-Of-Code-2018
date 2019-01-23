using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day18
    {
        public static long TotalTrees;
        public static long TotalYards;
        public static long PartOne(string input)
        {
            var grid = new char[50][];
            for (var j = 0; j < 50; j++)
            {
                grid[j] = new char[50];
            }
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    grid[y][x] = lines[y][x];
                    if (grid[y][x] == '|')
                    {
                        TotalTrees++;
                    }
                    if (grid[y][x] == '#')
                    {
                        TotalYards++;
                    }
                }
            }

            for (var i = 1; i <= 10; i++)
            {
                grid = MinutePass(grid);
            }
            return TotalTrees * TotalYards;
        }
        public static long PartTwo(string input)
        {
            var totals = new Dictionary<long, List<int>>();
            var grid = new char[50][];
            for (var j = 0; j < 50; j++)
            {
                grid[j] = new char[50];
            }
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (var y = 0; y < lines.Length; y++)
            {
                for (var x = 0; x < lines[y].Length; x++)
                {
                    grid[y][x] = lines[y][x];
                    if (grid[y][x] == '|')
                    {
                        TotalTrees++;
                    }
                    if (grid[y][x] == '#')
                    {
                        TotalYards++;
                    }
                }
            }
            for (var i = 1; i <= 1000; i++)
            {
                grid = MinutePass(grid);
                var score = TotalTrees * TotalYards;
                if(!totals.ContainsKey(score))
                {
                    totals.Add(score, new List<int>());
                }
                if(totals.ContainsKey(score))
                {
                    if(totals[score].Count > 3)
                    {
                        var period = (i +1 ) - totals[score].Max();
                        if((i+1) % period == 1000000000 % period)
                        {
                            break;
                        }
                    }
                }
                totals[score].Add(i);
            }
            return TotalTrees * TotalYards;
        }
        public static char[][] MinutePass(char[][] grid)
        {
            var tempGrid = new char[50][];
            for (var j = 0; j < 50; j++)
            {
                tempGrid[j] = new char[50];
                Array.Copy(grid[j], tempGrid[j], grid[j].Length);
            }
            for (var y = 0; y < grid.Length; y++)
            {
                for (var x = 0; x < grid[y].Length; x++)
                {
                    var trees = 0;
                    var lumber = 0;
                    for (var dy = -1; dy < 2; dy++)
                    {
                        for (var dx = -1; dx < 2; dx++)
                        {
                            if (dy == 0 && dx == 0)
                            {
                                continue;
                            }

                            var newX = x + dx;
                            var newY = y + dy;
                            if ((newX >= 0) && (newY >= 0) && newX < grid[y].Length && newY < grid.Length)
                            {
                                try
                                {
                                    if (grid[newY][newX] == '|')
                                    {
                                        trees++;
                                    }
                                    if (grid[newY][newX] == '#')
                                    {
                                        lumber++;
                                    }
                                }
                                catch (IndexOutOfRangeException)
                                {
                                    continue;
                                }

                            }
                        }
                    }
                    switch (grid[y][x])
                    {
                        case '.':
                            if (trees >= 3)
                            {
                                tempGrid[y][x] = '|';
                                TotalTrees++;
                            }
                            break;
                        case '|':
                            if (lumber >= 3)
                            {
                                tempGrid[y][x] = '#';
                                TotalYards++;
                                TotalTrees--;
                            }
                            break;
                        case '#':
                            if (lumber >= 1 && trees >= 1)
                            {
                                tempGrid[y][x] = '#';
                            }
                            else
                            {
                                tempGrid[y][x] = '.';
                                TotalYards--;
                            }
                            break;
                    }
                }
            }
            return tempGrid;
        }
    }
}
