using UnityEngine;
using Gameplay.Enemies.Static_Data; 

namespace Gameplay.Scoring
{
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "Game/ScoreSettings")]
    public class ScoreSettings : ScriptableObject
    {
        [System.Serializable]
        public class EnemyScore
        {
            public EnemyType EnemyType;
            public int ScoreValue;
        }

        public EnemyScore[] EnemyScores;

        public int GetScoreForEnemy(EnemyType enemyType)
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