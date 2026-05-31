using Fighters.GameProcess.Combat;
using Fighters.UI;

namespace Fighters.Commands;

public class PlayCommand : ICommand
{
    private readonly GameSession _session;
    private readonly GameManager _gameManager;
    private readonly IUiService _ui;

    public PlayCommand( GameSession session, GameManager gameManager, IUiService ui )
    {
        _session = session;
        _gameManager = gameManager;
        _ui = ui;
    }

    public void Execute()
    {
        if ( _session.Fighters.Count < 2 )
        {
            _ui.WriteLine( "Недостаточно бойцов." );
            return;
        }

        _gameManager.StartBattle();
    }
}