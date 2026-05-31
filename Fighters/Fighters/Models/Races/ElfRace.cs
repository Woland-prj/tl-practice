namespace Fighters.Models.Races;

public class ElfRace : IRace
{
    public string Name => "Эльф";
    public int Damage => 3;
    public int Health => 8;
    public int Armor => 0;
    public int Initiative => 5;
}