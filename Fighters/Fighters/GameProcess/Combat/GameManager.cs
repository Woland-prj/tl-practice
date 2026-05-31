using Fighters.Models.Fighters;
using Fighters.Random;
using Fighters.UI;

namespace Fighters.GameProcess.Combat;

public class GameManager
{
    private const int _initiativeMin = -2;
    private const int _initiativeMax = 3;

    private readonly List<IFighter> _fighters;
    private readonly IDamageController _damageController;
    private readonly IUiService _ui;
    private readonly IRandom _random;

    private List<IFighter> _turnOrder = [ ];
    private int _currentTurnIndex;
    private int _currentRound = 1;

    public GameManager(
        List<IFighter> fighters,
        IDamageController damageController,
        IUiService ui,
        IRandom random )
    {
        _fighters = fighters;
        _damageController = damageController;
        _ui = ui;
        _random = random;
    }

    public void StartBattle()
    {
        if ( _fighters.Count < 2 )
        {
            _ui.WriteLine( "Недостаточно бойцов." );
            return;
        }

        InitializeTurnOrder();

        RenderBattleStart();

        while ( !IsBattleOver() )
        {
            ExecuteTurn();
        }

        RenderWinner();
    }

    private void ExecuteTurn()
    {
        IFighter attacker = GetCurrentFighter();

        List<IFighter> targets = GetAliveOpponents( attacker );

        if ( targets.Count == 0 )
        {
            return;
        }

        IFighter defender = targets[ 0 ];

        ProcessAttack( attacker, defender );

        AdvanceTurn();
    }

    private void ProcessAttack( IFighter attacker, IFighter defender )
    {
        DamageResult result = _damageController.CalculateDamage( attacker, defender );

        defender.TakeDamage( result.FinalDamage );

        RenderAttack( attacker, defender, result );

        if ( !defender.IsAlive() )
        {
            _ui.WriteLine( $"{defender.Name} умирает!" );
        }
    }

    private void InitializeTurnOrder()
    {
        _turnOrder = _fighters
            .OrderByDescending( fighter => RandomizeInitiative(
                    fighter.GetInitiative()
                )
            )
            .ToList();

        _currentTurnIndex = 0;
        _currentRound = 1;
    }

    private IFighter GetCurrentFighter()
    {
        while ( !_turnOrder[ _currentTurnIndex ].IsAlive() )
        {
            AdvanceTurn();
        }

        return _turnOrder[ _currentTurnIndex ];
    }

    private List<IFighter> GetAliveOpponents( IFighter attacker )
    {
        return _fighters
            .Where( fighter =>
                fighter != attacker &&
                fighter.IsAlive()
            )
            .ToList();
    }

    private void AdvanceTurn()
    {
        _currentTurnIndex++;

        if ( _currentTurnIndex >= _turnOrder.Count )
        {
            _currentTurnIndex = 0;
            _currentRound++;
        }
    }

    private bool IsBattleOver()
    {
        return _fighters.Count( fighter => fighter.IsAlive() ) <= 1;
    }

    private IFighter? GetWinner()
    {
        return _fighters.FirstOrDefault( fighter => fighter.IsAlive() );
    }

    private int RandomizeInitiative( int initiative )
    {
        return initiative + _random.Next( _initiativeMin, _initiativeMax );
    }

    private void RenderBattleStart()
    {
        _ui.WriteLine( "Битва начинается!" );
        _ui.WriteLine( string.Empty );
    }

    private void RenderAttack( IFighter attacker, IFighter defender, DamageResult result )
    {
        string critical = result.IsCritical ? " КРИТ!" : string.Empty;

        _ui.WriteLine(
            $"[{_currentRound}] " +
            $"{attacker.Name} наносит " +
            $"{result.FinalDamage} урона " +
            $"{defender.Name}" +
            critical
        );
    }

    private void RenderWinner()
    {
        IFighter? winner = GetWinner();

        _ui.WriteLine( string.Empty );
        _ui.WriteLine( $"{winner?.Name} побеждает!" );
    }
}