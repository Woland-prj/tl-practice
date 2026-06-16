using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public interface IFighter
{
    string Name { get; }
    IArmor Armor { get; }
    IWeapon Weapon { get; }
    IClass Class { get; }
    IRace Race { get; }
    double CriticalChance { get; }
    int CriticalMultiplier { get; }

    void TakeDamage( int damage );
    bool IsAlive();
    int GetDamage();
    int GetArmor();
    int GetInitiative();
}
