using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _distanceToTarget;

    private Rigidbody _rigidbody;
    private Coroutine _moveCoroutine;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public IEnumerator MoveTo(Vector3 targetPosition)
    {
        while ((transform.position - targetPosition).sqrMagnitude > _distanceToTarget * _distanceToTarget)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                _rigidbody.MoveRotation(targetRotation);
            }

            Vector3 movement = direction * _speed;

            _rigidbody.velocity = new Vector3(movement.x, _rigidbody.velocity.y, movement.z);

            yield return null;
        }

        _rigidbody.velocity = Vector3.zero;
    }
}