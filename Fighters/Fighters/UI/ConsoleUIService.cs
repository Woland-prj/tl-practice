namespace Fighters.UI
{
    public class ConsoleUiService : IUiService
    {
        public void RenderLine( string message )
        {
            Console.WriteLine( message );
        }

        public string GetUserInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }
    }
}