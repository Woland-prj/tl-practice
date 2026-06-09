namespace Fighters.Random;

public class BasicRandom : IRandom
{
    public double NextDouble() => System.Random.Shared.NextDouble();

    public int Next( int min, int max ) => System.Random.Shared.Next( min, max );
}