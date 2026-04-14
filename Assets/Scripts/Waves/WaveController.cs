using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour, IResetable
{
    [SerializeField] private WaveSpawner _spawner;
    [SerializeField] private List<WaveData> _waves;
    [SerializeField] private Pool _mobPool;
    [SerializeField] private Pool _bossPool;
    [SerializeField] private TargetProvider _targetProvider;
    [SerializeField, Min(0f)] private float _nextWaveDelay;

    private int _currentWaveIndex = 0;
    private int _currentPhaseIndex = 0;
    private Coroutine _startWaveCoroutine;

    public event Action<int, int> WaveChanged;
    public event Action AllWavesCompleted;

    private void Start() 
    { 
        StartNextWave();
    }

    private void OnEnable()
    {
        _spawner.AllDied += PhaseCompleted;
    }

    private void OnDisable()
    {
        _spawner.AllDied -= PhaseCompleted;
    }

    public void Reset()
    {
        _currentWaveIndex = 0;

        _mobPool.Reset();
        _bossPool.Reset();

        if (_startWaveCoroutine != null)
        {
            StopCoroutine(_startWaveCoroutine);
            _startWaveCoroutine = null;
        }

        _startWaveCoroutine = StartCoroutine(DelayedStartWaveCoroutine());
    }

    private IEnumerator DelayedStartWaveCoroutine()
    {
        yield return new WaitForSeconds(_nextWaveDelay);

        StartNextWave();
        _startWaveCoroutine = null;
    }

    private void PhaseCompleted()
    {
        _currentPhaseIndex++;

        if (_currentPhaseIndex < _waves[_currentWaveIndex].phases.Length)
        {
            StartCurrentPhase();
        }
        else
        {
            FinishWave();
        }
    }

    private void StartNextWave()
    {
        if (0 < _waves.Count)
        {
            _currentPhaseIndex = 0;
            WaveData wave = _waves[_currentWaveIndex];
            WaveChanged?.Invoke(_currentWaveIndex + 1, _waves.Count);
            StartCurrentPhase();
        }
    }

    private void StartCurrentPhase()
    {
        WaveData currentWave = _waves[_currentWaveIndex];
        WaveData.WavePhase phase = currentWave.phases[_currentPhaseIndex];

        Pool pool = phase.PoolType == WaveData.PoolType.Mob ? _mobPool : _bossPool;
        _spawner.Launch(pool, phase.Count, phase.SpawnInterval, _targetProvider);
    }

    private void FinishWave()
    {
        _currentWaveIndex++;

        if (_currentWaveIndex < _waves.Count)
        {
            StartNextWave();
        }
        else
        {
            AllWavesCompleted?.Invoke();
        }
    }
}