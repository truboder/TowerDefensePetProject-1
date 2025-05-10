using UnityEngine;

namespace Gameplay.Scoring
{
    [CreateAssetMenu(fileName = "ScoreSettings", menuName = "Game/ScoreSettings")]
    public class ScoreSettings : ScriptableObject
    {
        [System.Serializable]
        public class EnemyScore
        {
            public string EnemyType;
            public int ScoreValue;
        }

        public EnemyScore[] EnemyScores;

        public int GetScoreForEnemy(string enemyType)
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