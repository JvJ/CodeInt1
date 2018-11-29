using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// This is for question 8.2
// Imagine a robot sitting on the upper left hand corner of an NxN grid.The robot can
// only move in two directions: right and down.How many possible paths are there for
// the robot?
// FOLLOW UP
// Imagine certain squares are “off limits”, such that the robot can not step on them.
// Design an algorithm to get all possible paths for the robot.
namespace CodeInt1
{
    public enum Square
    {
        Clear,
        Obstacle
    }

    public class RobotGrid
    {
        private Square[,] m_grid;

        public Square this[int x, int y]
        {
            get { return m_grid[x, y]; }
            set { m_grid[x, y] = value; }
        }

        public int Width => m_grid.GetLength(0);
        public int Height => m_grid.GetLength(1);

        public RobotGrid (int width, int height)
        {
            m_grid = new Square[width, height];
        }

        public override string ToString()
        {
            var ret = new char[(Width+1) * Height];

            for (int y = 0; y < Height; y++)
            {

                // Put a newline at the end of this line
                ret[y * (Width + 1) + Width] = '\n';

                for (int x = 0; x < Width; x++)
                {
                    char c = '?';

                    switch (this[x,y]) {
                        case Square.Clear: c = '.'; break;
                        case Square.Obstacle: c = 'X'; break;
                    }

                    ret[y * (Width + 1) + x] = c;
                }
            }

            return new string(ret);
        }
    }

    public class Robot
    {
        private int x;
        private int y;
        public (int, int) Location => (x, y);

        public Robot(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Robot ((int, int) loc)
        {
            this.x = loc.Item1;
            this.y = loc.Item2;
        }

        public void MoveDown()
        {
            y++;
        }

        public void MoveRight()
        {
            x++;
        }

        // 
        public int PossiblePaths(RobotGrid grid)
        {
            // Everything starts at 0 except the edges
            int[,] paths = new int[grid.Width, grid.Height];
            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    if (y == grid.Height - 1 || x == grid.Width - 1)
                    {
                        paths[x, y] = 1;
                    }
                    else
                    {
                        paths[x, y] = 0;
                    }
                }
            }

            return PossiblePaths(this.x, this.y, grid, paths);
        }

        public int PossiblePaths(int x, int y, RobotGrid grid, int [,] paths)
        {
            // Early return if the value has already been computed
            if (paths[x,y] > 0)
            {
                return paths[x, y];
            }
            else if (grid[x,y] == Square.Obstacle)
            {
                paths[x, y] = 0;
                return 0;
            }

            paths[x, y] =
                PossiblePaths(x + 1, y, grid, paths)
                + PossiblePaths(x, y + 1, grid, paths);

            return paths[x, y];
        }

    }
}
