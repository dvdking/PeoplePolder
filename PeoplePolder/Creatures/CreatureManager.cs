using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PeoplePolder.Creatures.Fabrics
{
    public class CreatureManager
    {
        private readonly List<Creature> _creatures;
        private readonly Queue<Creature> _addEuque;
        private readonly Queue<Creature> _deleteEuque;

        public CreatureManager()
        {
            _creatures = new List<Creature>();
            _addEuque = new Queue<Creature>();
            _deleteEuque = new Queue<Creature>();
        }

        public List<Creature> GetCreatures()
        {
            return _creatures.ToList();
        }

        public void Add(Creature creature)
        {
            _addEuque.Enqueue(creature);
        }

        public void Remove(Creature creature)
        {
            _deleteEuque.Enqueue(creature);
        }

        public void Update(float dt)
        {
            foreach (var creature in _creatures)
            {
                creature.Update(dt);
            }

            while (_addEuque.Any())
            {
                _creatures.Add(_addEuque.Dequeue());
            }
            while (_deleteEuque.Any())
            {
                _creatures.Remove(_deleteEuque.Dequeue());
            }

        }

        public void Draw(Rectangle rectangle, float dt)
        {
            foreach (var creature in _creatures)
            {
                if(rectangle.Contains(creature.Position))
                   creature.Draw(dt);
            }
        }
    }
}
