using System;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public event Action<Collider> SomeoneEntered;
    public event Action<Collider> SomeoneExited;

    private void OnTriggerEnter(Collider collider)
    {
        SomeoneEntered?.Invoke(collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        SomeoneExited?.Invoke(collider);
    }
}
