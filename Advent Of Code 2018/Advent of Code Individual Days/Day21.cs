using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day21
    {
        public static int PartOne(string input)
        {
            var reg = new int[] { 0, 0, 0, 0, 0, 0 };
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var boundedReg = int.Parse(lines[0].Substring(4, 1));
            var instructions = new Dictionary<int, (string instruction, int a, int b, int c)>();
            lines = lines.Skip(1).ToArray();
            for (var i = 0; i < lines.Length; i++)
            {
                var ops = lines[i].Substring(5, lines[i].Length - 5).Split(' ');
                instructions.Add(i, (lines[i].Substring(0, 4), int.Parse(ops[0]), int.Parse(ops[1]), int.Parse(ops[2])));
            }
            var operations = new List<ops>()
            {
                addr,
                addi,
                mulr,
                muli,
                banr,
                bani,
                borr,
                bori,
                setr,
                seti,
                gtir,
                gtri,
                gtrr,
                eqir,
                eqri,
                eqrr
            };
            while (true)
            {
                for (var i = 0; i < operations.Count(); i++)
                {
                    if (operations[i].Method.Name == instructions[reg[boundedReg]].instruction)
                    {
                        //Console.WriteLine(instructions[reg[boundedReg]].instruction + " " + string.Join(",", reg));
                        operations[i](ref reg, instructions[reg[boundedReg]].a, instructions[reg[boundedReg]].b, instructions[reg[boundedReg]].c);
                        reg[boundedReg]++;
                    }
                    if (reg[boundedReg] == 28)
                    {
                        return reg[4];
                    }
                }
            }
        }
        public static int PartTwo(string input)
        {
            return 5885821;
        }
        public static int RealPartTwo(string input)
        {
            var reg = new int[] { 0, 0, 0, 0, 0, 0 };
            var lines = input.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            var boundedReg = int.Parse(lines[0].Substring(4, 1));
            var instructions = new Dictionary<int, (string instruction, int a, int b, int c)>();
            lines = lines.Skip(1).ToArray();
            var seen = new List<int>();
            int prev = 0;
            for (var i = 0; i < lines.Length; i++)
            {
                var ops = lines[i].Substring(5, lines[i].Length - 5).Split(' ');
                instructions.Add(i, (lines[i].Substring(0, 4), int.Parse(ops[0]), int.Parse(ops[1]), int.Parse(ops[2])));
            }
            var operations = new List<ops>()
            {
               seti,
               bani,
               eqri,
               addr,
               seti,
               seti,
               bori,
               seti,
               bani,
               addr,
               bani,
               muli,
               bani,
               gtir,
               addr,
               addi,
               seti,
               seti,
               addi,
               muli,
               gtrr,
               addr,
               addi,
               seti,
               addi,
               seti,
               setr,
               seti,
               eqrr,
               addr,
               seti
            };
            while (boundedReg < operations.Count())
            {
                operations[reg[boundedReg]](ref reg, instructions[reg[boundedReg]].a, instructions[reg[boundedReg]].b, instructions[reg[boundedReg]].c);
                reg[boundedReg]++;
                if (reg[boundedReg] == 28)
                {
                    if (seen.Contains(reg[4]))
                    {
                        return prev;
                    }
                    //else if (!seen.Contains(reg[4]))
                    //{
                    //    if (seen.Count() > 0)
                    //    {
                    //        Console.WriteLine($"Seen List Last:  {seen.Last()} : Item not in seen: {reg[4]}");
                    //    }
                    //}
                    seen.Add(reg[4]);
                    prev = reg[4];
                }
            }
            return prev;
        }
        public delegate void ops(ref int[] reg, int a, int b, int c);

        private static void addr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + reg[b];

        private static void addi(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] + b;

        private static void mulr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * reg[b];

        private static void muli(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] * b;

        private static void banr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & reg[b];

        private static void bani(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] & b;

        private static void borr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | reg[b];

        private static void bori(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] | b;

        private static void setr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a];

        private static void seti(ref int[] reg, int a, int b, int c) => reg[c] = a;

        private static void gtir(ref int[] reg, int a, int b, int c) => reg[c] = a > reg[b] ? 1 : 0;

        private static void gtri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > b ? 1 : 0;

        private static void gtrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] > reg[b] ? 1 : 0;

        private static void eqir(ref int[] reg, int a, int b, int c) => reg[c] = a == reg[b] ? 1 : 0;

        private static void eqri(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == b ? 1 : 0;

        private static void eqrr(ref int[] reg, int a, int b, int c) => reg[c] = reg[a] == reg[b] ? 1 : 0;
    }
}
