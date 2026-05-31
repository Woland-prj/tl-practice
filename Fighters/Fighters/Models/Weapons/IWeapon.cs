namespace Fighters.Models.Weapons;

public interface IWeapon : INamed
{
    int Damage { get; }
    double CriticalChance { get; }
    int CriticalMultiplier { get; }
}