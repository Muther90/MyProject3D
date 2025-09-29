using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float _moveSpeed;
    [SerializeField] private Camera _mainCamera;

    private PlayerInput _playerInput;
    private Transform _transform;
    private ISelectable _currentSelection;

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
        _playerInput.Player.Select.performed += OnChoice;
        _playerInput.Player.Deployment.performed += OnDeployment;
    }

    private void OnDisable()
    {
        _playerInput.Disable(); 
        _playerInput.Player.Select.performed -= OnChoice;
        _playerInput.Player.Deployment.performed -= OnDeployment;
    }

    private void Move()
    {
        float scaledMoveSpeed = _moveSpeed * Time.deltaTime;
        Vector3 worldMovement = _playerInput.Player.Move.ReadValue<Vector3>() * scaledMoveSpeed;
        _transform.Translate(worldMovement, Space.World);
    }

    private void OnChoice(InputAction.CallbackContext context)
    {
        if (_currentSelection != null)
        {
            _currentSelection = null;
        }

        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<ISelectable>(out ISelectable selectable))
            {
                _currentSelection = selectable;
            }
        }
    }

    private void OnDeployment(InputAction.CallbackContext context)
    {
        if (_currentSelection != null)
        {
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _currentSelection.DeployFlag(hit.point);
            }
        }
    }
}