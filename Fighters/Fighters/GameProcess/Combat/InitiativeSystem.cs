using Fighters.Config;
using Fighters.Models.Fighters;

namespace Fighters.GameProcess.Combat;

public class InitiativeSystem : IInitiativeSystem
{
    private readonly List<InitiativeEntry> _turnOrder = [ ];

    private int _currentIndex = 0;

    public int CurrentRound { get; private set; } = 1;

    public void Initialize( IEnumerable<IFighter> fighters )
    {
        _turnOrder.Clear();

        Random random = new();

        foreach ( IFighter fighter in fighters )
        {
            int initiative =
                fighter.Class.Initiative +
                fighter.Race.Initiative +
                random.Next( -2, 3 );

            _turnOrder.Add( new InitiativeEntry(
                fighter,
                initiative
            ) );
        }

        _turnOrder.Sort( ( a, b ) =>
            b.Initiative.CompareTo( a.Initiative ) );
    }

    public IFighter GetCurrentFighter()
    {
        return _turnOrder[ _currentIndex ].Fighter;
    }

    public IEnumerable<IFighter> GetAliveOpponents( IFighter attacker )
    {
        return _turnOrder
            .Select( entry => entry.Fighter )
            .Where( fighter =>
                fighter != attacker &&
                fighter.IsAlive() );
    }

    public void NextTurn()
    {
        _currentIndex++;

        if ( _currentIndex >= _turnOrder.Count )
        {
            _currentIndex = 0;
            CurrentRound++;
        }

        SkipDeadFighters();
    }

    public bool IsBattleOver()
    {
        return _turnOrder
            .Count( x => x.Fighter.IsAlive() ) <= 1;
    }

    public IFighter? GetWinner()
    {
        return _turnOrder
            .Select( x => x.Fighter )
            .FirstOrDefault( f => f.IsAlive() );
    }

    private void SkipDeadFighters()
    {
        int aliveCount = _turnOrder.Count( x => x.Fighter.IsAlive() );

        if ( aliveCount <= 1 )
            return;

        while ( !_turnOrder[ _currentIndex ].Fighter.IsAlive() )
        {
            _currentIndex++;

            if ( _currentIndex >= _turnOrder.Count )
            {
                _currentIndex = 0;
                CurrentRound++;
            }
        }
    }
}

public record InitiativeEntry(
    IFighter Fighter,
    int Initiative
);