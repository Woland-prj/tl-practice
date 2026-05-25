using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.GameProcess.FighterCreation
{
    public class FighterBuilder : IFighterBuilder
    {
        private string? _name;
        private IRace? _race;
        private IClass? _class;
        private IWeapon _weapon = new Fists();
        private IArmor _armor = new NoArmor();

        public IFighterBuilder SetName( string name )
        {
            _name = name;
            return this;
        }

        public IFighterBuilder SetRace( IRace race )
        {
            _race = race;
            return this;
        }

        public IFighterBuilder SetClass( IClass fighterClass )
        {
            _class = fighterClass;
            return this;
        }

        public IFighterBuilder SetWeapon( IWeapon weapon )
        {
            _weapon = weapon;
            return this;
        }

        public IFighterBuilder SetArmor( IArmor armor )
        {
            _armor = armor;
            return this;
        }

        public Fighter Build()
        {
            Validate();

            Fighter fighter = new(
                _name!,
                _race!,
                _class!
            );

            fighter.Weapon = _weapon;
            fighter.Armor = _armor;

            return fighter;
        }

        private void Validate()
        {
            if ( string.IsNullOrWhiteSpace( _name ) )
                throw new InvalidOperationException( "Имя не задано" );

            if ( _race is null )
                throw new InvalidOperationException( "Раса не задана" );

            if ( _class is null )
                throw new InvalidOperationException( "Класс не задан" );
        }
    }
}