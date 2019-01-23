using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day15
    {
        private static readonly (int dx, int dy)[] movements = { (0, -1), (-1, 0), (1, 0), (0, 1) };
        private static string[] map;
        public static int? PartOne(string input)
        {
            var combatLines = input.Split('\n');
            var elvesAndGoblins = MapEOrG(combatLines, 3);
            map = combatLines.Select(s => s.Replace('G', '.').Replace('E', '.')).ToArray();
            return RunCombat(map, elvesAndGoblins, false);
        }
        public static int? PartTwo(string input)
        {
            var combatLines = input.Split('\n');
            string[] map;
            for (int attackPower = 4; ; attackPower++)
            {
                var elvesAndGoblins = MapEOrG(combatLines, attackPower);
                map = combatLines.Select(s => s.Replace('G', '.').Replace('E', '.')).ToArray();
                int? outcome = RunCombat(map, elvesAndGoblins, true);
                if (outcome.HasValue)
                {
                    return outcome;
                }
            }
        }
        private static List<ElvesAndGoblins> MapEOrG(string[] combatLines, int elfAttack)
        {
            var elvesAndGoblins = new List<ElvesAndGoblins>();
            for (var y = 0; y < combatLines.Length; y++)
            {
                for (var x = 0; x < combatLines[y].Length; x++)
                {
                    switch (combatLines[y][x])
                    {
                        case 'G':
                            elvesAndGoblins.Add(new ElvesAndGoblins { X = x, Y = y, AP = 3, HP = 200, goblin = true });
                            break;
                        case 'E':
                            elvesAndGoblins.Add(new ElvesAndGoblins { X = x, Y = y, AP = elfAttack, HP = 200, goblin = false });
                            break;
                    }
                }
            }
            return elvesAndGoblins;
        }
        private static int? RunCombat(string[] map, List<ElvesAndGoblins> elvesAndGoblins, bool partTwo)
        {
            for (var rounds = 0; ; rounds++)
            {
                elvesAndGoblins = elvesAndGoblins.OrderBy(e => e.Y).ThenBy(e => e.X).ToList();
                for (var i = 0; i < elvesAndGoblins.Count(); i++)
                {
                    var eOrG = elvesAndGoblins[i];
                    var availableTargets = elvesAndGoblins.Where(t => t.goblin != eOrG.goblin).ToList();
                    if (availableTargets.Count() == 0)
                    {
                        return rounds * elvesAndGoblins.Select(e => e.HP).Sum();
                    }
                    if (!availableTargets.Any(t => Math.Abs(eOrG.X - t.X) + Math.Abs(eOrG.Y - t.Y) == 1))
                    {
                        EOrGTurn(eOrG, availableTargets, map, elvesAndGoblins);
                    }
                    var bestAvailable = availableTargets.Where(t => Math.Abs(eOrG.X - t.X) + Math.Abs(eOrG.Y - t.Y) == 1).OrderBy(t => t.HP).ThenBy(t => t.Y).ThenBy(t => t.X).FirstOrDefault();
                    if (bestAvailable == null)
                    {
                        continue;
                    }
                    bestAvailable.HP -= eOrG.AP;
                    if (bestAvailable.HP > 0)
                    {
                        continue;
                    }
                    if (partTwo && !bestAvailable.goblin)
                    {
                        return null;
                    }
                    int index = elvesAndGoblins.IndexOf(bestAvailable);
                    elvesAndGoblins.RemoveAt(index);
                    if (index < i)
                    {
                        i--;
                    }

                }
            }
        }
        private static void EOrGTurn(ElvesAndGoblins eOrG, List<ElvesAndGoblins> availableTargets, string[] map, List<ElvesAndGoblins> elvesAndGoblins)
        {
            var inRange = new HashSet<(int x, int y)>();
            foreach (var target in availableTargets)
            {
                foreach ((int dx, int dy) in movements)
                {
                    (int nx, int ny) = (target.X + dx, target.Y + dy);
                    if (IsOpen(nx, ny, map, elvesAndGoblins))
                    {
                        inRange.Add((nx, ny));
                    }
                }
            }

            var queue = new Queue<(int x, int y)>();
            var prevs = new Dictionary<(int x, int y), (int px, int py)>();
            queue.Enqueue((eOrG.X, eOrG.Y));
            prevs.Add((eOrG.X, eOrG.Y), (-1, -1));
            while (queue.Count > 0)
            {
                (int x, int y) = queue.Dequeue();
                foreach ((int dx, int dy) in movements)
                {
                    (int x, int y) neighbors = (x + dx, y + dy);
                    if (prevs.ContainsKey(neighbors) || !IsOpen(neighbors.x, neighbors.y, map, elvesAndGoblins))
                    {
                        continue;
                    }
                    queue.Enqueue(neighbors);
                    prevs.Add(neighbors, (x, y));
                }
            }

            List<(int x, int y)> getPath(int destX, int destY)
            {
                if (!prevs.ContainsKey((destX, destY)))
                {
                    return null;
                }
                var path = new List<(int x, int y)>();
                (int x, int y) = (destX, destY);
                while (x != eOrG.X || y != eOrG.Y)
                {
                    path.Add((x, y));
                    (x, y) = prevs[(x, y)];
                }

                path.Reverse();
                return path;
            }

            var paths = inRange.Select(t => (t.x, t.y, path: getPath(t.x, t.y))).Where(t => t.path != null).OrderBy(t => t.path.Count).ThenBy(t => t.y).ThenBy(t => t.x).ToList();

            var bestPath = paths.FirstOrDefault().path;

            if (bestPath != null)
            {
                (eOrG.X, eOrG.Y) = bestPath[0];
            }
        }
        private static bool IsOpen(int x, int y, string[] map, List<ElvesAndGoblins> elvesAndGoblins) => map[y][x] == '.' && elvesAndGoblins.All(u => u.X != x || u.Y != y);
    }
    public class ElvesAndGoblins
    {
        public int X, Y;
        public bool goblin;
        public int HP = 200;
        public int AP;
    }
}