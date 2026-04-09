using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour, IResetable
{
    [SerializeField] private WaveSpawner _spawner;
    [SerializeField] private List<WaveData> _waves;
    [SerializeField] private Pool _mobPool;
    [SerializeField] private Pool _bossPool;
    [SerializeField] private MonoBehaviour _targetBase;
    [SerializeField, Min(0f)] private float _nextWaveDelay;

    private int _currentWaveIndex = 0;
    private int _currentPhaseIndex = 0;
    private ITargetable _target;

    public event Action<int, int> WaveChanged;
    public event Action AllWavesCompleted;

    private void Start()
    {
        _target = _targetBase as ITargetable;

        if (_target == null)
        {
            Debug.LogError("[WaveManager] Target object does not implement ITargetable!");

            return;
        }

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

        Invoke(nameof(StartNextWave), _nextWaveDelay);
    }

    private void PhaseCompleted()
    {
        if (_currentPhaseIndex + 1 < _waves[_currentWaveIndex].phases.Length)
        {
            _currentPhaseIndex++;
            StartCurrentPhase();
        }
        else
        {
            FinishWave();
        }
    }

    private void StartNextWave()
    {
        if (_waves.Count > 0)
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

        Pool pool = phase.poolType == WaveData.PoolType.Mob ? _mobPool : _bossPool;
        _spawner.Launch(pool, phase.count, phase.spawnInterval, _target);
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