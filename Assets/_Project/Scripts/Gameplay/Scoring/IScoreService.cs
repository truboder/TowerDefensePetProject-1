namespace Gameplay.Scoring
{
    public interface IScoreService
    {
        int CurrentScore { get; }
        event System.Action<int> OnScoreChanged;
        void AddScore(int amount);
        void ResetScore();
    }
}