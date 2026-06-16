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
        _gameManager.StartBattle( _session );
        _session.Fighters.Clear();
    }
}