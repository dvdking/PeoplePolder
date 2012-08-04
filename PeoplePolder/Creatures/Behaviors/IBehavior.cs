using PeoplePolder.Buildings;
using PeoplePolder.Creatures.Fabrics;

namespace PeoplePolder.Creatures.Behaviors
{
    public interface IBehavior
    {
        Creature Creature { get; set; }
        CreatureManager CreatureManager { get; set; }
        GameField GameField { get; set; }
        BuildingManager BuildingManager { get; set; }

        void Update(float dt);
    }
}
