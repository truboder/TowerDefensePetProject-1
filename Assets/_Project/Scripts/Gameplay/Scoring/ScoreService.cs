using System;
using UnityEngine;
using Gameplay.Enemies.Static_Data; // Добавлено для EnemyType

namespace Gameplay.Scoring
{
    public class ScoreService
    {
        private readonly ScoreSettings _scoreSettings;
        private int _currentScore;
        private int _startingScore = 100;

        public int CurrentScore => _currentScore;
        public event Action<int> OnScoreChanged;

        public ScoreService(ScoreSettings scoreSettings)
        {
            _scoreSettings = scoreSettings;
            _currentScore = _startingScore;
        }

        public void AddScore(EnemyType enemyType) // Изменено с string на EnemyType
        {
            int scoreToAdd = _scoreSettings.GetScoreForEnemy(enemyType);
            _currentScore += scoreToAdd;
            OnScoreChanged?.Invoke(_currentScore);
        }

        public bool TrySpendScore(int amount)
        {
            if (_currentScore >= amount)
            {
                _currentScore -= amount;
                OnScoreChanged?.Invoke(_currentScore);
                return true;
            }

            return false;
        }
    }
}