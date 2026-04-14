using UnityEngine;

public class WeaponRaycaster : MonoBehaviour
{
    private const float ViewCenterX = 0.5f;
    private const float ViewCenterY = 0.5f;

    [SerializeField] private Camera _camera;
    [SerializeField] private WeaponHandler _weaponHandler;

    private Weapon _currentWeapon;

    private void OnEnable()
    {
        _weaponHandler.WeaponChanged += OnWeaponChanged;
    }

    private void OnDisable()
    {
        _weaponHandler.WeaponChanged -= OnWeaponChanged;
    }

    private void OnWeaponChanged(Weapon weapon)
    {
        UnsubscribeWeapon();
        _currentWeapon = weapon;

        if (_currentWeapon is RangedWeapon ranged)
        {
            ranged.Shot += PerformRaycast;
        }
    }

    private void UnsubscribeWeapon()
    {
        if (_currentWeapon is RangedWeapon ranged)
        {
            ranged.Shot -= PerformRaycast;
        }
    }

    private void PerformRaycast(float damage, float range)
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(ViewCenterX, ViewCenterY, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            if (hit.collider.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
            }
        }
    }
}