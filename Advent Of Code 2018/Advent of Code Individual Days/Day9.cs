using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day9
    {
        public static long PartOne(string input)
        {
            var elfCount = 468;
            var marbleCount = 71843;
            return Compute(elfCount, marbleCount);
        }
        public static long PartTwo(string input)
        {
            var elfCount = 468;
            var marbleCount = 71843 * 100;
            return Compute(elfCount, marbleCount);
        }
        public static long Compute(int elfCount, long marbleCount)
        {
            var scores = new long[elfCount];
            var placed = new LinkedList<int>();
            var current = placed.AddFirst(0);

            for (int m = 0; m < marbleCount; ++m)
            {
                if (((m + 1) % 23) == 0)
                {
                    for(var i =0; i < 7; i++)
                    {
                        current = current.Previous ?? placed.Last;
                    }
                    
                    scores[m % elfCount] += m + 1 + current.Value;

                    var tmp = current;
                    current = current.Next ?? placed.First;
                    placed.Remove(tmp);
                }
                else
                {
                    current = current.Next ?? placed.First;
                    current = placed.AddAfter(current, m + 1);
                }
            }
            return scores.Max();
        }
    }
}
