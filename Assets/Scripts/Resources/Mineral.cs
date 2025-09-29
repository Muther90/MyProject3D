using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mineral : MonoBehaviour, IResource
{
    private Rigidbody _rigidbody;

    public event Action<IPoolObject> Taken;

    public Vector3 Position => transform.position;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Reset()
    {
        Drop();
        Taken?.Invoke(this);
    }

    public void PickUp(Transform parent, Transform holdPoint)
    {
        transform.SetParent(parent);
        transform.SetLocalPositionAndRotation(holdPoint.localPosition, holdPoint.localRotation);

        _rigidbody.isKinematic = true;
    }

    public void Drop()
    {
        transform.SetParent(null);
        _rigidbody.isKinematic = false;
    }
}