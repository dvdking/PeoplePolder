using Microsoft.Xna.Framework;
using PeoplePolder.Helpers;

namespace PeoplePolder.GUI.Elements
{
    public enum ButtonState
    {
        Pressed,
        Idle
    }

    class Button:UIObject
    {
        public SpriteType TextureIdle;
        public SpriteType TexturePressed;


        ButtonState State = ButtonState.Idle;

        public Button()
        {
            OnLeftMouseButtonClick += OnPress;
            OnLeftMouseButtonReleaseIn += OnRelease;
            OnLeftMouseButtonReleaseOut += OnRelease;

        }

        public override void Initialize()
        {
            //Size = TextureManager.GetTextureSize(TextureIdle);
        }

        internal override void Update(float gameTime)
        {

        }

        private void OnPress(object sender)
        {
            State = ButtonState.Pressed;
        }
        private void OnRelease(object sender)
        {
            State = ButtonState.Idle;
        }

        internal override void Draw(float gameTime)
        {
            TextureManager.DrawTexture(State == ButtonState.Idle ? TextureIdle : TexturePressed, Bounds, Color);
            //base.Draw(gameTime);
        }
    }
}
