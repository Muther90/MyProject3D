using UnityEngine;

public abstract class Weapon : MonoBehaviour, IResetable
{
    public abstract WeaponData Data { get; }

    public abstract void Reset();
    public abstract void Attack();
}