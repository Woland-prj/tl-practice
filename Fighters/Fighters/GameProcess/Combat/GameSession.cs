using Fighters.Models.Fighters;

namespace Fighters.GameProcess.Combat;

public class GameSession
{
    public List<IFighter> Fighters { get; } = [];
}