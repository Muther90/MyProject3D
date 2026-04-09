using System;

public interface IWeaponAmmo
{
    event Action<int, int> AmmoChanged;

    int CurrentAmmo { get; }
    int MaxAmmo { get; }
}