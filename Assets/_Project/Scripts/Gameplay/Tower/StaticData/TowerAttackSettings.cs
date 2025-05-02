using Gameplay.Tower.ProjectileFactory;
using UnityEngine;

namespace Gameplay.Tower.StaticData
{
    [CreateAssetMenu(fileName = "TowerSettings", menuName = "Game/TowerSettings")]
    public class TowerSettings : ScriptableObject
    {
        [Header("General Settings")]
        public Projectile ProjectilePrefab;
        public float Range = 5f;
        public float FireRate = 1f;
        public int Damage = 1;

        [Header("Pool Settings")]
        public int InitialPoolSize = 10;
        public int MaxPoolSize = 50;
    }
}