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
            NeedTime = 25;
        }

        public override void Update(float dt)
        {
        }

        public override bool DoWork(Creature creature, float dt)
        {
            if (creature.ResoursesStorage.CheckResourse(Resourses.Wood) <= 0)
                return false;

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
