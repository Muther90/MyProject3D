using TMPro;
using UnityEngine;

public class HealthTextUI : HealthUI
{
    [SerializeField] private TextMeshProUGUI _healthText;

    protected override void UpdateUI(float currentPoints, float maxPoints)
    {
        _healthText.text = $"HP: {currentPoints} / {maxPoints}";
    }
}