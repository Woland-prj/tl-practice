using Fighters.Context;

namespace Fighters.Commands;

public class ExitCommand(ApplicationContext context) : ICommand
{
    public void Execute()
    {
        context.Stop();
    }
}