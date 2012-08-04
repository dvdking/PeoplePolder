using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace PeoplePolder.Creatures.PathFinding
{
    public class Path
    {
        private List<Vector2> points;
        public List<Vector2> ControlPoints { get { return points; } }

        public bool Finished
        {
            get { return !ControlPoints.Any(); }
        }

        public Path()
        {
            points = new List<Vector2>();
        }

        public float Length()
        {
            float l = 0;
            for (int i = 0; i < ControlPoints.Count - 1; i++)
            {
                l += (ControlPoints[i] - ControlPoints[i + 1]).Length();
            }
            return l;
        }

        public Vector2 GetMoveDirection(Vector2 position, float speed)
        {
            while ((ControlPoints[0] - position).Length() <= speed)
            {
                ControlPoints.RemoveAt(0);
                if (ControlPoints.Count == 0)
                    break;
            }
            if (ControlPoints.Count != 0)
            {
                Vector2 dir = Vector2.Subtract(ControlPoints[0], position);
                dir.Normalize();
                return dir;
            }
            else
                return Vector2.Zero;
        }
        public Vector2 GetMoveOffset(Vector2 position, float speed)
        {
            if (ControlPoints.Count == 0) return Vector2.Zero;
            while ((ControlPoints[0] - position).Length() <= speed)
            {
                ControlPoints.RemoveAt(0);
                if (ControlPoints.Count == 0)
                    break;
            }
            if (ControlPoints.Count != 0)
            {
                Vector2 dir = Vector2.Subtract(ControlPoints[0], position);
                dir.Normalize();
                return dir * speed;
            }
            else
                return Vector2.Zero;
        }
        public void AddPoints(List<Vector2> points)
        {
            this.points.AddRange(points);
        }
        public void AddPoint(Vector2 point)
        {
            points.Add(point);
        }
        public void InsertPoint(int index, Vector2 point)
        {
            points.Insert(index, point);
        }

        public void Clear()
        {
            points.Clear();
            ControlPoints.Clear();
        }
    }
}
