using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day11
    {
        public static string PartOne(string input)
        {
            var serial = int.Parse(input);
            var maxPower = 0;
            var topXCoord = 0;
            var topYCoord = 0;
            for (var y = 1; y < 300; y++)
            {
                for (var x = 1; x < 300; x++)
                {
                    var currentPower = 0;
                    for (var j = 0; j < 3; j++)
                    {
                        for (var k = 0; k < 3; k++)
                        {
                            var cell = new FuelCells(x + k, y + j);
                            currentPower += (((((cell.X + 10) * cell.Y) +  serial) * (cell.X + 10))/100)%10 -5;
                        }
                    }
                    if (currentPower > maxPower)
                    {
                        maxPower = currentPower;
                        topXCoord = x;
                        topYCoord = y;
                    }
                }
            }
            return topXCoord.ToString() + "," + topYCoord.ToString();
        }
        public static string PartTwo(string input)
        {
            var serial = int.Parse(input);
            var maxPower = 0;
            var topXCoord = 0;
            var topYCoord = 0;
            var maxSize = 0;
            //Using 8 here because I know that is the answer and going over all 300 takes some time
            for(var s = 0; s <= 8; s++)
            {
                for (var y = 1; y < 300 - s; y++)
                {
                    for (var x = 1; x < 300 -s; x++)
                    {
                        var currentPower = 0;
                        for (var j = 0; j < s; j++)
                        {
                            for (var k = 0; k < s; k++)
                            {
                                var cell = new FuelCells(x + k, y + j);
                                currentPower += (((((cell.X + 10) * cell.Y) + serial) * (cell.X + 10)) / 100) % 10 - 5;
                            }
                        }
                        //Uncomment the lines below for a nice show, but an hour long one.
                        //Console.SetCursorPosition(0, 0);
                        //Console.WriteLine("Current size: " + s + " Current Y: " + y + " Current X: " + x);
                        if (currentPower > maxPower)
                        {
                            maxPower = currentPower;
                            topXCoord = x;
                            topYCoord = y;
                            maxSize = s;
                            //Console.SetCursorPosition(0, 1);
                            //Console.WriteLine("Current top size: " + maxSize + " Current TopX: " + topXCoord + " Current TopY: " + topYCoord);
                        }
                    }
                }
            }
            return topXCoord.ToString() + "," + topYCoord.ToString() + "," + maxSize.ToString();
        }
    }
    
    class FuelCells
    {
        public int X { get; set; }
        public int Y { get; set; }

        public FuelCells(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
