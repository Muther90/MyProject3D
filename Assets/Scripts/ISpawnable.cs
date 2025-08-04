using System;

public interface ISpawnable
{
    event Action<ISpawnable> Taken;
}
