using TMPro;
using UnityEngine;

public class WeaponAmmoUI : WeaponUI
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TextMeshProUGUI _ammoText;

    private IWeaponAmmo _weaponAmmo;

    protected override void OnWeaponChanged(Weapon weapon)
    {
        if (weapon is IWeaponAmmo weaponAmmo)
        {
            _weaponAmmo = weaponAmmo;
            _weaponAmmo.AmmoChanged += UpdateAmmoText;

            if (_container != null) 
            { 
                _container.SetActive(true); 
            }

            UpdateAmmoText(_weaponAmmo.CurrentAmmo, _weaponAmmo.MaxAmmo);
        }
        else
        {
            if (_container != null)
            {
                _container.SetActive(false);
                _weaponAmmo = null;
            }
        }
    }

    protected override void OnUnsubscribe()
    {
        if (_weaponAmmo != null)
        {
            _weaponAmmo.AmmoChanged -= UpdateAmmoText;
            _weaponAmmo = null;
        }
    }

    private void UpdateAmmoText(int current, int max)
    {
        if (_ammoText != null)
        { 
            _ammoText.text = $"{current} / {max}";
        }
    }
}