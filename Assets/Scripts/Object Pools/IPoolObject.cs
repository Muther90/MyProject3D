using System;

public interface IPoolObject : IResetable
{
    event Action<IPoolObject> Returned;
}