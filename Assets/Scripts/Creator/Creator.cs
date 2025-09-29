using UnityEngine;

public abstract class Creator<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    [SerializeField] protected int _cost;
    [SerializeField] protected float _heightOffset;

    public int Cost => _cost;

    public virtual T Create(Vector3 spawnPoint)
    {
        Vector3 spawnPosition = spawnPoint + Vector3.up * _heightOffset;
        T instance = Instantiate(_prefab, spawnPosition, Quaternion.identity);
        InitializeInstance(instance);

        return instance;
    }

    public virtual T Create(Vector3 spawnPoint, Transform parent)
    {
        Vector3 spawnPosition = spawnPoint + Vector3.up * _heightOffset;
        T instance = Instantiate(_prefab, spawnPosition, Quaternion.identity, parent);
        InitializeInstance(instance);

        return instance;

    }

    protected virtual void InitializeInstance(T instance) {}
}