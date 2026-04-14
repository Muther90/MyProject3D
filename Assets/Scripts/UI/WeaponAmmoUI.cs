using TMPro;
using UnityEngine;

public class WeaponAmmoUI : WeaponUI
{
    [SerializeField] private GameObject _container;
    [SerializeField] private TextMeshProUGUI _ammoText;

    private WeaponAmmoInfo _ammoInfo;

    protected override void OnWeaponChanged(Weapon weapon)
    {
        OnUnsubscribe();

        if (weapon != null && weapon.TryGetComponent<WeaponAmmoInfo>(out WeaponAmmoInfo ammoInfo))
        {
            _ammoInfo = ammoInfo;
            _ammoInfo.AmmoChanged += UpdateAmmoText;
            _container.SetActive(true);
            UpdateAmmoText(_ammoInfo.CurrentAmmo, _ammoInfo.MaxAmmo);
        }
        else
        {
            _container.SetActive(false);
            _ammoInfo = null;
        }
    }

    protected override void OnUnsubscribe()
    {
        if (_ammoInfo != null)
        {
            _ammoInfo.AmmoChanged -= UpdateAmmoText;
            _ammoInfo = null;
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