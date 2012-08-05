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
        private readonly CreatureManager _creatureManager;
        private readonly GameField _gameField;
        private readonly BuildingManager _buildingManager;
        private Camera2D _camera;

        public StrategyState()
        {
            _camera = new Camera2D(new Vector2(450,450), Settings.ScreenResolution.X,Settings.ScreenResolution.Y);
            _creatureManager = new CreatureManager();
            _gameField = new GameField(100,100);
            _buildingManager = new BuildingManager();
            Castle castle = new Castle(new Point(3, 3));
            
            castle.ResoursesStorage.AddResourse(Resourses.Wood, 50);
            _buildingManager.Add(castle);

            Sawmill sawmill = new Sawmill(new Point(10,10));
            
            _buildingManager.Add(sawmill);


            for (int i = 0; i < 6; i++)
            {
                Creature creature = CreatureFabric.CreateHuman(_creatureManager, _gameField, _buildingManager, CreatureRelation.Friendly);
                creature.Position = new Vector2(RandomTool.RandInt(0,200), RandomTool.RandInt(0, 100));
                creature.SetBehaviour(new CarpenterBehaviour());
                _creatureManager.Add(creature);
            }

            for (int i = 0; i < 40; i++)
            {
                Creature creature = CreatureFabric.CreateHuman(_creatureManager, _gameField, _buildingManager, CreatureRelation.Friendly);
                creature.Position = new Vector2(RandomTool.RandInt(0, 2000), RandomTool.RandInt(0, 2000));
                creature.SetBehaviour(new WoodcutterBehavior());
                _creatureManager.Add(creature);
            }


        }

        public override void Update(float dt)
        {
            if(MouseHelper.X > _camera.ViewportWidth - Settings.CameraSensivity)
            {
                _camera.MoveTo(new Vector2(_camera.X + dt*Settings.CameraSpeed, _camera.Y));
            }
            if (MouseHelper.X < Settings.CameraSensivity)
            {
                _camera.MoveTo(new Vector2(_camera.X - dt * Settings.CameraSpeed, _camera.Y));
            }
            if (MouseHelper.Y < Settings.CameraSensivity)
            {
                _camera.MoveTo(new Vector2(_camera.X, _camera.Y - dt * Settings.CameraSpeed));
            }
            if (MouseHelper.Y > _camera.ViewportHeight - Settings.CameraSensivity)
            {
                _camera.MoveTo(new Vector2(_camera.X, _camera.Y + dt * Settings.CameraSpeed));
            }
            _creatureManager.Update(dt);                               
            _gameField.Update(dt);      
            
            _buildingManager.Update(dt);       
        }                                                              
                                                                       
        public override void Draw(float dt)                            
        {                                                              
            TextureManager.SpriteBatch.Begin(SpriteSortMode.Immediate,
                                                  BlendState.AlphaBlend,
                                                                   SamplerState.PointClamp, 
                                                                   DepthStencilState.Default, 
                                                                   RasterizerState.CullClockwise,
                                                                   null,
                                                                   _camera.GetProjection());
            _gameField.Draw(_camera.VisibleTilesArea, dt);
            _creatureManager.Draw(_camera.VisibleArea, dt);
            _buildingManager.Draw(dt);


            TextureManager.SpriteBatch.End();

            ResoursesStorage resourses = _buildingManager.GetTotalResourses();
            QFont.Begin();
            TextureManager.DrawString("Wood = " + resourses.CheckResourse(Resourses.Wood) , new Vector2(20, 20));
            TextureManager.DrawString("Boards = " + resourses.CheckResourse(Resourses.Board), new Vector2(20, 60));
            QFont.End();
        }
    }
}
