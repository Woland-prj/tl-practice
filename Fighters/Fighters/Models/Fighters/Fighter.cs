using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Fighter : IFighter
{
    private readonly int _baseCriticalMultiplier = 2;
    private readonly double _baseCriticalChance = 0.15;
    private int _currentHealth;
    public string Name { get; }
    public IRace Race { get; }
    public IClass Class { get; }
    public IArmor Armor { get; }
    public IWeapon Weapon { get; }

    public double CriticalChance => _baseCriticalChance + Weapon.CriticalChance;
    public int CriticalMultiplier => _baseCriticalMultiplier + Weapon.CriticalMultiplier;

    public Fighter( string name, IRace race, IClass fighterClass, IArmor armor, IWeapon weapon )
    {
        Name = name;
        Race = race;
        Class = fighterClass;
        Armor = armor;
        Weapon = weapon;
        _currentHealth = Class.Health + Race.Health;
    }

    public void TakeDamage( int damage )
    {
        if ( damage > 0 )
        {
            _currentHealth -= damage;
        }

        if ( _currentHealth <= 0 )
        {
            _currentHealth = 0;
        }
    }

    public bool IsAlive()
    {
        return _currentHealth > 0;
    }

    public int GetDamage()
    {
        return Race.Damage + Class.Damage + Weapon.Damage;
    }

    public int GetArmor()
    {
        return Race.Armor + Armor.Armor;
    }

    public int GetInitiative()
    {
        return Race.Initiative + Class.Initiative;
    }
}