using Fighters.GameProcess.Combat;
using Fighters.UI;

namespace Fighters.Commands;

public class PlayCommand(
    GameSession session,
    GameManager gameManager,
    IUiService ui ) : ICommand
{
    public void Execute()
    {
        if ( session.Fighters.Count < 2 )
        {
            ui.RenderLine( "Недостаточно бойцов." );
            return;
        }

        gameManager.StartBattle();
    }
}