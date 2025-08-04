using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointSpawner<T> : SpawnerBase<T> where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private PointProvider _pointProvider;
    [SerializeField] private float _interval;
    [SerializeField] private bool _isAllSpawned;

    private Coroutine _spawnCoroutine;
    private Queue<Vector3> _availablePoints = new();
    private Dictionary<T, Vector3> _occupiedPoints = new();

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

    public override T Spawn(Vector3 position, Quaternion rotation)
    {
        T spawnedObject = base.Spawn(position, rotation);
        _occupiedPoints[spawnedObject] = position;

        return spawnedObject;
    }

    public override void Release(ISpawnable objToRelease)
    {
        T obj = (T)objToRelease;

        _availablePoints.Enqueue(_occupiedPoints[obj]);
        _occupiedPoints.Remove(obj);

        base.Release(objToRelease);

        if (_spawnCoroutine == null && _availablePoints.Count > 0)
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());
        }
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

    private void SpawnAllObjects()
    {
        int initialCount = _availablePoints.Count;

        for (int i = 0; i < initialCount; i++)
        {
            Spawn(_availablePoints.Dequeue(), Quaternion.identity);
        }
    }
}