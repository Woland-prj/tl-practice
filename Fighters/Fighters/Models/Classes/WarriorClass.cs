namespace Fighters.Models.Classes;

public class WarriorClass : IClass
{
    public string Name => "Воин";
    public int Damage => 3;
    public int Health => 20;
    public int Initiative => 0;
}