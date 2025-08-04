using UnityEngine;
using UnityEngine.Pool;

public class SpawnerBase<T> : MonoBehaviour where T : MonoBehaviour, ISpawnable
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    protected ObjectPool<T> _pool;

    protected virtual void Awake()
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

    public virtual T Spawn(Vector3 position, Quaternion rotation)
    {
        T obj = _pool.Get();

        obj.transform.position = position;
        obj.transform.rotation = rotation;

        return obj;
    }

    public virtual void Release(ISpawnable obj)
    {
        obj.Taken -= Release;
        _pool.Release((T)obj);
    }

    protected virtual void OnGetObject(T obj)
    {
        obj.Taken += Release;
        obj.gameObject.SetActive(true);
    }

    protected virtual void OnReleaseObject(T obj)
    {
        obj.gameObject.SetActive(false);
    }
}