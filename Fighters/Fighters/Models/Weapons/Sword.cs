namespace Fighters.Models.Weapons;

public class Sword : IWeapon
{
    public string Name => "Меч";
    public int Damage => 5;
    public double CriticalChance => 0.10;
    public int CriticalMultiplier => 2;
}