using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PeoplePolder.Creatures;
using PeoplePolder.Helpers;

namespace PeoplePolder.Buildings
{
    public class Castle:Building
    {
        public readonly ResoursesStorage ResoursesStorage;

        public Castle(Point cell) : base(cell)
        {
            Sprite = SpriteType.Castle;
            ResoursesStorage = new ResoursesStorage(5000, 0);
        }



        public override void Update(float dt)
        {
          //  base.Update(dt);
        }

        protected override bool DoWork(Creature creature, float dt)
        {
            return false;
        }

        public override void Draw(float dt)
        {
            base.Draw(dt);
        }
    }
}
