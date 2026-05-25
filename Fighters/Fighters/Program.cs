using Fighters;
using Fighters.Commands;
using Fighters.Config;
using Fighters.GameProcess.Combat;
using Fighters.GameProcess.FighterCreation;
using Fighters.UI;

IUiService ui = new ConsoleUiService();

IFighterVariantsProvider variantsProvider = new FighterVariantsProvider();

IFighterCreationService creation = new FighterCreationService( ui, variantsProvider );

GameSession session = new();

GameConfig config = GameConfig.Default;

IDamageController damageController = new DamageController();

IInitiativeSystem initiativeSystem = new InitiativeSystem();

GameManager gameManager = new(
    session.Fighters,
    damageController,
    initiativeSystem,
    ui,
    config
);

Dictionary<string, ICommand> commands = new()
{
    [ "add-fighter" ] = new AddFighterCommand( session, ui, creation ),
    [ "play" ] = new PlayCommand( session, gameManager, ui ),
};

CommandDispatcher dispatcher = new( commands );

while ( true )
{
    RenderMenu( commands );

    string command = ui.GetUserInput();

    if ( command == "exit" )
        break;

    dispatcher.Dispatch( command );
}

return;

void RenderMenu( Dictionary<string, ICommand> cmds )
{
    ui.RenderLine( string.Empty );
    ui.RenderLine( "Введите команду:" );
    foreach ( KeyValuePair<string, ICommand> command in cmds )
    {
        ui.RenderLine( command.Key );
    }
}