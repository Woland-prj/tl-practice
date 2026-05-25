using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.GameProcess.FighterCreation
{
    public interface IFighterBuilder
    {
        IFighterBuilder SetName( string name );
        IFighterBuilder SetRace( IRace race );
        IFighterBuilder SetClass( IClass fighterClass );
        IFighterBuilder SetWeapon( IWeapon weapon );
        IFighterBuilder SetArmor( IArmor armor );
        Fighter Build();
    }
}