using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeoplePolder.GUI.Elements;
using PeoplePolder.Helpers;

namespace PeoplePolder.GUI
{
    public class GUIManager
    {
        internal SpriteBatch SpriteBatch;
        private Container Elements;

        internal UIObject FocusedElement{ get; set; }

        public GUIManager()
        {
            Elements = new Container();
            Elements.GUIManager = this;
        }

        public void Add(UIObject uiobject)
        {
            Elements.Add(uiobject);
        }

        public void Update(float dt)
        {
            Elements.Update(dt);

            if(MouseHelper.LeftButton.Pressed)
            {
                OnLeftMouseButtonDown();
            }
            else if(MouseHelper.LeftButton.Released)
            {
                OnLeftMouseButtonUp();
            }
        }

        private void OnLeftMouseButtonDown()
        {
            FocusedElement = Elements.FoundFocus(MouseHelper.X, MouseHelper.Y);
            if (FocusedElement != null)
                FocusedElement.LeftMouseButtonDown();
        }
        private void OnLeftMouseButtonUp()
        {
            if (FocusedElement != null)
                {
                    FocusedElement.LeftMouseButtonUp();
                }
        }

        public void Draw(float dt)
        {
            Elements.Draw(dt);
        }

    }
}
