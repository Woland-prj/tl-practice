namespace Fighters.Models.Races
{
    public class DwarfRace : IRace
    {
        public int Damage => 1;
        public int Health => 16;
        public int Armor => 4;
        public int Initiative => -1;
    }
}