using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CubePointSpawner : SpawnerBase<Cube> , ISpawnerStats
{
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private PointProvider _pointProvider;
    [SerializeField] private float _interval;
    [SerializeField] private bool _isAllSpawned;

    private Coroutine _spawnCoroutine;
    private Queue<Vector3> _availablePoints = new();
    private Dictionary<Cube, Vector3> _occupiedPoints = new();

    public event Action<int, int> OnStateUpdated;

    protected override void Awake()
    {
        InitializeSpawnPoints();
        base.Awake();
    }

    private void OnEnable()
    {
        if (_isAllSpawned)
        {
            SpawnAllObjects();
        }
        else
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
    }

    private void OnDisable()
    {
        if (_spawnCoroutine != null)
        {
            StopCoroutine(_spawnCoroutine);
            _spawnCoroutine = null;
        }
    }

    public override Cube Spawn(Vector3 position, Quaternion rotation)
    {
        Cube spawnedObject = base.Spawn(position, rotation);
        _occupiedPoints[spawnedObject] = position;

        StateUpdate();

        return spawnedObject;
    }

    public override void Release(ISpawnable objToRelease)
    {
        Cube cube = (Cube)objToRelease;

        cube.BombRequested -= SpawnBombOnRequest;
        _availablePoints.Enqueue(_occupiedPoints[cube]);
        _occupiedPoints.Remove(cube);

        base.Release(objToRelease);

        if (_spawnCoroutine == null && _availablePoints.Count > 0)
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }

        StateUpdate();
    }

    protected override void OnGetObject(Cube cube)
    {
        base.OnGetObject(cube);
        cube.BombRequested += SpawnBombOnRequest;
    }

    private void SpawnBombOnRequest(Vector3 position)
    {
        _bombSpawner.Spawn(position, Quaternion.identity);
    }

    private IEnumerator SpawnCoroutine()
    {
        WaitForSeconds waitForSeconds = new(_interval);

        while (_availablePoints.Count > 0)
        {
            Spawn(_availablePoints.Dequeue(), Quaternion.identity);

            yield return waitForSeconds;
        }

        _spawnCoroutine = null;
    }

    private void InitializeSpawnPoints()
    {
        if (_pointProvider == null)
        {
            throw new System.Exception("Place(s) of spawn is not assigned!");
        }

        Vector3[] spawnPoints = _pointProvider.GetPoints();

        foreach (Vector3 point in spawnPoints)
        {
            _availablePoints.Enqueue(point);
        }
    }

    private void StateUpdate()
    {
        OnStateUpdated?.Invoke(_pool.CountActive, _pool.CountAll);
    }

    private void SpawnAllObjects()
    {
        int initialCount = _availablePoints.Count;

        for (int i = 0; i < initialCount; i++)
        {
            Spawn(_availablePoints.Dequeue(), Quaternion.identity);
        }
    }
}