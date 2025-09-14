using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mineral : MonoBehaviour, IResource
{
    private Rigidbody _rigidbody;

    public event Action<IPoolObject> Taken;

    public bool IsCarried { get; private set; }

    private void Start()
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

        transform.localPosition = holdPoint.localPosition;
        transform.localRotation = holdPoint.localRotation;

        _rigidbody.isKinematic = true;
        IsCarried = true;
    }

    public void Drop()
    {
        transform.SetParent(null);
        _rigidbody.isKinematic = false;
        IsCarried = false;
    }
}