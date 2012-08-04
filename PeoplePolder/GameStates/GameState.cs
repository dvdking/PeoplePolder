using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeoplePolder.GameStates
{
    public abstract class GameState
    {
        public GameState()
        {
            
        }

        public abstract void Update(float dt);
        public abstract void Draw(float dt);
    }
}
