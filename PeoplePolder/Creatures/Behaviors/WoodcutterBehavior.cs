using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PeoplePolder.Buildings;
using PeoplePolder.Creatures.Fabrics;

namespace PeoplePolder.Creatures.Behaviors
{
    public class WoodcutterBehavior:IBehavior
    {
        public Creature Creature { get; set; }
        public CreatureManager CreatureManager { get; set; }
        public GameField GameField { get; set; }
        public BuildingManager BuildingManager { get; set; }

        public StrategyManager StrategyManager { get; set; }

        private Castle _targetCastle; 

        public void Update(float dt)
        {
            if(Creature.ResoursesStorage.Overload(Resourses.Wood) && Creature.CreatureState == CreatureState.GatheringResourses)
            {
                _targetCastle = (Castle)BuildingManager.GetNearest<Castle>(Creature.Cell);
                if (_targetCastle != null)
                {
                    Creature.MoveTo(_targetCastle.Cell);
                    Creature.PathFinished += OnPathFinishedInCastle;
                }
            }
            else if(Creature.CreatureState == CreatureState.Idle)
            {
                Creature.MoveTo(GameField.GetNearest(Chunk.ChunkType.Forest, Creature.Cell, true));
                Creature.PathFinished += OnPathFinishedInForest;
            }
        }
        private void OnPathFinishedInForest()
        {
            Creature.PathFinished -= OnPathFinishedInForest;
            Creature.CreatureState = CreatureState.GatheringResourses;
        }
        private void OnPathFinishedInCastle()
        {
            Creature.PathFinished -= OnPathFinishedInCastle;
            Creature.CreatureState = CreatureState.Idle;

            _targetCastle.ResoursesStorage.AddResourses(Creature.ResoursesStorage, true);

            _targetCastle = null;
        }
    }
}
