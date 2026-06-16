namespace Fighters.Models.Classes;

public class KnightClass : IClass
{
    public string Name => "Рыцарь";
    public int Damage => 2;
    public int Health => 30;
    public int Initiative => -1;
}