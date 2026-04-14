using System;
using UnityEngine;

public class WeaponReloadInfo : MonoBehaviour
{
    public event Action<bool> ReloadingStateChanged;

    public bool IsReloading { get; private set; }

    public void SetReloading(bool value)
    {
        IsReloading = value;
        ReloadingStateChanged?.Invoke(value);
    }
}