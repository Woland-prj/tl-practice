using Fighters.Models.Fighters;

namespace Fighters.GameProcess.Combat;

public interface IInitiativeSystem
{
    void Initialize( IEnumerable<IFighter> fighters );
    IFighter GetCurrentFighter();
    IEnumerable<IFighter> GetAliveOpponents( IFighter fighter );
    void NextTurn();
    int CurrentRound { get; }
    bool IsBattleOver();
    IFighter? GetWinner();
}