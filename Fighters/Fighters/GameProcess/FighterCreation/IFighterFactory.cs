using Fighters.Models.Fighters;

namespace Fighters.GameProcess.FighterCreation;

public interface IFighterFactory
{
    IFighter Create();
}