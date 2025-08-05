using System;

public interface ISpawnerStats
{
    event Action<int, int> OnStateUpdated;
}
