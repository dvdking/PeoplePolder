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
        private readonly Queue<Creature> _addEuque;
        private readonly Queue<Creature> _deleteEuque;
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
            _addEuque = new Queue<Creature>();
            _deleteEuque = new Queue<Creature>();
        }

        public bool TryAddWorker(Creature creature)
        {
            if (Workers.Count + _addEuque.Count() + 1 > WorkerPlaces)
                return false;

            _addEuque.Enqueue(creature); 

            return true;
        }

        public void Remove(Creature creature)
        {
            _deleteEuque.Enqueue(creature);
        }


        public bool IsOnWork(Creature creature)
        {
            return Workers.Any(p => p == creature);
        }

        public virtual void Update(float dt)
        {
            foreach (var creature in Workers)
            {
                DoWork(creature, dt);
            }
            while (_addEuque.Any())
            {
                Creature creature = _addEuque.Dequeue();
                creature.CreatureState = CreatureState.Working;
                Workers.Add(creature);
            }
            while (_deleteEuque.Any())
            {
                Workers.Remove(_deleteEuque.Dequeue());
            }
        }

        protected abstract bool DoWork(Creature creature, float dt);

        public virtual void Draw(float dt)
        {
            TextureManager.DrawTexture(Sprite, Position, Color.White);
        }
    }
}
