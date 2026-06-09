namespace Fighters.Random;

public class BasicRandom : IRandom
{
    private static readonly System.Random _random = new System.Random();

    public double NextDouble()
    {
        return _random.NextDouble();
    }

    public int Next( int min, int max )
    {
        return _random.Next( min, max );
    }
}