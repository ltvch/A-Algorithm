using System;
using System.Collections.Generic;
 
public class Test
{
	public static void Main()
	{
		bool[,] map =
            {  { true,true, true, true, true, true, true },
               { true, false, false, true, false, false, true },
               { true, true, false, true, true, true, true },
               { true, true, false, true, true, true, true },               
               { true, true, true, true, false, false, false },
               { true, false, true, true, true, true, true },
               { true, true, true, true, true, true, true }
            };
		
        var path = MinPathBetween(new Point { X = 1, Y =0 }, new Point { X = 3, Y = 5 }, map);
        
		foreach (var point in path)
            {
                Console.WriteLine("X,Y = " + point.X + "," + point.Y);
            }
	}
 
    private static bool IsValidPos(int x, int y, int rows, int cols)
    {
        if (x >= 0 && x < rows && y >= 0 && y < cols)
        {
            return true;
        }
        return false;
    }
 
    // boolean map true= aviable, false = block
    public static Stack<Point> MinPathBetween(Point start, Point target, bool[,] map)
        {
            var path = new Stack<Point>();
            if(!map[start.X,start.Y] || ! map[start.X,start.Y] )return path;
 
            var mapPaths = new int[map.GetLength(0), map.GetLength(1)];
 
            MarkForward(map, mapPaths, start, target, 1);                
 
            SelectPath(mapPaths, map, start, target, path);
 
            return path;
        }
 
        private static void MarkForward(bool[,] map, int[,] mapPaths, Point start, Point target, int step)
        {
            if ((mapPaths[start.X, start.Y] == 0 || mapPaths[start.X, start.Y] > step) && map[start.X, start.Y])
                mapPaths[start.X, start.Y] = step;
            else return;
 
            if (start.X == target.X && start.Y == target.Y) return;
 
            var rows = map.GetLength(0);
            var cols = map.GetLength(1);
 
            var xdir = new int[] { -1, 1, 0, 0 };
            var ydir = new int[] { 0, 0, -1, 1 };
 
            for (int i = 0; i <4; i++)
            {
                if (IsValidPos(start.X + xdir[i], start.Y + ydir[i], rows, cols) && map[start.X + xdir[i], start.Y + ydir[i]])
                {
                    MarkForward(map, mapPaths, new Point { X= start.X + xdir[i], Y= start.Y + ydir[i] }, target, step + 1);
                }
            }            
        }
  private static void SelectPath(int[,] mapPaths, bool[,] map, Point start, Point target, Stack<Point> path)
        {
            path.Push(target);
            if (target.X == start.X && target.Y == start.Y) return;
 
            var rows = mapPaths.GetLength(0);
            var cols = mapPaths.GetLength(1);
            var xdir = new int[] { -1, 1, 0, 0 };
            var ydir = new int[] { 0, 0, -1, 1 };
 
            for (int i = 0; i < 4; i++)
            {
                if (IsValidPos(target.X + xdir[i], target.Y + ydir[i], rows, cols) && mapPaths[target.X + xdir[i], target.Y + ydir[i]] == mapPaths[target.X, target.Y] - 1)
                {
                    SelectPath(mapPaths, map, start, new Point { X = target.X + xdir[i], Y = target.Y + ydir[i] }, path);
                    return;
                }               
            }            
        }
 
}
 
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}
