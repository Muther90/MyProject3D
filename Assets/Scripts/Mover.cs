using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody _rigidbody;

    public void MoveTo(Vector3 targetPosition)
    {
        targetPosition.Normalize();

        _rigidbody.velocity = new Vector3(
            targetPosition.x * _speed,
            _rigidbody.velocity.y,
            targetPosition.z * _speed
        );
    }
}