using Fighters;
using Fighters.Commands;
using Fighters.Context;
using Fighters.GameProcess.Combat;
using Fighters.GameProcess.FighterCreation;
using Fighters.Random;
using Fighters.UI;

IRandom random = new BasicRandom();

IUiService ui = new ConsoleUiService();

IFighterVariantsProvider variantsProvider = new FighterVariantsProvider();
IFighterFactory factory = new FighterFactory( ui, variantsProvider );

GameSession session = new();
IDamageService damageService = new DamageService( random );

ApplicationContext context = new();
GameManager gameManager = new( damageService, ui, random );

Dictionary<string, ICommand> commands = new()
{
    [ "add-fighter" ] = new AddFighterCommand( session, ui, factory ),
    [ "play" ] = new PlayCommand( session, gameManager, ui ),
    [ "exit" ] = new ExitCommand( context )
};
CommandDispatcher dispatcher = new( commands, ui );

while ( context.IsRunning )
{
    RenderMenu( commands );

    string command = ui.ReadLine();

    dispatcher.Dispatch( command );
}

return;

void RenderMenu( Dictionary<string, ICommand> commands )
{
    ui.WriteLine( string.Empty );
    ui.WriteLine( "Введите команду:" );
    foreach ( KeyValuePair<string, ICommand> command in commands )
    {
        ui.WriteLine( command.Key );
    }
}