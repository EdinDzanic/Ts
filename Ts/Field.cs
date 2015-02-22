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
                    if (rowIndex % 2 != 0)
                    {
                        X += (CellSize + 1) / 2 + (CellSize + 1) * columnIndex + 1;
                        Y += (CellSize + 1) / 2 + (CellSize + 1) * (rowIndex / 2) + 1;
                    }
                    else
                    {
                        X += (CellSize + 1) * columnIndex + 1;
                        Y += (CellSize + 1) * (rowIndex / 2) + 1;
                    }
                    grid[rowIndex].Add(new CellInfo(0, X, Y));
                }
            }

            CreateFallingBlocks();
            //AddBlockAt(3, 0);
        }

        private void AddBlockAt(int x, int y)
        {
            grid[x][y] = new CellInfo(1, grid[x][y].X, grid[x][y].Y);
        }

        // method to create next falling block
        private void CreateFallingBlocks()
        {
            int spawnPositionX = 0;
            int spawnPositionY = 0;

            fallingBlocks.Clear();

            // randomly generate the next shape

            fallingBlocks.Add(new Position(spawnPositionX, spawnPositionY));
            fallingBlocks.Add(new Position(spawnPositionX + 1, spawnPositionY));

            AddBlockAt(spawnPositionX, spawnPositionY);
            AddBlockAt(spawnPositionX + 1, spawnPositionY);
        }

        // methods to update the state of field/grid
        public void MoveLeft()
        {
            bool IsMovable = true;

            foreach (var fallingBlock in fallingBlocks)
            {
                int x = fallingBlock.X;
                int y = fallingBlock.Y;

                bool IsInFirstColumn = (fallingBlock.Y == 0);

                if (IsInFirstColumn)
                {
                    IsMovable = false;
                    break;
                }
                else
                {
                    bool HasLeftNeighbor = (grid[x][y - 1].Value > 1);
                    if (HasLeftNeighbor)
                    {
                        IsMovable = false;
                        break;
                    }
                }
            }

            if (IsMovable)
            {
                // move all falling blocks to the left by 1
                for (int i = 0; i < fallingBlocks.Count; i++)
                {
                    int currentX = fallingBlocks[i].X;
                    int currentY = fallingBlocks[i].Y;
                    int cellValue = grid[currentX][currentY].Value;

                    grid[currentX][currentY].Value = 0;
                    fallingBlocks[i] = new Position(currentX, currentY - 1);
                    grid[currentX][currentY - 1].Value = cellValue;
                }
            }
        }

        public void MoveRight()
        {
            bool IsMovable = true;

            foreach (var fallingBlock in fallingBlocks)
            {
                int x = fallingBlock.X;
                int y = fallingBlock.Y;

                bool IsInLastColumn = (fallingBlock.Y == grid[x].Count - 1);

                if (IsInLastColumn)
                {
                    IsMovable = false;
                    break;
                }
                else
                {
                    bool HasRightNeighbor = (grid[x][y + 1].Value > 1);
                    if (HasRightNeighbor)
                    {
                        IsMovable = false;
                        break;
                    }
                }
            }

            if (IsMovable)
            {
                // move all falling blocks to the right by 1
                for (int i = 0; i < fallingBlocks.Count; i++)
                {
                    int currentX = fallingBlocks[i].X;
                    int currentY = fallingBlocks[i].Y;
                    int cellValue = grid[currentX][currentY].Value;

                    grid[currentX][currentY].Value = 0;
                    fallingBlocks[i] = new Position(currentX, currentY + 1);
                    grid[currentX][currentY + 1].Value = cellValue;
                }
            }
        }

        public void MoveDown()
        {
            //determine if the current block shape can be moved down
            bool IsMovable = true;

            foreach (var fallingBlock in fallingBlocks)
            {
                bool IsBottomRow = ((fallingBlock.X + 2) > (grid.Count - 1));

                if (IsBottomRow)
                {
                    IsMovable = false;
                    break;
                }
                else
                {
                    bool HasBottomNeighbor = (grid[fallingBlock.X + 2][fallingBlock.Y].Value > 0);
                    if (HasBottomNeighbor)
                    {
                        IsMovable = false;
                        break;
                    }
                }
            }

            //if the block shape can be moved down move it down
            if (IsMovable)
            {
                for (int i = 0; i < fallingBlocks.Count; i++)
                {
                    int currentX = fallingBlocks[i].X;
                    int currentY = fallingBlocks[i].Y;
                    int cellValue = grid[currentX][currentY].Value;

                    grid[currentX][currentY].Value = 0;
                    fallingBlocks[i] = new Position(currentX + 2, currentY);
                    grid[currentX + 2][currentY].Value = cellValue;
                }

                //after the block is moved test if the grid contains triples
                CheckForTriples();
            }

        }

        private void CheckForTriples()
        {
        }
    }
}
