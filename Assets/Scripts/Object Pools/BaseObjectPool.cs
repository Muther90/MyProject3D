using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseObjectPool<T> : Pool where T : MonoBehaviour, IPoolObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<T> _pool;
    private readonly HashSet<T> _activeObjects = new();

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => 
            {
                var instance = Instantiate(_prefab, transform);

                return instance;
            },     
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnRelease: (obj) => OnReleaseObject(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public override void Reset()
    {
        var objectsToReturn = new List<T>(_activeObjects);

        foreach (var obj in objectsToReturn)
        {
            if (obj != null && obj.gameObject.activeSelf)  
            {
                obj.Returned -= Release; 
                _pool.Release(obj);
            }
        }
    }

    public override IPoolObject GetGeneric()
    {
        return Get();
    }

    protected T Get()
    {
        return _pool.Get();
    }

    private void Release(IPoolObject obj)
    {
        _pool.Release((T)obj);
    }

    private void OnGetObject(T obj)
    {
        obj.gameObject.SetActive(true);
        obj.Returned += Release;
        _activeObjects.Add(obj);
    }

    private void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
        obj.Returned -= Release;
        _activeObjects.Remove(obj);
        obj.Reset();
    }
}