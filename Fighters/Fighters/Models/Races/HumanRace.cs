namespace Fighters.Models.Races
{
    public class HumanRace : IRace
    {
        public int Damage => 2;
        public int Health => 10;
        public int Armor => 1;
        public int Initiative => 1;
    }
}