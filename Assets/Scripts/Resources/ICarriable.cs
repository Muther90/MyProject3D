using UnityEngine;

public interface ICarriable
{
    Vector3 Position { get; }

    void PickUp(Transform parent, Transform holdPoint);
    void Drop();
}