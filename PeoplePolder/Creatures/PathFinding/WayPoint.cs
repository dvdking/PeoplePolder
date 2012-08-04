using System;
using Microsoft.Xna.Framework;

namespace PeoplePolder.Creatures.PathFinding
{
    internal sealed class WayPoint
    {
        public readonly WayPoint Parent;

        public int G; 
        public int H;

        public int Cost { get; private set; }

        public int PositionX;
        public int PositionY;

        public bool type;

        public WayPoint(Point Position, WayPoint Parent, bool type)
        {
            this.PositionX = Position.X;
            this.PositionY = Position.Y;
            this.Parent = Parent;
            this.type = type;
        }

        public void CalculateCost(Creature creature, Point endPoint, bool improved)
        {
            //H = Math.Max(Math.Abs(Position.X - endPoint.X), Math.Abs(Position.Y - endPoint.Y)) * 30;
            //   H = (Math.Abs(Position.X - endPoint.X) + Math.Abs(Position.Y - endPoint.Y))*10;
            //H = Math.Abs(Position.X - endPoint.X) + Math.Abs(Position.Y - endPoint.Y);

            if (!improved)
            {
                int xDistance = Math.Abs(PositionX - endPoint.X);
                int yDistance = Math.Abs(PositionY - endPoint.Y);
                if (xDistance > yDistance)
                    H = 14 * yDistance + 10 * (xDistance - yDistance);
                else
                    H = 14 * xDistance + 10 * (yDistance - xDistance);
            }
            else
                H = 0;

            if (Parent != null)
                if (type)
                    G = Parent.G + 10;
                else
                    G = Parent.G + 14;
            else
                G = 0;
            Cost = G + H;
          //  if (creature != null)
             //   Cost += (int)(100 / creature.Body.GetWalkSpeed(creature.Map.terrain[PositionX, PositionY]));
        }
    }
}