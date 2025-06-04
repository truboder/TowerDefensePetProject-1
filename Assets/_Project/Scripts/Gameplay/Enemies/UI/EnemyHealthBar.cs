using Gameplay.HealthSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Enemies.UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        [SerializeField] private Image _healthBarFill;
        [SerializeField] private Vector3 _offset = new Vector3(0, 1.5f, 0);

        private Enemy _enemy;
        private Health _health;
        private Camera _mainCamera;

        public void Initialize(Enemy enemy, Camera mainCamera)
        {
            _enemy = enemy;
            _health = enemy.Health;
            _mainCamera = mainCamera;

            _health.OnHealthChanged += UpdateHealthBar;
            _health.OnDeath += Hide;
            UpdateHealthBar(_health.CurrentHealth);
            gameObject.SetActive(true);
        }

        private void UpdateHealthBar(int currentHealth)
        {
            Debug.Log($"Updating health bar: {currentHealth}/{_health.MaxHealth}");
            _healthBarFill.fillAmount = currentHealth / (float)_health.MaxHealth;
        }

        private void Update()
        {
            transform.position = _enemy.transform.position + _offset;
            transform.rotation = _mainCamera.transform.rotation;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnHealthChanged -= UpdateHealthBar;
                _health.OnDeath -= Hide;
            }
        }
    }
}