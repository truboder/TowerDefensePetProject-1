using Gameplay.Enemies.Static_Data;

namespace Gameplay.Scoring
{
    public interface IScoreService
    {
        int CurrentScore { get; }
        event System.Action<int> OnScoreChanged;
        void AddScore(int amount);
        void AddScore(EnemyType enemyType);
        bool TrySpendScore(int amount);
        void ResetScore();
    }
}