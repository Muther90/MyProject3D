using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private Action CollisionEntered;

    private void OnCollisionEnter(Collision collision)
    {
        CollisionEntered?.Invoke();
        StopDetect();
    }

    public void StartDetect(Action CollisionEnter)
    {
        CollisionEntered = CollisionEnter;
        enabled = true;
    }

    public void StopDetect()
    {
        enabled = false;
        CollisionEntered = null;
    }
}