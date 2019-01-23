using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day14
    {
        public static string PartOne(string input)
        {
            var iterations = int.Parse(input);
            var recipes = new List<int> { 3, 7 };
            var elf1 = 0;
            var elf2 = 1;
            var winningScore = "";
            while(recipes.Count < iterations +10)
            {
                var currentSum = recipes[elf1] + recipes[elf2];
                recipes.AddRange(currentSum.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)));
                elf1 = (elf1 + recipes[elf1] + 1) % recipes.Count;
                elf2 = (elf2 + recipes[elf2] + 1) % recipes.Count;
            }
            foreach(var recipe in recipes.Skip(iterations).Take(10))
            {
                winningScore += recipe;
            }
            return winningScore;
        }

        public static int PartTwo(string input)
        {
            var recipesToCheck = new int[] { 5,8,0,7,4,1 };
            var index = 0;
            var positionToCheck = 0;
            var notFound = true;
            var recipes = new List<int> { 3, 7 };
            var elf1 = 0;
            var elf2 = 1;
            while (notFound)
            {
                var currentSum = recipes[elf1] + recipes[elf2];
                recipes.AddRange(currentSum.ToString().ToCharArray().Select(x => (int)Char.GetNumericValue(x)));
                elf1 = (elf1 + recipes[elf1] + 1) % recipes.Count;
                elf2 = (elf2 + recipes[elf2] + 1) % recipes.Count;

                while (index + positionToCheck < recipes.Count)
                {
                    if (recipesToCheck[positionToCheck] == recipes[index + positionToCheck])
                    {
                        if (positionToCheck == recipesToCheck.Length - 1)
                        {
                            notFound = false;
                            break;
                        }
                        positionToCheck++;
                    }
                    else
                    {
                        positionToCheck = 0;
                        index++;
                    }
                }
            }
            return index;
        }
    }
}
