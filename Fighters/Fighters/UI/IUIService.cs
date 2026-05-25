namespace Fighters.UI
{
    public interface IUiService
    {
        void RenderLine( string message );
        string GetUserInput();
    }
}