using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DungeonPlatformer.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeoplePolder.Buildings;
using PeoplePolder.Creatures;
using PeoplePolder.Creatures.Behaviors;
using PeoplePolder.Creatures.Fabrics;
using PeoplePolder.Helpers;
using QuickFont;

namespace PeoplePolder.GameStates
{
    public class StrategyState:GameState
    {
        public StrategyManager StrategyManager;

        public StrategyState()
        {
            StrategyManager = new StrategyManager();

        }

        public override void Update(float dt)
        {
            if(MouseHelper.X > StrategyManager.Camera.ViewportWidth - Settings.CameraSensivity)
            {
                StrategyManager.Camera.MoveTo(new Vector2(StrategyManager.Camera.X + dt * Settings.CameraSpeed, StrategyManager.Camera.Y));
            }
            if (MouseHelper.X < Settings.CameraSensivity)
            {
                StrategyManager.Camera.MoveTo(new Vector2(StrategyManager.Camera.X - dt * Settings.CameraSpeed, StrategyManager.Camera.Y));
            }
            if (MouseHelper.Y < Settings.CameraSensivity)
            {
                StrategyManager.Camera.MoveTo(new Vector2(StrategyManager.Camera.X, StrategyManager.Camera.Y - dt * Settings.CameraSpeed));
            }
            if (MouseHelper.Y > StrategyManager.Camera.ViewportHeight - Settings.CameraSensivity)
            {
                StrategyManager.Camera.MoveTo(new Vector2(StrategyManager.Camera.X, StrategyManager.Camera.Y + dt * Settings.CameraSpeed));
            }
            StrategyManager.Update(dt);
        }                                                              
                                                                       
        public override void Draw(float dt)                            
        {                                                              
            StrategyManager.Draw(dt);

            ResoursesStorage resourses = StrategyManager.BuildingManager.GetTotalResourses();
            QFont.Begin();
            TextureManager.DrawString("Wood = " + resourses.CheckResourse(Resourses.Wood) , new Vector2(20, 20));
            TextureManager.DrawString("Boards = " + resourses.CheckResourse(Resourses.Board), new Vector2(20, 60));
            QFont.End();
        }
    }
}
