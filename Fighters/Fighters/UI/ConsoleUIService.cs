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
}