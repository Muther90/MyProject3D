using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(GroupRendererController))]
public class Bomb : MonoBehaviour, ISpawnable
{
    [SerializeField] private int _minTimeOfLife;
    [SerializeField] private int _maxTimeOfLife;
    [SerializeField] private Explosion _explosion;
    [SerializeField] private BombRenderer _bombRenderer;

    private Coroutine _destructionCoroutine;

    public event Action<ISpawnable> Taken;

    private void OnEnable()
    {
        _destructionCoroutine = StartCoroutine(DelayedStartDestruction());
    }

    private void OnDisable()
    {
        if (_destructionCoroutine != null)
        {
            StopCoroutine(_destructionCoroutine);
            _destructionCoroutine = null;
        }
    }

    private IEnumerator DelayedStartDestruction()
    {
        yield return null;
        yield return StartCoroutine(DestructionSequence());
    }

    private IEnumerator DestructionSequence()
    {
        int timeDestruction = UnityEngine.Random.Range(_minTimeOfLife, _maxTimeOfLife);
        _bombRenderer.StartFadeOut(timeDestruction);

        yield return Timer.WaitSeconds(timeDestruction);

        _explosion.Explode(transform.position);
        Taken?.Invoke(this);
        _destructionCoroutine = null;
    }
}
