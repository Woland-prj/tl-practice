using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public interface IFighter
    {
        string Name { get; }
        IArmor Armor { get; }
        IWeapon Weapon { get; }
        IClass Class { get; }
        IRace Race { get; }
        int MaxHealth { get; }

        void TakeDamage( int damage );
        bool IsAlive();
        FighterStats GetStats();
    }

    public readonly record struct FighterStats(
        int Health,
        int Damage,
        int Armor
    );
}