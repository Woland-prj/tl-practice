using Fighters.Models;
using Fighters.Models.Armors;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Fighters.UI;

namespace Fighters.GameProcess.FighterCreation;

public class FighterFactory : IFighterFactory
{
    private readonly IUiService _ui;
    private readonly IFighterVariantsProvider _variantsProvider;

    public FighterFactory( IUiService ui, IFighterVariantsProvider variantsProvider )
    {
        _ui = ui;
        _variantsProvider = variantsProvider;
    }

    public IFighter Create()
    {
        string name = RequestName();

        IRace race = SelectOption( "Выберите расу", _variantsProvider.GetRaces() );
        IClass fighterClass = SelectOption( "Выберите класс", _variantsProvider.GetClasses() );
        IWeapon weapon = SelectOption( "Выберите оружие", _variantsProvider.GetWeapons() );
        IArmor armor = SelectOption( "Выберите броню", _variantsProvider.GetArmors() );

        return new Fighter(
            name,
            race,
            fighterClass,
            armor,
            weapon );
    }

    private string RequestName()
    {
        _ui.WriteLine( "Введите имя персонажа:" );
        string name = "";
        while ( string.IsNullOrEmpty( name ) )
        {
            name = _ui.ReadLine().Trim();
            if ( string.IsNullOrWhiteSpace( name ) )
            {
                _ui.WriteLine( "Имя не может быть пустым. Попробуйте снова:" );
            }
        }

        return name;
    }

    private T SelectOption<T>(
        string message,
        IReadOnlyDictionary<int, T> options )
        where T : INamed
    {
        _ui.WriteLine( message );

        foreach ( KeyValuePair<int, T> option in options )
        {
            _ui.WriteLine( $"{option.Key} - {option.Value.Name}" );
        }

        while ( true )
        {
            int index = _ui.ReadIndex( options.Count );

            if ( options.TryGetValue( index, out T? value ) )
            {
                return value;
            }
        }
    }
}