using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters
{
    public class Fighter : IFighter
    {
        private int _currentHealth;
        public string Name { get; }
        public IRace Race { get; }
        public IClass Class { get; }
        public IArmor Armor { get; set; }
        public IWeapon Weapon { get; set; }

        public int MaxHealth { get; }

        public int CurrentHealth { get; private set; }

        public Fighter( string name, IRace race, IClass fighterClass )
        {
            Name = name;
            Race = race;
            Class = fighterClass;
            Armor = new NoArmor();
            Weapon = new Fists();
            MaxHealth = Class.Health + Race.Health;
            _currentHealth = MaxHealth;
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

        public FighterStats GetStats() => new(
            Health: _currentHealth,
            Damage: Race.Damage + Class.Damage + Weapon.Damage,
            Armor: Race.Armor + Armor.Armor
        );
    }
}