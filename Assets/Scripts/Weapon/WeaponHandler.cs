using System;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour, IResetable
{
    [SerializeField, Min(0)] private int _startWeaponIndex = 0;

    private int _currentWeaponIndex;
    private readonly List<Weapon> _weapons = new();

    public event Action<Weapon> WeaponChanged;

    public Weapon CurrentWeapon => _weapons[_currentWeaponIndex];

    private void Awake()
    {
        int pointCount = transform.childCount;

        for (int i = 0; i < pointCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<Weapon>(out Weapon weapon))
            {
                _weapons.Add(weapon);
            }
        }

        foreach (Weapon weapon in _weapons)
        {
            weapon.gameObject.SetActive(true);
            weapon.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        SelectWeapon(_startWeaponIndex);
    }

    public void Reset()
    {
        SelectWeapon(_startWeaponIndex);

        foreach (Weapon weapon in _weapons)
        {
            if (weapon is IResetable resetableWeapon)
            {
                resetableWeapon.Reset();
            }
        }
    }

    public void NextWeapon()
    {
        SelectWeapon((++_currentWeaponIndex) % _weapons.Count);
    }

    private void SelectWeapon(int weaponIndex)
    {
        for (int i = 0; i < _weapons.Count; i++)
        {
            _weapons[i].gameObject.SetActive(i == weaponIndex);
        }

        _currentWeaponIndex = weaponIndex;

        WeaponChanged?.Invoke(CurrentWeapon);
    }
}