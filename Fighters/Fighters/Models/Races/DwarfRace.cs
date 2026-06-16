namespace Fighters.Models.Races;

public class DwarfRace : IRace
{
    public string Name => "Дворф";
    public int Damage => 1;
    public int Health => 16;
    public int Armor => 4;
    public int Initiative => -1;
}