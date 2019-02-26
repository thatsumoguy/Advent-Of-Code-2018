using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day19
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
                        operations[i](ref reg, instructions[reg[boundedReg]].a, instructions[reg[boundedReg]].b, instructions[reg[boundedReg]].c);
                        reg[boundedReg]++;
                    }
                    if(reg[boundedReg] > instructions.Count())
                    {
                        return reg[0];
                    }
                }
            }
        }
        public static int PartTwo(string input)
        {
            var reg = new int[] { 1, 0, 0, 0, 0, 0 };
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

                        operations[i](ref reg, instructions[reg[boundedReg]].a, instructions[reg[boundedReg]].b, instructions[reg[boundedReg]].c);
                        //So what happens is there is a massive loop that looks to see if Reg[2] * Reg[1] will equal Reg[4], if so then you add the value from Reg[2] to Reg[0] and keep going
                        //until Reg[1] is greater than Reg[4], which means it the original loop kept adding 1 to Reg[1] until it was equal to a massive number in Reg[4], and then start the loop
                        //all over again. I got part of this, but ended up having to go to reddit to look for some help. I ended up finding this:
                          //R1 = 1
                         //do
                         //{
                            //if R3 * R1 == R5 {
                                //R0 += R3
                                //R2 = 1
                            //}
                            //else
                            //{
                                //R2 = 0
                             //}
                            //R1 += 1
                        //} while (R1 <= R5)
                        //And worked into my solution using my values of R (where R == Reg)
                        if (reg[boundedReg] == 2 && reg[2] != 0)
                        {
                            if (reg[4] % reg[2] == 0)
                            {
                                reg[0] += reg[2];
                            }
                            reg[3] = 0;
                            reg[1] = reg[4];
                            reg[boundedReg] = 12;
                            continue;
                        }
                        reg[boundedReg]++;

                        if (reg[boundedReg] > instructions.Count())
                        {
                            return reg[0];
                        }
                    }
                }
            }
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
