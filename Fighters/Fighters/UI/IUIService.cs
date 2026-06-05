using Fighters.GameProcess.Combat;
using Fighters.Models.Fighters;

namespace Fighters.UI;

public interface IUiService
{
    void WriteLine( string message );
    string ReadLine();
    int ReadIndex( int max);
    void RenderBattleStart();
    void RenderAttack( IFighter attacker, IFighter defender, DamageResult result, int round );
    void RenderWinner( IFighter? winner );
}