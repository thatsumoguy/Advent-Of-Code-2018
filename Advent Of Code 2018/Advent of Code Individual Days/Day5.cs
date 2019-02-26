using System.Collections.Generic;

namespace Advent_Of_Code_2018
{
    class Day5
    {
        public static int PartOne(string input)
        {
            var output = new Stack<char>();

            foreach(var c in input)
            {
                if(output.Count == 0)
                {
                    output.Push(c);
                }
                else
                {
                    var current = output.Peek();
                    var same = c != current && char.ToUpper(c) == char.ToUpper(current);
                    if(same)
                    {
                        output.Pop();
                    }
                    else
                    {
                        output.Push(c);
                    }
                }
            }
            return output.Count;
        }
        public static int PartTwo(string input)
        {
            var min = int.MaxValue;
            for(var i = 'a'; i < 'z'; i++)
            {
                var newPolymor = input.Replace(i.ToString(), "").Replace(char.ToUpper(i).ToString(), "");
                var output = new Stack<char>();

                foreach (var c in newPolymor)
                {
                    if (output.Count == 0)
                    {
                        output.Push(c);
                    }
                    else
                    {
                        var current = output.Peek();
                        var same = c != current && char.ToUpper(c) == char.ToUpper(current);
                        if (same)
                        {
                            output.Pop();
                        }
                        else
                        {
                            output.Push(c);
                        }
                    }
                }
                if(output.Count < min)
                {
                    min = output.Count;
                }
            }
            return min;
        }
    }
}
