using Fighters.Context;

namespace Fighters.Commands;

public class ExitCommand : ICommand
{
    private readonly ApplicationContext _context;

    public ExitCommand(ApplicationContext context)
    {
        _context = context;
    }

    public void Execute()
    {
        _context.Stop();
    }
}