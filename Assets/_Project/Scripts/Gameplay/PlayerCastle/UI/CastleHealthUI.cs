using DG.Tweening;
using Gameplay.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.PlayerCastle.UI
{
    public class CastleHealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        
        private ILevelDataService _levelDataService;
        private Castle _castle;
        private Vector3 _originalScale;
        private float _maxHealth;

        [Inject]
        public void Construct(ILevelDataService levelDataService)
        {
            _levelDataService = levelDataService;
        }

        private void Awake()
        {
            _originalScale = _healthBar.transform.localScale;
        }

        private void Start()
        {
            InitializeCastle();
        }

        private void InitializeCastle()
        {
            _castle = _levelDataService.GetCastle();

            _maxHealth = _castle.Health.CurrentHealth;
            UpdateHealthBar(_castle.Health.CurrentHealth);
            SubscribeToCastleEvents();
        }

        private void OnEnable()
        {
            SubscribeToCastleEvents();
        }

        private void OnDisable()
        {
            if (_castle != null && _castle.Health != null)
            {
                _castle.Health.OnHealthChanged -= UpdateHealthBar;
                _castle.Health.OnDamageTaken -= PlayPulseAnimation;
            }
        }
        
        private void SubscribeToCastleEvents()
        {
            if (_castle != null && _castle.Health != null)
            {
                _castle.Health.OnHealthChanged += UpdateHealthBar;
                _castle.Health.OnDamageTaken += PlayPulseAnimation;
            }
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