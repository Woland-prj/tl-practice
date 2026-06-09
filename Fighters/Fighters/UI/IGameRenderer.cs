using Fighters.Commands;
using Fighters.GameProcess.Combat;
using Fighters.Models.Fighters;

namespace Fighters.UI;

public interface IGameRenderer
{
    void RenderMenu( Dictionary<string, ICommand> commands );
    void RenderBattleStart();
    void RenderAttack( IFighter attacker, IFighter defender, DamageResult result, int round );
    void RenderWinner( IFighter? winner );
}