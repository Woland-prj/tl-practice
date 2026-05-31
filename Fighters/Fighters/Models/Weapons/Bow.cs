namespace Fighters.Models.Weapons;

public class Bow : IWeapon
{
    public string Name => "Лук";
    public int Damage => 4;
    public double CriticalChance => 0.08;
    public int CriticalMultiplier => 4;
}