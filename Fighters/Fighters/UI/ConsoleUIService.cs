using Fighters.GameProcess.Combat;
using Fighters.Models.Fighters;

namespace Fighters.UI;

public class ConsoleUiService : IUiService
{
    public void WriteLine( string message )
    {
        Console.WriteLine( message );
    }

    public string ReadLine()
    {
        return Console.ReadLine() ?? string.Empty;
    }

    public int ReadIndex( int max )
    {
        while ( true )
        {
            string input = ReadLine();

            if ( !int.TryParse( input, out int index ) )
            {
                WriteLine( "Введите число" );
                continue;
            }

            if ( index < 0 || index >= max )
            {
                WriteLine( "Неверный индекс" );
                continue;
            }

            return index;
        }
    }

    public void RenderWinner( IFighter? winner )
    {
        WriteLine( string.Empty );
        if (winner is null)
        {
            WriteLine("Все бойцы погибли. Ничья!");
            return;
        }
        WriteLine($"{winner.Name} побеждает!");
    }

    public void RenderBattleStart()
    {
        WriteLine( "Битва начинается!" );
        WriteLine( string.Empty );
    }

    public void RenderAttack( IFighter attacker, IFighter defender, DamageResult result, int round )
    {
        string critical = result.IsCritical ? " КРИТ!" : string.Empty;

        WriteLine(
            $"[{round}] " +
            $"{attacker.Name} наносит " +
            $"{result.Damage} урона " +
            $"{defender.Name}" +
            critical
        );
    }
}