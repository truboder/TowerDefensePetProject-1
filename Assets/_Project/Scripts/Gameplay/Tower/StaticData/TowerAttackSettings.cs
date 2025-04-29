using UnityEngine;

namespace Gameplay.Tower.StaticData
{
    [CreateAssetMenu(fileName = "TowerAttackSettings", menuName = "Game/TowerAttackSettings")]
    public class TowerAttackSettings : ScriptableObject
    {
        [Header("Attack Settings")]
        public float AttackRadius = 5f;
        public float AttackInterval = 1f;
        public int Damage = 1;
    }
}