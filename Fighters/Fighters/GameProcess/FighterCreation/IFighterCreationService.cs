using Fighters.Models.Fighters;

namespace Fighters.GameProcess.FighterCreation
{
    public interface IFighterCreationService
    {
        IFighter Create();
    }
}