using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.GameProcess.FighterCreation;

public class FighterVariantsProvider : IFighterVariantsProvider
{
    private readonly IReadOnlyDictionary<int, IRace> _races =
        new Dictionary<int, IRace>
        {
            [ 0 ] = new HumanRace(),
            [ 1 ] = new OrcRace(),
            [ 2 ] = new ElfRace(),
            [ 3 ] = new DwarfRace()
        };

    private readonly IReadOnlyDictionary<int, IWeapon> _weapons =
        new Dictionary<int, IWeapon>
        {
            [ 0 ] = new Fists(),
            [ 1 ] = new Sword(),
            [ 2 ] = new Axe(),
            [ 3 ] = new Dagger(),
            [ 4 ] = new WarHammer(),
            [ 5 ] = new Bow()
        };

    private readonly IReadOnlyDictionary<int, IArmor> _armors =
        new Dictionary<int, IArmor>
        {
            [ 0 ] = new NoArmor(),
            [ 1 ] = new ClothArmor(),
            [ 2 ] = new LeatherArmor(),
            [ 3 ] = new ChainArmor(),
            [ 4 ] = new PlateArmor()
        };

    private readonly IReadOnlyDictionary<int, IClass> _classes =
        new Dictionary<int, IClass>
        {
            [ 0 ] = new WarriorClass(),
            [ 1 ] = new BerserkerClass(),
            [ 2 ] = new KnightClass(),
            [ 3 ] = new AssassinClass(),
            [ 4 ] = new RangerClass(),
            [ 5 ] = new PaladinClass()
        };

    public IReadOnlyDictionary<int, IRace> GetRaces() => _races;
    
    public IReadOnlyDictionary<int, IClass> GetClasses() => _classes;

    public IReadOnlyDictionary<int, IWeapon> GetWeapons() => _weapons;

    public IReadOnlyDictionary<int, IArmor> GetArmors() => _armors;
}