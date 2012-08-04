using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeoplePolder.Buildings;
using PeoplePolder.Helpers;

namespace PeoplePolder.Creatures.Fabrics
{
    public static class CreatureFabric
    {
        public static Creature CreateHuman(CreatureManager creatureManager,
                                           GameField gameField,
                                           BuildingManager buildingManager,
                                           CreatureRelation relation)
        {
            Creature human = new Creature(creatureManager, gameField,buildingManager)
                                 {
                                     MoveRightSprite = TextureManager.GetAnimSprite(SpriteType.HumanMoveRight),

                                     Kind = CreatureKind.Human,
                                     Relation = relation
                                 };
            human.MoveRightSprite.TimePerFrame = 250f;
            return human;
        }
    }
}
