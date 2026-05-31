namespace Fighters.Models.Classes;

public class PaladinClass : IClass
{
    public string Name => "Паладин";
    public int Damage => 3;
    public int Health => 24;
    public int Initiative => 0;
}