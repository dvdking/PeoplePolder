using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PeoplePolder.Buildings
{
    public class BuildingManager
    {
        private readonly List<Building> _buldings;

        public BuildingManager()
        {
            _buldings = new List<Building>();
        }

        public void Add(Building building)
        {
            _buldings.Add(building);
        }

        public void Remove(Building building)
        {
            _buldings.Remove(building);
        }

        public Building GetNearest<T>(Point point) where T : Building
        {
            float distance = float.MaxValue;
            Building building = null;

            foreach (var b in _buldings.Where(c => c is T))
            {
                float curDistance =
                    (float) Math.Sqrt(Math.Pow(b.Cell.X - point.X, 2) + Math.Pow(b.Cell.Y - point.Y, 2));
                if (curDistance < distance)
                {
                    distance = curDistance;
                    building = b;
                }
            }
            return building;
        }

        public ResoursesStorage GetTotalResourses()
        {
            ResoursesStorage resoursesStorage = new ResoursesStorage(int.MaxValue, 0);
            foreach (var building in _buldings.Where(b => b is Castle))
            {
                resoursesStorage.AddResourses(((Castle) building).ResoursesStorage);
            }
            return resoursesStorage;
        }

        public Building GetInCell(Point cell)
        {
            return _buldings.Find(p => p.Cell == cell);
        }


        public void Update(float dt)
        {
            foreach (var bulding in _buldings)
            {
                bulding.Update(dt);
            }
        }

        public void Draw(float dt)
        {
            foreach (var bulding in _buldings)
            {
                bulding.Draw(dt);
            }
        }

    }
}
