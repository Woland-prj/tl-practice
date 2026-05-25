namespace Fighters.Config
{
    public class GameConfig
    {
        public double DamageRangeMin { get; init; } = -0.20;
        public double DamageRangeMax { get; init; } = 0.10;

        public double CriticalChance { get; init; } = 0.15;
        public int CriticalMultiplier { get; init; } = 2;

        public int BaseInitiative { get; init; } = 10;
        public int InitiativeRange { get; init; } = 3;

        public int MaxRounds { get; init; } = 100;
        public bool AllowFriendlyFire { get; init; } = false;

        public static GameConfig Default => new();
    }
}