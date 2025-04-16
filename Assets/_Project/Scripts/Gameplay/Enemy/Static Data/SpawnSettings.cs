using System.Collections.Generic;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "Game/EnemySpawnSettings")]
    public class SpawnSettings : ScriptableObject
    {
        [Header("General Settings")]
        public Enemy DefaultEnemyPrefab;

        [Header("Pool Settings")]
        public int InitialPoolSize = 10;
        public int MaxPoolSize = 50;

        [Header("Waves")]
        public List<WaveConfig> Waves;
        public bool InfiniteWaves = false;
    }
}