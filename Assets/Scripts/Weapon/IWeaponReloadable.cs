using System;

public interface IWeaponReloadable
{
    event Action<bool> ReloadingStateChanged;

    bool IsReloading { get; }
}