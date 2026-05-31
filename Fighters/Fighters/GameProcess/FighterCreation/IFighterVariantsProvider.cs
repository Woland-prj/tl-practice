using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.GameProcess.FighterCreation;

public interface IFighterVariantsProvider
{
    IReadOnlyDictionary<int, IRace> GetRaces();
    IReadOnlyDictionary<int, IClass> GetClasses();
    IReadOnlyDictionary<int, IWeapon> GetWeapons();
    IReadOnlyDictionary<int, IArmor> GetArmors();
}