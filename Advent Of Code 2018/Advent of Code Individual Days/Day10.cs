using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day10
    {
        public static int time = 0;
        public static string PartOne(string input)
        {
            var points = input.Split(new string[] { "\n", "position=<", "velocity=<", ">", ", ", " ", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            var point = new Points();
            var allPoints = new List<Points>();
            var message = "";
            for (var i = 0; i < points.Count(); i += 4)
            {
                allPoints.Add(new Points
                {
                    XPosition = points[i],
                    YPosition = points[i + 1],
                    XVelocity = points[i + 2],
                    YVelocity = points[i + 3]
                });
            }
            var (xPosMin, yPosMin, xPosMax, yPosMax) = point.PointsMaxMin(allPoints);
            while (true)
            {
                var current = allPoints.Select(x => x).ToList();
                allPoints = point.Translate(allPoints);
                var (newXPosMin, newYPosMin, newXPosMax, newYPosMax) = point.PointsMaxMin(allPoints);
                if ((newXPosMax - newXPosMin) > (xPosMax - xPosMin) || (newYPosMax - newYPosMin) > (yPosMax - yPosMin))
                {
                    for (var i = yPosMin; i <= yPosMax; i++)
                    {
                        for (var j = xPosMin; j <= xPosMax; j++)
                        {
                            message += current.Any(x => x.YPosition == i && x.XPosition == j) ? '#' : '.';
                        }
                        message += Environment.NewLine;
                    }
                    return message;
                }
                (xPosMin, yPosMin, xPosMax, yPosMax) = (newXPosMin, newYPosMin, newXPosMax, newYPosMax);
                time++;
            }
        }
        public static int PartTwo(string input)
        {
            return time;
        }
        
    }
    class Points
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public (int xPosMin, int yPosMin, int xPosMax, int yPosMax) PointsMaxMin(List<Points> allPoints) =>
            (xPosMin: allPoints.Min(x => x.XPosition), yPosMin: allPoints.Min(y => y.YPosition), xPosMax: allPoints.Max(x => x.XPosition), yPosMax: allPoints.Max(y => y.YPosition));
        public List<Points> Translate(List<Points> allPoints)
        {
            for (var i = 0; i < allPoints.Count(); i++)
            {
                allPoints[i] = new Points
                {
                    XPosition = allPoints[i].XPosition + allPoints[i].XVelocity,
                    YPosition = allPoints[i].YPosition + allPoints[i].YVelocity,
                    XVelocity = allPoints[i].XVelocity,
                    YVelocity = allPoints[i].YVelocity
                };
            }
            return allPoints;
        }
    }
}
