using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(RendererController))]
public class Cube : MonoBehaviour, ISpawnable
{
    [SerializeField] private int _minTimeOfLife;
    [SerializeField] private int _maxTimeOfLife;

    private BombSpawner _bombSpawner;
    private RendererController _rendererController;
    private Coroutine _destructionCoroutine;

    public event Action<ISpawnable> Taken;

    private void Awake()
    {
        _rendererController = GetComponent<RendererController>();
    }

    public void Initialize(BombSpawner bombSpawner)
    {
        _bombSpawner = bombSpawner;
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

        _bombSpawner.Spawn(this.transform.position, Quaternion.identity);
        Taken?.Invoke(this);
        _destructionCoroutine = null;
    }
}
