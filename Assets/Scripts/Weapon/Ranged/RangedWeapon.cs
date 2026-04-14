using System;
using System.Collections;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [SerializeField] private RangedWeaponData _data;
    [SerializeField] private WeaponAmmoInfo _ammoInfo;
    [SerializeField] private WeaponReloadInfo _reloadInfo;

    private int _currentAmmo;
    private bool _isReloading;
    private Coroutine _reloadCoroutine;

    public event Action<float, float> Shot;

    public override WeaponData Data => _data;

    private void Start()
    {
        _currentAmmo = _data.MagazineSize;
        UpdateAmmoUI();
    }

    private void OnEnable()
    {
        StopReload();
        UpdateAmmoUI();
    }

    private void OnDisable() => StopReload();

    public override void Reset()
    {
        StopReload();
        _currentAmmo = _data.MagazineSize;
        UpdateAmmoUI();
    }

    public override void Attack()
    {
        if (_isReloading == false)
        {
            if (_currentAmmo > 0)
            {
                _currentAmmo--;
                UpdateAmmoUI();

                Shot.Invoke(_data.Damage, _data.Range);
            }
            else
            {
                _reloadCoroutine = StartCoroutine(ReloadCoroutine());
            }
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        _reloadInfo.SetReloading(true);

        yield return new WaitForSeconds(_data.ReloadTime);

        _currentAmmo = _data.MagazineSize;
        _isReloading = false;
        _reloadInfo.SetReloading(false);
        UpdateAmmoUI();

        _reloadCoroutine = null;
    }

    private void UpdateAmmoUI()
    {
        _ammoInfo.SetAmmo(_currentAmmo, _data.MagazineSize);
    }

    private void StopReload()
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        _isReloading = false;
        _reloadInfo.SetReloading(false);
    }
}