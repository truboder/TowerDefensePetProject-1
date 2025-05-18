// Static_Data/UI/CastleHealthUI.cs
using DG.Tweening;
using Gameplay.PlayerCastle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Static_Data.UI
{
    public class CastleHealthUI : MonoBehaviour
    {
        [SerializeField] private Image _healthBar;
        
        private Castle _castle;
        private Vector3 _originalScale;
        private float _maxHealth;

        [Inject]
        public void Construct(Castle castle)
        {
            _castle = castle;
        }

        private void Awake()
        {
            _originalScale = _healthBar.transform.localScale;
            _maxHealth = _castle.Health.CurrentHealth;
            UpdateHealthBar(_castle.Health.CurrentHealth);
        }

        private void OnEnable()
        {
            _castle.Health.OnHealthChanged += UpdateHealthBar;
            _castle.Health.OnDamageTaken += PlayPulseAnimation;
        }

        private void OnDisable()
        {
            _castle.Health.OnHealthChanged -= UpdateHealthBar;
            _castle.Health.OnDamageTaken -= PlayPulseAnimation;
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