using System;
using UnityEngine;

public class CubePointSpawner : PointSpawner<Cube>
{
    [SerializeField] private BombSpawner _bombSpawner;

    public event Action<int, int> OnStateUpdated;

    public override Cube Spawn(Vector3 position, Quaternion rotation)
    {
        Cube obj = base.Spawn(position, rotation);
        StateUpdate();

        return obj;
    }

    public override void Release(ISpawnable objToRelease)
    {
        base.Release(objToRelease);
        StateUpdate();
    }

    protected override void OnGetObject(Cube cube)
    {
        base.OnGetObject(cube);
        cube.Initialize(_bombSpawner);
    }

    private void StateUpdate()
    {
        OnStateUpdated?.Invoke(_pool.CountActive, _pool.CountAll);
    }
}