using System.Drawing;
using Microsoft.Xna.Framework;
using PeoplePolder.Helpers;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
namespace PeoplePolder.GUI.Elements
{
    public delegate void MouseEventHandler(object sender);

   abstract public class UIObject
    {
        public event MouseEventHandler OnLeftMouseButtonClick;
        public event MouseEventHandler OnLeftMouseButtonReleaseIn;
        public event MouseEventHandler OnLeftMouseButtonReleaseOut;
        internal GUIManager GUIManager;

        public Color Color = Color.White;

        public bool Visible = true;


        public Container Parent { get; set; }

        private float _x, _y;

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public float X
        {
            get { return _x + ((Parent != null)?Parent.X:0); }
            set { _x = value; }
        }
        public float Y
        {
            get { return _y + ((Parent != null) ? Parent.Y : 0); }
            set { _y = value; }
        }

        public Size Size { get; set; }

        public Rectangle Bounds
        {
            get { return new Rectangle((int)X, (int)Y, Size.Width, Size.Height); }
        }

        public void LeftMouseButtonDown()
        {
            if (OnLeftMouseButtonClick != null)
                OnLeftMouseButtonClick(this);
        }
        public void LeftMouseButtonUp()
        {
            if (MouseInteserect((int)MouseHelper.X, (int)MouseHelper.Y))
            {
                if (OnLeftMouseButtonReleaseIn != null)
                    OnLeftMouseButtonReleaseIn(this);
            }
            else if (OnLeftMouseButtonReleaseOut != null)
                OnLeftMouseButtonReleaseOut(this);

        }

       public abstract void Initialize();

       internal abstract void Update(float dt);

       internal abstract void Draw(float dt);

        internal bool MouseInteserect(int x, int y)
        {
            return MouseInteserect(new Vector2(x, y));
        }

        internal bool MouseInteserect(Vector2 mousePosition)
        {
            return (Bounds.Contains((int)mousePosition.X, (int)mousePosition.Y));
        }
    }
}
