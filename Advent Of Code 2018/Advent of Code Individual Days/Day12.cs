using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day12
    {
        public static long PartOne(string input)
        {
            return PotGenerations(input, 20);
        }
        public static long PartTwo(string input)
        {
            return PotGenerations(input, 50000000000);
        }
        private static long PotGenerations(string input, long maxGenerations)
        {
            var parts = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var initialState = parts[0].Remove(0, 15).Trim();
            var rules = new Dictionary<string, char>();
            for (var i = 1; i <= parts.Length - 1; i++)
            {
                rules.Add(parts[i].Substring(0, 5), parts[i].ElementAt(9));
            }
            var startZeroPos = 0;
            var sum = 0L;
            var initialDiff = 0L;
            var matchingDiffs = new List<long>();
            for (var i = 0L; i < maxGenerations; i++)
            {
                var currentZero = initialState.IndexOf('#');
                for (var s = 0; s < 4 - currentZero; s++)
                {
                    initialState = "." + initialState;
                    startZeroPos--;
                }
                var temp = initialState;
                var currentEnd = initialState.LastIndexOf('#');
                for (var e = 0; e < 5 - (temp.Length - currentEnd); e++)
                {
                    initialState += ".";
                }
                var currentState = initialState.ToCharArray();
                for (var x = 0; x < initialState.Length - 5; x++)
                {
                    if (rules.ContainsKey(initialState.Substring(x, 5)))
                    {
                        currentState[x + 2] = rules[initialState.Substring(x, 5)];
                    }
                    else
                    {
                        currentState[x + 2] = '.';
                    }
                }
                var currentSum = 0L;
                for (var y = 0; y < currentState.Length; y++)
                {
                    if (currentState[y] == '#')
                    {
                        currentSum += y + startZeroPos;
                    }

                }
                var currentDiff = currentSum - sum;

                if (currentDiff == initialDiff)
                {
                    if (matchingDiffs.Count(x => x == currentDiff) == 3)
                    {
                        currentSum = currentSum + (50000000000 - (i + 1L)) * currentDiff;
                        sum = currentSum;
                        break;
                    }
                    matchingDiffs.Add(currentDiff);
                }

                initialDiff = currentDiff;
                initialState = new string(currentState);
                sum = currentSum;
            }

            return sum;
        }
    }
}
