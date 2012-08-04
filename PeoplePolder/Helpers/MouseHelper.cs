using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PeoplePolder.Helpers
{
    static public class MouseHelper
    {
        static public State LeftButton;
        static public State RightButton;

        public static sbyte Wheel;
        private static int WheelDelta;
        private static int PreviousWheel;

        public static Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            private set 
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public static float X { get; private set; }

        public static float Y { get; private set; }
    

        static public void Update()
        {
            MouseState mouseState = Mouse.GetState();

            X = mouseState.X;
            Y = mouseState.Y;

            SetButtonState(ref LeftButton, mouseState.LeftButton);
            SetButtonState(ref RightButton, mouseState.RightButton);
            UpdateWheel(mouseState);
        }

        private static void SetButtonState(ref State button, ButtonState buttonState)
        {
            if (buttonState == ButtonState.Pressed)
            {
                if (button.Pressed || button.Hold)
                {
                    button.Pressed = false;
                    button.Hold = true;
                }
                else
                    button.Pressed = true;
            }
            if (button.Released)
            {
                button.Released = false;
            }
            else if (buttonState == ButtonState.Released && (button.Hold || button.Pressed))
            {
                button.Hold = false;
                button.Pressed = false;
                button.Released = true;
            }
        }
        private static void UpdateWheel(MouseState mouseState)
        {
            WheelDelta = mouseState.ScrollWheelValue - PreviousWheel;

            Wheel = 0;
            if(WheelDelta >= 120)
            {
                Wheel = 1;
            }
            else if(WheelDelta <= -120)
            {
                Wheel = -1;
            }
            PreviousWheel = mouseState.ScrollWheelValue;
        }
    }
    public struct State
    {
        public bool Hold;
        public bool Pressed;
        public bool Released;
    }

}
