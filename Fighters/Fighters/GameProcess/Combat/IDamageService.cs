using Fighters.Models.Fighters;

namespace Fighters.GameProcess.Combat;

public interface IDamageService
{
    DamageResult CalculateDamage( IFighter attacker, IFighter defender );
}