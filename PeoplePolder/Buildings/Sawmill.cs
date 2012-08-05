using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PeoplePolder.Creatures;
using PeoplePolder.Helpers;

namespace PeoplePolder.Buildings
{
    public class Sawmill:Building
    {
        public Sawmill(Point cell) : base(cell)
        {
            Sprite = SpriteType.Sawmill;
            NeedTime = 155;
            WorkerPlaces = 3;
        }
        public override void Update(float dt)
        {
            base.Update(dt);
        }

        protected override bool DoWork(Creature creature, float dt)
        {
            if (creature.ResoursesStorage.CheckResourse(Resourses.Wood) <= 0)
            {
                creature.CreatureState = CreatureState.Idle;
                Remove(creature);
                return false;

            }

            Elapsed += dt;

            if(Elapsed >= NeedTime)
            {
                Elapsed -= NeedTime;
                creature.ResoursesStorage.AddResourse(Resourses.Board, 1);
                creature.ResoursesStorage.DiscardResourse(Resourses.Wood, 1);
            }
            return true;
        }
    }
}
