using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Day8
    {
        public static int PartOne(string input)
        {
            var nums = new Queue<int>(input.Split(" ").Select(s => int.Parse(s)).ToList());
            return (GetNodes(nums)).Sum();
        }
        public static int PartTwo(string input)
        {
            var nums = new Queue<int>(input.Split(" ").Select(s => int.Parse(s)).ToList());
            return (GetNodes(nums)).TotalValue();
        }
        private static Node GetNodes(Queue<int> nums)
        {
            var children = nums.Dequeue();
            var metadata = nums.Dequeue();
            var node = new Node()
            {
                Children = Enumerable.Range(0, children).Select(x => GetNodes(nums)).ToList(),
                Metadata = Enumerable.Range(0, metadata).Select(x => nums.Dequeue()).ToList()
            };
            return node;
        }
    }
    class Node
    {
        public List<int> Metadata { get; set; } = new List<int>();
        public List<Node> Children { get; set; } = new List<Node>();
        public int Sum() => Metadata.Sum() + Children.Sum(x => x.Sum());
        public int TotalValue() => !Children.Any() ? Metadata.Sum() : Metadata.Where(i => i - 1 < Children.Count()).Select(i => Children[i - 1].TotalValue()).Sum();

    }
}
