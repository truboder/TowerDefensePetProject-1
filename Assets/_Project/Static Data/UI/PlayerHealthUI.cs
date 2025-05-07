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
            if (_healthBar == null)
            {
                Debug.LogError($"PlayerHealthUI: HealthBar Image reference is missing on {gameObject.name}");
                enabled = false;
                return;
            }

            if (_healthService == null)
            {
                Debug.LogError($"PlayerHealthUI: HealthService is not injected on {gameObject.name}");
                enabled = false;
                return;
            }

            if (_healthBar.type != Image.Type.Filled)
            {
                Debug.LogWarning($"PlayerHealthUI: HealthBar Image on {gameObject.name} should have Image Type set to Filled");
            }

            _originalScale = _healthBar.transform.localScale;
            _maxHealth = _healthService.Health; // Предполагаем, что начальное здоровье равно максимальному
            UpdateHealthBar(_healthService.Health);
            Debug.Log($"PlayerHealthUI initialized on {gameObject.name}, initial health: {_healthService.Health}, fillAmount: {_healthBar.fillAmount}");
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
            Debug.Log($"PlayerHealthUI: Updated health bar, health: {health}, fillAmount: {_healthBar.fillAmount}");
        }

        private void PlayPulseAnimation()
        {
            _healthBar.transform.DOKill();
            _healthBar.transform.localScale = _originalScale;
            _healthBar.transform.DOScale(_originalScale * 1.2f, 0.2f)
                .SetLoops(2, LoopType.Yoyo)
                .OnComplete(() => _healthBar.transform.localScale = _originalScale);
            
            Debug.Log($"PlayerHealthUI: Playing pulse animation on health bar");
        }
    }
}