using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;

    private PlayerInput _playerInput;
    private Transform _transform;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _transform = transform;
    }

    private void LateUpdate()
    {
        Move();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void Move()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 worldMovement = _playerInput.Player.Move.ReadValue<Vector3>() * scaledMoveSpeed;
        _transform.Translate(worldMovement, Space.World);
    }
}