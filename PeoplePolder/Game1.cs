#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using PeoplePolder.GameStates;
using PeoplePolder.Helpers;

#endregion

namespace PeoplePolder
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private StateManager stateManager;
        private float dt;


        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth =Settings.ScreenResolution.X;
            graphics.PreferredBackBufferHeight = Settings.ScreenResolution.Y;
            
            stateManager = new StateManager();
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            graphics.IsFullScreen = true;


        }



        protected override void Initialize()
        {


            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TextureManager.Init(spriteBatch);
            TextureManager.LoadTextures(Content);
            graphics.ApplyChanges();


            Settings.ScreenResolution.X = graphics.PreferredBackBufferWidth;
            Settings.ScreenResolution.Y = graphics.PreferredBackBufferHeight;

            stateManager.SetState(new StrategyState());

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (KeyboardHelper.WasPressed(Keys.Escape))
                Exit();

            dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            MouseHelper.Update();
            KeyboardHelper.Update(dt);

            stateManager.Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            stateManager.Draw(dt);
            base.Draw(gameTime);
        }
    }
}
