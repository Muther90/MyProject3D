using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BaseObjectPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private ObjectPool<T> _pool;
    private readonly HashSet<T> _activeObjects = new();

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => OnGetObject(obj),
            actionOnRelease: (obj) => OnReleaseObject(obj),
            actionOnDestroy: (obj) => Destroy(obj.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCapacity,
            maxSize: _maxSize
        );
    }

    public virtual void Reset()
    {
        var objectsToReturn = new List<T>(_activeObjects);

        foreach (var obj in objectsToReturn)
        {
            if (obj != null && obj.gameObject.activeSelf)  
            {
                obj.Taken -= Release; 
                _pool.Release(obj);
            }
        }

        _activeObjects.Clear();
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
        obj.Taken += Release;
        obj.gameObject.SetActive(true);
        _activeObjects.Add(obj);
    }

    private void OnReleaseObject(T obj)
    {
        obj.Taken -= Release;
        obj.gameObject.SetActive(false);
        _activeObjects.Remove(obj);
    }
}