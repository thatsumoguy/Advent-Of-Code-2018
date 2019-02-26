using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day16
    {
        public static int[] RegZero;
        public static int PartOne(string input)
        {
            var count = 0;
            var operation = new Operation();
            var foundOperations = new List<Operation.ops>();
            var partOneInput = input.Substring(0, input.IndexOf("Stop")).Split("\n\r\n", StringSplitOptions.RemoveEmptyEntries);
            var correctOps = new List<(int opCode, Operation.ops)>();
            var foundOps = new List<(int opCode, Operation.ops)>();

            foreach (var line in partOneInput)
            {
                var lines = line.Split('\n');
                var before = lines[0];
                var beforeOperations = new int[]
                {
                    int.Parse(before.Substring(9,1)),
                    int.Parse(before.Substring(12,1)),
                    int.Parse(before.Substring(15,1)),
                    int.Parse(before.Substring(18,1))
                };
                var after = lines[2];
                var afterOperations = new int[]
                {
                    int.Parse(after.Substring(9,1)),
                    int.Parse(after.Substring(12,1)),
                    int.Parse(after.Substring(15,1)),
                    int.Parse(after.Substring(18,1))
                };
                var command = lines[1].Split(' ');
                var o = int.Parse(command[0]);
                var a = int.Parse(command[1]);
                var b = int.Parse(command[2]);
                var c = int.Parse(command[3]);
                var correct = 0;
                var operations = Operation.Commands;
                foreach (var op in operations)
                {
                    var reg = new int[] { beforeOperations[0], beforeOperations[1], beforeOperations[2], beforeOperations[3] };
                    op(ref reg, a, b, c);
                    if (reg.SequenceEqual(afterOperations))
                    {
                        correct++;
                        if (!correctOps.Contains((o, op)))
                        {
                            correctOps.Add((o, op));
                        }
                    }
                }
                if (correct >= 3)
                {
                    count++;
                }
            }
            
            var test = File.ReadAllText(@"Inputs\Day16Part2.txt");
            var partTwoInput = test.Split("\n");
            int[] registry = { 0, 0, 0, 0 };
            
            foreach (var s in partTwoInput)
            {
                var commands = Operation.Commands;
                string[] coms = s.Split(' ');
                var opCode = Convert.ToInt32(coms[0]);
                var a = Convert.ToInt32(coms[1]);
                var b = Convert.ToInt32(coms[2]);
                var c = Convert.ToInt32(coms[3]);
                commands[opCode](ref registry, a, b, c);
            }
            RegZero = registry;
            return count;
        }
        public static string PartTwo(string input)
        {
            return string.Join(",", RegZero);
        }
    }
    
    class Operation
    {
        public delegate void ops(ref int[] reg, int a, int b, int c);

        public static void Addr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + reg[b];

        public static void Addi(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + b;
        
        public static void Mulr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * reg[b];
        
        public static void Muli(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * b;
        
        public static void Banr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & reg[b];
        
        public static void Bani(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & b;
        
        public static void Borr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | reg[b];
        
        public static void Bori(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | b;
        
        public static void Setr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a];
        
        public static void Seti(ref int[] reg, int a, int b, int c) => reg[c] = a;
        
        public static void Gtir(ref int[] reg, int a, int b, int c) => reg[c] = a > reg[b] ? 1 : 0;
        
        public static void Gtri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > b ? 1 : 0;
        
        public static void Gtrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > reg[b] ? 1 : 0;
        
        public static void Eqir(ref int[] reg, int a, int b, int c) => reg[c] = a == reg[b] ? 1 : 0;
        
        public static void Eqri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == b ? 1 : 0;
        
        public static void Eqrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == reg[b] ? 1 : 0;

        public static List<ops> Commands;

        public Operation()
        {
            Commands = new List<ops>()
            {
                Borr,
                Addr,
                Eqrr,
                Addi,
                Eqri,
                Eqir,
                Gtri,
                Mulr,
                Setr,
                Gtir,
                Muli,
                Banr,
                Seti,
                Gtrr,
                Bani,
                Bori
            };
        }

    }
}
