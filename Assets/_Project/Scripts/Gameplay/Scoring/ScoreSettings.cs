using UnityEngine;
using Gameplay.Enemies.Static_Data; // Добавлено для EnemyType

namespace Gameplay.Scoring
{
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "Game/ScoreSettings")]
    public class ScoreSettings : ScriptableObject
    {
        [System.Serializable]
        public class EnemyScore
        {
            public EnemyType EnemyType; // Изменено с string на EnemyType
            public int ScoreValue;
        }

        public EnemyScore[] EnemyScores;

        public int GetScoreForEnemy(EnemyType enemyType) // Изменено с string на EnemyType
        {
            foreach (var score in EnemyScores)
            {
                if (score.EnemyType == enemyType)
                {
                    return score.ScoreValue;
                }
            }
            
            return 0;
        }
    }
}