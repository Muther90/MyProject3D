using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Gameplay/Full Wave")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public class WavePhase
    {
        public PoolType PoolType;
        [Min(1)] public int Count;
        [Min(0)] public float SpawnInterval;
    }

    public enum PoolType { Mob, Boss }

    public WavePhase[] phases = new WavePhase[1]
    {
        new WavePhase { PoolType = PoolType.Mob, Count = 3, SpawnInterval = 1f }
    };
}