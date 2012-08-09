using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using DungeonPlatformer.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PeoplePolder.Buildings;
using PeoplePolder.Creatures;
using PeoplePolder.Creatures.Behaviors;
using PeoplePolder.Creatures.Fabrics;
using PeoplePolder.GUI;
using PeoplePolder.GUI.Elements;
using PeoplePolder.Helpers;
using Point = Microsoft.Xna.Framework.Point;

namespace PeoplePolder
{
    public class StrategyManager
    {
        public readonly CreatureManager CreatureManager;
        public readonly GameField GameField;
        public readonly BuildingManager BuildingManager;
        private readonly GUIManager _guiManager;
        public readonly Camera2D Camera; 

        public StrategyManager()
        {
            Camera = new Camera2D(new Vector2(450, 450), Settings.ScreenResolution.X, Settings.ScreenResolution.Y);
            CreatureManager = new CreatureManager();
            GameField = new GameField(100, 100);
            BuildingManager = new BuildingManager();
            _guiManager = new GUIManager();
            ConstructionMenu constructionMenu = new ConstructionMenu(1,new Size(64,64));
            constructionMenu.ButtonSize = new Size(64, 64);
            constructionMenu.Position = new Vector2(0, Settings.ScreenResolution.Y - 64);
            constructionMenu.AddElement(SpriteType.SawmillMenuElement, "Sawmill", typeof(Sawmill));



            _guiManager.Add(constructionMenu);

            Castle castle = new Castle(new Point(3, 3));

            castle.ResoursesStorage.AddResourse(Resourses.Wood, 50);
            BuildingManager.Add(castle);

            Sawmill sawmill = new Sawmill(new Point(10, 10));

            BuildingManager.Add(sawmill);


            for (int i = 0; i < 6; i++)
            {
                Creature creature = CreatureFabric.CreateHuman(this, CreatureRelation.Friendly);
                creature.Position = new Vector2(RandomTool.RandInt(0, 200), RandomTool.RandInt(0, 100));
                creature.SetBehaviour(new CarpenterBehaviour());
                CreatureManager.Add(creature);
            }

            for (int i = 0; i < 40; i++)
            {
                Creature creature = CreatureFabric.CreateHuman(this, CreatureRelation.Friendly);
                creature.Position = new Vector2(RandomTool.RandInt(0, 2000), RandomTool.RandInt(0, 2000));
                creature.SetBehaviour(new WoodcutterBehavior());
                CreatureManager.Add(creature);
            }


        }

        public void Update(float dt)
        {
            CreatureManager.Update(dt);
            GameField.Update(dt);
            BuildingManager.Update(dt);
            _guiManager.Update(dt);
        }

        public void Draw(float dt)
        {
            TextureManager.SpriteBatch.Begin(SpriteSortMode.Immediate,
                                      BlendState.AlphaBlend,
                                                       SamplerState.PointClamp,
                                                       DepthStencilState.Default,
                                                       RasterizerState.CullClockwise,
                                                       null,
                                                       Camera.GetProjection());
            GameField.Draw(Camera.VisibleTilesArea ,dt);
            CreatureManager.Draw(Camera.VisibleArea, dt);
            BuildingManager.Draw(dt);
            TextureManager.SpriteBatch.End();
            TextureManager.SpriteBatch.Begin();
            _guiManager.Draw(dt);
            TextureManager.SpriteBatch.End();
        }
    }
}
