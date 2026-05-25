using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.GameProcess.FighterCreation
{
    public record SelectionOption<T>(
        string Name,
        Func<T> CreateFunc
    );

    public interface IFighterVariantsProvider
    {
        IReadOnlyList<SelectionOption<IRace>> GetRaces();
        IReadOnlyList<SelectionOption<IClass>> GetClasses();
        IReadOnlyList<SelectionOption<IWeapon>> GetWeapons();
        IReadOnlyList<SelectionOption<IArmor>> GetArmors();
    }
}