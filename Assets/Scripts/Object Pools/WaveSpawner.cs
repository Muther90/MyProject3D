using System;
using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour, IResetable
{
    [SerializeField] private Transform[] _spawnPoints;

    private Pool _currentPool;
    private int _toSpawn;
    private int _activeCount;
    private float _spawnInterval;
    private TargetProvider _targetProvider;

    public event Action AllDied;

    private void Awake()
    {
        if (_spawnPoints == null || _spawnPoints.Length == 0)
        {
            _spawnPoints = new Transform[] { this.transform };
        }
    }

    public void Reset()
    {
        if (_currentPool != null)
        {
            _currentPool.Reset();
        }

        _activeCount = 0;
        _toSpawn = 0;
    }

    public void Launch(Pool pool, int count, float interval, TargetProvider targetProvider)
    {
        if (pool != null)
        {
            _targetProvider = targetProvider;
            _currentPool = pool;
            _toSpawn = count;
            _spawnInterval = interval;
            _activeCount = 0;

            StartCoroutine(SpawnRoutine());
        }
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds wait = new (_spawnInterval);

        while (_toSpawn > 0)
        {
            Spawn();
            _toSpawn--;

            yield return wait;
        }
    }

    private void Spawn()
    {
        IPoolObject obj = _currentPool.GetGeneric();

        if (obj is IInitializable<TargetProvider> initializable)
        {
            initializable.Initialize(_targetProvider);
        }

        MonoBehaviour monoObj = obj as MonoBehaviour;

        obj.Returned += ObjectReturned;

        Transform point = _spawnPoints[UnityEngine.Random.Range(0, _spawnPoints.Length)];
        monoObj.transform.SetPositionAndRotation(point.position, point.rotation);

        _activeCount++;
    }

    private void ObjectReturned(IPoolObject obj)
    {
        obj.Returned -= ObjectReturned;
        _activeCount--;

        if (_toSpawn == 0 && _activeCount == 0)
        {
            AllDied?.Invoke();
        }
    }
}