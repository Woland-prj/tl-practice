using Fighters.Models.Fighters;
using Fighters.Random;

namespace Fighters.GameProcess.Combat;

public class DamageController : IDamageController
{
    private readonly double _damageRangeMin = -0.20;
    private readonly double _damageRangeMax = 0.10;
    private readonly IRandom _random;

    public DamageController( IRandom random )
    {
        _random = random;
    }

    public DamageResult CalculateDamage( IFighter attacker, IFighter defender )
    {
        int baseDamage = Math.Max( attacker.GetDamage() - defender.GetArmor(), 0 );

        bool isCritical = _random.NextDouble() < attacker.CriticalChance;
        if ( isCritical )
        {
            baseDamage *= attacker.CriticalMultiplier;
        }

        double variation = _random.NextDouble() * ( _damageRangeMax - _damageRangeMin ) + _damageRangeMin;
        int finalDamage = ( int )Math.Round( baseDamage * ( 1 + variation ) );
        finalDamage = Math.Max( finalDamage, 0 );

        return new DamageResult(
            BaseDamage: baseDamage,
            FinalDamage: finalDamage,
            IsCritical: isCritical
        );
    }
}