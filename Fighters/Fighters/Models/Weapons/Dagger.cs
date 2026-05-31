namespace Fighters.Models.Weapons;

public class Dagger : IWeapon
{
    public string Name => "Кинжал";
    public int Damage => 3;
    public double CriticalChance => 0.25;
    public int CriticalMultiplier => 2;
}