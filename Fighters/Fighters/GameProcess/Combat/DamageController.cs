using Fighters.Config;
using Fighters.Models.Fighters;

namespace Fighters.GameProcess.Combat
{
    public class DamageController : IDamageController
    {
        private readonly Random _random = new Random();

        public DamageResult CalculateDamage( IFighter attacker, IFighter defender, GameConfig config )
        {
            FighterStats attackerStats = attacker.GetStats();
            FighterStats defenderStats = defender.GetStats();
            int baseDamage = Math.Max( attackerStats.Damage - defenderStats.Armor, 0 );

            bool isCritical = _random.NextDouble() < config.CriticalChance;
            if ( isCritical )
            {
                baseDamage *= config.CriticalMultiplier;
            }

            double variation = _random.NextDouble() * ( config.DamageRangeMax - config.DamageRangeMin ) +
                               config.DamageRangeMin;
            int finalDamage = ( int )Math.Round( baseDamage * ( 1 + variation ) );
            finalDamage = Math.Max( finalDamage, 0 );

            return new DamageResult(
                BaseDamage: baseDamage,
                FinalDamage: finalDamage,
                IsCritical: isCritical
            );
        }
    }
}