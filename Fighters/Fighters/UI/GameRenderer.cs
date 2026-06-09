using Fighters.Commands;
using Fighters.GameProcess.Combat;
using Fighters.Models.Fighters;

namespace Fighters.UI;

public class GameRenderer : IGameRenderer
{
    private readonly IUiService _ui;

    public GameRenderer( IUiService ui )
    {
        _ui = ui;
    }
    
    public void RenderMenu( Dictionary<string, ICommand> commands )
    {
        _ui.WriteLine( string.Empty );
        _ui.WriteLine( "Введите команду:" );
        foreach ( KeyValuePair<string, ICommand> command in commands )
        {
            _ui.WriteLine( command.Key );
        }
    }

    public void RenderWinner( IFighter? winner )
    {
        _ui.WriteLine( string.Empty );
        if ( winner is null )
        {
            _ui.WriteLine( "Все бойцы погибли. Ничья!" );
            return;
        }

        _ui.WriteLine( $"{winner.Name} побеждает!" );
    }

    public void RenderBattleStart()
    {
        _ui.WriteLine( "Битва начинается!" );
        _ui.WriteLine( string.Empty );
    }

    public void RenderAttack( IFighter attacker, IFighter defender, DamageResult result, int round )
    {
        string critical = result.IsCritical ? " КРИТ!" : string.Empty;

        _ui.WriteLine(
            $"[{round}] " +
            $"{attacker.Name} наносит " +
            $"{result.Damage} урона " +
            $"{defender.Name}" +
            critical
        );
    }
}