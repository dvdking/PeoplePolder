using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeoplePolder.Helpers;

namespace DungeonPlatformer.Helpers
{
    public delegate void AnimationEndEventHandler();

    public class AnimSprite
    {
        public int Frame;
        public float TimePerFrame;
        public AnimationEndEventHandler AnimationEnd;
        private readonly int _framesCount;

      //  public Texture2D Texture { get; private set; }

        public SpriteType SpriteType { get; set; }

        public Rectangle SourceRect
        {
            get { return new Rectangle(Frame*FrameWidth, 
                                        0,
                                        FrameWidth,
                                        FrameHeight); }
        }

        public readonly int FrameWidth;
        public readonly int FrameHeight;
        private float _elapsed;

        public AnimSprite(SpriteType spriteType, int framesCount, int frameWidth, int frameHeight)
        {
            FrameHeight = frameHeight;
            FrameWidth = frameWidth;
            _framesCount = framesCount ;
            SpriteType = spriteType;
        }


        public void Update(float dt)
        {
            _elapsed += dt;
            if(_elapsed > TimePerFrame)
            {
                Frame++;
                if (Frame > _framesCount - 1)
                {
                    Frame = 0;
                    if(AnimationEnd != null)
                        AnimationEnd();
                }

                _elapsed -= TimePerFrame;
            }
        }

        public AnimSprite Clone()
        {
            AnimSprite animSprite = new AnimSprite(SpriteType, _framesCount, FrameWidth, FrameHeight);
            animSprite.TimePerFrame = this.TimePerFrame;
            return animSprite;
        }
    }

    public static class AnimExtenstion
    {
        
        public static void Draw(this SpriteBatch s, AnimSprite animSprite, Rectangle rect, Color color)
        {
            s.Draw(TextureManager.GetTexture(animSprite.SpriteType), rect, animSprite.SourceRect, color);
        }
        public static void Draw(this SpriteBatch s, AnimSprite animSprite, Vector2 vector, Color color)
        {
            s.Draw(TextureManager.GetTexture(animSprite.SpriteType), vector, animSprite.SourceRect, color);
        }
    }
}
