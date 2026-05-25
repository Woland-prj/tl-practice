namespace Fighters.Models.Classes
{
    public interface IClass
    {
        int Damage { get; }
        int Health { get; }

        int Initiative { get; }
    }
}