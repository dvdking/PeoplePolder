using System.Collections.Generic;
using System.Drawing;
using DungeonPlatformer.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using QuickFont;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace PeoplePolder.Helpers
{
    public enum SpriteType
    {
        HumanMoveLeft,
        HumanMoveRight,

        Castle,
        Sawmill,

        Grass,
        Forest
    }

    public static class TextureManager
    {
        private static Dictionary<SpriteType, Texture2D> _textures = new Dictionary<SpriteType, Texture2D>();
        static private Dictionary<SpriteType, AnimSprite> _animations = new Dictionary<SpriteType, AnimSprite>();

        private static QFont font;

        public static SpriteBatch SpriteBatch;

        public static void Init(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }

        static public AnimSprite GetAnimSprite(SpriteType at)
        {
            return _animations[at].Clone();
        }
        static public Texture2D GetTexture(SpriteType at)
        {
            return _textures[at];
        }


        static public void DrawAnimationSprite(AnimSprite animSprite, Rectangle rectangle, Color color)
        {
            SpriteBatch.Draw(animSprite, rectangle, color);
        }
        static public void DrawAnimationSprite(AnimSprite animSprite, Vector2 v, Color color)
        {
            SpriteBatch.Draw(animSprite, v, color);
        }

        static public void DrawTexture(SpriteType texture, Rectangle rectangle, Color color)
        {
            SpriteBatch.Draw(_textures[texture], rectangle, color);
        }
        static public void DrawTexture(SpriteType texture, Vector2 vector, Color color)
        {
            SpriteBatch.Draw(_textures[texture], vector, color);
        }

        static public void DrawString(string str, Vector2 position)
        {
            font.Print(str, new OpenTK.Vector2(position.X, position.Y));
        }

        static public void LoadTextures(ContentManager content)
        {
            _textures.Add(SpriteType.HumanMoveRight, content.Load<Texture2D>("HumanMoveDown"));
            _animations.Add(SpriteType.HumanMoveRight,new AnimSprite(SpriteType.HumanMoveRight, 2,16,16));

            _textures.Add(SpriteType.Grass, content.Load<Texture2D>("Field//Grass"));
            _textures.Add(SpriteType.Forest, content.Load<Texture2D>("Field//Forest"));
            _textures.Add(SpriteType.Castle, content.Load<Texture2D>("Buildings//Castle"));
            _textures.Add(SpriteType.Sawmill, content.Load<Texture2D>("Buildings//Sawmill"));

            font = new QFont(new Font("Times new roman", 12));
        }
    }
}
