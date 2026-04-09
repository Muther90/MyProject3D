using UnityEngine;

public class WeaponReloadUI : WeaponUI
{
    [SerializeField] private GameObject _reloadIndicator;

    private IWeaponReloadable _weaponReloadable;

    protected override void OnWeaponChanged(Weapon weapon)
    {
        if (weapon is IWeaponReloadable weaponReloadable)
        {
            _weaponReloadable = weaponReloadable;
            _weaponReloadable.ReloadingStateChanged += UpdateReloadState;

            UpdateReloadState(_weaponReloadable.IsReloading);
        }
        else
        {
            if (_reloadIndicator != null)
            {
                _reloadIndicator.SetActive(false);
                _weaponReloadable = null;
            }
        }
    }

    protected override void OnUnsubscribe()
    {
        if (_weaponReloadable != null)
        {
            _weaponReloadable.ReloadingStateChanged -= UpdateReloadState;
            _weaponReloadable = null;
        }
    }

    private void UpdateReloadState(bool isReloading)
    {
        if (_reloadIndicator != null)
        { 
            _reloadIndicator.SetActive(isReloading);
        }
    }
}