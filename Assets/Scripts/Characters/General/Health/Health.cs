using System;
using UnityEngine;

public class Health : MonoBehaviour, IResetable
{
    [field: SerializeField] public float MaxPoints { get; private set; }
    [field: SerializeField] public float CurrentPoints { get; private set; }

    public event Action<float, float> ValueChanged;
    public event Action Died;

    public void Reset()
    {
        CurrentPoints = MaxPoints;
        ValueChanged?.Invoke(CurrentPoints, MaxPoints);
    }

    public void ApplyDamage(float damage)
    {
        if (IsPositiveValue(damage))
        {
            CurrentPoints -= damage;
            ValueChanged?.Invoke(CurrentPoints, MaxPoints);

            if (CurrentPoints <= 0)
            {
                Died?.Invoke();
            }
        }
    }

    public void ApplyHeal(float heal)
    {
        if (IsPositiveValue(heal))
        {
            CurrentPoints = Mathf.Min(CurrentPoints + heal, MaxPoints);
            ValueChanged?.Invoke(CurrentPoints, MaxPoints);
        }
    }

    private bool IsPositiveValue(float value) 
    {
        return value > 0;
    }
}