using System;
using System.Collections;
using UnityEngine;

public class RangedWeapon : Weapon, IWeaponAmmo, IWeaponReloadable
{
    [SerializeField] private RangedWeaponData _data;
    [SerializeField] private WeaponRaycaster _weaponRaycaster;

    private int _currentAmmo;
    private bool _isReloading;
    private Coroutine _reloadCoroutine;

    public override WeaponData Data => _data;
    public int CurrentAmmo => _currentAmmo;
    public int MaxAmmo => _data.MagazineSize; 
    public bool IsReloading => _isReloading;

    public event Action<int, int> AmmoChanged;
    public event Action<bool> ReloadingStateChanged;

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

    private void OnDisable()
    {
        StopReload();
    }

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

                _weaponRaycaster.PerformRaycast(_data.Damage, _data.Range);
            }
            else
            {
                _reloadCoroutine = StartCoroutine(ReloadRoutine());
            }
        }
    }

    private IEnumerator ReloadRoutine()
    {
        _isReloading = true;
        ReloadingStateChanged?.Invoke(true);

        yield return new WaitForSeconds(_data.ReloadTime);

        _currentAmmo = _data.MagazineSize;
        _isReloading = false;
        ReloadingStateChanged?.Invoke(false);
        UpdateAmmoUI();

        _reloadCoroutine = null;
    }

    private void UpdateAmmoUI()
    {
        AmmoChanged?.Invoke(_currentAmmo, _data.MagazineSize);
    }

    private void StopReload()
    {
        if (_reloadCoroutine != null)
        {
            StopCoroutine(_reloadCoroutine);
            _reloadCoroutine = null;
        }

        _isReloading = false;
        ReloadingStateChanged?.Invoke(false);
    }
}