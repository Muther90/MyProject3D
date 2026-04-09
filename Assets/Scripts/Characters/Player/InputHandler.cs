using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector3 MoveInput { get; private set; }
    public Vector2 LookInput { get; private set; }

    public event Action Attacked;
    public event Action WeaponSwitched;

    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.Attack.performed += OnAttack;
        _playerInput.Player.SwitchWeapon.performed += OnSwitchWeapon;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        _playerInput.Player.Attack.performed -= OnAttack;
        _playerInput.Player.SwitchWeapon.performed -= OnSwitchWeapon;
    }

    private void Update()
    {
        MoveInput = _playerInput.Player.Move.ReadValue<Vector3>();
        LookInput = _playerInput.Player.Look.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Attacked?.Invoke();
    }

    private void OnSwitchWeapon(InputAction.CallbackContext context)
    {
        WeaponSwitched?.Invoke();
    }
}