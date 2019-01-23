using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day7
    {
        public static string PartOne(string input)
        {
            var instructions = new List<(string first, string second)>();
            input.Split("\n").ToList().ForEach(x => instructions.Add((x.ElementAt(5).ToString(), x.ElementAt(36).ToString())));
            var sortedInstructions = instructions.Select(s => s.first).Concat(instructions.Select(s => s.second)).Distinct().OrderBy(a => a).ToList();
            var output = "";
            while(sortedInstructions.Count > 0)
            {
                var correctInstruction = sortedInstructions.Where(s => !instructions.Any(d => d.second == s)).First();
                output += correctInstruction;

                sortedInstructions.Remove(correctInstruction);
                instructions.RemoveAll(s => s.first == correctInstruction);
            }
            return output;
        }
        public static int PartTwo(string input)
        {
            var instructions = new List<(string first, string second)>();
            input.Split("\n").ToList().ForEach(x => instructions.Add((x.ElementAt(5).ToString(), x.ElementAt(36).ToString())));
            var sortedInstructions = instructions.Select(s => s.first).Concat(instructions.Select(s => s.second)).Distinct().OrderBy(a => a).ToList();
            var completedTime = 0;
            var workers = new int[] { 0, 0, 0, 0, 0 };
            var doneList = new List<(string step, int finish)>();
            while (instructions.Count > 0 || workers.Any(x => x > completedTime))
            {
                doneList.Where(d => d.finish <= completedTime).ToList().ForEach(x => instructions.RemoveAll(d => d.first == x.step));
                doneList.RemoveAll(d => d.finish <= completedTime);
                var correctInstruction = sortedInstructions.Where(s => !instructions.Any(d => d.second == s)).ToList();
                for(var x =0; x < workers.Count() && correctInstruction.Count > 0; x++)
                {
                    if(workers[x] <= completedTime)
                    {
                        workers[x] = ((correctInstruction.First()[0] - 'A') + 61) + completedTime;
                        sortedInstructions.Remove(correctInstruction.First());
                        doneList.Add((correctInstruction.First(), workers[x]));
                        correctInstruction.RemoveAt(0);
                    } 
                }
                completedTime++;
            }
            return completedTime;
        }
    }
}
