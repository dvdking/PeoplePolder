using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonPlatformer.Helpers;
using Microsoft.Xna.Framework;
using PeoplePolder.Creatures;
using PeoplePolder.Helpers;

namespace PeoplePolder.Buildings
{
    public abstract class Building
    {
        public Point Cell { get; set; }

        protected List<Creature> Workers;
        protected int WorkerPlaces { get; set; }
    



        protected float Elapsed;
        protected float NeedTime;



        public Vector2 Position
        {
            get { return new Vector2(Cell.X*Settings.CellSize, Cell.Y*Settings.CellSize); }
        }


        public SpriteType Sprite { get; set; }

        public Building(Point cell)
        {
            Cell = cell;
            Workers = new List<Creature>();
        }

        public void AddWorker(Creature creature)
        {
            if(Workers.Count + 1 > WorkerPlaces)
                throw new Exception("This one is too much for this building");

            Workers.Add(creature);
        }

        public abstract void Update(float dt);
        public abstract bool DoWork(Creature creature, float dt);

        public virtual void Draw(float dt)
        {
            TextureManager.DrawTexture(Sprite, Position, Color.White);
        }
    }
}
