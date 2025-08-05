using System;
using UnityEngine;

public class BombSpawner : SpawnerBase<Bomb> , ISpawnerStats
{
    public event Action<int, int> OnStateUpdated;

    public override Bomb Spawn(Vector3 position, Quaternion rotation)
    {
        Bomb obj = base.Spawn(position, rotation);
        StateUpdate();

        return obj;
    }

    public override void Release(ISpawnable obj)
    {
        base.Release(obj);
        StateUpdate();
    }

    private void StateUpdate()
    {
        OnStateUpdated?.Invoke(_pool.CountActive, _pool.CountAll);
    }
}