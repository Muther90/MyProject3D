using UnityEngine;

public interface ICarriable
{
    bool IsCarried { get; }

    void PickUp(Transform parent, Transform holdPoint);
    void Drop();
}