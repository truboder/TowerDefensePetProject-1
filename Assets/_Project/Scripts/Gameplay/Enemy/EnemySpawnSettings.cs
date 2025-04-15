using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnSettings", menuName = "Game/EnemySpawnSettings")]
public class EnemySpawnSettings : ScriptableObject
{
    [Header("General Settings")]
    public Enemy DefaultEnemyPrefab;

    [Header("Pool Settings")]
    public int InitialPoolSize = 10;
    public int MaxPoolSize = 50;

    [Header("Waves")]
    public List<EnemyWaveConfig> Waves;
    public bool InfiniteWaves = false;
}