using Fighters.Models.Fighters;

namespace Fighters.GameProcess.Combat;

public interface IDamageController
{
    DamageResult CalculateDamage( IFighter attacker, IFighter defender );
}