using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeoplePolder.GameStates
{
    public class StateManager
    {
        private GameState _currentState;

        public void SetState(GameState gameState)
        {
            _currentState = gameState;
        }

        public void Update(float dt)
        {
            _currentState.Update(dt);
        }
        public void Draw(float dt)
        {
            _currentState.Draw(dt);
        }
    }
}
