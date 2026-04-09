using System;
using UnityEngine;

public class MeleeAnimationEvents : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public event Action OnAttackStart;
    public event Action OnAttackEnd;

    public void Initialize(AnimatorOverrideController overrideController)
    {
        _animator.runtimeAnimatorController = overrideController;
        _animator.writeDefaultValuesOnDisable = true;
    }

    public void ResetTransform(Transform transform, Vector3 position, Quaternion rotation)
    {
        transform.SetLocalPositionAndRotation(position, rotation);
    }

    public void PlayAttack()
    {
        _animator.SetTrigger(MeleeAnimatorData.Attack);
    }

    public void InvokeAttackStart() => OnAttackStart?.Invoke();
    public void InvokeAttackEnd() => OnAttackEnd?.Invoke();
}