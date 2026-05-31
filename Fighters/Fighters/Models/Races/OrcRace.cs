namespace Fighters.Models.Races;

public class OrcRace : IRace
{
    public string Name => "Орк";
    public int Damage => 5;
    public int Health => 18;
    public int Armor => 0;
    public int Initiative => -2;
}