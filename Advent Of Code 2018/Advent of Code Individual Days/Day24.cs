using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_Of_Code_2018
{
    class Day24
    {
        public static int PartOne(string input)
        {
            var lines = input.Split("\n");
            return Fight(lines, 0).winningUnits;
        }
        public static int PartTwo (string input)
        {
            var lines = input.Split("\n");
            var boost = 1;
            while(!Fight(lines, boost).immuneLeft)
            {
                boost++;
            }
            return Fight(lines, boost).winningUnits;
        }
        public static (bool immuneLeft, int winningUnits) Fight(string[] lines, int boost)
        {
            var army = GetArmy(lines);
            foreach (var group in army)
            {
                if(group.ImmuneSystem)
                {
                    group.Damage += boost;
                }
            }
            var attack = true;
            while (attack)
            {
                attack = false;
                var remainingTarget = new HashSet<Group>(army);
                var targets = new Dictionary<Group, Group>();
                foreach (var g in army.OrderByDescending(g => g.EffectivePower).ThenByDescending(g => g.Initiative))
                {
                    var maxDamage = remainingTarget.Select(t => g.AmountOfDamage(t)).Max();
                    if (maxDamage > 0)
                    {
                        var possibleTargets = remainingTarget.Where(t => g.AmountOfDamage(t) == maxDamage);
                        targets[g] = possibleTargets.OrderByDescending(t => t.EffectivePower).ThenByDescending(t => t.Initiative).First();
                        remainingTarget.Remove(targets[g]);
                    }
                }
                foreach (var g in targets.Keys.OrderByDescending(g => g.Initiative))
                {
                    if (g.Units > 0)
                    {
                        var target = targets[g];
                        var damage = g.AmountOfDamage(target);
                        if (damage > 0 && target.Units > 0)
                        {
                            var dies = damage / target.HP;
                            target.Units = Math.Max(0, target.Units - dies);
                            if (dies > 0)
                            {
                                attack = true;
                            }
                        }
                    }
                }
                army = army.Where(g => g.Units > 0).ToList();
            }
            return (army.All(x => x.ImmuneSystem), army.Select(x => x.Units).Sum());
        }
        public static List<Group> GetArmy(string[] lines)
        {
            List<Group> army = new List<Group>();
            var immuneSystem = false;
            foreach (var line in lines)
            {
                if (line.Contains("Immune System:"))
                {
                    immuneSystem = true;
                }
                else if (line.Contains("Infection:"))
                {
                    immuneSystem = false;
                }
                else if (line != "")
                {
                    var reg = @"(\d+) units each with (\d+) hit points(.*)with an attack that does (\d+)(.*)damage at initiative (\d+)";
                    var match = Regex.Match(line, reg);
                    if (match.Success)
                    {
                        Group g = new Group
                        {
                            ImmuneSystem = immuneSystem,
                            Units = int.Parse(match.Groups[1].Value),
                            HP = int.Parse(match.Groups[2].Value),
                            Damage = int.Parse(match.Groups[4].Value),
                            AttackType = match.Groups[5].Value.Trim(),
                            Initiative = int.Parse(match.Groups[6].Value)
                        };
                        var weakness = match.Groups[3].Value.Trim();
                        if (weakness != "")
                        {
                            weakness = weakness.Substring(1, weakness.Length - 2);
                            foreach (var part in weakness.Split(";"))
                            {
                                var separator = part.Split(" to ");
                                var set = new HashSet<string>(separator[1].Split(", "));
                                var weak = separator[0].Trim();
                                if (weak == "immune")
                                {
                                    g.immuneTo = set;
                                }
                                else if (weak == "weak")
                                {
                                    g.weakTo = set;
                                }
                                else
                                {
                                    throw new Exception();
                                }
                            }
                        }
                        army.Add(g);
                    }
                }
            }
            return army;
        }
    }
    class Group
    {
        public bool ImmuneSystem { get; set; }
        public int Units { get; set; }
        public int HP { get; set; }
        public int Damage { get; set; }
        public int Initiative { get; set; }
        public string AttackType { get; set; }
        public HashSet<string> immuneTo = new HashSet<string>();
        public HashSet<string> weakTo = new HashSet<string>();

        public int EffectivePower
        {
            get
            {
                return Units * Damage;
            }
        }

        public int AmountOfDamage(Group target)
        {
            if (target.ImmuneSystem == ImmuneSystem)
            {
                return 0;
            }
            else if (target.immuneTo.Contains(AttackType))
            {
                return 0;
            }
            else if (target.weakTo.Contains(AttackType))
            {
                return EffectivePower * 2;
            }
            else
            {
                return EffectivePower;
            }
        }
    }
}
