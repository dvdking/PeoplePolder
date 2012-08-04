using Microsoft.Xna.Framework;
using PeoplePolder;

namespace DungeonPlatformer.Helpers
{
    public class Camera2D
    {
        private float _x, _y ;

        public Vector2 Position
        {
            get { return new Vector2(_x, _y); }
            protected set
            {
                _x = value.X;
                _y = value.Y;
            }
        }

        public float X
        {
            get { return _x; }
            protected set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            protected set { _y = value; }
        }

        //   protected Vector2 goalPosition;
     //   protected Vector2 direction;

        protected bool moving = false;

        protected float rotation = 0.0f;
        protected float zoom = 1.0f;

        public int ViewportWidth, ViewportHeight;

        public Rectangle VisibleArea
        {
            get 
            { 
                Rectangle rectangle = VisibleTilesArea;

                rectangle.X *= Settings.CellSize;
                rectangle.Y *= Settings.CellSize;
                rectangle.Width = ViewportWidth * 2;
                rectangle.Height = ViewportHeight*2;
                return rectangle;
            }
        }

        public Rectangle VisibleTilesArea
        {
            get
            {
                return new Rectangle(((int)Position.X / Settings.CellSize - ViewPortCellsWidth/2 - 1),
                    (int)Position.Y / Settings.CellSize - ViewPortCellsHeight/2 - 1,
                                     ViewPortCellsWidth * 2,
                                     ViewPortCellsHeight * 2);
            }
        }

        private int ViewPortCellsWidth
        {
            get { return ViewportWidth/Settings.CellSize; }
        }
        private int ViewPortCellsHeight
        {
            get { return ViewportHeight / Settings.CellSize; }
        }

        public Camera2D(Vector2 position, int ViewportWidth, int ViewportHeight)
        {
            this.Position = position;
            this.ViewportWidth = ViewportWidth;
            this.ViewportHeight = ViewportHeight;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void MoveTo(Vector2 position)
        {
            this.Position = position;
        }

        public void Zoom(float amount)
        {
            zoom += amount;
            if (zoom < 0)
            {
                zoom = 0;
            }
        }

        public Matrix GetProjection()
        {

            return Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                                         Matrix.CreateRotationZ(rotation) *
                                         Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                                         Matrix.CreateTranslation(new Vector3(ViewportWidth * 0.5f, ViewportHeight * 0.5f, 0));
        }
    }

}