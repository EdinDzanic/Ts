using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ts
{
    public class CellInfo
    {
        public int Value;
        public float X;
        public float Y;

        public CellInfo(int value, float x, float y)
        {
            Value = value;
            X = x;
            Y = y;
        }
    }
    
    public class Field
    {
        private Vector2 position;

        // a grid that represents the field state
        private List<List<CellInfo>> grid;
        // list of coordinates in the grid that represents the current
        // falling block
        private List<Position> fallingBlocks;

        public List<List<CellInfo>> Grid { get { return grid; } }
        public Vector2 Position { get { return position; } }
        public int Width { get; set; }
        public int Height { get; set; }
        public int CellSize { get; set; }
        // a list of coordinates that represent the current falling block

        // contructor needs to init all static game objects
        // and create an instance of dynamic game objects
        // params that the constructor will need are the size of the grid and the location
        public Field(int width, int height, Vector2 position, int cellSize)
        {
            grid = new List<List<CellInfo>>();
            fallingBlocks = new List<Position>();
            
            Width = width;
            Height = height;
            CellSize = cellSize;
            this.position = position;

            grid = new List<List<CellInfo>>();

            int gridHeight = height * 2;
            for (int k = 0; k < gridHeight; k++)
            {
                grid.Add(new List<CellInfo>());
            }

            for (int rowIndex = 0; rowIndex < grid.Count; rowIndex++)
            {
                int extraColumn = (rowIndex % 2);
                for (int columnIndex = 0; columnIndex < width - extraColumn; columnIndex++)
                {
                    float X = position.X;
                    float Y = position.Y;
                    if (columnIndex % 2 != 0)
                    {
                        X += (CellSize + 1) / 2 + (CellSize + 1) * rowIndex + 1;
                        Y += (CellSize + 1) / 2 + (CellSize + 1) * (columnIndex / 2) + 1;
                    }
                    else
                    {
                        X += (CellSize + 1) * rowIndex + 1;
                        Y += (CellSize + 1) * (columnIndex / 2) + 1;
                    }
                    grid[rowIndex].Add(new CellInfo(0, X, Y));
                }
            }

            CreateFallingBlocks();
        }

        private void AddBlockAt(int x, int y)
        {
            grid[x][y] = new CellInfo(1, grid[x][y].X, grid[x][y].Y);
        }

        // method to create next falling block
        private void CreateFallingBlocks()
        {
            int spawnPositionX = 4;
            int spawnPositionY = 0;

            fallingBlocks.Clear();

            // randomly generate the next shape

            fallingBlocks.Add(new Position(spawnPositionX, spawnPositionY));
            
            // this call needs to be deleted after the builder classes for game objects are created
            AddBlockAt(spawnPositionX, spawnPositionY);
        }

        // methods to update the state of field/grid
        public void MoveLeft()
        {
            bool IsMovable = true;

            foreach (var fallingBlock in fallingBlocks)
            {
                if (fallingBlock.X == 0)
                {
                    IsMovable = false;
                    break;
                }    
            }

            if (IsMovable)
            {
                for (int i = 0; i < fallingBlocks.Count; i++)
                {
                    grid[fallingBlocks[i].X][fallingBlocks[i].Y].Value = 0;
                    fallingBlocks[i] = new Position(fallingBlocks[i].X - 1, fallingBlocks[i].Y);
                    grid[fallingBlocks[i].X][fallingBlocks[i].Y].Value = 1;
                }
            }
        }

    }
}
