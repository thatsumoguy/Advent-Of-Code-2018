using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Advent_Of_Code_2018
{
    class Day13
    {
        public static (int x, int y) PartOne(string input)
        {
            var parts = input.Split('\n');
            var width = input.Split('\n').First().Length + 1;
            var cartSymbols = new char[] { '>', '<', '^', 'v' };
            var carts = new List<Cart>();
            for(var i =0; i < parts.Length; i++)
            {
                for(var j =0; j < parts[i].Length; j++)
                {
                    if(cartSymbols.Contains(parts[i][j]))
                    {
                        var x = j;
                        var y = i;
                        var nextTurn = 0;
                        var dir = 0;
                        switch(parts[i][j])
                        {
                            case '<':
                                dir = 0;
                                break;
                            case '^':
                                dir = 1;
                                break;
                            case '>':
                                dir = 2;
                                break;
                            case 'v':
                                dir = 3;
                                break;
                        }
                        carts.Add(new Cart(x, y, nextTurn, dir, false));
                    }
                }
            }
            return Cart.MoveCarts(carts, width, input, true);
        }
        public static (int x, int y) PartTwo(string input)
        {
            var parts = input.Split('\n');
            var width = input.Split('\n').First().Length + 1;
            var cartSymbols = new char[] { '>', '<', '^', 'v' };
            var carts = new List<Cart>();
            for (var i = 0; i < parts.Length; i++)
            {
                for (var j = 0; j < parts[i].Length; j++)
                {
                    if (cartSymbols.Contains(parts[i][j]))
                    {
                        var x = j;
                        var y = i;
                        var nextTurn = 0;
                        var dir = 0;
                        switch (parts[i][j])
                        {
                            case '<':
                                dir = 0;
                                break;
                            case '^':
                                dir = 1;
                                break;
                            case '>':
                                dir = 2;
                                break;
                            case 'v':
                                dir = 3;
                                break;
                        }
                        carts.Add(new Cart(x, y, nextTurn, dir, false));
                    }
                }
            }

            return Cart.MoveCarts(carts, width, input, false);
        }
    }
}
class Cart
{
    public int X { get; set; }
    public int Y { get; set; }
    public int NextTurn { get; set; }
    public int Dir { get; set; }
    public bool Crashed { get; set; }

    public Cart(int cartX, int cartY, int cartnextTurn, int cartDir, bool cartCrashed)
    {
        X = cartX;
        Y = cartY;
        NextTurn = cartnextTurn;
        Dir = cartDir;
        Crashed = cartCrashed;
    }
    public static (int x, int y) MoveCarts(List<Cart> carts, int width, string input, bool firstCrash)
    {
        while (true)
        {
            carts = carts.OrderBy(x => x.Y).ThenBy(x => x.X).ToList();

            if (carts.Count(c => !c.Crashed) == 1)
            {
                return ((carts.First(x => !x.Crashed).X, carts.First(y => !y.Crashed).Y));
            }

            foreach (var cart in carts)
            {
                if (cart.Crashed)
                {
                    continue;
                }
                var (x, y) = GetNextPos(cart.X, cart.Y, cart.Dir);
                if (carts.Any(c => c.X == x && c.Y == y && !c.Crashed))
                {
                    if (firstCrash)
                    {
                        
                        return ((x, y));
                    }
                }
                var CrashedCartIndex = carts.FindIndex(c => !c.Crashed && c.X == x && c.Y == y);
                if (CrashedCartIndex >= 0)
                {
                    cart.Crashed = true;
                    carts[CrashedCartIndex].Crashed = true;
                    cart.X = x;
                    cart.Y = y;
                    carts[CrashedCartIndex].X = x;
                    carts[CrashedCartIndex].Y = y;
                    continue;
                }
                cart.X = x;
                cart.Y = y;
                var track = input[(cart.Y * width) + cart.X];
                switch (track)
                {
                    case '/':
                        cart.Dir = 3 - cart.Dir;
                        break;
                    case '\\':
                        cart.Dir = (5 - cart.Dir) % 4;
                        break;
                    case '+':
                        switch (cart.NextTurn)
                        {
                            case 0:
                                cart.Dir = (4 + cart.Dir - 1) % 4;
                                break;
                            case 2:
                                cart.Dir = (4 + cart.Dir + 1) % 4;
                                break;
                            default:
                                break;
                        }
                        cart.NextTurn = (3 + cart.NextTurn + 1) % 3;
                        break;
                }
                
            }
        }
    }
    public static (int x, int y) GetNextPos(int x, int y, int dir)
    {
        switch (dir)
        {
            case 0:
                x--;
                break;
            case 1:
                y--;
                break;
            case 2:
                x++;
                break;
            case 3:
                y++;
                break;
        }
        return ((x, y));
    }
}
