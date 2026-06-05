using Fighters.UI;

namespace Fighters.Commands;

public class CommandDispatcher
{
    private readonly Dictionary<string, ICommand> _commands;
    private readonly IUiService _ui;

    public CommandDispatcher(Dictionary<string, ICommand> commands, IUiService ui)
    {
        _commands = commands;
        _ui = ui;
    }

    public void Dispatch( string commandName )
    {
        if ( _commands.TryGetValue( commandName, out ICommand? command ) )
        {
            command.Execute();
            return;
        }

        _ui.WriteLine( "Неизвестная команда" );
    }
}