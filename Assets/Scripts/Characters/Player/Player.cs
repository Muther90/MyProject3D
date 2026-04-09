using System;
using UnityEngine;

public class Player : MonoBehaviour, IHealth, IResetable, ITargetable
{
    [SerializeField] private Health _health;
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Mover _mover;
    [SerializeField] private MouseLook _mouselook;
    [SerializeField] private WeaponHandler _weaponHandler;
    [SerializeField] private Transform _cameraTransform;

    private Transform _transform;
    private Vector3 _initialLocalPosition;
    private Quaternion _initialLocalRotation;

    public event Action Died;

    public Vector3 Position => _transform.position;

    private void Awake()
    {
        _transform = transform;
        _initialLocalPosition = _transform.position;
        _initialLocalRotation = _transform.rotation;
    }

    private void OnEnable()
    {
        _inputHandler.Attacked += Attack;
        _inputHandler.WeaponSwitched += SwitchWeapon;
        _health.Died += Die;
    }

    private void OnDisable()
    {
        _inputHandler.Attacked -= Attack;
        _inputHandler.WeaponSwitched -= SwitchWeapon;
        _health.Died -= Die;
    }

    private void FixedUpdate()
    {
        Vector3 input = _inputHandler.MoveInput;
        Vector3 moveDirection = _transform.forward * input.z + _transform.right * input.x;

        _mover.MoveTo(moveDirection);
    }

    private void LateUpdate()
    {
        _mouselook.Look(_inputHandler.LookInput);
    }

    public void Reset()
    {
        _health.Reset();
        _weaponHandler.Reset();
        _transform.SetPositionAndRotation(_initialLocalPosition, _initialLocalRotation);
    }

    public void TakeDamage(float amount)
    {
        _health.ApplyDamage(amount);
    }

    public void TakeHeal(float amount)
    {
        _health.ApplyHeal(amount);
    }

    private void Attack()
    {
        _weaponHandler.CurrentWeapon.Attack();
    }

    private void SwitchWeapon()
    {
        _weaponHandler.NextWeapon();
    }

    private void Die()
    {
        Died?.Invoke();
    }
}