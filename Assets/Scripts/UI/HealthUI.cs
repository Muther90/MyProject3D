using UnityEngine;

public abstract class HealthUI : MonoBehaviour
{
    [SerializeField] protected Health _health;

    protected virtual void OnEnable()
    {
        _health.ValueChanged += UpdateUI;
        UpdateUI(_health.CurrentPoints, _health.MaxPoints);
    }

    protected virtual void OnDisable()
    {
        _health.ValueChanged -= UpdateUI;
    }

    protected abstract void UpdateUI(float currentPoints, float maxPoints);
}