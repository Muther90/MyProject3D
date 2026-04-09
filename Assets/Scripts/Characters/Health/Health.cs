using System;
using UnityEngine;

public class Health : MonoBehaviour, IResetable
{
    [SerializeField] private float _maxPoints;
    [SerializeField] private float _currentPoints;
    public float CurrentPoints => _currentPoints;
    public float MaxPoints => _maxPoints;

    public event Action<float, float> ValueChanged;
    public event Action Died;

    public void Reset()
    {
        _currentPoints = _maxPoints;
        ValueChanged?.Invoke(_currentPoints, _maxPoints);
    }

    public void ApplyDamage(float damage)
    {
        if (IsPositiveValue(damage))
        {
            _currentPoints -= damage;
            ValueChanged?.Invoke(_currentPoints, _maxPoints);

            if (_currentPoints <= 0)
            {
                Died?.Invoke();
            }
        }
    }

    public void ApplyHeal(float heal)
    {
        if (IsPositiveValue(heal))
        {
            _currentPoints = Mathf.Min(_currentPoints + heal, _maxPoints);
            ValueChanged?.Invoke(_currentPoints, _maxPoints);
        }
    }

    private bool IsPositiveValue(float value) 
    {
        return value > 0;
    }
}