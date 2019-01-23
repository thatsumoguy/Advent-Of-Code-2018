using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day4
    {
        public static List<GuardData> AllGuardAction { get; set; }
        public static Dictionary<int, Guard> AllGuards { get; set; }
        public static int PartOne(string input)
        {
            var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            AllGuardAction = SetData(lines);
            AllGuards = GuardMovements();

            var sleepiestGuard = AllGuards.OrderByDescending(t => t.Value.TotalTimeAsleep).First();
            
            var mostTimeAsleep = sleepiestGuard.Value.GuardSleepTime.OrderByDescending(x => x.Value).First().Key;
            var sleepiestGuardId = sleepiestGuard.Key;
            return sleepiestGuardId * mostTimeAsleep;
        }

        public static int PartTwo(string input)
        {
            var lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            AllGuardAction = SetData(lines);
            AllGuards = GuardMovements();
            int maxMinute = 0;
            int minuteSpentAsleepMost = 0;
            int guardId = 0;

            foreach(var guard in AllGuards)
            {
                foreach(var minute in guard.Value.GuardSleepTime)
                {
                    if(minute.Value > maxMinute)
                    {
                        maxMinute = minute.Value;
                        minuteSpentAsleepMost = minute.Key;
                        guardId = guard.Key;
                    }
                }
            }
            return minuteSpentAsleepMost * guardId;
        }

        private static List<GuardData> SetData(string[] input)
        {
            List<GuardData> GuardAction = new List<GuardData>();
            char[] separators = new char[] { '[', ']' };
            foreach(var line in input)
            {
                var parts = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                GuardData guardData = new GuardData()
                {
                    Date = DateTime.Parse(parts[0]),
                    Action = parts[1].Remove(0, 1)
                };
                GuardAction.Add(guardData);
            }
            return GuardAction.OrderBy(t => t.Date).ToList();
        }

        private static Dictionary<int, Guard> GuardMovements()
        {
            Dictionary<int, Guard> guards = new Dictionary<int, Guard>();
            int currentGuardId = 0;
            int minutesAsleep = 0;

            foreach (var guardAction in AllGuardAction)
            {
                if(guardAction.Action.Contains("Guard"))
                {
                    string[] guardId = guardAction.Action.Split(new char[] { ' ', '#' }, StringSplitOptions.RemoveEmptyEntries);
                    currentGuardId = int.Parse(guardId[1]);

                    if (!guards.ContainsKey(currentGuardId))
                    {
                        guards.Add(currentGuardId, new Guard());
                    }
                }
                else if (guardAction.Action.Contains("falls asleep"))
                {
                    minutesAsleep = guardAction.Date.Minute;
                }
                else if(guardAction.Action.Contains("wakes up"))
                {
                    
                    for(var i = minutesAsleep; i < guardAction.Date.Minute; i++)
                    {
                        if (!guards[currentGuardId].GuardSleepTime.ContainsKey(i))
                        {
                            guards[currentGuardId].GuardSleepTime.Add(i, 0);
                        }
                        guards[currentGuardId].GuardSleepTime[i] += 1;
                        guards[currentGuardId].TotalTimeAsleep += 1;
                    }
                }
            }
            return guards;
        }
    }

    class GuardData
    {
        public DateTime Date { get; set; }
        public string Action { get; set; }
    }

    class Guard
    {
        public Guard()
        {
            Actions = new List<GuardData>();
            GuardSleepTime = new Dictionary<int, int>();
        }

        public List<GuardData> Actions { get; set; }
        public Dictionary<int, int> GuardSleepTime { get; set; }
        public int TotalTimeAsleep { get; set; }
    }
}
