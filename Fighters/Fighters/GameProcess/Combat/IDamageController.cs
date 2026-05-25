using Fighters.Config;
using Fighters.Models.Fighters;

namespace Fighters.GameProcess.Combat
{
    public interface IDamageController
    {
        DamageResult CalculateDamage( IFighter attacker, IFighter defender, GameConfig config );
    }

    public record DamageResult( int BaseDamage, int FinalDamage, bool IsCritical );
}