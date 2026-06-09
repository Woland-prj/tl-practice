namespace Fighters.Random;

public interface IRandom
{
    double NextDouble();
    int Next( int min, int max );
}