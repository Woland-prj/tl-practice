using Fighters.Config;
using Fighters.Models.Fighters;
using Fighters.UI;

namespace Fighters.GameProcess.Combat;

public class GameManager(
    List<IFighter> fighters,
    IDamageController damageController,
    IInitiativeSystem initiativeSystem,
    IUiService ui,
    GameConfig config )
{
    public void StartBattle()
    {
        if ( fighters.Count < 2 )
        {
            ui.RenderLine( "Недостаточно бойцов." );
            return;
        }

        initiativeSystem.Initialize( fighters );

        RenderBattleStart();

        while ( !initiativeSystem.IsBattleOver() )
        {
            ExecuteTurn();
        }

        RenderWinner();
    }

    private void ExecuteTurn()
    {
        IFighter attacker =
            initiativeSystem.GetCurrentFighter();

        if ( !attacker.IsAlive() )
        {
            initiativeSystem.NextTurn();
            return;
        }

        List<IFighter> targets =
            initiativeSystem
                .GetAliveOpponents( attacker )
                .ToList();

        if ( targets.Count == 0 )
            return;

        IFighter defender = SelectTarget(
            attacker,
            targets
        );

        ProcessAttack( attacker, defender );

        initiativeSystem.NextTurn();
    }

    private void ProcessAttack(
        IFighter attacker,
        IFighter defender )
    {
        DamageResult result =
            damageController.CalculateDamage(
                attacker,
                defender,
                config
            );

        defender.TakeDamage( result.FinalDamage );

        RenderAttack(
            attacker,
            defender,
            result
        );

        if ( !defender.IsAlive() )
        {
            ui.RenderLine(
                $"{defender.Name} умирает!"
            );
        }
    }

    private IFighter SelectTarget(
        IFighter attacker,
        List<IFighter> targets )
    {
        return targets[ 0 ];
    }

    private void RenderBattleStart()
    {
        ui.RenderLine( "Битва начинается!" );
        ui.RenderLine( "" );
    }

    private void RenderAttack(
        IFighter attacker,
        IFighter defender,
        DamageResult result )
    {
        string critical =
            result.IsCritical
                ? " КРИТ!"
                : "";

        ui.RenderLine(
            $"[{initiativeSystem.CurrentRound}] " +
            $"{attacker.Name} наносит " +
            $"{result.FinalDamage} урона " +
            $"{defender.Name}" +
            critical
        );
    }

    private void RenderWinner()
    {
        IFighter? winner =
            initiativeSystem.GetWinner();

        ui.RenderLine( "" );
        ui.RenderLine(
            $"{winner?.Name} побеждает!"
        );
    }
}