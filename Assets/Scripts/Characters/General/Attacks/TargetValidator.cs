public static class TargetValidator
{
    public static IDamageable GetValidTarget(IDamageable hit, IDamageable intendedTarget)
    {
        if (hit == null || intendedTarget == null) return null;

        if (hit == intendedTarget) return hit;

        if (hit is DamageReceiver receiver && receiver.Owner == intendedTarget) return hit;

        return null;
    }
}