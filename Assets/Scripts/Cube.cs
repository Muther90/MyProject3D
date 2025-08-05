using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RendererController))]
public class Cube : MonoBehaviour, ISpawnable
{
    [SerializeField] private int _minTimeOfLife;
    [SerializeField] private int _maxTimeOfLife;

    private RendererController _rendererController;
    private Coroutine _destructionCoroutine;

    public event Action<ISpawnable> Taken;
    public event Action<Vector3> BombRequested;

    private void Awake()
    {
        _rendererController = GetComponent<RendererController>();
    }

    public void OnPlatformCollision()
    {
        if (_destructionCoroutine == null)
        {
            _destructionCoroutine = StartCoroutine(DestructionSequence());
        }
    }

    private IEnumerator DestructionSequence()
    {
        _rendererController.RandomColor();

        yield return Timer.WaitSeconds(UnityEngine.Random.Range(_minTimeOfLife, _maxTimeOfLife));

        BombRequested?.Invoke(this.transform.position);
        Taken?.Invoke(this);
        _destructionCoroutine = null;
    }
}
