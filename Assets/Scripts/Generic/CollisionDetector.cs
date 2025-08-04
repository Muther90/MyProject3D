using UnityEngine;

public abstract class CollisionDetector<T> : MonoBehaviour where T : UnityEngine.Component
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<T>(out _))
        {
            OnComponentDetected();
        }
    }

    protected abstract void OnComponentDetected();
}
