using Fighters.Models.Fighters;
using Fighters.Random;
using Fighters.UI;

namespace Fighters.GameProcess.Combat;

public class GameManager
{
    private const int InitiativeMin = -2;
    private const int InitiativeMax = 3;

    private List<IFighter> _fighters;
    private readonly IDamageService _damageService;
    private readonly IUiService _ui;
    private readonly IGameRenderer _renderer;
    private readonly IRandom _random;

    private int _currentTurnIndex;
    private int _currentRound = 1;

    public GameManager( IDamageService damageService, IUiService ui, IGameRenderer renderer, IRandom random )
    {
        _damageService = damageService;
        _ui = ui;
        _random = random;
        _renderer = renderer;
    }

    public void StartBattle( GameSession session )
    {
        _fighters = session.Fighters;

        if ( !HasEnoughAliveFighters() )
        {
            _ui.WriteLine( "Недостаточно бойцов. Необходимо минимум 2 живых бойца, чтобы начать" );
            return;
        }

        _currentRound = 1;

        ShuffleFighters();

        _renderer.RenderBattleStart();

        while ( !IsBattleOver() )
        {
            PlayRound();
        }

        _renderer.RenderWinner( GetWinner() );
    }

    private bool HasEnoughAliveFighters()
    {
        return _fighters.Count( fighter => fighter.IsAlive() ) >= 2;
    }

    private void ShuffleFighters()
    {
        _fighters = _fighters
            .OrderByDescending( fighter => RandomizeInitiative( fighter.GetInitiative() ) )
            .ToList();

        _currentTurnIndex = 0;
    }

    private bool IsBattleOver()
    {
        return _fighters.Count( fighter => fighter.IsAlive() ) <= 1;
    }

    private void PlayRound()
    {
        IFighter attacker = GetCurrentFighter();

        List<IFighter> targets = GetAliveOpponents( attacker );

        if ( targets.Count == 0 )
        {
            return;
        }

        ProcessAttack( attacker, targets[ 0 ] );

        AdvanceTurn();

        if ( IsRoundEnd() )
        {
            _currentRound++;
        }
    }

    private int RandomizeInitiative( int initiative )
    {
        return initiative + _random.Next( InitiativeMin, InitiativeMax );
    }

    private IFighter GetCurrentFighter()
    {
        while ( !_fighters[ _currentTurnIndex ].IsAlive() )
        {
            AdvanceTurn();
        }

        return _fighters[ _currentTurnIndex ];
    }

    private List<IFighter> GetAliveOpponents( IFighter attacker )
    {
        return _fighters.FindAll( fighter => fighter != attacker && fighter.IsAlive() );
    }

    private void ProcessAttack( IFighter attacker, IFighter defender )
    {
        DamageResult result = _damageService.CalculateDamage( attacker, defender );

        defender.TakeDamage( result.Damage );

        _renderer.RenderAttack( attacker, defender, result, _currentRound );

        if ( !defender.IsAlive() )
        {
            _ui.WriteLine( $"{defender.Name} умирает!" );
        }
    }

    private void AdvanceTurn()
    {
        _currentTurnIndex++;

        if ( _currentTurnIndex >= _fighters.Count )
        {
            _currentTurnIndex = 0;
        }
    }

    private bool IsRoundEnd()
    {
        return _currentTurnIndex == 0;
    }

    private IFighter? GetWinner()
    {
        return _fighters.FirstOrDefault( fighter => fighter.IsAlive() );
    }
}