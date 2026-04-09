using UnityEngine;

public abstract class WeaponUI : MonoBehaviour
{
    [SerializeField] protected WeaponHandler _weaponHandler;

    private Weapon _currentWeapon;

    protected virtual void OnEnable()
    {
        _weaponHandler.WeaponChanged += WeaponChanged;
    }

    protected virtual void OnDisable()
    {
        _weaponHandler.WeaponChanged -= WeaponChanged;
        OnUnsubscribe();
    }

    private void WeaponChanged(Weapon weapon)
    {
        _currentWeapon = weapon;
        OnUnsubscribe();
        OnWeaponChanged(weapon);
    }

    protected abstract void OnWeaponChanged(Weapon weapon);

    protected abstract void OnUnsubscribe();
}