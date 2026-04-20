using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _moveSpeed;
    [SerializeField, Range(0f, 1f)] private float _fallSpeed;
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private CharacterController _characterController;

    private Vector3 _moveVector;

    private void OnEnable() => _playerInput.Moved += OnMoved;
    private void OnDisable() => _playerInput.Moved -= OnMoved;

    private void FixedUpdate()
    {
        if (_characterController.isGrounded == false)
        {
            _characterController.Move(Vector3.down * _fallSpeed);
        }

        _characterController.Move(_moveVector);
    }

    private void OnMoved(Vector2 direction)
    {
        _moveVector = new Vector3(direction.x, 0, direction.y) * Time.deltaTime * _moveSpeed;
    }
}