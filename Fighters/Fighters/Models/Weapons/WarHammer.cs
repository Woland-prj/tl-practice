namespace Fighters.Models.Weapons;

public class WarHammer : IWeapon
{
    public string Name => "Боевой молот";
    public int Damage => 8;
    public double CriticalChance => 0.03;
    public int CriticalMultiplier => 4;
}