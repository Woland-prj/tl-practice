namespace Fighters.Models.Races;

public class HumanRace : IRace
{
    public string Name => "Человек";
    public int Damage => 2;
    public int Health => 10;
    public int Armor => 1;
    public int Initiative => 1;
}