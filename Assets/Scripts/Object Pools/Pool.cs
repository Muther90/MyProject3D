using UnityEngine;

public abstract class Pool : MonoBehaviour
{
    public abstract IPoolObject GetGeneric();
    public abstract void Reset();
}