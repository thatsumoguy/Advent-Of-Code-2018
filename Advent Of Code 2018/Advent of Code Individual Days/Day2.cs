using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day2
    {
        public static int PartOne(string input)
        {
            var totaltwos = input.Split("\n").Where(x => x.GroupBy(y => y).Any(z => z.Count() == 2)).Count();
            var totalthrees = input.Split("\n").Where(x => x.GroupBy(y => y).Any(z => z.Count() == 3)).Count();

            return totalthrees * totaltwos;
        }
        public static string PartTwo(string input)
        {
            var words = input.Split("\r\n").Distinct().ToArray();
            for (var i = 0; i < words[0].Length; i++)
            {
                var x = words.Select(w => w.Remove(i, 1)).GroupBy(w => w).FirstOrDefault(g => g.Count() > 1);
                if (x != null)
                {
                    var common = x.First();
                    return common;
                }
            }
            return "";
        }
    }
}
