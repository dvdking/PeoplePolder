using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeoplePolder.Buildings;
using PeoplePolder.Creatures.Fabrics;

namespace PeoplePolder.Creatures.Behaviors
{
    internal class CarpenterBehaviour : IBehavior
    {
        public Creature Creature { get; set; }
        public CreatureManager CreatureManager { get; set; }
        public GameField GameField { get; set; }
        public BuildingManager BuildingManager { get; set; }

        private Castle _targetCastle;
        private bool _backFromCastle;
        private bool _waiting = false;

        public void Update(float dt)
        {
            if (_waiting)
            {
                TryTakeWood();
            }
            else if ((Creature.ResoursesStorage.Overload(Resourses.Board) && Creature.CreatureState == CreatureState.Working)
                     || Creature.ResoursesStorage.IsAbscent(Resourses.Wood) && Creature.CreatureState != CreatureState.Moving)
            {
                _targetCastle = (Castle) BuildingManager.GetNearest<Castle>(Creature.Cell);
                if (_targetCastle != null)
                {
                    Creature.MoveTo(_targetCastle.Cell);
                    Creature.PathFinished += OnPathFinishedInCastle;
                }
            }
            else if (Creature.CreatureState == CreatureState.Idle)
            {
                Creature.MoveTo(BuildingManager.GetNearest<Sawmill>(Creature.Cell).Cell);
                Creature.PathFinished += OnPathFinishedInSawmill;
            }
            else if (Creature.ResoursesStorage.IsAbscent(Resourses.Wood))
            {

            }
        }

        private void OnPathFinishedInSawmill()
        {
            Creature.PathFinished -= OnPathFinishedInSawmill;
            Creature.CreatureState = CreatureState.Working;
        }

        private void OnPathFinishedInCastle()
        {
            Creature.PathFinished -= OnPathFinishedInCastle;
            Creature.CreatureState = CreatureState.Idle;

            _targetCastle.ResoursesStorage.AddResourses(Creature.ResoursesStorage, true);
            TryTakeWood();
        }

        private void TryTakeWood()
        {
            int res = 0;
            Creature.ResoursesStorage.AddResourse(Resourses.Wood,
                                                  res =
                                                  _targetCastle.ResoursesStorage.DiscardResourse(Resourses.Wood, 50));
            if (res == 0)
            {
                _waiting = true;
            }
            else
                _waiting = false;
        }
    }
}
