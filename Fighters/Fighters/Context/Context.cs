namespace Fighters.Context;

public class ApplicationContext
{
    public bool IsRunning { get; private set; } = true;

    public void Stop()
    {
        IsRunning = false;
    }
}