using Fighters.GameProcess.Combat;
using Fighters.GameProcess.FighterCreation;
using Fighters.Models.Fighters;
using Fighters.UI;

namespace Fighters.Commands;

public class AddFighterCommand : ICommand
{
    private readonly GameSession _session;
    private readonly IUiService _ui;
    private readonly IFighterFactory _factory;

    public AddFighterCommand( GameSession session, IUiService ui, IFighterFactory factory )
    {
        _session = session;
        _ui = ui;
        _factory = factory;
    }

    public void Execute()
    {
        _ui.WriteLine( "Создание персонажа:" );

        IFighter fighter = _factory.Create();

        _session.Fighters.Add( fighter );

        _ui.WriteLine( $"Боец {fighter.Name} добавлен." );
    }
}