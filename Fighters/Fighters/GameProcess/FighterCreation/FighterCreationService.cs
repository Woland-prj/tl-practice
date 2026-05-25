using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.UI;

namespace Fighters.GameProcess.FighterCreation
{
    public class FighterCreationService( IUiService ui, IFighterVariantsProvider variantsProvider )
        : IFighterCreationService
    {
        public IFighter Create()
        {
            FighterBuilder builder = new();

            string name = RequestName();

            IRace race = Select( "расу", variantsProvider.GetRaces() );
            IClass fighterClass = Select( "класс", variantsProvider.GetClasses() );
            IWeapon weapon = Select( "оружие", variantsProvider.GetWeapons() );
            IArmor armor = Select( "броню", variantsProvider.GetArmors() );

            return builder
                .SetName( name )
                .SetRace( race )
                .SetClass( fighterClass )
                .SetWeapon( weapon )
                .SetArmor( armor )
                .Build();
        }

        private string RequestName()
        {
            ui.RenderLine( "Введите имя персонажа:" );
            return ui.GetUserInput();
        }

        private T Select<T>(
            string label,
            IReadOnlyList<SelectionOption<T>> opts )
        {
            ui.RenderLine( $"Выберите {label}:" );

            for ( int i = 0; i < opts.Count; i++ )
            {
                ui.RenderLine( $"{i} - {opts[ i ].Name}" );
            }

            int index = ReadIndex( opts.Count );

            return opts[ index ].CreateFunc();
        }

        private int ReadIndex( int max )
        {
            while ( true )
            {
                string input = ui.GetUserInput();

                if ( !int.TryParse( input, out int index ) )
                {
                    ui.RenderLine( "Введите число" );
                    continue;
                }

                if ( index < 0 || index >= max )
                {
                    ui.RenderLine( "Неверный индекс" );
                    continue;
                }

                return index;
            }
        }
    }
}