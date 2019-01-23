using System;
using System.IO;
using System.Reflection;
using System.Linq;

namespace Advent_Of_Code_2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //string Path = @"inputs\Day" + 15.ToString() + ".txt";
            //var input = File.ReadAllText(Path);
            //Console.WriteLine(Day15.PartTwo(input));
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(t => String.Equals(t.Namespace, "Advent_Of_Code_2018", StringComparison.Ordinal) && t.Name.Contains("Day")).ToList();

            for (var i = 0; i <= types.Count(); i++)
            {
                var type = types.Where(x => x.Name.Equals("Day" + i.ToString()));
                foreach (var t in type)
                {
                    Console.WriteLine("Day " + i + " Output: ");
                    string Path = @"inputs\Day" + i.ToString() + ".txt";
                    var input = File.ReadAllText(Path);
                    var activator = Activator.CreateInstance(type.First());
                    var partOneOutput = type.First().InvokeMember("PartOne", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static, null, activator, new object[] { input });
                    Console.WriteLine("Part One: ");
                    Console.WriteLine(partOneOutput);
                    if (i == 21)
                    {
                        Console.WriteLine("*********************************************");
                        Console.WriteLine("Do you want the real run time of Part 2? Note it takes about 3 to 4 minutes to run.");
                        Console.WriteLine("Y or N");
                        var answer = Console.ReadLine();
                        answer = answer.ToUpper();
                        if (answer == "Y")
                        {
                            Console.WriteLine("Part Two: ");
                            var realPartTwoOutput = type.First().InvokeMember("RealPartTwo", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static, null, activator, new object[] { input });
                            Console.WriteLine(realPartTwoOutput);
                            Console.WriteLine("-----------------------------------------");
                        }
                        else
                        {
                            Console.WriteLine("Part Two: ");
                            var easyPartTwoOutput = type.First().InvokeMember("PartTwo", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static, null, activator, new object[] { input });
                            Console.WriteLine(easyPartTwoOutput);
                            Console.WriteLine("-----------------------------------------");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Part Two: ");
                        var partTwoOutput = type.First().InvokeMember("PartTwo", BindingFlags.InvokeMethod | BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static, null, activator, new object[] { input });
                        Console.WriteLine(partTwoOutput);
                        Console.WriteLine("-----------------------------------------");
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
