using System;
using UnityEngine;

public class WeaponAmmoInfo : MonoBehaviour
{
    public event Action<int, int> AmmoChanged;

    public int CurrentAmmo { get; private set; }
    public int MaxAmmo { get; private set; }

    public void SetAmmo(int current, int max)
    {
        CurrentAmmo = current;
        MaxAmmo = max;
        AmmoChanged?.Invoke(current, max);
    }
}