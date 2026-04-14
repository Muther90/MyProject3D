using UnityEngine;

public class Jumper : MonoBehaviour, IResetable
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField, Min(0.1f)] private float _jumpPower = 5f;
    [SerializeField, Range(10f, 80f)] private float _jumpDegree = 45f;
    [SerializeField, Min(0f)] private float _maxOffset = 4f;
    [SerializeField, Range(0f, 1f)] private float _accuracy = 0.5f;

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }

    public void JumpTo(Vector3 targetPosition)
    {
        Vector3 actualJumpPosition = GetRandomPosition(targetPosition);

        Vector3 offset = actualJumpPosition - _rigidbody.position;
        Vector3 horizontal = new (offset.x, 0f, offset.z);
        float distance = horizontal.magnitude;
        Vector3 direction = distance > 0.001f ? horizontal.normalized : Vector3.forward;

        float angleRad = _jumpDegree * Mathf.Deg2Rad;
        float horizontalSpeed = _jumpPower * Mathf.Cos(angleRad);
        float verticalSpeed = _jumpPower * Mathf.Sin(angleRad);

        _rigidbody.velocity = new Vector3(
            direction.x * horizontalSpeed,
            verticalSpeed,
            direction.z * horizontalSpeed
        );
    }

    private Vector3 GetRandomPosition(Vector3 target)
    {
        if (Random.value > _accuracy)
        {
            Vector2 circle = Random.insideUnitCircle * _maxOffset;

            return target + new Vector3(circle.x, 0f, circle.y);
        }
        else
        {
            return target;
        }
    }
}