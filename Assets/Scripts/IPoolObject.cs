using System;

public interface IPoolObject
{
    event Action<IPoolObject> Taken;

    void Reset();
}