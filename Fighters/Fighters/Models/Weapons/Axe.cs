namespace Fighters.Models.Weapons;

public class Axe : IWeapon
{
    public string Name => "Топор";
    public int Damage => 7;
    public double CriticalChance => 0.05;
    public int CriticalMultiplier => 3;
}