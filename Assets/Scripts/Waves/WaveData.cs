using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Gameplay/Full Wave")]
public class WaveData : ScriptableObject
{
    [System.Serializable]
    public class WavePhase
    {
        public PoolType poolType;      // MobPool или BossPool
        [Min(1)] public int count;
        public float spawnInterval;
    }

    public enum PoolType { Mob, Boss }

    public WavePhase[] phases = new WavePhase[1]
    {
        new WavePhase { poolType = PoolType.Mob, count = 3, spawnInterval = 1f }
    };
}