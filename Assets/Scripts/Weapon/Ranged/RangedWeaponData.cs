using UnityEngine;

[CreateAssetMenu(fileName = "New Ranged Data", menuName = "Weapons/Ranged Weapon Data")]
public class RangedWeaponData : WeaponData
{
    public float Range;
    public float FireRate;
    public int MagazineSize;
    public float ReloadTime;
}