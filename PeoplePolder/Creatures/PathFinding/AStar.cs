using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;

namespace PeoplePolder.Creatures.PathFinding
{
    public delegate void AsyncMethodCaller(Point startPosition, Point endPosition);

    public class AStar
    {
        private Path path;

        public Path Path
        {
            get
            {
                if (HasEnded)
                {
                    return path;
                }
                return null;
            }
        }

        public StrategyManager StrategyManager;
        public Creature Creature;

        public byte[,] FogMap;

        public bool HasEnded = false;
        public bool ImprovedPathFinding = false;
        public bool IgnoreObstacles = false;
        const bool DiagonalMovesAllowed = false;



        byte[,] pointMap;
        BinaryHeap OpenList;

        public AStar() { }

        public AStar(StrategyManager strategyManager, Creature creature)
        {
            this.StrategyManager = strategyManager;
            this.Creature = creature;
        }

        public void FoundWayAsync(Point startPosition, Point endPosition)
        {
            HasEnded = false;
            AsyncMethodCaller caller = new AsyncMethodCaller(FoundPath);
            caller.BeginInvoke(startPosition, endPosition, null, null);
            // Thread thread = new Thread(caller(startPosition, endPosition, out path, creature, improvedPathFinding, ignoreObstacles)});
        }
        public void FoundPath(Point startPosition, Point endPosition)
        {
            path = new Path();

            List<Vector2> wayList = FoundCellWay(startPosition, endPosition);
            if (wayList != null)
            {
                foreach (Vector2 p in wayList)
                    path.AddPoint(new Vector2(p.X * Settings.CellSize,// + Settings.CellSize/2,
                                              p.Y * Settings.CellSize));// + Settings.CellSize/2));
            }
            HasEnded = true;
        }

        public List<Vector2> FoundCellWay(Point startCell, Point endCell)
        {
            pointMap = new byte[StrategyManager.GameField.Width, StrategyManager.GameField.Height];
            OpenList = new BinaryHeap();

            WayPoint startPoint = new WayPoint(startCell, null, true);
            startPoint.CalculateCost(Creature, endCell, ImprovedPathFinding);

            OpenList.Add(startPoint);

            while (OpenList.Count != 0)
            {
                WayPoint node = OpenList.Get();
                if (node.PositionX == endCell.X && node.PositionY == endCell.Y)
                {
                    WayPoint nodeCurrent = node;
                    List<Vector2> points = new List<Vector2>();

                    while (nodeCurrent != null)
                    {
                        points.Insert(0, new Vector2(nodeCurrent.PositionX, nodeCurrent.PositionY));
                        nodeCurrent = nodeCurrent.Parent;
                    }
                    return points;
                }

                OpenList.Remove(); 
                Point temp = new Point(node.PositionX, node.PositionY);
                if (DiagonalMovesAllowed)
                {
                    if (CheckPassability(temp.X - 1, temp.Y) && CheckPassability(temp.X, temp.Y - 1))
                        AddNode(node, -1, -1, false, endCell);
                    if (CheckPassability(temp.X + 1, temp.Y) && CheckPassability(temp.X, temp.Y - 1))
                        AddNode(node, 1, -1, false, endCell);
                    if (CheckPassability(temp.X - 1, temp.Y) && CheckPassability(temp.X, temp.Y + 1))
                        AddNode(node, -1, 1, false, endCell);
                    if (CheckPassability(temp.X + 1, temp.Y) && CheckPassability(temp.X, temp.Y + 1))
                        AddNode(node, 1, 1, false, endCell);
                }

                AddNode(node, -1, 0, true, endCell);
                AddNode(node, 0, -1, true, endCell);
                AddNode(node, 1, 0, true, endCell);
                AddNode(node, 0, 1, true, endCell);
            }
            return null;
        }

        private bool CheckPassability(int x, int y)
        {
            //if (!IgnoreObstacles)
            //{
            //    MapObject obstacle = gameField.things.QueryObject(new Point(x, y));
            //    if (obstacle != null)
            //        if (!obstacle.Passable)
            //            return false;
            //}
            //if(FogMap!= null)
            //    if (FogMap[x, y] == 1) return false;
            return StrategyManager.GameField.IsPassable(x, y);
        }

        private bool CheckPassability(Point cell)
        {
            return CheckPassability(cell.X, cell.Y);
        }

        private void AddNode(WayPoint node, sbyte offSetX, sbyte offSetY, bool type, Point endCell)
        {
            Point pos = new Point(node.PositionX + offSetX, node.PositionY + offSetY);

            if (!CheckPassability(pos.X, pos.Y))
                return;
            if(!StrategyManager.GameField.InRange(pos.X, pos.Y))
                return;
            if (pointMap[pos.X, pos.Y] != 1)
            {
                WayPoint temp = new WayPoint(pos, node, type);
                temp.CalculateCost(Creature, endCell, ImprovedPathFinding);
                OpenList.Add(temp);
                pointMap[pos.X, pos.Y] = 1;
            }
        }

        private Point VectorToPoint(Vector2 vector)
        {
            return new Point((int)vector.X, (int)vector.Y);
        }

        private Vector2 PointToVector(Point point)
        {
            return new Vector2(point.X, point.Y);
        }
    }
}