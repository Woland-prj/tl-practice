namespace Fighters.Models.Weapons;

public class Fists : IWeapon
{
    public string Name => "Без оружия";
    public int Damage => 1;
    public double CriticalChance => 0.00;
    public int CriticalMultiplier => 1;
}