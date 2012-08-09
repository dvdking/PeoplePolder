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
        Nothing,

        HumanMoveLeft,
        HumanMoveRight,

        Castle,
        Sawmill,

        Grass,
        Forest,

        SawmillMenuElement,
        ArrowButtonLeft,
        ArrowButtonRight,
    }

    public static class TextureManager
    {
        static private readonly Dictionary<SpriteType, Texture2D> Textures = new Dictionary<SpriteType, Texture2D>();
        static private readonly Dictionary<SpriteType, AnimSprite> Animations = new Dictionary<SpriteType, AnimSprite>();

        private static QFont font;

        public static SpriteBatch SpriteBatch;

        public static void Init(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }

        static public AnimSprite GetAnimSprite(SpriteType at)
        {
            return Animations[at].Clone();
        }
        static public Texture2D GetTexture(SpriteType at)
        {
            return Textures[at];
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
            if (texture == SpriteType.Nothing) return;

            SpriteBatch.Draw(Textures[texture], rectangle, color);
        }
        static public void DrawTexture(SpriteType texture, Vector2 vector, Color color)
        {
            if (texture == SpriteType.Nothing) return;

            SpriteBatch.Draw(Textures[texture], vector, color);
        }

        static public void DrawString(string str, Vector2 position)
        {
            font.Print(str, new OpenTK.Vector2(position.X, position.Y));
        }

        static public void LoadTextures(ContentManager content)
        {
            Textures.Add(SpriteType.HumanMoveRight, content.Load<Texture2D>("HumanMoveDown"));
            Animations.Add(SpriteType.HumanMoveRight,new AnimSprite(SpriteType.HumanMoveRight, 2,16,16));

            Textures.Add(SpriteType.Grass, content.Load<Texture2D>("Field//Grass"));
            Textures.Add(SpriteType.Forest, content.Load<Texture2D>("Field//Forest"));

            Textures.Add(SpriteType.Castle, content.Load<Texture2D>("Buildings//Castle"));
            Textures.Add(SpriteType.Sawmill, content.Load<Texture2D>("Buildings//Sawmill"));

            Textures.Add(SpriteType.SawmillMenuElement, content.Load<Texture2D>("GUI//Sawmill"));
            Textures.Add(SpriteType.ArrowButtonLeft, content.Load<Texture2D>("GUI//ArrowButtonLeft"));
            Textures.Add(SpriteType.ArrowButtonRight, content.Load<Texture2D>("GUI//ArrowButtonRight"));

            font = new QFont(new Font("Times new roman", 12));
        }
    }
}
