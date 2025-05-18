using UnityEngine;

namespace Gameplay.PlayerCastle.StaticData
{
    [CreateAssetMenu(fileName = "CastleSettings", menuName = "Game/CastleSettings")]
    public class CastleSettings : ScriptableObject
    {
        [Header("General Settings")]
        public Castle CastlePrefab;
        public int MaxHealth = 10;

        [Header("Pool Settings")]
        public int InitialPoolSize = 1;
        public int MaxPoolSize = 1;
    }
}