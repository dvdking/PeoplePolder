using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PeoplePolder.Helpers
{
    public static class KeyboardHelper
    {
        public delegate void KeyPressedEventHandler();

        static private KeyboardState _currentState;
        private static KeyboardState _previousState;

        public static void Update(float dt)
        {
            _previousState = _currentState;
            _currentState = Keyboard.GetState(0);
        }

        static public bool Press(Keys key)
        {
            return _currentState.IsKeyDown(key);
        }

        static public bool WasPressed(Keys key)
        {
            return _currentState.IsKeyDown(key) && _previousState.IsKeyUp(key);
        }

        static public bool WasReleased(Keys key)
        {
            return _currentState.IsKeyUp(key) && _previousState.IsKeyDown(key);
        }
    }
}
