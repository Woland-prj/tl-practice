using Fighters.Commands;

namespace Fighters;

public class CommandDispatcher( Dictionary<string, ICommand> commands )
{
    public void Dispatch( string commandName )
    {
        if ( commands.TryGetValue( commandName, out ICommand? command ) )
        {
            command.Execute();
            return;
        }

        Console.WriteLine( "Неизвестная команда" );
    }
}