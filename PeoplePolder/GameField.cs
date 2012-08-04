using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PeoplePolder.Helpers;

namespace PeoplePolder
{
    public struct Chunk
    {
        public int Resourses;
        public ChunkType Type;

        public Chunk(ChunkType type, int resoureses)
        {
            Resourses = resoureses;
            Type = type;
        }

        public enum ChunkType
        {
            Nothing,
            Grass,
            Forest
        }

    }
    public class GameField
    {
        private Chunk[,] _field;

        public int Width { get;private set; }
        public int Height { get;private set; }

        public GameField(int width, int height)
        {
            Width = width;
            Height = height;
            _field = new Chunk[Width,Height];
            Generate();
        }

        public void Update(float dt)
        {

        }

        public void Generate()
        {
            //TODO generation
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    _field[i, j] = new Chunk(Chunk.ChunkType.Grass, 100);
                    if(RandomTool.RandBool(0.5f))
                    {
                        _field[i, j] = new Chunk(Chunk.ChunkType.Forest, 100);
                    }
                }
            }
        }

        public bool IsPassable(int x, int y)
        {
            return true;
        }
        public bool InRange(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Width && y < Height;
        }

        public bool CheckCell(Point cell, Chunk.ChunkType chunkType)
        {
            return _field[cell.X, cell.Y].Type == chunkType;
        }

        public int GetResourses(Point cell, int amount)
        {
            if (_field[cell.X, cell.Y].Resourses == 0) return 0;
            int res = 0;

            if(_field[cell.X, cell.Y].Resourses - amount <= 0)
            {
                res = amount - _field[cell.X, cell.Y].Resourses;
                _field[cell.X, cell.Y].Resourses = 0;
                _field[cell.X, cell.Y].Type = Chunk.ChunkType.Nothing;
                return res;
            }
            _field[cell.X, cell.Y].Resourses -= amount;
            return amount;
        }

        public Point GetNearest(Chunk.ChunkType chunkType, Point point, bool hasResourses = false)
        {
            float distance = float.MaxValue;
            Point p = new Point(-1,-1);

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    if (_field[i, j].Type == chunkType)
                    {
                        if(hasResourses)
                        {
                            if (_field[i, j].Resourses <= 0) continue;
                        }
                        float curDistance = (float) Math.Sqrt(Math.Pow(i - point.X, 2) + Math.Pow(j - point.Y, 2));

                        if (curDistance < distance)
                        {
                            distance = curDistance;
                            p = new Point(i, j);
                        }
                    }
            return p;
        }


        public void CheckRectangle(ref Rectangle rectangle)
        {
            if (rectangle.X < 0)
                rectangle.X = 0;
            if (rectangle.Y < 0)
                rectangle.Y = 0;
            if (rectangle.Right > Width)
                rectangle.Width = Width - rectangle.X;
            if (rectangle.Bottom > Height)
                rectangle.Height = Height - rectangle.Y;
        }


        public Chunk[,] GetRectangle(Rectangle rectangle)
        {
          //  CheckRectangle(ref rectangle);
            Chunk[,] rect = new Chunk[rectangle.Width,rectangle.Height];

            for (int i = rectangle.X; i < rectangle.Width; i++)
            {
                for (int j = rectangle.Y; j < rectangle.Height; j++)
                {
                    rect[i, j] = _field[i, j];
                }
            }
            return rect;

        }

        public void Draw(Rectangle rectangle, float dt)
        {
            CheckRectangle(ref rectangle);

            for (int i = rectangle.X; i < rectangle.Right ; i++)
            {
                for (int j = rectangle.Y; j < rectangle.Bottom; j++)
                {
                    switch (_field[i,j].Type)
                    {
                        case Chunk.ChunkType.Grass:
                            TextureManager.DrawTexture(SpriteType.Grass,
                                                        new Rectangle(i * Settings.CellSize,
                                                                    j * Settings.CellSize,
                                                                    Settings.CellSize,
                                                                    Settings.CellSize), 
                                                        Color.White );
                            break;
                        case Chunk.ChunkType.Forest:
                            TextureManager.DrawTexture(SpriteType.Forest,
                                                        new Rectangle(i * Settings.CellSize,
                                                                    j * Settings.CellSize,
                                                                    Settings.CellSize,
                                                                    Settings.CellSize), 
                                                        Color.White);
                            break;
                    }
                }
            }

        }



    }
}
