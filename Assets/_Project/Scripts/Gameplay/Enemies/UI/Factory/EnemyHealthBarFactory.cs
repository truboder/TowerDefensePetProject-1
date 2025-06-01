using Gameplay.Inputs;
using UnityEngine;
using Utils;
using Zenject;

namespace Gameplay.Enemies.UI.Factory
{
    public class EnemyHealthBarFactory
    {
        private readonly ComponentPool<EnemyHealthBar> _pool;
        private readonly Camera _mainCamera;

        public EnemyHealthBarFactory(DiContainer container, GameObject healthBarPrefab, CameraService cameraService)
        {
            _pool = new ComponentPool<EnemyHealthBar>(healthBarPrefab.GetComponent<EnemyHealthBar>(), container);
            _mainCamera = cameraService.GetMainCamera();
        }

        public EnemyHealthBar Create(Enemy enemy)
        {
            var healthBar = _pool.Get();
            healthBar.Initialize(enemy, _mainCamera);
            return healthBar;
        }

        public void Return(EnemyHealthBar healthBar)
        {
            healthBar.gameObject.SetActive(false);
            _pool.Return(healthBar);
        }
    }
}