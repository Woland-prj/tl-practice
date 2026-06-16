using Fighters.Models.Fighters;
using Fighters.Random;

namespace Fighters.GameProcess.Combat;

public class DamageService : IDamageService
{
    private const double DamageRangeMin = -0.20;
    private const double DamageRangeMax = 0.10;
    private const int MinDamage = 2;
    private readonly IRandom _random;

    public DamageService( IRandom random )
    {
        _random = random;
    }

    public DamageResult CalculateDamage( IFighter attacker, IFighter defender )
    {
        int baseDamage = Math.Max( attacker.GetDamage() - defender.GetArmor(), MinDamage );

        bool isCritical = _random.NextDouble() < attacker.CriticalChance;
        if ( isCritical )
        {
            baseDamage *= attacker.CriticalMultiplier;
        }

        double variation = _random.NextDouble() * ( DamageRangeMax - DamageRangeMin ) + DamageRangeMin;
        int finalDamage = ( int )Math.Round( baseDamage * ( 1 + variation ) );
        finalDamage = Math.Max( finalDamage, 0 );

        return new DamageResult(
            Damage: finalDamage,
            IsCritical: isCritical
        );
    }
}