using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day1
    {
        public static int PartOne(string input)
        {
            var frequency = input.Split("\n").Select(x => int.Parse(x)).Sum();
            return frequency;
        }
        public static int PartTwo(string input)
        {
            var frequency = input.Split("\n").Select(x => int.Parse(x)).ToList();
            var set = new HashSet<int>();
            var i = 0;
            var currentFreq = 0;
            while (true)
            {
                currentFreq += frequency[i % frequency.Count];
                if (set.Contains(currentFreq))
                {
                    return currentFreq;
                }
                set.Add(currentFreq);
                i++;
            }
        }
    }
}
