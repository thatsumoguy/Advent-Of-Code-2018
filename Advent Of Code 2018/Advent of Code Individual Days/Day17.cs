using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day17
    {
        static char[,] grid;
        static int maxY = 0;
        static int minY = int.MaxValue;

        public static int PartOne(string input)
        {
            var lines = input.Split('\n');
            var x = 2000;
            var y = 2000;

            grid = new char[x, y];

            foreach (var line in lines)
            {
                var l = line.Split(new[] { '=', ',', '.' });

                if (l[0] == "x")
                {
                    x = int.Parse(l[1]);
                    y = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var a = y; a <= len; a++)
                    {
                        grid[x, a] = '#';
                    }
                }
                else
                {
                    y = int.Parse(l[1]);
                    x = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var a = x; a <= len; a++)
                    {
                        grid[a, y] = '#';
                    }
                }

                if (y > maxY)
                {
                    maxY = y;
                }

                if (y < minY)
                {
                    minY = y;
                }
            }

            var springX = 500;
            var springY = 0;
            
            GoDown(springX, springY);
            
            var t = 0;
            for (y = minY; y < grid.GetLength(1); y++)
            {
                for (x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y] == 'W' || grid[x, y] == '|')
                    {
                        t++;
                    }
                }
            }

            return t;
        }
        public static int PartTwo(string input)
        {
            var lines = input.Split('\n');
            var x = 2000;
            var y = 2000;

            grid = new char[x, y];

            foreach (var line in lines)
            {
                var l = line.Split(new[] { '=', ',', '.' });

                if (l[0] == "x")
                {
                    x = int.Parse(l[1]);
                    y = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var a = y; a <= len; a++)
                    {
                        grid[x, a] = '#';
                    }
                }
                else
                {
                    y = int.Parse(l[1]);
                    x = int.Parse(l[3]);
                    var len = int.Parse(l[5]);
                    for (var a = x; a <= len; a++)
                    {
                        grid[a, y] = '#';
                    }
                }

                if (y > maxY)
                {
                    maxY = y;
                }

                if (y < minY)
                {
                    minY = y;
                }
            }

            var springX = 500;
            var springY = 0;
            
            GoDown(springX, springY);
            
            var t = 0;
            for (y = minY; y < grid.GetLength(1); y++)
            {
                for (x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x,y] == 'W')
                    {
                        t++;
                    }
                }
            }

            return t;
        }
        private static bool SpaceTaken(int x, int y)
        {
            return grid[x, y] == '#' || grid[x, y] == 'W';
        }

        public static void GoDown(int x, int y)
        {
            grid[x, y] = '|';
            while (grid[x, y + 1] != '#' && grid[x, y + 1] != 'W')
            {

                y++;
                if (y > maxY)
                {
                    return;
                }
                grid[x, y] = '|';
            };

            do
            {
                bool goDownLeft = false;
                bool goDownRight = false;
                
                int minX;
                for (minX = x; minX >= 0; minX--)
                {
                    if (SpaceTaken(minX, y + 1) == false)
                    {
                        goDownLeft = true;
                        break;
                    }

                    grid[minX, y] = '|';

                    if (SpaceTaken(minX - 1, y))
                    {
                        break;
                    }

                }

                int maxX;
                for (maxX = x; maxX < grid.GetLength(0); maxX++)
                {
                    if (SpaceTaken(maxX, y + 1) == false)
                    {
                        goDownRight = true;

                        break;
                    }

                    grid[maxX, y] = '|';

                    if (SpaceTaken(maxX + 1, y))
                    {
                        break;
                    }

                }
                
                if (goDownLeft)
                {
                    if (grid[minX, y] != '|')
                    {
                        GoDown(minX, y);
                    }
                        
                }

                if (goDownRight)
                {
                    if (grid[maxX, y] != '|')
                    {
                        GoDown(maxX, y);
                    }
                        
                }

                if (goDownLeft || goDownRight)
                {
                    return;
                }
                
                for (int a = minX; a < maxX + 1; a++)
                {
                    grid[a, y] = 'W';
                }

                y--;
            }
            while (true);
        }
    }
}
