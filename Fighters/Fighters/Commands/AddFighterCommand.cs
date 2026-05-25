using Fighters.GameProcess.Combat;
using Fighters.GameProcess.FighterCreation;
using Fighters.Models.Classes;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.UI;

namespace Fighters.Commands
{
    public class AddFighterCommand(
        GameSession session,
        IUiService ui,
        IFighterCreationService creation ) : ICommand
    {
        public void Execute()
        {
            ui.RenderLine( "Создание персонажа:" );

            IFighter fighter = creation.Create();

            session.Fighters.Add( fighter );

            ui.RenderLine( $"Боец {fighter.Name} добавлен." );
        }
    }
}