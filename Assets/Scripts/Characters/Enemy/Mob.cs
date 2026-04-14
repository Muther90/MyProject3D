using UnityEngine;

public class Mob : Enemy 
{
    [SerializeField] private Shaker _shaker;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private DistanceDetector _distanceDetector;
    [SerializeField] private RadiusAttack _radiusAttack;
    [SerializeField] private CollisionDetector _collisionDetector;

    private bool _isAttacking;

    protected override void OnEnable()
    {
        base.OnEnable();
        _distanceDetector.TargetDetected += StartJumpAttack;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _distanceDetector.TargetDetected -= StartJumpAttack;
    }

    public override void Reset()
    {
        base.Reset();
        _isAttacking = false;
        _shaker.Reset();
        _jumper.Reset();
    }

    public override void Initialize(TargetProvider targetProvider)
    {
        base.Initialize(targetProvider);
        _distanceDetector.Initialize(targetProvider);
    }

    private void StartJumpAttack()
    {
        if (_isAttacking == false)
        {
            _isAttacking = true;
            DisableMovement();
            _shaker.StartShake(JumpTo);
        }
    }

    private void JumpTo()
    {
        _jumper.JumpTo(_targetProvider.CurrentTarget.Position);
        _collisionDetector.StartDetect(OnCollide);
    }

    private void OnCollide()
    {
        _radiusAttack.TryDealDamage(_targetProvider.CurrentTarget);
        _isAttacking = false;
        EnableMovement();
    }
}