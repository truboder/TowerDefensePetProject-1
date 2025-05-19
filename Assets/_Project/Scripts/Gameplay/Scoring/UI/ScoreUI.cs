using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay.Scoring.UI
{
    public class ScoreUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        
        private IScoreService _scoreService;

        [Inject]
        public void Construct(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void OnEnable()
        {
            _scoreService.OnScoreChanged += UpdateScoreDisplay;
            UpdateScoreDisplay(_scoreService.CurrentScore);
        }

        private void OnDisable()
        {
            _scoreService.OnScoreChanged -= UpdateScoreDisplay;
        }

        private void UpdateScoreDisplay(int score)
        {
            _scoreText.text = $"Score: {score}";
            PlayPulseAnimation();
        }

        private void PlayPulseAnimation()
        {
            _scoreText.transform.DOKill();
            _scoreText.transform.localScale = Vector3.one;
            _scoreText.transform.DOScale(1.2f, 0.2f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => _scoreText.transform.localScale = Vector3.one);
        }
    }
}