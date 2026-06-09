namespace Fighters.UI;

public interface IUiService
{
    void WriteLine( string message );
    string ReadLine();
    int ReadIndex( int max );
}