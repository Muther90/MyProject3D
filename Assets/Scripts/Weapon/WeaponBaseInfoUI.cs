using TMPro;
using UnityEngine;

public class WeaponBaseInfoUI : WeaponUI
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _damageText;

    protected override void OnWeaponChanged(Weapon weapon)
    {
        if (_nameText != null)
        {
            _nameText.text = weapon.Data.WeaponName;
        }

        if (_damageText != null)
        {
            _damageText.text = $"DMG: {weapon.Data.Damage}";
        }
    }

    protected override void OnUnsubscribe(){}
}