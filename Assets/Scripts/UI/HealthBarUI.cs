using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : HealthUI
{
    [SerializeField] private Image _healthBarFill;

    protected override void UpdateUI(float currentPoints, float maxPoints)
    {
        _healthBarFill.fillAmount = currentPoints / maxPoints;
    }
}