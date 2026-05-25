using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.GameProcess.FighterCreation
{
    public class FighterVariantsProvider : IFighterVariantsProvider
    {
        public IReadOnlyList<SelectionOption<IRace>> GetRaces() =>
        [
            new( "Человек", () => new HumanRace() ),
            new( "Орк", () => new OrcRace() ),
            new( "Эльф", () => new ElfRace() ),
            new( "Дворф", () => new DwarfRace() ),
        ];

        public IReadOnlyList<SelectionOption<IClass>> GetClasses() =>
        [
            new( "Воин", () => new WarriorClass() ),
            new( "Берсерк", () => new BerserkerClass() ),
            new( "Рыцарь", () => new KnightClass() ),
            new( "Ассасин", () => new AssassinClass() ),
            new( "Следопыт", () => new RangerClass() ),
            new( "Паладин", () => new PaladinClass() )
        ];

        public IReadOnlyList<SelectionOption<IWeapon>> GetWeapons() =>
        [
            new( "Без оружия", () => new Fists() ),
            new( "Меч", () => new Sword() ),
            new( "Топор", () => new Axe() ),
            new( "Кинжал", () => new Dagger() ),
            new( "Боевой молот", () => new WarHammer() ),
            new( "Лук", () => new Bow() )
        ];

        public IReadOnlyList<SelectionOption<IArmor>> GetArmors() =>
        [
            new( "Без брони", () => new NoArmor() ),
            new( "Тканевая одежда", () => new ClothArmor() ),
            new( "Кожаная броня", () => new LeatherArmor() ),
            new( "Кольчуга", () => new ChainArmor() ),
            new( "Латная броня", () => new PlateArmor() )
        ];
    }
}