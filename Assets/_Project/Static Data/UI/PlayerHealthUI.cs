using DG.Tweening;
using Gameplay.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Static_Data.UI
{
    public class PlayerHealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        
        private HealthService _healthService;
        private Vector3 _originalScale;
        private float _maxHealth;

        [Inject]
        public void Construct(HealthService healthService)
        {
            _healthService = healthService;
        }

        private void Awake()
        {
            _originalScale = _healthBar.transform.localScale;
            _maxHealth = _healthService.Health; 
            UpdateHealthBar(_healthService.Health);
        }

        private void OnEnable()
        {
            _healthService.OnHealthChanged += UpdateHealthBar;
            _healthService.OnDamageTaken += PlayPulseAnimation;
        }

        private void OnDisable()
        {
            _healthService.OnHealthChanged -= UpdateHealthBar;
            _healthService.OnDamageTaken -= PlayPulseAnimation;
        }

        private void UpdateHealthBar(int health)
        {
            _healthBar.fillAmount = health / _maxHealth;
        }

        private void PlayPulseAnimation()
        {
            _healthBar.transform.DOKill();
            _healthBar.transform.localScale = _originalScale;
            _healthBar.transform.DOScale(_originalScale * 1.2f, 0.2f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => _healthBar.transform.localScale = _originalScale);
        }
    }
}