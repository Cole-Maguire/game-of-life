using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace game_of_life
{
    public class Grid
    {
        public bool[,] ActualGrid { get; set; }
        public int Height { get; }
        public int Width { get; }
        private int Iteration;
        public Grid(bool[,] grid)
        {
            ActualGrid = grid;
            Width = grid.GetLength(0);
            Height = grid.GetLength(1);
        }
        public Grid(int height, int width)
        {
            Height = height;
            Width = width;

            ActualGrid = new bool[Width, Height];
            // Fill array with random values.
            // This is a really disgusting way of doing things, but I'm not sure there's really a more idiomatic method
            var random = new Random();
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ActualGrid[x, y] = random.Next(0, 2) == 0;
                }
            }
        }

        public bool[,] GetTempGrid(){
            // Get a blank grid for mutating, so our timestep happens all at once
            return new bool[Width, Height];
        }
        private bool GetCellValue(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return false;
            }
            else
            {
                return ActualGrid[x, y];
            }
        }
        private IEnumerable<Boolean> GetAdjacentCells(int x, int y)
        {
            Func<int, int[]> CellRange = n => new int[] { n - 1, n, n + 1 };

            var xRange = CellRange(x);
            var yRange = CellRange(y);

            var combos = xRange
                .SelectMany(X => yRange.Select(Y => (x: X, y: Y)))
                .Where(cell => cell != (x, y))
                .Select(cell => GetCellValue(cell.x, cell.y));
            return combos;
        }
        public bool GetLifeStatus(int x, int y)
        {
            var adjacentLives = GetAdjacentCells(x, y).Where(a => a).Count();
            var thisCell = GetCellValue(x, y);
            //this is super verbose, but it's clearer that way
            if (thisCell)
            {
                if (adjacentLives < 2)
                {
                    //underpopulation
                    return false;
                }
                else if (adjacentLives == 2 || adjacentLives == 3)
                {
                    //sweetspot
                    return true;
                }
                else
                {
                    //overpopulation
                    return false;
                }
            }
            else
            {
                if (adjacentLives == 3)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void SetCell(int x, int y)
        {
            ActualGrid[x, y] = GetLifeStatus(x, y);
        }
        public void Draw()
        {
            var drawable = new StringBuilder($"{Width}x{Height} Grid - Iteration {Iteration} \n");
            Iteration++;
            
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    drawable.Append(ActualGrid[x, y] ? "#" : ".");
                }
                drawable.Append('\n');
            }
            Console.Clear();
            Console.WriteLine(drawable);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var width = Convert.ToInt32(args[0]);
            var height = Convert.ToInt32(args[1]);

            var grid = new Grid(height, width);

            while (true)
            {
                var tempGrid = grid.GetTempGrid();
                Console.WriteLine("----");
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        tempGrid[x, y] = grid.GetLifeStatus(x, y);
                    }
                }
                grid.ActualGrid = tempGrid;
                grid.Draw();
                System.Threading.Thread.Sleep(200);
            }

        }
    }
}
